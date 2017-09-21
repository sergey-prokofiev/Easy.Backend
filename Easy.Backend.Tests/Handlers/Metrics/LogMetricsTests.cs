using System;
using Common.Logging;
using Easy.Backend.Handlers.Metrics;
using NSubstitute;
using NUnit.Framework;

namespace Easy.Backend.Tests.Handlers.Metrics
{
	[TestFixture]
	public class LogMetricsTests
	{
		public class MockAdapter :  ILoggerFactoryAdapter
		{

			private ILog _log;
			public MockAdapter(ILog log)
			{
				_log = log;
			}
			
			

			public ILog GetLogger(Type type)
			{
				return _log;
			}

			public ILog GetLogger(string name)
			{
				return _log;
			}

		}

		private static readonly ILog _log = Substitute.For<ILog>();
		private LogsMetricsAggregator _aggr;
		
		[SetUp]
		public void Init()
		{
			var adaptor = new MockAdapter(_log);
			LogManager.Adapter = adaptor;
			_aggr = new  LogsMetricsAggregator();
		}

		[Test]
		public void AddDispatchedInputTest()
		{
			_log.ClearReceivedCalls();
			_aggr.AddDispatchedInput(typeof(int), typeof(double));
			_log.Received(1).Info("Input of type System.Int32 was dispatched to System.Double");
		}
		
		[Test]
		public void AddProcessedInputTest()
		{
			_log.ClearReceivedCalls();
			_aggr.AddProcessedInput(typeof(int), typeof(double));
			_log.Received(1).Info("Input of type System.Int32 was processed by System.Double");
		}

		[Test]
		public void AddErrorProcessingInputTest()
		{
			_log.ClearReceivedCalls();
			_aggr.AddErrorProcessingInput(typeof(int));
			_log.Received(1).Info("Input of type System.Int32 was NOT processed");
		}
		
		[Test]
		public void NullMetricsAggregatorFakes()
		{
			var agg = new NullMetricsAggregator();
			agg.AddDispatchedInput(typeof(int), typeof(double));
			agg.AddProcessedInput(typeof(int), typeof(double));
			agg.AddErrorProcessingInput(typeof(int));
			//and nothing happens as expected
		}
	}
}