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
using ACE_lib2.Content;
using ACE_lib2.Content.Controllers;
using ACE_lib2.Content.Props;

namespace ACE_Testing
{
	class Program
	{
		static void Main(string[] args)
		{
			Canvas2 can = Canvas2.CreateCanvasSingleton("Test", 96, 32);

			Controller2 con = new Controller2(can, 32, 16, 5, 2);

			con.Title = new TitleProps("Test");

			Entity2 ent = new Entity2(con, 5, 5, 2, 1);
			ent.Clear('1');
			ent.ClearColors(ConsoleColor.Red);

			Entity2 ent2 = new Entity2(con, 5, 5, 9, 1);
			ent2.Clear('2');
			ent2.ClearColors(ConsoleColor.Green);

			Entity2 ent3 = new Entity2(con, 5, 10, 16, 1);
			ent3.Clear('3');
			ent3.ClearColors(ConsoleColor.Blue);

			can.Draw();
			can.DrawConnectionsColors();

			Console.ReadKey();
		}
	}
}
