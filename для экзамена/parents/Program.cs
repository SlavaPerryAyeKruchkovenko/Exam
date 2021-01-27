using System;
using System.Collections.Generic;
using System.IO;

namespace parents
{
	class Relation
	{
		public int firstPersonId;
		public int secondPersonId;
		public string relation;
	}
	class Person
	{
		public int id;
		public string firstname;
		public string secondName;
		public DateTime birthDay;
	}
	class Program
	{
		private static readonly string wayToFile = Environment.CurrentDirectory + "\\parents.txt";
		static void Main(string[] args)
		{
			int n,id1,id2;
			string[] parents = File.ReadAllLines(wayToFile);			
			string[] order = parents[0].Split(';');
			List<Person> persons = FoundFamily(parents, order,out n);
			List<Relation> relations = FoundRelations(parents,n);
			readId(out id1, out id2);
			Console.WriteLine(WriteRelation(relations, id1, id2));
		}
		static void readId(out int id1,out int id2)
		{
			Console.WriteLine("Введите 1 id");
			id1 = Convert.ToInt32(Console.ReadLine());
			Console.WriteLine("Введите 2 id");
			id2 = Convert.ToInt32(Console.ReadLine());
		}

		static List<Person> FoundFamily(string[] parents,string[] order,out int i)
		{
			List<Person> persons = new List<Person>();
			string[] parent;
			i = 1;
			while(parents[i].Trim()!="")
			{
				parent = parents[i].Split(';');
				persons.Add(FoundInformation(parent, order));
				i++;
			}
			return persons;
		}
		static Person FoundInformation(string[] parent, string[] order)
		{
			Person person = new Person();
			for (int i = 0; i < 4; i++)
			{
				switch(order[i].ToLower())
				{
					case "id": person.id = Convert.ToInt32(parent[i]);break;
					case "lastname":person.secondName = parent[i];break;
					case "firstname":person.firstname = parent[i];break;
					case "birthdate":person.birthDay = Convert.ToDateTime(parent[i]);break;
				}
			}
			return person;
		}
		static List<Relation> FoundRelations(string[] parents,int n)
		{
			n ++;
			List<Relation> relations = new List<Relation>();
			while (parents[n].Trim()!="+")
			{
				string parent = parents[n];
				relations.Add(FoundRelationInformation(parent));
				n++;
			}
			return relations;
		}
		static Relation FoundRelationInformation(string parent)
		{
			Relation relation = new Relation();
			string[] users = parent.Split("<->");
			relation.firstPersonId = Convert.ToInt32(users[0]);
			relation.secondPersonId = Convert.ToInt32(users[1].Split("=")[0]);
			relation.relation = users[1].Split("=")[1];
			return relation;
		}
		static string WriteRelation(List<Relation> relations,int id1,int id2)
		{
			string relation;
			if (SearchInDataBase(relations, id1, id2, out relation))
				return relation;

			return FoundRelationship(relations, id1, id2);
		}
		static bool SearchInDataBase(List<Relation> relations,int id1,int id2,out string relation)
		{
			relation = "none";
			for (int i = 0; i < relations.Count; i++)
			{
				if(relations[i].firstPersonId==id1&& relations[i].secondPersonId == id2)
				{
					relation = relations[i].relation;
					return true;
				}					
			}
			return false;
		}
		static string FoundRelationship(List<Relation> relations, int id1, int id2)
		{
			bool isSibling = default;
			int id=0;
			bool isParent = false;
			for (int i = 0; i < relations.Count; i++)
			{
				if (relations[i].firstPersonId == id1 && relations[i].relation == "parent") 
				{					
					isParent = true;
					id = relations[i].secondPersonId;
				}
			}
			if (isParent)
			{
				for (int i = 0; i < relations.Count; i++)
				{
					if (relations[i].firstPersonId == id2 && relations[i].relation == "sibling" && relations[i].secondPersonId == id) 					
						isSibling = true;											
				}
				if (isSibling)
					return "parents";
				else
					return "sibiling";
			}
			else
				return "sibling";
		}
	}
}
