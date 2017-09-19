using System;

namespace Easy.Backend.Handlers.Metrics
{
	/// <summary>
	/// Metrics aggregator aggregates some statistics on this library usage
	/// </summary>
	public interface IMetricsAggregator
	{
		/// <summary>
		/// Adds information about a dispatched input to statistics
		/// </summary>
		void AddDispatchedInput(Type inputType, Type handlerType);

		/// <summary>
		/// Adds information a processed input by a handler to statistics. 
		/// </summary>
		void AddProcessedInput(Type inputType, Type handlerType);

		/// <summary>
		/// Adds information about critical error of input processing
		/// </summary>
		void AddErrorProcessingInput(Type inputType);
	}
}