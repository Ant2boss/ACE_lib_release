using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ACE_lib.Regions;
using ACE_lib.Vectors;

namespace ACE_lib.Content.Entities
{
	public class SprEnt2 : Ent2
	{
		public SprEnt2() { }
		public SprEnt2(IConnectable2 Parent) : base(Parent) { }
		public SprEnt2(Vec2i Size) : base(Size) { }
		public SprEnt2(Reg2 Region) : base(Region) { }
		public SprEnt2(int xSize, int ySize) : base(xSize, ySize) { }
		public SprEnt2(Vec2i Size, Vec2i Position) : base(Size, Position) { }
		public SprEnt2(Vec2i Size, Vec2d Position) : base(Size, Position) { }
		public SprEnt2(IConnectable2 Parent, Vec2i Size) : base(Parent, Size) { }
		public SprEnt2(IConnectable2 Parent, Reg2 Region) : base(Parent, Region) { }
		public SprEnt2(IConnectable2 Parent, int xSize, int ySize) : base(Parent, xSize, ySize) { }
		public SprEnt2(IConnectable2 Parent, Vec2i Size, Vec2i Position) : base(Parent, Size, Position) { }
		public SprEnt2(IConnectable2 Parent, Vec2i Size, Vec2d Position) : base(Parent, Size, Position) { }
		public SprEnt2(int xSize, int ySize, double xPos, double yPos) : base(xSize, ySize, xPos, yPos) { }
		public SprEnt2(IConnectable2 Parent, int xSize, int ySize, double xPos, double yPos) : base(Parent, xSize, ySize, xPos, yPos) { }

		public void SaveSpriteToFile(BinaryWriter bw)
		{
			Vec2Utils.SaveVec2iToFile(bw, this.GetSize());

			for (int y = 0; y < this.GetSize().Y; ++y)
			{
				for (int x = 0; x < this.GetSize().X; ++x)
				{
					bw.Write(BitConverter.GetBytes(this.GetAt(x, y)));
					bw.Write(BitConverter.GetBytes((UInt16)this.GetColorAt(x, y)));
				}
			}
		}

		public static SprEnt2 LoadSpriteFromFile(BinaryReader br)
		{
			SprEnt2 spr = new SprEnt2(Vec2Utils.LoadVec2iFromFile(br));

			for (int y = 0; y < spr.GetSize().Y; ++y)
			{
				for (int x = 0; x < spr.GetSize().X; ++x)
				{
					//Hmm... it sure do be like that, yes... interesting
					spr.SetAt(BitConverter.ToChar(br.ReadBytes(2), 0), x, y);
					spr.SetColorAt((ConsoleColor)br.ReadUInt16(), x, y);
				}
			}

			return spr;
		}
		public static SprEnt2 LoadSpriteFromFile(BinaryReader br, double xPos, double yPos)
		{
			SprEnt2 spr = LoadSpriteFromFile(br);

			spr.SetPosition(xPos, yPos);

			return spr;
		}
		public static SprEnt2 LoadSpriteFromFile(BinaryReader br, IConnectable2 Parent, double xPos, double yPos)
		{
			SprEnt2 spr = LoadSpriteFromFile(br, xPos, yPos);

			Parent.AddConnection(spr);

			return spr;
		}

		public static SprEnt2 LoadSpriteFromFile(BinaryReader br, IConnectable2 Parent) => LoadSpriteFromFile(br, Parent, 0, 0);
		public static SprEnt2 LoadSpriteFromFile(BinaryReader br, IConnectable2 Parent, Vec2i Pos) => LoadSpriteFromFile(br, Parent, Pos.X, Pos.Y);
		public static SprEnt2 LoadSpriteFromFile(BinaryReader br, IConnectable2 Parent, Vec2d Pos) => LoadSpriteFromFile(br, Parent, Pos.X, Pos.Y);
	}
}
