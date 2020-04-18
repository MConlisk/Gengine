using System;
using System.IO;

namespace PineLog
{
	public class Pinelog
	{
		public static readonly string DateStamp = DateTime.Now.ToString("MMddyyyy");
		private readonly string LogPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
		private readonly string LogFilename = $"LOG{DateStamp}.txt";

		public Pinelog() { }

		public Pinelog(string logPath, string logFilename)
		{
			LogPath = IsNullOrEmpty(logPath) ? 
				Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) : logPath;
			LogFilename = IsNullOrEmpty(logFilename) ?
				LogFilename = $"LOG{DateStamp}.txt" : logFilename;
		}

		private bool IsNullOrEmpty(string str) => string.IsNullOrEmpty(str);

		public void WriteEntry(object sender, string entry)
		{
			string TimeStamp = DateTime.Now.ToString("hh:mm:ss:fff");
			File.AppendAllText(Path.Combine(LogPath, LogFilename), $"[{TimeStamp}-{sender}] {entry}\n");
		}
	}
}
