using Common.Logging;
using Easy.Backend.Handlers.Dispatching;

namespace Easy.Backend.Handlers.Handlers
{
	/// <inheritdoc />
	/// <summary>
	/// Dispatches an input to a all handlers using IDispatcher.DispatchToAllHandlers method. 
	/// Usually needed as a last step of input handling, to trigger new processing pipelines
	/// </summary>
	public class DispatchToAllHandlers<TInput, TContext> : IHandler<TInput, EmptyObject, TContext> 
		where TContext : CancellationTokenExecutionContext
	{
		private readonly IDispatcher _dispatcher;
		private static readonly ILog _logger = LogManager.GetLogger<DispatchToSingleHandler<TInput, TContext>>();
		
		public DispatchToAllHandlers(IDispatcher dispatcher)
		{
			_dispatcher = dispatcher;
		}
		
		public EmptyObject Handle(TInput input, TContext context)
		{
			_logger.Trace($"Dispatching input '{input}' with context '{context}' to handler");
			_dispatcher.DispatchToAllHandlers(input, context);
			return EmptyObject.Default;
		}
		
	}
}