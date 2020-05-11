using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.IO.MemoryMappedFiles;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Gengine.Core
{
	public class FileWriter
	{
		private long Offset = 0x10000000; // 256 megabytes
		private long Length = 0x20000000; // 512 megabytes

		public FileWriter(string path, string filename, string mapname, long offset = -1, long length = -1)
		{
			Offset = offset >= 0 ? offset : Offset;
			Length = length >= 0 ? length : Length;

			using (var mmf = MemoryMappedFile.CreateFromFile(@$"{path}{filename}", FileMode.Open, mapname))  // opens file
			{
				// Create a random access view, from the 256th megabyte (the offset)
				// to the 768th megabyte (the offset plus length).
				using (var accessor = mmf.CreateViewAccessor(offset, length)) // opens memory at location for length
				{
					// Make changes to the view.
					for (long memoryposition = 0; memoryposition < length; memoryposition++)
					{
						int memorystructure;                        // declare var 
						accessor.Read(memoryposition, out memorystructure); // read into var from mem


						memorystructure++;                          // modify var



						accessor.Write(memoryposition, ref memorystructure);    // write var to mem
					}
				}
			}
		}
	}
}