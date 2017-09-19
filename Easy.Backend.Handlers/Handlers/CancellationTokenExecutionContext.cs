using System.Threading;

namespace Easy.Backend.Handlers.Handlers
{
	/// <inheritdoc />
	/// <summary>
	/// Implements IExecutionContext using CancellationToken as a InterruptionRequest provider
	/// </summary>
	public class CancellationTokenExecutionContext : IExecutionContext
	{
		public CancellationTokenExecutionContext(CancellationToken token)
		{
			Token = token;
		}
		
		public bool InterruptionRequested => Token.IsCancellationRequested;

		public CancellationToken Token { get; }
	}
}