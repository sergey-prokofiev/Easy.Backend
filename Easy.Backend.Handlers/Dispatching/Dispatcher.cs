using System;
using Common.Logging;
using Easy.Backend.Handlers.Exceptions;
using Easy.Backend.Handlers.Handlers;
using Easy.Backend.Handlers.Metrics;

namespace Easy.Backend.Handlers.Dispatching
{	
	/// <inheritdoc />
	/// <summary>
	/// </summary>
	public class Dispatcher : IDispatcher
	{
		private readonly IServiceLocator _serviceLocator;
		private readonly IMetricsAggregator _aggregator;
		private static readonly ILog _logger = LogManager.GetLogger<Dispatcher>();

		public Dispatcher(IServiceLocator serviceLocator, IMetricsAggregator aggregator)
		{
			_serviceLocator = serviceLocator;
			_aggregator = aggregator;
		}

		public void DispatchToHandler<TInput, TContext>(TInput input, TContext context, string resolverHint = null) 
			where TContext : IExecutionContext
		{
			var handler = String.IsNullOrEmpty(resolverHint)
				? _serviceLocator.Resolve<IHandler<TInput, EmptyObject, TContext>>()
				: _serviceLocator.Resolve<IHandler<TInput, EmptyObject, TContext>>(resolverHint);
			if (handler == null)
			{
				_logger.Error($"Handler was not found for input '{input}', context '{context}' and resolverHint '{resolverHint}'");
				_aggregator.AddErrorProcessingInput(typeof(TInput));
				throw new HandlerNotFoundException();
			}
			_logger.Trace($"Input '{input}' in context '{context}' is about to be dispatched to handler '{handler}'");
			_aggregator.AddDispatchedInput(typeof(TInput), handler.GetType());
			try
			{
				handler.Handle(input, context);
			}
			catch (Exception e)
			{
				_aggregator.AddErrorProcessingInput(input.GetType());
				_logger.Error($"Error processing input '{input}', context '{context}' and resolverHint '{resolverHint}'", e);
				throw;
			}
			_aggregator.AddProcessedInput(typeof(TInput), handler.GetType());
		}		
	}
}