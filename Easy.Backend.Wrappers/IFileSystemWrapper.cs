using System.Collections.Generic;

namespace Easy.Wrappers
{
	/// <summary>
	/// A wrapepr for file system operations
	/// </summary>
	public interface IFileSystemWrapper
	{
		/// <summary>
		/// Create a subfoilder in %TEMP% fodler
		/// </summary>
		string CreateTempEmptyFolder();

		/// <summary>
		/// Delete a file
		/// </summary>
		void DeleteFile(string fname);

		/// <summary>
		/// Delete an empty directory
		/// </summary>
		void DeleteDirectory(string dirName);

		/// <summary>
		/// Compine parts of path
		/// </summary>
		string Combine(string path1, string path2);

		/// <summary>
		/// Search for files in a directory
		/// </summary>
		IReadOnlyCollection<string> FindFiles(string dir, string mask, bool searchInSubdirs=false);
	}
}