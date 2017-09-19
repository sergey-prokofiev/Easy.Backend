using System;
using Common.Logging;
using Easy.Backend.Handlers.Dispatching;

namespace Easy.Backend.Handlers.Metrics
{
	/// <inheritdoc />
	/// <summary>
	/// Aggregates metrics to log files on 'Info' level
	/// </summary>
	public class LogsMetricsAggregator: IMetricsAggregator
	{
		private static readonly ILog _logger = LogManager.GetLogger<LogsMetricsAggregator>();

		public void AddDispatchedInput(Type inputType, Type handlerType)
		{
			_logger.Info($"Input of type {inputType} was dispatched to {handlerType}");
		}

		public void AddProcessedInput(Type inputType, Type handlerType)
		{
			_logger.Info($"Input of type {inputType} was processed by {handlerType}");
		}

		public void AddErrorProcessingInput(Type inputType)
		{
			_logger.Info($"Input of type {inputType} was NOT processed");
		}
	}
}