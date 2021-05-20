using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

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

			LayoutCon2 lay = new LayoutCon2(can, 64, 30, 3, 3);

			lay.AddConnection(new Ent2(20, 3));
			lay.AddConnection(new Ent2(10, 2));
			lay.AddConnection(new Ent2(7, 3));
			lay.AddConnection(new Ent2(10, 5));

			lay.LayoutDirection = LayoutCon2.LayDir.TopToBottom;
			lay.ContentInitialOffset = new Vec2i(2, 1);

			for (int i = 0; i < lay.ConnectedCount; ++i)
			{
				lay.GetConnectedDetails(i).Clear((char)('0' + i));
			}

			can.Draw();

			Console.ReadKey();
		}
	}
}
