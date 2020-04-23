using System;
using System.IO;
using System.Threading.Tasks;

namespace PineLog
{
	public class Pinelog
	{
		private static string LogFile = null;

		public Pinelog() 
		{
			LogFile = string.Format(@"{0}\Game\Log\LOG{1}.txt"
			, Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)
			, DateTime.Now.ToString("MMddyyyy"));
		}

		public static async Task WriteEntry(object sender, string entry)
		{
			LogFile = string.Format(@"{0}\Game\Log\LOG{1}.txt"
			, Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)
			, DateTime.Now.ToString("MMddyyyy"));
			using (StreamWriter outputFile = File.AppendText(LogFile))
			{
				await outputFile.WriteAsync($"[{DateTime.Now:hh:mm:ss:fff}-{sender}] {entry}\n");
			}
		}
	}
}
