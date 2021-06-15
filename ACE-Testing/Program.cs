using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

using ACE_2D_Base.Vectors;
using ACE_2D_Base.Regions;

using ACE_lib3.Content.Canvases;

namespace ACE_Testing
{
	class Program
	{
		static void Main(string[] args)
		{
			Canvas2 can = Canvas2.GetInstance("Example project", 48, 16);

			can.SetAt('#', 0, 0);
			can.SetAt('#', 2, 0);
			can.SetAt('#', 0, 2);

			can.Draw();

			can.DrawColorAt(ConsoleColor.Green, 0, 0);

			Console.ReadKey();
		}
	}
}
