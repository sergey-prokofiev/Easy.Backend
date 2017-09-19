namespace Easy.Backend.Handlers.Handlers
{
	/// <summary>
	/// Interface for execution context.
	/// </summary>
	public interface IExecutionContext
	{
		/// <summary>
		/// Identifies if work interruption was requested
		/// </summary>
		bool InterruptionRequested { get; }
	}
}