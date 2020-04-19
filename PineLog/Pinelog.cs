using System;
using System.IO;

namespace PineLog
{
	public class Pinelog
	{
		private string LogFile = string.Format(@"{0}\Game\Log\LOG{1}.txt"
			, Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)
			, DateTime.Now.ToString("MMddyyyy"));

		public Pinelog() { }

		public void WriteEntry(object sender, string entry)
		{
			string TimeStamp = DateTime.Now.ToString("hh:mm:ss:fff");
			File.AppendAllText(LogFile, $"[{TimeStamp}-{sender}] {entry}\n");
		}
	}
}
