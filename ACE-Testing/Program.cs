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

			LayoutController2 laycon = new LayoutController2(can, 64, 28, 2, 1);
			
			laycon.Title = new TitleProps("Layout", "--[", "]--");
			laycon.InitialOffset = new Vec2i(2, 1);
			laycon.ElementLayoutDirection = LayoutController2.LayoutDirection.LeftToRight;

			laycon.AddConnection(new Entity2(10 / 2, 3));
			laycon.AddConnection(new Entity2(12 / 2, 1));
			laycon.AddConnection(new Entity2(23 / 2, 5));
			laycon.AddConnection(new Entity2(15 / 2, 2));
			laycon.AddConnection(new Entity2(10 / 2, 3));

			for (int i = 0; i < laycon.ConnectedCount; ++i)
			{
				laycon.GetConnected(i).Clear((char)('0' + i));
			}

			can.Draw();
			//can.DrawConnectionsColors();

			Console.ReadKey();
		}
	}
}
