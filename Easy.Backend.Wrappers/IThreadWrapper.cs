using System;
using System.Collections.Generic;

namespace Easy.Wrappers
{
	/// <summary>
	/// Wraps static thread calls
	/// </summary>
	public interface IThreadWrapper
	{
		/// <summary>
		/// Sleep given time
		/// </summary>
		void Sleep(TimeSpan time);

	}
}