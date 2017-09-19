using System;

namespace Easy.Backend.Handlers
{
	/// <inheritdoc />
	/// <summary>
	/// Does not aggregates metrics
	/// </summary>
	public class NullMetricsAggregator: IMetricsAggregator
	{
		public void AddDispatchedInput(Type inputType)
		{
			//do nothing
		}
	}
}