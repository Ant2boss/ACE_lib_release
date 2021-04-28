using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ACE_lib.Vectors;
using ACE_lib.Regions;
using ACE_lib.Content.Canvases;

namespace ACE_Testing
{
	class Program
	{
		static void Main(string[] args)
		{
			Can2 can = Can2.CreateCanvasSingleton("Example", 96, 32);

			can.Clear('.');

			can.SetAt('#', 0, 0);
			can.SetAt('+', 2, 0);
			can.SetAt('+', 0, 2);

			can.SetAt(can.GetAt(0,0), 2, 2);

			can.Draw();
			can.DrawColorAt(ConsoleColor.Red, 2, 2);

			Console.ReadKey();
		}
	}
}
