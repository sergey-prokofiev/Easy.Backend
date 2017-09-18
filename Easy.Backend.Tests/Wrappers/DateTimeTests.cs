using System;
using Easy.Wrappers;
using NUnit.Framework;

namespace Easy.Handlers.Tests.Wrappers
{
	[TestFixture]
	public class DateTimeTests
	{
		private readonly DateTimeWrapper _wrapper = new DateTimeWrapper();
		
		[Test]
		public void NowTest()
		{
			var now1 = _wrapper.Now();
			var now2 = DateTime.Now;
			var diff = now2 - now1;
			Assert.IsTrue(diff < TimeSpan.FromSeconds(3));
		}
		
		[Test]
		public void UtcNowTest()
		{
			var now1 = _wrapper.UtcNow();
			var now2 = DateTime.UtcNow;
			var diff = now2 - now1;
			Assert.IsTrue(diff < TimeSpan.FromSeconds(3));
		}
		
		[Test]
		public void CurrentOffsetTest()
		{
			var offset1 = _wrapper.CurrentOffset();
			var offset2 = DateTimeOffset.Now.Offset;
			Assert.AreEqual(offset1, offset2);
		}
	}
}