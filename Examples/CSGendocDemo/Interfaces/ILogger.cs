using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSGendocDemo.Interfaces
{
	/// <summary>
	/// Interface for implementing loggers.
	/// </summary>
	public interface ILogger
	{
		/// <summary>
		/// Logs an info message.
		/// </summary>
		/// <param name="message">Message to log</param>
		void Info(string message);
		/// <summary>
		/// Logs an warning message.
		/// </summary>
		/// <param name="message">Message to log</param>
		void Warn(string message);
		/// <summary>
		/// Logs an error message.
		/// </summary>
		/// <param name="message">Message to log</param>
		void Error(string message);
		/// <inheritdoc cref="Error(string)"/>
		void Error(string message, Exception exception);
		/// <summary>
		/// Logs an fatal message.
		/// </summary>
		/// <param name="message">Message to log</param>
		void Fatal(string message);
		/// <inheritdoc cref="Fatal(string)"/>
		void Fatal(string message, Exception exception);
		/// <summary>
		/// Logs an debug message.
		/// </summary>
		/// <param name="message">Message to log</param>
		void Debug(string message);
	}
}
