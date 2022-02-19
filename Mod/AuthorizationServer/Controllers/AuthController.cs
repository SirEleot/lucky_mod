using AuthorizationServer.Database;
using AuthorizationServer.Email;
using AuthorizationServer.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TokenService.Models;

namespace AuthorizationServer.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {      

        private readonly TokenService.TokenService _tokenService;
        private readonly AuthDbContext _dbContext;
        private readonly List<string> _allowedAppKeys;
        private readonly AuthEmailService _emailService;

        public AuthController(IConfiguration configuration, TokenService.TokenService tokenService, AuthDbContext dbContext, AuthEmailService emailService)
        {
            _tokenService = tokenService;
            _dbContext = dbContext;
            _allowedAppKeys = new List<string>();
            _emailService = emailService;
            configuration.GetSection("AllowedAppKeys").Bind(_allowedAppKeys);
        }

        /// <summary>
        /// User registration api
        /// </summary>
        /// <param name="registerData">
        /// Registration form data
        /// </param>
        /// <returns></returns>
        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterForm registerData)
        {
            AuthResponceModel responce;
            if (registerData.AppKey == null || registerData.Login == null || registerData.Email == null || registerData.Password == null || registerData.ConfirmPassword == null)
                responce = new AuthResponceModel(Enums.StatusCodes.badEntryData);
            else if (!IsAppAllowed(registerData)) 
                responce = new AuthResponceModel(Enums.StatusCodes.badApplication);
            else if(registerData.Password != registerData.ConfirmPassword)
                responce = new AuthResponceModel(Enums.StatusCodes.passwordConfirmError);
            else
            {
                var user = _dbContext.Users.FirstOrDefault(u => u.Login == registerData.Login || u.Email == registerData.Email);
                if(user != null)
                {
                    if(user.Login == registerData.Login)
                        responce = new AuthResponceModel(Enums.StatusCodes.userAlredyExists);
                    else
                        responce = new AuthResponceModel(Enums.StatusCodes.emailAlreadyExists);
                }
                else
                {
                    user = new User
                    {
                        Login = registerData.Login,
                        Password = BCrypt.Net.BCrypt.HashPassword(registerData.Password),
                        Email = registerData.Email,
                        EmailConfirmCode = await _emailService.SendEmailWithConfirmationCode(registerData.Email),
                        EmailConfirmed = false,
                        Promocode = registerData.Promocode ?? ""
                    };
                    _dbContext.Add(user);
                    await _dbContext.SaveChangesAsync();
                    responce = new AuthResponceModel(Enums.StatusCodes.emailNotConfirmed, user.Email);
                }
            }

            return Ok(responce);
        }

        /// <summary>
        /// User authorization api
        /// </summary>
        /// <param name="loginData">
        /// Login form data
        /// </param>
        /// <returns></returns>
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginForm loginData)
        {
            AuthResponceModel responce;
            if(loginData.AppKey == null || loginData.Login == null || loginData.Password == null)
                responce = new AuthResponceModel(Enums.StatusCodes.badEntryData);
            else if (!IsAppAllowed(loginData))
                responce = new AuthResponceModel(Enums.StatusCodes.badApplication);
            else
            {
                var user = _dbContext.Users.FirstOrDefault(u => u.Login == loginData.Login);
                if (user == null || !BCrypt.Net.BCrypt.Verify(loginData.Password, user.Password))
                    responce = new AuthResponceModel(Enums.StatusCodes.userNotFound);
                else if (!user.EmailConfirmed)
                    responce = new AuthResponceModel(Enums.StatusCodes.emailNotConfirmed, user.Email);
                else            
                    responce = await CreateResponceWithTokens(user.Login, user.Email, loginData.AppKey);
            }   

            return Ok(responce);
        }

        /// <summary>
        /// Confirm eamail api
        /// </summary>
        /// <param name="emailConfirm">
        /// Confirm email form data
        /// </param>
        /// <returns></returns>
        [HttpPost("emailconfirm")]
        public async Task<IActionResult> EmailConfirm(ConfirmEmailForm emailConfirm)
        {
            if (emailConfirm.AppKey == null || emailConfirm.Email == null || emailConfirm.Code == null)
                return Ok(new AuthResponceModel(Enums.StatusCodes.badEntryData));
            if (!IsAppAllowed(emailConfirm.AppKey))
                return Ok(new AuthResponceModel(Enums.StatusCodes.badApplication));
            var user = _dbContext.Users.FirstOrDefault(u => u.Email == emailConfirm.Email);
            if (user == null)
                return NotFound();
            if (user.EmailConfirmCode != emailConfirm.Code)
                return Ok(new AuthResponceModel(Enums.StatusCodes.badEmailCode));
            user.EmailConfirmed = true;
            await _dbContext.SaveChangesAsync();
            return Ok(await CreateResponceWithTokens(user.Login, user.Email, emailConfirm.AppKey));
        }


        /// <summary>
        /// Update access token with refresh token
        /// </summary>
        /// <param name="updateToken">
        /// Update token form data
        /// </param>
        /// <returns></returns>
        [HttpPost("tokenupdate")]
        public async Task<IActionResult> TokenUpdate(UpdateTokenForm updateToken)
        {
            AuthResponceModel responce;
            if (updateToken.AppKey == null || updateToken.RefreshToken == null)
                responce = new AuthResponceModel(Enums.StatusCodes.badEntryData);
            else if (!IsAppAllowed(updateToken.AppKey))
                responce = new AuthResponceModel(Enums.StatusCodes.badApplication);
            else
            {
                var token = _tokenService.DeserealizeToken(updateToken.RefreshToken);
                if (token == null || !_dbContext.Tokens.Any(t => t.Id == token.Id))
                    responce = new AuthResponceModel(Enums.StatusCodes.accessDined);
                else
                {
                    _dbContext.Remove(token);
                    _dbContext.SaveChanges();
                    if (token.AppKey == updateToken.AppKey)
                        responce = await CreateResponceWithTokens(token.Login, token.Email, token.AppKey);
                    else
                        responce = new AuthResponceModel(Enums.StatusCodes.accessDined);
                }
            }            

            return Ok(responce);
        }

        private async Task<AuthResponceModel> CreateResponceWithTokens(string login, string email, string appKey)
        {
            var tokens = _tokenService.CreateTokensPair(login, email, appKey);
            _dbContext.Add(tokens.RefreshToken);
            await _dbContext.SaveChangesAsync();
            _tokenService.SignTokens(tokens);
            return new AuthResponceModel(Enums.StatusCodes.ok, _tokenService.SerealizeTokens(tokens));
        }             
        private bool IsAppAllowed(RegisterForm regData)
        {
            return IsAppAllowed(regData.AppKey);
        }
        private bool IsAppAllowed(LoginForm loginData)
        {
            return IsAppAllowed(loginData.AppKey);
        }
        private bool IsAppAllowed(string appKey)
        {
            return _allowedAppKeys.Contains(appKey);
        }

    }
}
