using System;
using System.Runtime.Serialization;

namespace Easy.Backend.Handlers.Exceptions
{
	[Serializable]
	public class HandlerNotFoundException : Exception
	{
		public HandlerNotFoundException()
		{
		}

		public HandlerNotFoundException(string message)
			: base(message)
		{
		}

		public HandlerNotFoundException(string message, Exception inner)
			: base(message, inner)
		{
		}

		protected HandlerNotFoundException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{

		}

	}
}