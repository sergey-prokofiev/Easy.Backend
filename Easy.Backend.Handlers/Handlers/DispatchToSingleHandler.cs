using Common.Logging;
using Easy.Backend.Handlers.Dispatching;

namespace Easy.Backend.Handlers.Handlers
{
	
	/// <inheritdoc />
	/// <summary>
	/// Dispatches an input to a handler using IDispatcher.DispatchToHandler method. 
	/// Usually needed as a last step of input handling, during which context is clarified and additional steps are needed.
	/// </summary>
	public class DispatchToSingleHandler<TInput, TContext> : IHandler<TInput, EmptyObject, TContext> where TContext : IExecutionContext
	{
		private readonly IDispatcher _dispatcher;
		private static readonly ILog _logger = LogManager.GetLogger<DispatchToSingleHandler<TInput, TContext>>();
		
		public DispatchToSingleHandler(IDispatcher dispatcher)
		{
			_dispatcher = dispatcher;
		}
		
		public EmptyObject Handle(TInput input, TContext context)
		{
			_logger.Trace($"Dispatching input '{input}' with context '{context}' to handler");
			_dispatcher.DispatchToHandler(input, context);
			return EmptyObject.Default;
		}
	}
}