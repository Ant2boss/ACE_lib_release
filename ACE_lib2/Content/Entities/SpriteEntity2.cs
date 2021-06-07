using ACE_2D_Base.Regions;
using ACE_2D_Base.Vectors;
using ACE_lib2.Content.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACE_lib2.Content.Entities
{
	public class SpriteEntity2 : Entity2
	{
		public SpriteEntity2() { }
		public SpriteEntity2(IConnectable2 Parent) : base(Parent) { }
		public SpriteEntity2(Vec2i Size) : base(Size) { }
		public SpriteEntity2(Reg2 Reg) : base(Reg) { }
		public SpriteEntity2(int xSize, int ySize) : base(xSize, ySize) { }
		public SpriteEntity2(Vec2i Size, Vec2i Pos) : base(Size, Pos) { }
		public SpriteEntity2(Vec2i Size, Vec2d Pos) : base(Size, Pos) { }
		public SpriteEntity2(IConnectable2 Parent, Vec2i Size) : base(Parent, Size) { }
		public SpriteEntity2(IConnectable2 Parent, Reg2 Reg) : base(Parent, Reg) { }
		public SpriteEntity2(IConnectable2 Parent, int xSize, int ySize) : base(Parent, xSize, ySize) { }
		public SpriteEntity2(IConnectable2 Parent, Vec2i Size, Vec2i Pos) : base(Parent, Size, Pos) { }
		public SpriteEntity2(IConnectable2 Parent, Vec2i Size, Vec2d Pos) : base(Parent, Size, Pos) { }
		public SpriteEntity2(int xSize, int ySize, double xPos, double yPos) : base(xSize, ySize, xPos, yPos) { }
		public SpriteEntity2(IConnectable2 Parent, int xSize, int ySize, double xPos, double yPos) : base(Parent, xSize, ySize, xPos, yPos) { }

		public static SpriteEntity2 ParseSpriteFromContent(IContent2 ParseFrom)
		{
			SpriteEntity2 spr = new SpriteEntity2(ParseFrom.GetSize());
			ContentUtils.CopyContentWithColorTo(ParseFrom, spr);
			return spr;
		}

		public static SpriteEntity2 LoadSpriteFromFile(BinaryReader Reader)
		{
			Vec2i Size = Vec2Utils.LoadVec2iFromFile(Reader);
			SpriteEntity2 spr = new SpriteEntity2(Size);

			for (int y = 0; y < Size.Y; ++y)
			{ 
				for(int x = 0; x < Size.X; ++x)
				{
					spr.SetAt(BitConverter.ToChar(Reader.ReadBytes(2), 0), x, y);
					spr.SetColorAt((ConsoleColor)(BitConverter.ToUInt16(Reader.ReadBytes(2), 0)), x, y);
				}
			}

			return spr;
		}
		public static SpriteEntity2 LoadSpriteFromFile(BinaryReader Reader, double xPos, double yPos)
		{
			SpriteEntity2 spr = LoadSpriteFromFile(Reader);
			spr.Region.SetPosition(xPos, yPos);
			return spr;
		}
		public static SpriteEntity2 LoadSpriteFromFile(BinaryReader Reader, IConnectable2 Parent, double xPos, double yPos)
		{
			SpriteEntity2 spr = LoadSpriteFromFile(Reader, xPos, yPos);
			Parent.AddConnection(spr);
			return spr;
		}

		public static SpriteEntity2 LoadSpriteFromFile(BinaryReader Reader, Vec2i Postion) => LoadSpriteFromFile(Reader, Postion.X, Postion.Y);
		public static SpriteEntity2 LoadSpriteFromFile(BinaryReader Reader, Vec2d Postion) => LoadSpriteFromFile(Reader, Postion.X, Postion.Y);
		public static SpriteEntity2 LoadSpriteFromFile(BinaryReader Reader, IConnectable2 Parent, Vec2i Postion) => LoadSpriteFromFile(Reader, Parent, Postion.X, Postion.Y);
		public static SpriteEntity2 LoadSpriteFromFile(BinaryReader Reader, IConnectable2 Parent, Vec2d Postion) => LoadSpriteFromFile(Reader, Parent, Postion.X, Postion.Y);

		public void SaveSpriteToFile(BinaryWriter Writer)
		{
			Vec2i Size = this.GetSize();
			Vec2Utils.SaveVec2iToFile(Writer, Size);

			for(int y = 0; y < Size.Y; ++y)
			{
				for (int x = 0; x < Size.X; ++x)
				{
					Writer.Write(BitConverter.GetBytes(this.GetAt(x, y)));
					Writer.Write(BitConverter.GetBytes((ushort)this.GetColorAt(x, y)));
				}
			}
		}

	}
}
