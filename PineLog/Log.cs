using System;
using System.IO;
using System.Threading.Tasks;

namespace Pine
{
	public static class Log
	{
		private static string LogFile = null;

		static Log() 
		{
			LogFile = string.Format(@"{0}\Game\Log\LOG{1}.txt"
			, Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)
			, DateTime.Now.ToString("MMddyyyy"));
		}

		public static void Write(object sender, string entry)
		{
			LogFile = string.Format(@"{0}\Game\Log\LOG{1}.txt"
			, Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)
			, DateTime.Now.ToString("MMddyyyy"));
			using (StreamWriter outputFile = File.AppendText(LogFile))
			{
				outputFile.WriteAsync($"[{DateTime.Now:hh:mm:ss:fff}-{sender}] {entry}\n");
			}
		}
	}
}
