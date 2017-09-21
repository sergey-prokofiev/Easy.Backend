using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Easy.Backend.Handlers.TasksManagement;
using NSubstitute;
using NUnit.Framework;
using Easy.Backend.Wrappers;

namespace Easy.Backend.Tests.Handlers.TaskManagement
{
	[TestFixture]
	public class TasksManagerTests
	{
		private TasksManager _mgr;
		private ITaskWrapper _taskWrapper;
		private TaskScheduler _scheduler;
		private TaskCreationOptions _taskCreationOptions;
		private TimeSpan _waitTimeout;

		[SetUp]
		public void Init()
		{
			_taskWrapper = Substitute.For<ITaskWrapper>();
			_scheduler = TaskScheduler.Current;
			_taskCreationOptions = TaskCreationOptions.AttachedToParent;
			_waitTimeout = TimeSpan.FromMinutes(42);
			_mgr = new TasksManager(_taskWrapper, _scheduler, _waitTimeout, _taskCreationOptions);
		}

		[Test]
		public void StartTaskTest()
		{
			var action = new Action(() => { });
			var token = new CancellationToken();
			_mgr.StartTask(action, token);

			_taskWrapper.Received().Start(action, token, _taskCreationOptions, _scheduler);
		}
		
		[Test]
		public void WaitAllTasksTest()
		{
			var tasks = new List<Task>();
			_mgr.WaitAllTasks(tasks);

			_taskWrapper.Received().WaitAll(tasks, _waitTimeout);
		}
	}
}