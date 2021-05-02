﻿using System;
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

			//AnimationEnt2 anim = new AnimationEnt2(can, 5, 5);
			AnimationEnt2 anim;

			using (BinaryReader br = new BinaryReader(new FileStream("example.bin", FileMode.Open)))
			{
				anim = AnimationEnt2.LoadAnimationFromFile(br, 9, 3);
			}

			anim.CurrentFrameIndex = 2;
			anim.AppendTo(can);

			//anim.AddFrame();
			//anim.Clear('1');

			//anim.AddFrame(true);
			//anim.Clear('2');

			//anim.AddFrame(true);
			//anim.Clear('3');

			//anim.SetAtFrame(0, '$', 0, 0);

			//anim.SetSize(3, 3);
			//anim.SetPosition(21, 7);

			//anim.CurrentFrameIndex = 2;

			//using (BinaryWriter bw = new BinaryWriter(new FileStream("example.bin", FileMode.OpenOrCreate)))
			//{
			//	anim.SaveAnimationToFile(bw);
			//}

			can.Draw();
			//can.DrawConnectionsColors();

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
