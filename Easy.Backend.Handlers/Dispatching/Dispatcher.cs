using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Common.Logging;
using Easy.Backend.Handlers.Exceptions;
using Easy.Backend.Handlers.Handlers;
using Easy.Backend.Handlers.Metrics;
using Easy.Backend.Handlers.TasksManagement;
using Easy.Backend.Wrappers;

namespace Easy.Backend.Handlers.Dispatching
{	
	/// <inheritdoc />
	public class Dispatcher : IDispatcher
	{
		private readonly IServiceLocator _serviceLocator;
		private readonly IMetricsAggregator _aggregator;
		private readonly ITasksManager _tasksManager;
		private static readonly ILog _logger = LogManager.GetLogger<Dispatcher>();

		public Dispatcher(IServiceLocator serviceLocator, IMetricsAggregator aggregator, ITasksManager tasksManager)
		{
			_serviceLocator = serviceLocator;
			_aggregator = aggregator;
			_tasksManager = tasksManager;
		}

		public void DispatchToHandler<TInput, TContext>(TInput input, TContext context) 
			where TContext : IExecutionContext
		{
			var handler = String.IsNullOrEmpty(context.ResolverHint)
				? _serviceLocator.Resolve<IHandler<TInput, EmptyObject, TContext>>()
				: _serviceLocator.Resolve<IHandler<TInput, EmptyObject, TContext>>(context.ResolverHint);
			if (handler == null)
			{
				_logger.Error($"Handler was not found for input '{input}', context '{context}' and resolverHint '{context.ResolverHint}'");
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
				_logger.Error($"Error processing input '{input}', context '{context}' and resolverHint '{context.ResolverHint}'", e);
				throw;
			}
			_aggregator.AddProcessedInput(typeof(TInput), handler.GetType());
		}

		public void DispatchToAllHandlers<TInput, TContext>(TInput input, TContext context) where TContext : CancellationTokenExecutionContext
		{
			_logger.Trace($"Dispatching input '{input}' with context {context} to all handlers");
			var handlers = _serviceLocator.ResolveAll<TInput>();
			var tasks = new List<Task>(handlers.Count);

			foreach (var handler in handlers)
			{
				var task = _tasksManager.StartTask(() => DispatchToHandler(input, context), context.Token);
				_logger.Trace($"Input '{input}' was dispatched to handler '{handler}'");
				tasks.Add(task);
			}
			_tasksManager.WaitAllTasks(tasks);
			_logger.Trace($"Dispatching input '{input}' was handled by all handlers");			
		}
	}
}