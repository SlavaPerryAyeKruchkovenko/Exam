using System;
using System.Collections.Generic;
using System.Text;

namespace subtitri
{
	class Table
	{
		public static void WriteTable()
		{
			int x = Console.WindowWidth / 2;
			int y = Console.WindowHeight / 2;
			int cursorX = Console.WindowWidth / 4;
			int cursorY = Console.WindowHeight / 4;

			WriteOpenAndCloseBar(cursorX, cursorY, x, 0);
			WriteLeftAndRightBar(cursorX, cursorY, x, y);
			WriteOpenAndCloseBar(cursorX, cursorY, x, y - 2);
		}
		static void WriteOpenAndCloseBar(int cursorX, int cursorY, int x, int z)
		{
			Console.SetCursorPosition(cursorX, cursorY + z);
			Console.Write("+");
			WriteBar(x);
			Console.Write("+");
		}
		static void WriteBar(int x)
		{
			for (int i = 0; i < x - 3; i++)
			{
				Console.Write("-");
			}
		}
		static void WriteLeftAndRightBar(int cursorX, int cursorY, int x, int y)
		{
			for (int i = 0; i < y - 3; i++)
			{
				Console.SetCursorPosition(cursorX, cursorY + i + 1);
				Console.Write("|");
				Console.SetCursorPosition(cursorX + x - 2, cursorY + i + 1);
				Console.Write("|");
			}
		}
	}
}
