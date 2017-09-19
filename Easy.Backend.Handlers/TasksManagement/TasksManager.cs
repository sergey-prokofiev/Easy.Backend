using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Easy.Backend.Wrappers;

namespace Easy.Backend.Handlers.TasksManagement
{
	/// <inheritdoc />
	public class TasksManager : ITasksManager
	{
		private readonly ITaskWrapper _taskWrapper;
		private readonly TaskScheduler _scheduler;
		private readonly TaskCreationOptions _taskCreationOptions;
		private readonly TimeSpan _waitTimeout;

		public TasksManager(ITaskWrapper taskWrapper, TaskScheduler scheduler, TimeSpan waitTimeout, 
			TaskCreationOptions taskCreationOptions = TaskCreationOptions.PreferFairness)
		{
			_taskWrapper = taskWrapper;
			_taskCreationOptions = taskCreationOptions;
			_scheduler = scheduler;
			_waitTimeout = waitTimeout;
		}

		public Task StartTask(Action action, CancellationToken token)
		{
			var result = _taskWrapper.Start(action, token, _taskCreationOptions, _scheduler);
			return result;
		}

		public void WaitAllTasks(IEnumerable<Task> tasks)
		{
			_taskWrapper.WaitAll(tasks, _waitTimeout);
		}
	}
}