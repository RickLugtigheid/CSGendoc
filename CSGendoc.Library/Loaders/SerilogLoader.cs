﻿using Serilog;

namespace CSGendoc.Library.Loaders
{
	public static class SerilogLoader
	{
		private const string LOG_BASE_TEMPLATE = "{Timestamp:yyyy-MM-dd HH:mm:ss.fff} [{Level:u3}] {Message}{NewLine}{Exception}";
		private const string LOG_VERBOSE_TEMPLATE = "{Timestamp:yyyy-MM-dd HH:mm:ss.fff} [{Level:u3}] ({SourceContext}) {Message}{NewLine}{Exception}";

		private static LoggerConfiguration _logConfig;
		public static ILogger Log { get; private set; }

		public static void Load(bool enableDebug = false)
		{
			// Create our configuration object
			//
			_logConfig = new LoggerConfiguration();
			_logConfig.MinimumLevel.Information();
			_logConfig.Enrich.FromLogContext();

			string logTemplate = LOG_BASE_TEMPLATE;

			// Enable debug
			//
			if (enableDebug)
			{
				_logConfig.MinimumLevel.Verbose();
				logTemplate = LOG_VERBOSE_TEMPLATE;
			}

			/// TODO: Add configured logWriters to our serilog configuration
			// Add console log writer
			_logConfig.WriteTo.Console(outputTemplate: logTemplate);

			// Create logger
			Log = _logConfig.CreateLogger();
		}

		/// <summary>
		/// Gets a logger for the given object type.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <returns></returns>
		public static ILogger GetLogger<T>() => Log.ForContext<T>();
		/// <summary>
		/// Gets a logger for the given object type.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <returns></returns>
		public static ILogger GetLogger(Type type) => Log.ForContext(type);
	}
}
