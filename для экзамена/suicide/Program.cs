using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;

namespace suicide
{
	enum Color
	{
		red,blue,yellow,green
	}
	static class Game
	{
		public static int lives { set; private get; } = 5;
		static readonly int thread = 200;
		private static char ReadLetter()
		{
			Console.SetCursorPosition(0, 0);
			Console.WriteLine("Введите букву");
			return Convert.ToChar(Console.ReadLine().Trim().ToLower());
		}
		public static bool LogicGame()
		{
			string nowWord = ConvertCharArrayToString(Drawer.secretWord).Trim('-');
			if(lives<=0)
			{
				Drawer.WriteLineEnd((ConsoleColor)Color.red, "Вы проиграли(((");
				return true;
			}
			else if (!nowWord.Equals(Word.word))
			{
				Drawer.DrawLives();
				Drawer.DrawUnvisibeleWord();
				CheckLetter();			
				return false;
			}
			else
			{
				var rnd = new Random();
				rnd.Next(4);
				Drawer.WriteLineEnd((ConsoleColor)Color.blue,"Вы Победили!!!");
				return true;
			}	
		}
		private static string ConvertCharArrayToString(char[] array)
		{
			string answer="";
			for (int i = 0; i < array.Length; i++)
			{
				answer += array[i];
			}
			return answer;
		}
		public static void CheckLetter()
		{
			Letter letter  = new Letter(ReadLetter());
			if (Word.word.Contains(letter.letter))
			{
				Console.WriteLine("Такая Буква есть");
				var indexs = FoundIndex(letter.letter);

				for (int i = 0; i < indexs.Count; i++)
				{
					Drawer.ReplaceOnRightLetter(letter.letter, indexs[i]);
				}
				Thread.Sleep(thread);
			}
			else
			{
				Console.WriteLine("Такой буквы нету");
				DecreaseLives();
				Thread.Sleep(thread);
			}				
		}
		private static void DecreaseLives()
		{
			lives--;
			Drawer.lives=Drawer.lives.Remove(Drawer.lives.Length - 1);
		}
		private static List<int> FoundIndex(char letter)
		{
			List<int> indexs = new List<int>();
			for (int i = 0; i < Word.word.Length; i++)
			{
				if (Word.word[i] == letter)
					indexs.Add(i);
			}
			return indexs;
		}
	}
	static class Word
	{
		public static string word { get;set; }
	}
	class Letter
	{
		public char letter { get; private set; }
		public Letter(char text)
		{
			letter = text;
		}

	}
	static class Drawer
	{
		public static int x = Console.WindowWidth / 2;
		public static int y = Console.WindowHeight / 2;
		public static char[] secretWord =new string('-', Word.word.Length).ToCharArray();
		public static string lives = "*****";
		public static void DrawUnvisibeleWord()
		{			
			Console.SetCursorPosition(x, y);
			Console.Write(secretWord);
		}
		public static void ReplaceOnRightLetter(char letter,int index)
		{
			secretWord[index] = letter;
		}
		public static void WriteLineEnd(ConsoleColor color,string result)
		{
			Console.ForegroundColor = color;
			Console.SetCursorPosition(x, y);
			Console.WriteLine(result);
			Console.SetCursorPosition(x, y+1);
			Console.Write($"Загадонное слов:{Word.word}");
		}
		public static void DrawLives()
		{
			Console.SetCursorPosition(Console.WindowWidth - (lives.Length + 1), 0);
			Console.Write(lives);
		}
		
	}
	class Program
	{

		static void Main(string[] args)
		{
			string[] text = File.ReadAllLines(Environment.CurrentDirectory + "\\text.txt");
			var rnd = new Random();
			Word.word = text[rnd.Next(text.Length)].ToLower();
			bool win = default;
			while(!win)
			{
				Console.Clear();				
				win = Game.LogicGame();
			}
		}
	}
}
