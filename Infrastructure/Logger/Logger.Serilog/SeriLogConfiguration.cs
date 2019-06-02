namespace Infrastructure.Logger.Serilog
{
	using global::Serilog;

	public static class SeriLogConfiguration
    {
        public static void Configure(string filePath = "")
        {
            var loggerConfiguration = new LoggerConfiguration().MinimumLevel.Debug().WriteTo.ColoredConsole();
            if (!string.IsNullOrEmpty(filePath))
                loggerConfiguration.WriteTo.File(filePath, rollingInterval: RollingInterval.Day);
            Log.Logger = loggerConfiguration.CreateLogger();
        }
    }
}
