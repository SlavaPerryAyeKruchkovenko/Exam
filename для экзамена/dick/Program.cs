using System;
using System.Collections.Generic;
using System.Threading;
using System.Timers;

namespace dick
{
	class Program
	{
        static void Main(string[] args)
        {

            bool f = default;
            int c = f ? 10 : 11;
            Dictionary<int, string> countries = new Dictionary<int, string>();
            countries.Add(1, "0");
            Console.WriteLine(countries[1]);
            var a = new[] { 1, 2, 3 };
            var b = new[] { 1, 2, 3 };
            Console.WriteLine(a.Equals(b));

            while (c==11)
			{
                Console.WriteLine(c);
                break;
                
            }
            
            TimerCallback tm = new TimerCallback(Count);
			System.Threading.Timer timer = new System.Threading.Timer(tm, null, 0, 2000);
            Console.ReadKey();
        }
        public static void Count(object o)
        {
            if (Console.WindowWidth > 26)
                Console.WriteLine("Бутько мой отец");
            else
                Console.WriteLine("Бутько Сосал член негра");
        }
        
	}
}
