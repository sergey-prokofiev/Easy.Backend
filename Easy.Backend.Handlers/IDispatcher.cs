namespace Easy.Backend.Handlers
{
	/// <summary>
	/// Dispatch input to a handler. It is assumed that one or more handlers are registered in IServiceLocator.
	/// </summary>
	public interface IDispatcher
	{
		/// <summary>
		/// Dispatchs one handler according to resolverHint, if null - resolves instance without name
		/// </summary>
		/// <typeparam name="TInput">Type of input</typeparam>
		/// <typeparam name="TContext">Type of context</typeparam>
		/// <param name="input">Input data wich will be used for handling</param>
		/// <param name="context">Context for handling</param>
		/// <param name="resolverHint">Hint for resolving handle</param>
		void DispatchToHandler<TInput, TContext>(TInput input, TContext context, string resolverHint = null);
	}
}