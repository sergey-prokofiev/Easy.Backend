using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Runtime.Serialization.Formatters.Binary;
using Easy.Backend.Handlers.Exceptions;
using NUnit.Framework;

namespace Easy.Backend.Tests.Handlers.Exceptions
{
	[TestFixture]
	public class HasndlerNotFoundExceptionTests
	{
		[Test]
		public void DefaultCtorTest()
		{
			var e = new HandlerNotFoundException();
			Assert.AreEqual("Exception of type 'Easy.Backend.Handlers.Exceptions.HandlerNotFoundException' was thrown.", e.Message);
			Assert.AreEqual(e.InnerException, null);
		}

		[Test]
		public void MsgCtorTest()
		{
			var e = new HandlerNotFoundException("Hello");
			Assert.AreEqual("Hello", e.Message);
			Assert.AreEqual(e.InnerException, null);
		}
		
		[Test]
		public void MsgAndInnerCtorTest()
		{
			var inner = new Exception();
			var e = new HandlerNotFoundException("Hello", inner);
			Assert.AreEqual("Hello", e.Message);
			Assert.AreEqual(e.InnerException, inner);
		}

		[Test]
		public void SerializeCtorTest()
		{
			var e = new HandlerNotFoundException("Hello");
			var bf = new BinaryFormatter();
			var buffer = new byte[1024];
			var sout = new MemoryStream(buffer);  
			bf.Serialize(sout, e);
			sout.Position = 0;
			e = (HandlerNotFoundException) bf.Deserialize(sout);  
			Assert.AreEqual("Hello", e.Message);
			Assert.AreEqual(e.InnerException, null);
		}

	}
}