using System.Security.Principal;
using System.Threading;
using Common.Logging;
using Easy.Backend.Handlers.Handlers;
using NUnit.Framework;

namespace Easy.Backend.Tests.Handlers.Handlers
{
	[TestFixture]
	public class ChainedHandlerTests
	{
		private class IncrementHandler : IHandler<int, int, CancellationTokenExecutionContext>
		{
			private readonly CancellationTokenSource _source;
			public IncrementHandler(CancellationTokenSource source)
			{
				_source = source;
			}

			public bool ChangeCancellation;
			
			public int HandleCallsCount;
			
			public int Handle(int input, CancellationTokenExecutionContext context)
			{
				Assert.AreEqual(context.Token, _source.Token);
				HandleCallsCount++;
				if (ChangeCancellation)
				{
					_source.Cancel();
				}
				return input + 1;
			}
		}

		private CancellationTokenExecutionContext _context;
		private CancellationTokenSource _source;
		private IncrementHandler _increment;
		[SetUp]
		public void Init()
		{
			_source = new CancellationTokenSource();
			_context = new CancellationTokenExecutionContext(_source.Token);
			_increment = new IncrementHandler(_source);
		}
		
		[Test]
		public void HandleTest()
		{
			var handler = new ChainedHandler<int, int, int, int, int, int, int, int, CancellationTokenExecutionContext>(
				_increment, _increment, _increment, _increment, _increment, _increment, _increment);
			var result = handler.Handle(42, _context);
			Assert.AreEqual(result, 49);
			Assert.AreEqual(_increment.HandleCallsCount, 7);
		}
		
		[Test]
		public void HandleBreak7Test()
		{
			var handler = new ChainedHandler<int, int, int, int, int, int, int, int, CancellationTokenExecutionContext>(
				_increment, _increment, _increment, _increment, _increment, _increment, _increment);
			_increment.ChangeCancellation = true;
			var result = handler.Handle(42, _context);
			Assert.AreEqual(result, 0);
			Assert.AreEqual(_increment.HandleCallsCount, 1);
		}
		
		[Test]
		public void HandleBreak6Test()
		{
			var handler = new ChainedHandler<int, int, int, int, int, int, int, CancellationTokenExecutionContext>(
				_increment, _increment, _increment, _increment, _increment, _increment);
			_increment.ChangeCancellation = true;
			var result = handler.Handle(42, _context);
			Assert.AreEqual(result, 0);
			Assert.AreEqual(_increment.HandleCallsCount, 1);
		}
		
		[Test]
		public void HandleBreak5Test()
		{
			var handler = new ChainedHandler<int, int, int, int,int, int, CancellationTokenExecutionContext>(
				_increment, _increment, _increment, _increment, _increment);
			_increment.ChangeCancellation = true;
			var result = handler.Handle(42, _context);
			Assert.AreEqual(result, 0);
			Assert.AreEqual(_increment.HandleCallsCount, 1);
		}

		[Test]
		public void HandleBreak4Test()
		{
			var handler = new ChainedHandler<int, int, int, int,int, CancellationTokenExecutionContext>(
				_increment, _increment, _increment, _increment);
			_increment.ChangeCancellation = true;
			var result = handler.Handle(42, _context);
			Assert.AreEqual(result, 0);
			Assert.AreEqual(_increment.HandleCallsCount, 1);
		}
		
		[Test]
		public void HandleBreak3Test()
		{
			var handler = new ChainedHandler<int, int, int, int, CancellationTokenExecutionContext>(
				_increment, _increment, _increment);
			_increment.ChangeCancellation = true;
			var result = handler.Handle(42, _context);
			Assert.AreEqual(result, 0);
			Assert.AreEqual(_increment.HandleCallsCount, 1);
		}
		
		[Test]
		public void HandleBreak2Test()
		{
			var handler = new ChainedHandler<int, int, int, CancellationTokenExecutionContext>(
				_increment, _increment);
			_increment.ChangeCancellation = true;
			var result = handler.Handle(42, _context);
			Assert.AreEqual(result, 0);
			Assert.AreEqual(_increment.HandleCallsCount, 1);
		}
	}
}