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
			Canvas2 can = Canvas2.CreateCanvasSingleton("Test", 96, 32);

			TextEntity2 txt = new TextEntity2(can, 32, 16);

			txt.Clear('-');

			txt.TextBreak = true;
			txt.TextColor = ConsoleColor.Yellow;

			txt.WriteLine("Hello once");
			txt.WriteLine("Hello twice", ConsoleColor.Red);
			txt.WriteLine("Hello thrice", ConsoleColor.White);
			txt.WriteLine("Hello four times", ConsoleColor.Cyan);

			can.Draw();
			//can.DrawConnectionsColors();

			Console.ReadKey();
		}
	}
}
