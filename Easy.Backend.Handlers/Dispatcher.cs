using System;
using Common.Logging;

namespace Easy.Backend.Handlers
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
		{
			var handler = String.IsNullOrEmpty(resolverHint)
				? _serviceLocator.Resolve<IEmptyResultHandler<TInput, TContext>>()
				: _serviceLocator.Resolve<IEmptyResultHandler<TInput, TContext>>(resolverHint);
			handler.Handle(input, context);
			_logger.Trace($"Input {input} in context {context} was handled by handler {handler}");
			_aggregator.AddDispatchedInput(typeof(TInput));
		}		
	}
}