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

			Controller2 Con = new Controller2(can, 32, 16, 32, 1);

			ContentUtils.AppendRectangleAndFillWithColor(Con, '#', ConsoleColor.Green, 1, 1, 22, 11);
			ContentUtils.CopyContentTo(Con, can);

			can.Draw();
			can.DrawConnectionsColors();

			Console.ReadKey();
		}
	}
}
