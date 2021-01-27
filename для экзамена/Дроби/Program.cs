using System;
using System.Collections.Generic;

namespace Дроби
{
	class Fraction
	{
		public int num=0;
		public int numerator;
		public int denominator;
		public void NormalizeFraction()
		{
			if(numerator>=denominator&&denominator>0)
			{
				num += numerator / denominator;
				numerator -= denominator * (numerator / denominator);
			}
			
		}
		public static Fraction GhostInNormalForm(Fraction a)
		{
			int m = a.numerator;
			int n = a.denominator;
			while(n!=m)
			{
				if (m > n)
					m -= n;
				else
					n -= m;
			}
			a.numerator /= m;
			a.denominator /= m;
			return a;
		}
	}
	abstract class Operation
	{
		public string operation;
		public  Fraction verobile1 { get; set; }
		public Fraction verobile2 { get; set; }
		public Operation(string text,Fraction a, Fraction b)
		{
			operation = text;
			verobile1 = a;
			verobile2 = b;
		}
		public virtual Fraction calculate()
		{

		}
	}
	class Sum : Operation
	{
		public Fraction verobile1;
		public Fraction verobile2;

		public override Fraction calculate()
		{
			return verobile1+verobile2;
		}

	}
	class Program
	{
		static void Main(string[] args)
		{
			Console.WriteLine("Введите уравнение");
			string calculate = Console.ReadLine().Trim();
			List<char> operation = FoundOperation(calculate);
			var fractions = ParserText(calculate);

			for (int i = 0; i < operation.Count; i++)
			{
				fractions[0].NormalizeFraction();
				fractions[1].NormalizeFraction();
				fractions[0]=СalculationFraction(fractions[0], fractions[1], operation[i]);
				fractions.Remove(fractions[1]);
			}

			WriteLineAnswer(fractions[0]);
		}
		static List<Fraction> ParserText(string calculate)
		{
			string[] nums = calculate.Split(" ");
			List<Fraction> fractions = new List<Fraction>();
			Fraction fraction = new Fraction();
			foreach (var item in nums)
			{
				if (item.Contains("/") && item.Length > 1)
				{
					fraction.numerator = Convert.ToInt32(item.Split("/")[0]);
					fraction.denominator = Convert.ToInt32(item.Split("/")[1]);
					fractions.Add(fraction);
					
					fraction = new Fraction();
				}

				else if (fraction.num == 0)
					Int32.TryParse(item, out fraction.num);

				else if (fraction.num != 0 && Int32.TryParse(item, out _)) 
				{
					fractions.Add(fraction);
					fraction = new Fraction();
					fraction.num = Convert.ToInt32(item);					
				}						
			}
			if (fraction.num != 0)
				fractions.Add(fraction);
			
			return fractions;
		}
		static List<char> FoundOperation(string text)
		{
			List<char> operation = new List<char>();
			foreach (var item in text.Split(' '))
			{
				switch(item)
				{
					case "+": operation.Add('+'); break;
					case "-": operation.Add('-'); break;
					case "*": operation.Add('*'); break;
					case "/": operation.Add('/'); break;
				}
			}
			return operation;
		}
		static Fraction СalculationFraction(Fraction a, Fraction b, char operation)
		{

			var c = new Fraction();
			if (a.denominator != b.denominator)
			{
				int i = a.denominator;
				a.denominator *= b.denominator;
				a.numerator *= b.denominator;
				b.denominator *= i;
				b.numerator *= i;
			}
			switch (operation)
			{
				case '+':
					c.num = a.num + b.num;
					c.numerator = b.numerator + a.numerator;
					c.denominator = a.denominator;
					break;
				case '-':
					c.num = a.num - b.num;
					c.numerator = a.numerator - b.numerator;
					c.denominator = a.denominator;
					break;
				case '*':
					c.num = a.num * b.num;
					c.numerator = a.numerator * b.numerator;
					c.denominator = a.denominator;
					break;
				case '/':
					c.num = a.num / b.num;
					c.numerator = a.numerator / b.numerator;
					c.denominator = a.denominator;
					break;
			}
			return c;
		}
		static void WriteLineAnswer(Fraction answer)
		{		
			answer = Fraction.GhostInNormalForm(answer);
			answer.NormalizeFraction();
			Console.WriteLine($"Ответ: {answer.num} {answer.numerator}/{answer.denominator}") ;
		}

	}
}
