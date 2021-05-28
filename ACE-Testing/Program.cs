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

namespace ACE_Testing
{
	class Program
	{
		static void Main(string[] args)
		{
			Canvas2 can = Canvas2.CreateCanvasSingleton("Test", 96, 32);

			Entity2 ent = new Entity2(can, can.GetSize());

			//ent.Clear('-');

			//ContentUtils.AppendLine(ent, '#', 14, 14, 2, 2);

			Vec2i Start = new Vec2i(61, 20);
			Vec2i End = new Vec2i(5, 5);

			ent.SetAt('S', Start);
			ent.SetAt('E', End);

			ContentUtils.AppendElipseAndFill(ent, '#', '.', ConsoleColor.Red, ConsoleColor.Green, Start, End);

			can.Draw();
			can.DrawConnectionsColors();

			Console.ReadKey();
		}
	}
}
