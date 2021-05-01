using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ACE_lib.Vectors;
using ACE_lib.Regions;
using ACE_lib.Content.Canvases;
using ACE_lib.Content.Entities;
using ACE_lib.Content.Controllers;
using ACE_lib.Content.Props;

namespace ACE_Testing
{
	class Program
	{
		static void Main(string[] args)
		{
			Can2 can = Can2.CreateCanvasSingleton("Example", 96, 32);

			Con2 con = new Con2(can, 16, 8, 12, 6);

			Ent2 e1 = new Ent2(con, 3, 3, 1, 1);
			e1.Clear('1');

			Ent2 e2 = new Ent2(con, 3, 3, 5, 1);
			e2.Clear('2');

			Ent2 e3 = new Ent2(con, 3, 3, 9, 1);
			e3.Clear('3');


			con.AddConnection(e3);
			con.AddConnection(e3);
			con.AddConnection(e3);
			con.AddConnection(e3);
			con.AddConnection(e3);

			can.SetAt((char)((int)'0' + con.ConnectedCount), 0, 0);

			can.DrawAndReadLine(ConsoleColor.Red, 2, 2);

			Console.ReadKey();
		}
	}
}
