using System.Threading;
using Easy.Backend.Handlers.Dispatching;
using Easy.Backend.Handlers.Handlers;
using NSubstitute;
using NUnit.Framework;

namespace Easy.Backend.Tests.Handlers.Handlers
{
	[TestFixture]
	public class DispatchToAllHandlersTests
	{
		private IDispatcher _dispatcher;
		private DispatchToAllHandlers<int, CancellationTokenExecutionContext> _handler;
		
		[SetUp]
		public void Init()
		{
			_dispatcher = Substitute.For<IDispatcher>();
			_handler = new DispatchToAllHandlers<int, CancellationTokenExecutionContext>(_dispatcher);
		}

		[Test]
		public void HandleTest()
		{
			var context = new CancellationTokenExecutionContext(new CancellationToken());
			var result = _handler.Handle(42, context);
			Assert.AreEqual(result, EmptyObject.Default);
			_dispatcher.Received(1).DispatchToAllHandlers(42, context);
		}
	}
}