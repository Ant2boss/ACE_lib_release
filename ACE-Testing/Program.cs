using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ACE_lib.Vectors;
using ACE_lib.Regions;
using ACE_lib.Content.Canvases;
using ACE_lib.Content.Entities;

namespace ACE_Testing
{
	class Program
	{
		static void Main(string[] args)
		{
			Can2 can = Can2.CreateCanvasSingleton("Example", 96, 32);

			Ent2 ent = new Ent2(can, 5, 5);

			ent.SetPosition(9, 3);
			ent.SetSize(10, 5);

			ent.Clear('#');
			
			ent.ClearColors(ConsoleColor.Cyan);
			ent.SetColorAt(ConsoleColor.Green, 0, 0);


			can.SetAt((char)((int)'0' + can.ConnectedCount), 0, 0);

			can.Draw();

			Console.ReadKey();
		}
	}
}
