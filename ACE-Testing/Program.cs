using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

using ACE_2D_Base.Vectors;
using ACE_2D_Base.Regions;

using ACE_lib2.Content.Canvases;

namespace ACE_Testing
{
	class Program
	{
		static void Main(string[] args)
		{
			Canvas2 can = new Canvas2("Testing", 96, 32);

			can.SetAt('0', 0, 0);
			can.SetAt('1', 95, 0);
			can.SetAt('2', 0, 31);
			can.SetAt('3', 95, 31);

			can[1, 1] = '+';

			can.Draw();
			can.DrawColorAt(ConsoleColor.Red, 0, 0);
			can.DrawColorAt(ConsoleColor.Cyan, new Vec2i(1, 1));

			Console.ReadKey();
		}
	}
}
