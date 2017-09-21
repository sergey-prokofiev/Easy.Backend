using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Easy.Backend.Handlers.Handlers;
using Easy.Backend.Handlers.TasksManagement;
using NSubstitute;
using NUnit.Framework;

namespace Easy.Backend.Tests.Handlers.Handlers
{
	[TestFixture]
	public class ConcurrentHandlerTests
	{
		private ConcurrentHandler<int, CancellationTokenExecutionContext> _handler;
		private ITasksManager _mgr;
		private IHandler<int, EmptyObject, CancellationTokenExecutionContext> _inner;
		[SetUp]
		public void Init()
		{
			_inner = Substitute.For<IHandler<int, EmptyObject, CancellationTokenExecutionContext>>();
			_mgr = Substitute.For<ITasksManager>();

			_handler = new ConcurrentHandler<int, CancellationTokenExecutionContext>(new[]{_inner, _inner, _inner}, _mgr);
		}

		[Test]
		public void HandleTest()
		{
			var context = new CancellationTokenExecutionContext(new CancellationToken());
			var result = _handler.Handle(42, context);
			Assert.AreEqual(result, EmptyObject.Default);
			_mgr.Received(3).StartTask(Arg.Any<Action>(), context.Token);
			_mgr.Received(1).WaitAllTasks(Arg.Any<IEnumerable<Task>>());
		}

	}
}