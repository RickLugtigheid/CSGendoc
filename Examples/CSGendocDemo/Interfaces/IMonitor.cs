using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSGendocDemo.Interfaces
{
	/// <summary>
	/// Interface for implementing monitor classes.
	/// </summary>
	public interface IMonitor
	{
		/// <summary>
		/// If the monitor is running.
		/// </summary>
		bool IsMonitoring { get; }
		/// <summary>
		/// If any errors where detected during monitoring.
		/// </summary>
		bool HasErrors { get; }
		/// <summary>
		/// Starts the monitor.
		/// </summary>
		void Start();
		/// <summary>
		/// Stops the monitor.
		/// </summary>
		void Stop();
		/// <summary>
		/// Restarts the monitor after the given timeout.
		/// </summary>
		/// <param name="timeout">The timeout to wait before starting the monitor after stopping.</param>
		void Restart(int timeout = 1000);
	}
}
