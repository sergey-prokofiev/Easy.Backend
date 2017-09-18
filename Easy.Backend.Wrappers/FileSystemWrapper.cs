using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Easy.Wrappers
{
	public class FileSystemWrapper : IFileSystemWrapper
	{
		public string CreateTempEmptyFolder()
		{
			var tmp = Path.GetTempPath();
			var dir = Guid.NewGuid().ToString();
			var result = Path.Combine(tmp, dir);
			Directory.CreateDirectory(result);
			return result;
		}

		public void DeleteFile(string fname)
		{
			File.Delete(fname);
		}

		public void DeleteDirectory(string dirName)
		{
			Directory.Delete(dirName, true);
		}

		public string Combine(string path1, string path2)
		{
			var result = Path.Combine(path1, path2);
			return result;
		}

		public IReadOnlyCollection<string> FindFiles(string dir, string mask, bool searchInSubdirs)
		{
			var opt = searchInSubdirs ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly;
			var files = Directory.EnumerateFiles(dir, mask, opt);
			return files.ToArray();
		}
		
	}
}