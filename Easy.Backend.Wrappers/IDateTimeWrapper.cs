using System;

namespace Easy.Backend.Wrappers
{
	/// <summary>
	/// Wraps date time static members to be reused for testing.
	/// </summary>
	public interface IDateTimeWrapper
	{
		/// <summary>
		/// return current date/time
		/// </summary>
		DateTime Now();

		/// <summary>
		/// Returns current date/time as utc
		/// </summary>
		DateTime UtcNow();

		/// <summary>
		/// Returns current offset of the current time zone from utc. Date lights settongs are considered.
		/// </summary>
		TimeSpan CurrentOffset();		
	}
}