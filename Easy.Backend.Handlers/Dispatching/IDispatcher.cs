using Easy.Backend.Handlers.Handlers;

namespace Easy.Backend.Handlers.Dispatching
{
	/// <summary>
	/// Dispatch input to a handler. It is assumed that one or more handlers are registered in IServiceLocator.
	/// </summary>
	public interface IDispatcher
	{
		/// <summary>
		/// Dispatchs input to predefined handler of type IHandler {TInput, EmptyObject, TContext} 
		/// So IHandler {TInput, EmptyObject, TContext} should be registered in DI container that is operated by 
		/// IServiceLocator.
		/// If resolverHint is not provided, default(not named) instance is used for resolving.
		/// If resolverHint is provided, named instance is used for resolving
		/// </summary>
		/// <typeparam name="TInput">Type of input</typeparam>
		/// <typeparam name="TContext">Type of context</typeparam>
		/// <param name="input">Input data wich will be used for handling</param>
		/// <param name="context">Context for handling</param>
		/// <param name="resolverHint">Hint for resolving handle</param>
		void DispatchToHandler<TInput, TContext>(TInput input, TContext context, string resolverHint = null)
			where TContext : IExecutionContext;
	}
}