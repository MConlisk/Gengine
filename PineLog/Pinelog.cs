using System;
using System.IO;
using System.Threading.Tasks;

namespace PineLog
{
	public class Pinelog
	{
		private static object Header = null;
		private static string LogFile = null;

		public Pinelog(object sender) 
		{
			Header = sender;
			LogFile = string.Format(@"{0}\Game\Log\LOG{1}.txt"
			, Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)
			, DateTime.Now.ToString("MMddyyyy"));
		}

		public void WriteEntryold(string entry)
		{
			string TimeStamp = DateTime.Now.ToString("hh:mm:ss:fff");
			using (StreamWriter writer = File.CreateText(LogFile))
			{
				writer.Write($"[{TimeStamp}-{Header}] {entry}\n");
			}
			
		}

		public static async Task WriteEntry(string entry)
		{
			string TimeStamp = DateTime.Now.ToString("hh:mm:ss:fff");
			using (StreamWriter outputFile = new StreamWriter(LogFile))
			{
				await outputFile.WriteAsync($"[{TimeStamp}-{Header}] {entry}\n");
			}
		}
	}
}
