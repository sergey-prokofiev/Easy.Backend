using System.IO;
using System.Linq;
using Easy.Backend.Wrappers;
using NUnit.Framework;

namespace Easy.Backend.Tests.Wrappers
{
	[TestFixture]
	public class FileSystemTests
	{
		private readonly FileSystemWrapper _wrapper = new FileSystemWrapper();
		private string _path;

		[SetUp]
		public void Init()
		{
			_path = _wrapper.CreateTempEmptyFolder();
		}
		
		[TearDown]
		public void Cleanup()
		{
			_wrapper.DeleteDirectory(_path);
		}

		[Test]
		public void CreateTempEmptyFolderTest()
		{
			var dir = _wrapper.CreateTempEmptyFolder();
			var b = Directory.Exists(dir);
			_wrapper.DeleteDirectory(dir);
			Assert.IsTrue(b);
		}

		[Test]
		public void DeleteFileTest()
		{
			var fname = Path.Combine(_path, "1.txt");
			File.AppendAllLines(fname, new[] {"Hello world"});
			_wrapper.DeleteFile(fname);
			var b = File.Exists(fname);
			Assert.IsFalse(b);
		}
		
		[Test]
		public void DeleteDirectoryTest()
		{
			var dname = Path.Combine(_path, "1");
			Directory.CreateDirectory(dname);
			var fname = Path.Combine(dname, "1.txt");
			File.AppendAllLines(fname, new[] {"Hello world"});
			_wrapper.DeleteDirectory(dname);
			var b = Directory.Exists(dname);
			Assert.IsFalse(b);
		}
		
		[Test]
		public void CombineTest()
		{
			var path1 = _wrapper.Combine(_path, "1");
			var path2 = Path.Combine(_path, "1");
			Assert.AreEqual(path1, path2);
		}
		
		[Test]
		public void FindFilesTest()
		{
			for (var i = 0; i < 10; i++)
			{
				var fname = Path.Combine(_path, i.ToString());
				var ext = i % 2 == 0 ? ".log" : ".txt";
				fname = fname + ext;
				File.AppendAllLines(fname, new[] {"Hello world"});
			}
			var result = _wrapper.FindFiles(_path, "*.txt", true);
			Assert.AreEqual(result.Count, 5);
			for (var i = 1; i < 10; i += 2)
			{
				var fname = Path.Combine(_path, i.ToString()) + ".txt";
				var b = result.Contains(fname);
				Assert.IsTrue(b);
			}
		}
	}
}