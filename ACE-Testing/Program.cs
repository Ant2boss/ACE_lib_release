using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

using ACE_2D_Base.Vectors;
using ACE_2D_Base.Regions;

using ACE_lib2.Content.Canvases;
using ACE_lib2.Content.Entities;

namespace ACE_Testing
{
	class Program
	{
		static void Main(string[] args)
		{
			Canvas2 can = new Canvas2("Testing", 96, 32);

			Entity2 ent = new Entity2(7, 5);

			ent.Clear('1');
			ent.ClearColors(ConsoleColor.Cyan);

			ent.Region.SetPosition(4, 2);

			ent.SetAt('0', 3, 1);

			ent.AppendTo(can);

			can.Draw();
			ent.DrawColorTo(can);

			Console.ReadKey();
		}
	}
}
