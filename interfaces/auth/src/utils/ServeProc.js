const waitMaxTime = 500;
const checkDelay = 50;
const calbacks = {};
window.serverProcCallback = (key, args)=>{
    calbacks[key] = args;
}
let rpcKey = 0;

async function waitCallback(key){
    return new Promise((resolve)=>{
        const expiriedDate = Date.now() + waitMaxTime;
        var interval = setInterval(()=>{
            if(calbacks[key]){
                resolve(calbacks[key]);
                delete calbacks[key];
                clearInterval(interval);
            }
            if(expiriedDate < Date.now()){
                resolve(undefined);
                clearInterval(interval);
            }
        }, checkDelay)
    })    
}

export async function callAyncServerProc(procName, arg){
    const key = rpcKey++;
    window.mp.trigger("common:server:proc:call", "rpc:cb:auth", procName, key, arg);
    const result = await waitCallback(key);
    return result;
}