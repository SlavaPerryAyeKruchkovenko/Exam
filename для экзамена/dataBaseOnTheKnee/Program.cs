using System;
using System.Collections.Generic;
using System.IO;

namespace dataBaseOnTheKnee
{
	class Table
	{
		public string nameTable;
		public List<string> nameCloum = new List<string>();
		public List<string> dataType = new List<string>();
		public bool isHaveLinkOnTable = true;
		public string linkOnAnotherTable;
		public string linkOnAnotherTableNameCloum;
	}
	class DataBase
	{
		public string nameData;
		public List<string> strings = new List<string>();
		public List<bool> boolean = new List<bool>();
		public List<int> integer = new List<int>();
	}
	class Program
	{
		static readonly string wayOfFile = Environment.CurrentDirectory + "\\database.txt";
		static void Main(string[] args)
		{
			string[] database = File.ReadAllLines(wayOfFile);
			int i;
			sortFile(ReadExample(database, out i), database, i + 2); 
		}
		static List<Table>  ReadExample(string[]database,out int i)
		{
			var tables = new List<Table>();
			i=0;
			while(database[i].Trim()!= "======================")
			{
				tables.Add(ReadExampleTablet(database, i, out i));
				i++;
			}
			return tables;
		}
		static Table ReadExampleTablet(string[] database,int i,out int n)
		{
			Table table = new Table();
			table.nameTable = database[i].Trim();
			i+=2;
			while(database[i].Trim()!="")
			{
				string[] stringTable = database[i].Split(':');
				table.nameCloum.Add(stringTable[0]);
				table.dataType.Add(stringTable[1]);
				if (stringTable.Length > 2)
				{
					table.linkOnAnotherTable = stringTable[2].Trim().Remove(0, 1);
					table.linkOnAnotherTableNameCloum = stringTable[3].Trim().Remove(stringTable[3].Length - 1);
				}
				else
					table.isHaveLinkOnTable = default;
				i++;
			}
			n = i;
			return table;
		}
		static List<DataBase> sortFile(List<Table>tables,string[]database,int i)
		{
			List<DataBase> datas = new List<DataBase>();
			while(database[i].Trim()!="+")
			{
				datas.Add(sortFileTable(tables, database, i, out i));
				i++;
			}
			return datas;
		}
		static DataBase sortFileTable(List<Table> tables, string[] database, int i,out int o)
		{
			DataBase data = new DataBase();
			data.nameData = database[i].Trim();

			i+=2;
			int n = SearchTable(tables, data.nameData);
			while(database[i].Trim()!="")
			{
				string[] elements = database[i].Split(';');
				string a = database[i];
				for (int j = 0; j < elements.Length; j++)
				{
					if (tables[n].dataType[j].ToLower().Trim() == "int")
						data.integer.Add(Convert.ToInt32(elements[j]));
					else if (tables[n].dataType[j].ToLower().Trim() == "string")
						data.strings.Add(elements[j]);
					else if (tables[n].dataType[j].ToLower().Trim() == "bool")
						data.boolean.Add(Convert.ToBoolean(elements[j]));
				}
				i++;
			}
			o = i;
			return data;
			
		}
		static int SearchTable(List<Table> tables,string dataName)
		{
			for (int i = 0; i < tables.Count; i++)
			{
				if (tables[i].nameTable == dataName)
					return i;
			}
			return default;
		}
	}
}
