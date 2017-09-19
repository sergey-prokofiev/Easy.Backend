using System.Threading;

namespace Easy.Backend.Handlers.Handlers
{
	/// <inheritdoc />
	/// <summary>
	/// Implements IExecutionContext using CancellationToken as a InterruptionRequest provider
	/// </summary>
	public class CancellationTokenExecutionContext : IExecutionContext
	{
		private readonly CancellationToken _token;

		public CancellationTokenExecutionContext(CancellationToken token)
		{
			_token = token;
		}
		
		public bool InterruptionRequested => _token.IsCancellationRequested;
	}
}