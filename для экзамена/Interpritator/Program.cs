using System;
using System.Collections.Generic;
using System.IO;

namespace Interpritator
{
	class Date
	{
		public string name;
		public double mean;
	}
	class Program
	{
		static readonly string WayOfFile = Environment.CurrentDirectory + "text.txt";
		static void Main(string[] args)
		{
			string[] text = File.ReadAllLines(WayOfFile);
			ReadDateFile(text);
		}
		static List<Date> ReadDateFile(string[] file)
		{
			List<Date> dates = new List<Date>();
			for (int i = 0; i < file.Length; i++)
			{
				if (file[i].Contains("var"))
					dates.Add(CreateDate(file[i]));
				else
					dates = ReplaceNum(file[i], dates);
			}
			return dates;
		}
		static Date CreateDate(string text)
		{
			Date date = new Date();
			date.name = text.Split(' ')[1].Trim();
			return date;
		}
		
		static List<Date> ReplaceNum(string text,List<Date>dates)
		{
			double num = Convert.ToDouble(text.Split(',')[1].Trim());
			string operation = text.Split(',')[0].Trim().Split(' ')[0].Trim();
			string name = text.Split(',')[0].Trim().Split(' ')[1].Trim();

			for (int i = 0; i < dates.Count; i++)
			{
				if (dates[i].name == name)
					switch(operation)
					{
						case "mov":dates[i].mean = num;break;
						case "add":dates[i].mean += num;break;
						case "div":dates[i].mean /= num;break;
						case "mul":dates[i].mean *= num; break;
						case "sub":dates[i].mean -= num; break;
					}
			}
			return dates;
		}
	}
}
