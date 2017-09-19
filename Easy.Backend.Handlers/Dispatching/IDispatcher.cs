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
		/// context.ResolverHint is used for resolving.
		/// If context.ResolverHint is null, default(not named) instance is used for resolving.
		/// If context.ResolverHint is not null, named instance is used for resolving. 
		/// Consider that empty string will be used for named resolving.
		/// </summary>
		/// <typeparam name="TInput">Type of input</typeparam>
		/// <typeparam name="TContext">Type of context</typeparam>
		/// <param name="input">Input data wich will be used for handling</param>
		/// <param name="context">Context for handling</param>
		void DispatchToHandler<TInput, TContext>(TInput input, TContext context)
			where TContext : IExecutionContext;
		
		/// <summary>
		/// Dispatchs input to all preregistered in DIcontainer handler of type IHandler {TInput, EmptyObject, TContext} 
		/// So IHandler {TInput, EmptyObject, TContext} should be registered in DI container that is operated by 
		/// IServiceLocator.
		/// </summary>
		/// <typeparam name="TInput">Type of input</typeparam>
		/// <typeparam name="TContext">Type of context</typeparam>
		/// <param name="input">Input data wich will be used for handling</param>
		/// <param name="context">Context for handling</param>
		void DispatchToAllHandlers<TInput, TContext>(TInput input, TContext context)
			where TContext : CancellationTokenExecutionContext;
	}
}