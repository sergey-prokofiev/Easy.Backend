using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Easy.Backend.Wrappers
{
	/// <summary>
	/// Wraps static methods of Task class for unit testing 
	/// </summary>
	public interface ITaskWrapper
	{
		/// <summary>
		/// Starts a task
		/// </summary>
		Task Start(Action action, CancellationToken token, TaskCreationOptions opts, TaskScheduler scheduler);		

		/// <summary>
		/// Continues a task with another one.
		/// </summary>
		Task ContinueWith(Task parentTask, Action<Task, object> action, TaskContinuationOptions opts, object state=null);

		/// <summary>
		/// Creates a task that completes after a time delay.
		/// </summary>
		void Delay(TimeSpan delay, CancellationToken token);

		/// <summary>
		/// Waits for all tasks complition. Throws TimeoutException if tasks were not completed in time.
		/// </summary>
		void WaitAll(IEnumerable<Task> tasks, TimeSpan timeout);

		/// <summary>
		/// Waits for all tasks complition
		/// </summary>
		void WaitAll(IEnumerable<Task> tasks);
	}
}