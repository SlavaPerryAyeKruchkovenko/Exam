using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;

namespace subtitri
{
	class Word
	{
		public DateTime timeStart;
		public DateTime timeFinish;
		public string position;
		public ConsoleColor color;
		public string sentence;
	}
	class Program
	{
		static ushort sec = 0;
		static ushort min = 0;
		static ushort hours = 0;
		static DateTime timer = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, hours, min, sec);
		static readonly string  wayOfFile = Environment.CurrentDirectory + "\\subs.txt";
		
		static void Main()
		{
			string[] subsFile = File.ReadAllLines(wayOfFile);
			Table.WriteTable();
			GetSubs(subsFile);
		}
		static void GetSubs(string[] subsFile)
		{
			var words = new List<Word>();
			foreach (string sub in subsFile)
			{			
				words.Add(SortSubs(sub));					
			}
			while (true)
				counTimer(words);
		}
		static Word SortSubs(string sub)
		{
			int j = 0;
			Word word = new Word();
			word.timeStart = Convert.ToDateTime("00:"+sub.Split(' ')[0].Split("-")[0]);
			word.timeFinish = Convert.ToDateTime("00:" + sub.Split(' ')[0].Split("-")[1]);
			if (sub.Contains('[')&& sub.Contains(']'))
			{				
				word.position = sub.Split(' ')[1].Remove(0, 1).Trim(',');
				word.color = (ConsoleColor)Enum.Parse(typeof(ConsoleColor), sub.Split(' ')[2].Remove(sub.Split(' ')[2].Length - 1));
				j = 2;
			}
			else
			{
				word.position = "bottom";
				word.color = ConsoleColor.White;
			}
			for (int i = 1+j; i < sub.Split(' ').Length; i++)
			{
				word.sentence += sub.Split(' ')[i]+" ";
			}
			return word;
		}
		static void counTimer(List<Word> words)
		{
			timer = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, hours,min,sec);
			Thread.Sleep(1000);
			sec++;
			if(sec==60)
			{
				sec = 0;
				min += 1;
			}
			if(min==60)
			{
				min = 0;
				hours += 1;
			}
			if(hours==24)			
				hours = 0;

			for (int i = 0; i < words.Count; i++)
			{
				if (words[i].timeStart == timer)
					WriteWord(words[i]);
				if (words[i].timeFinish == timer)
					DeleteWord(words[i]);

			}
		}
		static void WriteWord(Word word)
		{
			Console.ForegroundColor = word.color;
			SetCursor(word);
			Console.WriteLine(word.sentence.Trim());
		}
		static void DeleteWord(Word word)
		{
			SetCursor(word);
			for (int i = 0; i < word.sentence.Trim().Length; i++)
			{
				Console.Write(" ");
			}
		}
		static void SetCursor(Word word)
		{
			switch(word.position.ToLower())
			{
				case "top":Console.SetCursorPosition(Console.WindowWidth / 2-word.sentence.Length/2, Console.WindowHeight / 4+1);break;
				case "bottom": Console.SetCursorPosition(Console.WindowWidth / 2 - word.sentence.Length / 2, Console.WindowHeight / 4*3-2); break;
				case "left": Console.SetCursorPosition(Console.WindowWidth / 4+1, Console.WindowHeight / 2-1); break;
				case "right": Console.SetCursorPosition(Console.WindowWidth / 4*3- word.sentence.Length, Console.WindowHeight / 2-1); break;
			}
		}
	}
}
