using System;
using System.Threading;
using Easy.Backend.Handlers.Dispatching;
using Easy.Backend.Handlers.Exceptions;
using Easy.Backend.Handlers.Handlers;
using Easy.Backend.Handlers.Metrics;
using Easy.Backend.Handlers.TasksManagement;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using NUnit.Framework;

namespace Easy.Backend.Tests.Handlers.Dispatching
{
	[TestFixture]
	public class DispatcherTests
	{
		private IServiceLocator _serviceLocator;
		private IMetricsAggregator _aggregator;
		private ITasksManager _tasksManager;
		private IHandler<int, EmptyObject, CancellationTokenExecutionContext> _handler;
		private Dispatcher _dispatcher;

		[SetUp]
		public void Init()
		{
			_handler = Substitute.For<IHandler<int, EmptyObject, CancellationTokenExecutionContext>>();
			_serviceLocator = Substitute.For<IServiceLocator>();			
			_serviceLocator.Resolve<IHandler<int, EmptyObject, CancellationTokenExecutionContext>>().Returns(_handler);
			_serviceLocator.Resolve<IHandler<int, EmptyObject, CancellationTokenExecutionContext>>("Hello").Returns(_handler);
			_serviceLocator.ResolveAll<IHandler<int, EmptyObject, CancellationTokenExecutionContext>>().Returns(new [] {_handler});
			_aggregator = Substitute.For<IMetricsAggregator>();
			_tasksManager = Substitute.For<ITasksManager>();
			_dispatcher = new Dispatcher(_serviceLocator,_aggregator, _tasksManager);
		}

		[Test]
		public void ResolveNotNamedSuccessTest()
		{
			var context = new CancellationTokenExecutionContext(new CancellationToken());
			_dispatcher.DispatchToHandler(42, context);

			_serviceLocator.Received(1).Resolve<IHandler<int, EmptyObject, CancellationTokenExecutionContext>>();
			_serviceLocator.Received(0).Resolve<IHandler<int, EmptyObject, CancellationTokenExecutionContext>>(Arg.Any<string>());
			_serviceLocator.Received(0).ResolveAll<IHandler<int, EmptyObject, CancellationTokenExecutionContext>>();
			_aggregator.Received(1).AddDispatchedInput(typeof(int), _handler.GetType());
			_handler.Received(1).Handle(42, context);
			_aggregator.Received(1).AddProcessedInput(typeof(int), _handler.GetType());
			_aggregator.Received(0).AddErrorProcessingInput(typeof(int));
		}

		[Test]
		public void ResolveNamedSuccessTest()
		{
			var context = new CancellationTokenExecutionContext(new CancellationToken());
			context.ResolverHint = "Hello";
			
			_dispatcher.DispatchToHandler(42, context);

			_serviceLocator.Received(0).Resolve<IHandler<int, EmptyObject, CancellationTokenExecutionContext>>();
			_serviceLocator.Received(1).Resolve<IHandler<int, EmptyObject, CancellationTokenExecutionContext>>(Arg.Any<string>());
			_serviceLocator.Received(0).ResolveAll<IHandler<int, EmptyObject, CancellationTokenExecutionContext>>();
			_aggregator.Received(1).AddDispatchedInput(typeof(int), _handler.GetType());
			_handler.Received(1).Handle(42, context);
			_aggregator.Received(1).AddProcessedInput(typeof(int), _handler.GetType());
			_aggregator.Received(0).AddErrorProcessingInput(typeof(int));
		}
		
		[Test]
		public void NotNamedHandlerNotFoundTest()
		{
			var context = new CancellationTokenExecutionContext(new CancellationToken());
			_serviceLocator.Resolve<IHandler<int, EmptyObject, CancellationTokenExecutionContext>>().Returns((IHandler<int, EmptyObject, CancellationTokenExecutionContext>)null);
			
			Assert.Catch<HandlerNotFoundException>(()=> _dispatcher.DispatchToHandler(42, context));

			_serviceLocator.Received(1).Resolve<IHandler<int, EmptyObject, CancellationTokenExecutionContext>>();
			_serviceLocator.Received(0).Resolve<IHandler<int, EmptyObject, CancellationTokenExecutionContext>>(Arg.Any<string>());
			_serviceLocator.Received(0).ResolveAll<IHandler<int, EmptyObject, CancellationTokenExecutionContext>>();
			_aggregator.Received(0).AddDispatchedInput(typeof(int), _handler.GetType());
			_handler.Received(0).Handle(42, context);
			_aggregator.Received(0).AddProcessedInput(typeof(int), _handler.GetType());
			_aggregator.Received(1).AddErrorProcessingInput(typeof(int));
		}
		
		[Test]
		public void NamedHandlerNotFoundTest()
		{
			var context = new CancellationTokenExecutionContext(new CancellationToken());
			context.ResolverHint = "Hello";
			_serviceLocator.Resolve<IHandler<int, EmptyObject, CancellationTokenExecutionContext>>("Hello").Returns((IHandler<int, EmptyObject, CancellationTokenExecutionContext>)null);
			
			Assert.Catch<HandlerNotFoundException>(()=> _dispatcher.DispatchToHandler(42, context));

			_serviceLocator.Received(0).Resolve<IHandler<int, EmptyObject, CancellationTokenExecutionContext>>();
			_serviceLocator.Received(1).Resolve<IHandler<int, EmptyObject, CancellationTokenExecutionContext>>("Hello");
			_serviceLocator.Received(0).ResolveAll<IHandler<int, EmptyObject, CancellationTokenExecutionContext>>();
			_aggregator.Received(0).AddDispatchedInput(typeof(int), _handler.GetType());
			_handler.Received(0).Handle(42, context);
			_aggregator.Received(0).AddProcessedInput(typeof(int), _handler.GetType());
			_aggregator.Received(1).AddErrorProcessingInput(typeof(int));
		}
		
		[Test]
		public void HandlerThrowsExceptionTest()
		{
			var context = new CancellationTokenExecutionContext(new CancellationToken());
			_handler.Handle(42, context).Throws<ArgumentException>();
			
			Assert.Catch<ArgumentException>(()=> _dispatcher.DispatchToHandler(42, context));

			_serviceLocator.Received(1).Resolve<IHandler<int, EmptyObject, CancellationTokenExecutionContext>>();
			_serviceLocator.Received(0).Resolve<IHandler<int, EmptyObject, CancellationTokenExecutionContext>>("Hello");
			_serviceLocator.Received(0).ResolveAll<IHandler<int, EmptyObject, CancellationTokenExecutionContext>>();
			_aggregator.Received(1).AddDispatchedInput(typeof(int), _handler.GetType());
			_handler.Received(1).Handle(42, context);
			_aggregator.Received(0).AddProcessedInput(typeof(int), _handler.GetType());
			_aggregator.Received(1).AddErrorProcessingInput(typeof(int));
		}
	}
}