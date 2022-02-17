using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;

namespace Server.Utils
{
    class LuckyLogger
    {
        private enum LogTypes
        {
            Error,
            Info,
            Warning,
            Debug,
            Client
        }

        private static Thread _luckyLoggetThread;
        private class LogModel
        {
            public LogModel(LogTypes type, string text)
            {
                Type = type;
                Text = text;
                Date = DateTime.Now;
            }
            public DateTime Date { get; set; }
            public LogTypes Type { get; set; }
            public string Text { get; set; }
        }

        private string _type;

        private static string _dirName;
        private static ConcurrentQueue<LogModel> _queue = new ConcurrentQueue<LogModel>();
        public LuckyLogger(Type type)
        {
            _type = type.FullName;
        }
        static LuckyLogger()
        {
            if (!Directory.Exists("Logs"))
                Directory.CreateDirectory("Logs");

            _dirName = $"Logs/{DateTime.Now.ToString("yyyy_MM_dd")}";
            if (!Directory.Exists(_dirName))
                Directory.CreateDirectory(_dirName);
            Start();
        }
        public void WriteDebug(string text)
        {
#if DEBUG
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(text);
            Console.ResetColor();
            _queue.Enqueue(new LogModel(LogTypes.Debug, text));
#endif
        }
        public void WriteInfo(string text)
        {
            _queue.Enqueue(new LogModel(LogTypes.Info, text));
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine(text);
            Console.ResetColor();
        }
        public void WriteError(string text)
        {
            _queue.Enqueue(new LogModel(LogTypes.Error, text));
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(text);
            Console.ResetColor();
        }
        public void WriteWarning(string text)
        {
            _queue.Enqueue(new LogModel(LogTypes.Warning, text));
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(text);
            Console.ResetColor();
        }

        public void WriteClient(string text)
        {
            _queue.Enqueue(new LogModel(LogTypes.Client, text));
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine(text);
            Console.ResetColor();
        }

        private static async void Logic()
        {
            while (_queue.Count > 0)
            {
                if (_queue.TryDequeue(out LogModel log))
                {
                    try
                    {
                        var path = _dirName;
                        switch (log?.Type)
                        {
                            case LogTypes.Error:
                                path += "/Errors.log";
                                break;
                            case LogTypes.Info:
                                path += "/Infos.log";
                                break;
                            case LogTypes.Warning:
                                path += "/Warnings.log";
                                break;
                            case LogTypes.Debug:
                                path += "/Debugs.log";
                                break;
                            case LogTypes.Client:
                                path += "/Client.log";
                                break;
                            default:
                                path += "/Other.log";
                                break;
                        }
                        using (var w = new StreamWriter(path, true))
                        {
                            await w.WriteLineAsync($"{log.Date}: {log.Type}\n{log.Text}");
                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine($"Logger: {e}");
                    }
                }
            }
        }

        private static void Start()
        {
            _luckyLoggetThread = new Thread(Logic);
            _luckyLoggetThread.Start();
        }
    }
}
