using System;

namespace Easy.Backend.Handlers
{
	/// <summary>
	/// Metrics aggregator aggregates some statistics on this library usage
	/// </summary>
	public interface IMetricsAggregator
	{
		/// <summary>
		/// Adds information about one input processing to statistics
		/// </summary>
		void AddDispatchedInput(Type inputType);
	}
}