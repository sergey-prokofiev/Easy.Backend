using System;
using System.Collections.Generic;

namespace Easy.Backend.Handlers.Dispatching
{
	/// <summary>
	/// Service locator that resolves previously registered instancers of objects.
	/// Actually it is assumed to be a wrapper on DI container. 
	/// </summary>
	public interface IServiceLocator
	{
		/// <summary>
		/// Resolve an instance with default registration
		/// </summary>
		/// <typeparam name="T">Instance type</typeparam>
		/// <returns>Resolved instance</returns>
		T Resolve<T>();

		/// <summary>
		/// Resolve an instance with named registration
		/// </summary>
		/// <typeparam name="T">Instance type</typeparam>
		/// <returns>Resolved instance</returns>
		T Resolve<T>(string name);

		/// <summary>
		/// Resolves all registrations for a type.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <returns>Resolved instances</returns>
		IReadOnlyCollection<T> ResolveAll<T>();

	}
}