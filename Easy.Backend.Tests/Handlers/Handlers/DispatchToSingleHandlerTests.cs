using System.Threading;
using Common.Logging;
using Easy.Backend.Handlers.Dispatching;
using Easy.Backend.Handlers.Handlers;
using NSubstitute;
using NSubstitute.Exceptions;
using NUnit.Framework;

namespace Easy.Backend.Tests.Handlers.Handlers
{
	[TestFixture]
	public class DispatchToSingleHandlerTests
	{
		private IDispatcher _dispatcher;
		private DispatchToSingleHandler<int, CancellationTokenExecutionContext> _handler;
		
		[SetUp]
		public void Init()
		{
			_dispatcher = Substitute.For<IDispatcher>();
			_handler = new DispatchToSingleHandler<int, CancellationTokenExecutionContext>(_dispatcher);
		}

		[Test]
		public void HandleTest()
		{
			var context = new CancellationTokenExecutionContext(new CancellationToken());
			var result = _handler.Handle(42, context);
			Assert.AreEqual(result, EmptyObject.Default);
			_dispatcher.Received(1).DispatchToHandler(42, context);
		}
	}
}