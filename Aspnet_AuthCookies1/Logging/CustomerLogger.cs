using System;
using System.IO;
using Microsoft.Extensions.Logging;

namespace ApiCatalogo.Logging
{
    public class CustomerLogger : ILogger
    {
        private readonly string loggerName;
        private readonly CustomLoggerProviderConfiguration loggerConfig;

        public CustomerLogger(string name, CustomLoggerProviderConfiguration config)
        {
            this.loggerName = name;
            this.loggerConfig = config;
        }

        public IDisposable BeginScope<TState>(TState state)
        {
            return null;
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            return logLevel == this.loggerConfig.LogLevel;
        }

        public void Log<TState>(
            LogLevel logLevel,
            EventId eventId,
            TState state,
            Exception exception,
            Func<TState, Exception, string> formatter)
        {
            string mensagem = $"{logLevel.ToString()}: {eventId.Id} - {formatter(state, exception)}";

            EscreverTextoNoArquivo(mensagem);
        }

        private void EscreverTextoNoArquivo(string mensagem)
        {
            string caminhoArquivoLog = @"C:\teste\log";
            Console.WriteLine(mensagem);
            //using (StreamWriter streamWriter = new StreamWriter(caminhoArquivoLog))
            //{
            //    streamWriter.WriteLine(mensagem);
            //    streamWriter.Close();
            //}
        }
    }
}