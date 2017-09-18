using System;
using System.Threading;

namespace Easy.Wrappers
{
	public class ThreadWrapper : IThreadWrapper
	{
		public void Sleep(TimeSpan time)
		{
			Thread.Sleep(time);
		}
	}
}