namespace Easy.Handlers
{
	/// <inheritdoc />
	/// <summary>
	/// Interface for a handler thatdoes not return results. EmptyObject is used as a result.
	/// </summary>
	/// <typeparam name="TInput">Type of input object</typeparam>
	/// <typeparam name="TContext">Context of object handling</typeparam>
	public interface IEmptyResultHandler<in TInput, in TContext> : IHandler<TInput, TContext, EmptyObject>
	{
		
	}
}