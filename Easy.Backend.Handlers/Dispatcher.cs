using System;
using Easy.Wrappers;

namespace Easy.Backend.Handlers
{	
	/// <inheritdoc />
	/// <summary>
	/// </summary>
	public class Dispatcher : IDispatcher
	{
		private readonly IServiceLocator _serviceLocator;

		public Dispatcher(IServiceLocator serviceLocator)
		{
			_serviceLocator = serviceLocator;
		}

		public void DispatchToHandler<TInput, TContext>(TInput input, TContext context, string resolverHint = null)
		{
			var handler = String.IsNullOrEmpty(resolverHint)
				? _serviceLocator.Resolve<IEmptyResultHandler<TInput, TContext>>()
				: _serviceLocator.Resolve<IEmptyResultHandler<TInput, TContext>>(resolverHint);
			var wrapper = new FileSystemWrapper();
			wrapper.CreateTempEmptyFolder();
			handler.Handle(input, context);
		}		
	}
}