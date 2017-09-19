using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Common.Logging;
using Easy.Backend.Wrappers;

namespace Easy.Backend.Handlers.Handlers
{
	/// <summary>
	/// Starts handling an input concurrently by all inner handlers 
	/// Results, returned by inner handlers are ignored.
	/// EmptyObject is returned as a result and all inner handlers shoud return EmptyObject too.
	/// CancellationTokenExecutionContext(or inherited types) is requered as TPL is used for concurrent runs.
	/// </summary>
	/// <typeparam name="TInput">Type of input message</typeparam>
	public class ConcurrentHandler<TInput> : IHandler<TInput, EmptyObject, CancellationTokenExecutionContext>
	{
		private readonly ITaskWrapper _taskWrapper;
		private readonly List<IHandler<TInput, EmptyObject, CancellationTokenExecutionContext>> _innerHandlers;
		private readonly TaskScheduler _scheduler;
		private readonly TimeSpan _waitTimeout;
		private static readonly ILog _logger = LogManager.GetLogger<ConcurrentHandler<TInput>>();

		public ConcurrentHandler(IEnumerable<IHandler<TInput, EmptyObject, CancellationTokenExecutionContext>> innerHandlers,
			ITaskWrapper taskWrapper, TaskScheduler scheduler, TimeSpan waitTimeout)
		{
			_taskWrapper = taskWrapper;
			_scheduler = scheduler;
			_waitTimeout = waitTimeout;
			_innerHandlers = new List<IHandler<TInput, EmptyObject, CancellationTokenExecutionContext>>(innerHandlers);
		}

		public EmptyObject Handle(TInput input, CancellationTokenExecutionContext context)
		{
			var tasks = new List<Task>(_innerHandlers.Count);
			foreach (var h in _innerHandlers)
			{
				var t = _taskWrapper.Start(
					() => h.Handle(input, context),
					context.Token,
					TaskCreationOptions.PreferFairness,
					_scheduler);
				tasks.Add(t);
				_logger.Trace($"Handler '{h}' was started to handle an input '{input}'");
			}
			_taskWrapper.WaitAll(tasks, _waitTimeout);
			_logger.Trace("All concurrent handlers were successfully finished");
			return EmptyObject.Default;
		}
	}
}