﻿using System;

namespace Easy.Backend.Wrappers
{
	/// <inheritdoc />
	public class DateTimeWrapper : IDateTimeWrapper
	{
		public DateTime Now()
		{
			return DateTime.Now;
		}

		public DateTime UtcNow()
		{
			return DateTime.UtcNow;
		}

		public TimeSpan CurrentOffset()
		{
			return DateTimeOffset.Now.Offset;
		}
	}
}