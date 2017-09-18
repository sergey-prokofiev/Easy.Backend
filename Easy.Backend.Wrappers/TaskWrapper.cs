using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Easy.Wrappers
{
	public class TaskWrapper : ITaskWrapper
	{
		public Task Start(Action action, CancellationToken token, TaskCreationOptions opts, TaskScheduler scheduler)
		{
			var result = Task.Factory.StartNew(action, token, opts, scheduler);
			return result;
		}

		public Task ContinueWith(Task parentTask, Action<Task, object> action, TaskContinuationOptions opts, object state = null)
		{
			var result = parentTask.ContinueWith(action, state, opts);
			return result;
		}

		public void Delay(TimeSpan delay, CancellationToken token)
		{
			var delayTask = Task.Delay(delay, token);
			delayTask.Wait(token);
		}

		public void WaitAll(IEnumerable<Task> tasks, TimeSpan timeout)
		{
			var succeeded = Task.WaitAll(tasks.ToArray(), timeout);
			if (!succeeded)
			{
				throw new TimeoutException("Tasks were not finished in given timeout");
			}
		}

		public void WaitAll(IEnumerable<Task> tasks)
		{
			Task.WaitAll(tasks.ToArray());
		}
	}
}