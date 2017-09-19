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
		private static readonly ILog _logger = LogManager.GetLogger<Dispatcher>();

		public void AddDispatchedInput(Type inputType)
		{
			_logger.Info($"Input of type {inputType} was processed");
		}
	}
}