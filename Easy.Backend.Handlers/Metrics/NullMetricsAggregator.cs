using System;

namespace Easy.Backend.Handlers.Metrics
{
	/// <inheritdoc />
	/// <summary>
	/// Does not aggregates metrics
	/// </summary>
	public class NullMetricsAggregator: IMetricsAggregator
	{
		public void AddDispatchedInput(Type inputType, Type handlerType)
		{
			//do nothing
		}

		public void AddProcessedInput(Type inputType, Type handlerType)
		{
			// do nothing
		}

		public void AddErrorProcessingInput(Type inputType)
		{
			//do nothing
		}
	}
}