namespace Easy.Backend.Handlers.Handlers
{
	/// <summary>
	/// Handler of some input. Takes input and context and convert it to output.
	/// </summary>
	/// <typeparam name="TInput">Type of input</typeparam>
	/// <typeparam name="TContext">Type of context. It can be CancellationToken, StackTrace, TranscationScope etc.</typeparam>
	/// <typeparam name="TOutput">Type of output. Use EmptyObject if nothing should be returned</typeparam>
	public interface IHandler<in TInput, in TContext, out TOutput>
	{
		/// <summary>
		/// Handles input according to context
		/// </summary>
		/// <param name="input">Data to process</param>
		/// <param name="context">Context which can affect handling</param>
		/// <returns>Handling result</returns>
		TOutput Handle(TInput input, TContext context);
	}
}