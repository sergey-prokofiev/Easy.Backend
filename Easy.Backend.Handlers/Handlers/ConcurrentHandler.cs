using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Common.Logging;
using Easy.Backend.Wrappers;

namespace Easy.Backend.Handlers.Handlers
{
	/// <inheritdoc />
	/// <summary>
	/// Starts handling an input concurrently by all inner handlers 
	/// Results, returned by inner handlers are ignored.
	/// EmptyObject is returned as a result and all inner handlers shoud return EmptyObject too.
	/// CancellationTokenExecutionContext(or inherited types) is requered as TPL is used for concurrent runs.
	/// </summary>
	/// <typeparam name="TInput">Type of input message</typeparam>
	/// <typeparam name="TContext">Execution context. Should be CancellationTokenExecutionContext.</typeparam>
	public class ConcurrentHandler<TInput, TContext> : IHandler<TInput, EmptyObject, TContext>
		where TContext: CancellationTokenExecutionContext
	{
		private readonly ITaskWrapper _taskWrapper;
		private readonly List<IHandler<TInput, EmptyObject, CancellationTokenExecutionContext>> _innerHandlers;
		private readonly TaskScheduler _scheduler;
		private readonly TimeSpan _waitTimeout;
		private readonly TaskCreationOptions _taskCreationOptions;

		private static readonly ILog _logger = LogManager.GetLogger<ConcurrentHandler<TInput, TContext>>();

		public ConcurrentHandler(IEnumerable<IHandler<TInput, EmptyObject, CancellationTokenExecutionContext>> innerHandlers,
			ITaskWrapper taskWrapper, TaskScheduler scheduler, TimeSpan waitTimeout, 
			TaskCreationOptions taskCreationOptions = TaskCreationOptions.PreferFairness)
		{
			_taskWrapper = taskWrapper;
			_scheduler = scheduler;
			_waitTimeout = waitTimeout;
			_taskCreationOptions = taskCreationOptions;
			_innerHandlers = new List<IHandler<TInput, EmptyObject, CancellationTokenExecutionContext>>(innerHandlers);
		}

		public EmptyObject Handle(TInput input, TContext context)
		{
			var tasks = new List<Task>(_innerHandlers.Count);
			foreach (var h in _innerHandlers)
			{
				var t = _taskWrapper.Start(() => h.Handle(input, context),
					context.Token, _taskCreationOptions, _scheduler);
				tasks.Add(t);
				_logger.Trace($"Handler '{h}' was started to handle an input '{input}'");
			}
			_taskWrapper.WaitAll(tasks, _waitTimeout);
			_logger.Trace("All concurrent handlers were successfully finished");
			return EmptyObject.Default;
		}
	}
}