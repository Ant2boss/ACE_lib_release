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

			TextEnt2 txt = new TextEnt2(can, 64, 16);

			txt.Clear('.');

			txt.TextColor = ConsoleColor.Red;
			txt.TextBreak = true;

			txt.Write("Upisite broj godina: ");

			string godine = txt.ReadLineOnWithColors(can);

			txt.Write(godine);

			can.Draw();
			can.DrawConnectionsColors();

			Console.ReadKey();
		}
	}
}
