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

			SprEnt2 spr = new SprEnt2(7, 5);

			//pSaveSprite(spr);

			using (BinaryReader br = new BinaryReader(new FileStream("example.bin", FileMode.Open)))
			{
				spr = SprEnt2.LoadSpriteFromFile(br, can, 7, 12);
			}

			can.Draw();
			can.DrawConnectionsColors();

			Console.ReadKey();
		}

		private static void pSaveSprite(SprEnt2 spr)
		{
			spr.Clear('+');
			spr.ClearColors(ConsoleColor.Cyan);

			using (BinaryWriter bw = new BinaryWriter(new FileStream("example.bin", FileMode.Create)))
			{
				spr.SaveSpriteToFile(bw);
			}
		}
	}
}
