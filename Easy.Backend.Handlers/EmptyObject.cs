namespace Easy.Handlers
{
	/// <summary>
	/// Empty object that might be returned by handlers to indicate that 'nothing' should be returned.
	/// </summary>
	public struct EmptyObject
	{
		/// <summary>
		/// Default empty instance.
		/// </summary>
		public static EmptyObject Default { get; } = new EmptyObject();
	}
}