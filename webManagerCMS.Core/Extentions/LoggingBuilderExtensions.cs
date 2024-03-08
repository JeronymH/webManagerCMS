using S9.Core.Logging;

namespace webManagerCMS.Core.Extentions
{
    public static class LoggingBuilderExtensions
    {
        public static ILoggingBuilder AddDataStorageLogger(this ILoggingBuilder builder, Action<DataStorageLoggerOptions> options)
        {
            builder.Services.AddSingleton<ILoggerProvider, DataStorageLoggerProvider>();
            builder.Services.Configure(options);
            return builder;
        }
    }
}
