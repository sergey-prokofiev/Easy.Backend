using Common.Logging;

namespace Easy.Backend.Handlers.Handlers
{
	/// <inheritdoc />
	/// <summary>
	/// Implements chain of responsibility pattern as a compile time chain. 
	/// Allows to create a chaint of responsibility of handlers for 2 handlers.
	/// Output of first handler is input of another. 
	/// Each next handler should be able to process output of a previous one. 
	/// </summary>
	/// <typeparam name="TInput">Input type</typeparam>
	/// <typeparam name="TIntermediate">Output of first handler and input of second</typeparam>
	/// <typeparam name="TOutput">Output type of second handler</typeparam>
	/// <typeparam name="TContext">Context</typeparam>
	public class ChainedHandler<TInput, TIntermediate, TOutput, TContext> : IHandler<TInput, TOutput, TContext>
		where TContext : IExecutionContext
	{
		private readonly IHandler<TInput, TIntermediate, TContext> _firstChainLink;
		private readonly IHandler<TIntermediate, TOutput, TContext> _secondChainLink;
		private static readonly ILog _logger = LogManager.GetLogger<ChainedHandler<TInput, TIntermediate, TOutput, TContext>>();

		public ChainedHandler(
			IHandler<TInput, TIntermediate,TContext> firstChainLink,
			IHandler<TIntermediate, TOutput, TContext> secondChainLink)
		{
			_firstChainLink = firstChainLink;
			_secondChainLink = secondChainLink;
		}

		public TOutput Handle(TInput input, TContext context)
		{
			var intermediate = _firstChainLink.Handle(input, context);
		    _logger.Trace($"Input '{input}' was processed by handler '{_firstChainLink}'");
			if (context.InterruptionRequested)
			{
				_logger.Debug($"Stop pipeline requested. Input: {input}");
				return default(TOutput);
			}

			var result = _secondChainLink.Handle(intermediate, context);
		    _logger.Trace($"Input '{input}' was processed by handler '{_secondChainLink}'");
			return result;
		}
	}
    
    /// <inheritdoc />
    /// <summary>
    /// Allows to create a chaint of responsibility of handlers for 3 handlers.
    /// </summary>
    public class ChainedHandler<TInput, TIntermediate, TIntermediate2, TOutput, TContext> : IHandler<TInput, TOutput, TContext>
        where TContext : IExecutionContext
    {
        private readonly IHandler<TInput, TIntermediate, TContext> _firstChainLink;
        private readonly ChainedHandler<TIntermediate, TIntermediate2, TOutput, TContext> _otherChainLinks;
        private static readonly ILog _logger = LogManager.GetLogger<ChainedHandler<TInput, TIntermediate, TIntermediate2, TOutput, TContext>>();

        public ChainedHandler(
            IHandler<TInput, TIntermediate, TContext> firstChainLink,
            IHandler<TIntermediate, TIntermediate2, TContext> secondChainLink,
	        IHandler<TIntermediate2, TOutput, TContext> thirdChainLink)
        {
	        _firstChainLink = firstChainLink;
	        _otherChainLinks = new ChainedHandler<TIntermediate, TIntermediate2, TOutput, TContext>(secondChainLink, thirdChainLink);
        }

        public TOutput Handle(TInput input, TContext context)
        {
            var intermediate = _firstChainLink.Handle(input, context);
            if (context.InterruptionRequested)
            {
                _logger.Debug($"Stop pipeline requested. Input: {input}");
                return default(TOutput);
            }

            var result = _otherChainLinks.Handle(intermediate, context);
	        _logger.Trace($"Input '{input}' was processed by handler '{_otherChainLinks}'");
	        return result;
        }
    }   

	/// <inheritdoc />
	/// <summary>
	/// Allows to create a chaint of responsibility of handlers for 4 handlers.
	/// </summary>
	public class ChainedHandler<TInput, TIntermediate, TIntermediate2, TIntermediate3, TOutput, TContext> : IHandler<TInput, TOutput, TContext>
		where TContext : IExecutionContext
	{
		private readonly IHandler<TInput, TIntermediate, TContext> _firstChainLink;
		private readonly ChainedHandler<TIntermediate, TIntermediate2, TIntermediate3, TOutput, TContext> _otherChainLinks;
		private static readonly ILog _logger = LogManager.GetLogger<ChainedHandler<TInput, TIntermediate, TIntermediate2, TIntermediate3, TOutput, TContext>>();

		public ChainedHandler(
			IHandler<TInput, TIntermediate, TContext> firstChainLink,
			IHandler<TIntermediate, TIntermediate2, TContext> secondChainLink,
			IHandler<TIntermediate2, TIntermediate3, TContext> thirdChainLink,
			IHandler<TIntermediate3, TOutput, TContext> fourthChainLink)
		{
			_firstChainLink = firstChainLink;
			_otherChainLinks = new ChainedHandler<TIntermediate, TIntermediate2, TIntermediate3, TOutput, TContext>(secondChainLink, thirdChainLink, fourthChainLink);
		}

		public TOutput Handle(TInput input, TContext context)
		{
			var intermediate = _firstChainLink.Handle(input, context);
			if (context.InterruptionRequested)
			{
				_logger.Debug($"Stop pipeline requested. Input: {input}");
				return default(TOutput);
			}

			var result = _otherChainLinks.Handle(intermediate, context);
			_logger.Trace($"Input '{input}' was processed by handler '{_otherChainLinks}'");
			return result;
		}
	}
	
	/// <inheritdoc />
	/// <summary>
	/// Allows to create a chaint of responsibility of handlers for 5 handlers.
	/// </summary>
	public class ChainedHandler<TInput, TIntermediate, TIntermediate2, TIntermediate3, TIntermediate4, TOutput, TContext> : IHandler<TInput, TOutput, TContext>
		where TContext : IExecutionContext
	{
		private readonly IHandler<TInput, TIntermediate, TContext> _firstChainLink;
		private readonly ChainedHandler<TIntermediate, TIntermediate2, TIntermediate3, TIntermediate4, TOutput, TContext> _otherChainLinks;
		private static readonly ILog _logger = LogManager.GetLogger<ChainedHandler<TInput, TIntermediate, TIntermediate2, 
			TIntermediate3, TIntermediate4, TOutput, TContext>>();

		public ChainedHandler(
			IHandler<TInput, TIntermediate, TContext> firstChainLink,
			IHandler<TIntermediate, TIntermediate2, TContext> secondChainLink,
			IHandler<TIntermediate2, TIntermediate3, TContext> thirdChainLink,
			IHandler<TIntermediate3, TIntermediate4, TContext> fourthChainLink,
			IHandler<TIntermediate4, TOutput, TContext> fifthChainLink)
		{
			_firstChainLink = firstChainLink;
			_otherChainLinks = new ChainedHandler<TIntermediate, TIntermediate2, TIntermediate3, TIntermediate4, TOutput, TContext>
				(secondChainLink, thirdChainLink, fourthChainLink, fifthChainLink);
		}

		public TOutput Handle(TInput input, TContext context)
		{
			var intermediate = _firstChainLink.Handle(input, context);
			if (context.InterruptionRequested)
			{
				_logger.Debug($"Stop pipeline requested. Input: {input}");
				return default(TOutput);
			}

			var result = _otherChainLinks.Handle(intermediate, context);
			_logger.Trace($"Input '{input}' was processed by handler '{_otherChainLinks}'");
			return result;
		}
	}
	
	/// <inheritdoc />
	/// <summary>
	/// Allows to create a chaint of responsibility of handlers for 6 handlers.
	/// </summary>
	public class ChainedHandler<TInput, TIntermediate, TIntermediate2, TIntermediate3, TIntermediate4, TIntermediate5, 
		TOutput, TContext> : IHandler<TInput, TOutput, TContext>
		where TContext : IExecutionContext
	{
		private readonly IHandler<TInput, TIntermediate, TContext> _firstChainLink;
		private readonly ChainedHandler<TIntermediate, TIntermediate2, TIntermediate3, TIntermediate4, TIntermediate5, TOutput, TContext> _otherChainLinks;
		private static readonly ILog _logger = LogManager.GetLogger<ChainedHandler<TInput, TIntermediate, TIntermediate2, 
			TIntermediate3, TIntermediate4, TIntermediate5, TOutput, TContext>>();

		public ChainedHandler(
			IHandler<TInput, TIntermediate, TContext> firstChainLink,
			IHandler<TIntermediate, TIntermediate2, TContext> secondChainLink,
			IHandler<TIntermediate2, TIntermediate3, TContext> thirdChainLink,
			IHandler<TIntermediate3, TIntermediate4, TContext> fourthChainLink,
			IHandler<TIntermediate4, TIntermediate5, TContext> fifthChainLink,
			IHandler<TIntermediate5, TOutput, TContext> sixthChainLink)
		{
			_firstChainLink = firstChainLink;
			_otherChainLinks = new ChainedHandler<TIntermediate, TIntermediate2, TIntermediate3, TIntermediate4, TIntermediate5, TOutput, TContext>
				(secondChainLink, thirdChainLink, fourthChainLink, fifthChainLink, sixthChainLink);
		}

		public TOutput Handle(TInput input, TContext context)
		{
			var intermediate = _firstChainLink.Handle(input, context);
			if (context.InterruptionRequested)
			{
				_logger.Debug($"Stop pipeline requested. Input: {input}");
				return default(TOutput);
			}

			var result = _otherChainLinks.Handle(intermediate, context);
			_logger.Trace($"Input '{input}' was processed by handler '{_otherChainLinks}'");
			return result;
		}
	}
	
	/// <inheritdoc />
	/// <summary>
	/// Allows to create a chaint of responsibility of handlers for 7 handlers.
	/// </summary>
	public class ChainedHandler<TInput, TIntermediate, TIntermediate2, TIntermediate3, TIntermediate4, TIntermediate5, 
		TIntermediate6, TOutput, TContext> : IHandler<TInput, TOutput, TContext>
		where TContext : IExecutionContext
	{
		private readonly IHandler<TInput, TIntermediate, TContext> _firstChainLink;
		private readonly ChainedHandler<TIntermediate, TIntermediate2, TIntermediate3, TIntermediate4, TIntermediate5, 
			TIntermediate6, TOutput, TContext> _otherChainLinks;
		private static readonly ILog _logger = LogManager.GetLogger<ChainedHandler<TInput, TIntermediate, TIntermediate2, 
			TIntermediate3, TIntermediate4, TIntermediate5, TIntermediate6, TOutput, TContext>>();

		public ChainedHandler(
			IHandler<TInput, TIntermediate, TContext> firstChainLink,
			IHandler<TIntermediate, TIntermediate2, TContext> secondChainLink,
			IHandler<TIntermediate2, TIntermediate3, TContext> thirdChainLink,
			IHandler<TIntermediate3, TIntermediate4, TContext> fourthChainLink,
			IHandler<TIntermediate4, TIntermediate5, TContext> fifthChainLink,
			IHandler<TIntermediate5, TIntermediate6, TContext> sixthChainLink,
			IHandler<TIntermediate6, TOutput, TContext> seventhChainLink)
		{
			_firstChainLink = firstChainLink;
			_otherChainLinks = new ChainedHandler<TIntermediate, TIntermediate2, TIntermediate3, TIntermediate4, 
					TIntermediate5, TIntermediate6, TOutput, TContext>
				(secondChainLink, thirdChainLink, fourthChainLink, fifthChainLink, sixthChainLink, seventhChainLink);
		}

		public TOutput Handle(TInput input, TContext context)
		{
			var intermediate = _firstChainLink.Handle(input, context);
			if (context.InterruptionRequested)
			{
				_logger.Debug($"Stop pipeline requested. Input: {input}");
				return default(TOutput);
			}

			var result = _otherChainLinks.Handle(intermediate, context);
			_logger.Trace($"Input '{input}' was processed by handler '{_otherChainLinks}'");
			return result;
		}
	}
}