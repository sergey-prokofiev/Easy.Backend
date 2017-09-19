using System;
using Common.Logging;

namespace Easy.Backend.Handlers
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