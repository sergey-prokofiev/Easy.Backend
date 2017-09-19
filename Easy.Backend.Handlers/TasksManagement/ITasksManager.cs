using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Easy.Backend.Handlers.TasksManagement
{
	/// <summary>
	/// Incapsulates all needestuff to manage start startand waits such as scheduler, timeout, options etc.
	/// </summary>
	public interface ITasksManager
	{
		/// <summary>
		/// Starts a task considetring internal options
		/// </summary>
		Task StartTask(Action action, CancellationToken token);

		/// <summary>
		/// Waits for a task competition considering internal timeout
		/// </summary>
		void WaitAllTasks(IEnumerable<Task> tasks);
	}
}