using ACE_2D_Base.Regions;
using ACE_2D_Base.Vectors;
using ACE_lib2.Content.Abstracts;
using ACE_lib2.Content.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACE_lib2.Content.Entities
{
	public class Entity2 : abs_Content2
	{
		public Entity2() 
		{ 
			this.Region = new Reg2();
		}
		public Entity2(int xSize, int ySize) : this()
		{
			this.Region.SetSize(xSize, ySize);
		}
		public Entity2(int xSize, int ySize, double xPos, double yPos) : this(xSize, ySize)
		{
			this.Region.SetPosition(xPos, yPos);
		}
		public Entity2(IConnectable2 Parent) : this() => Parent.AddConnection(this);
		public Entity2(IConnectable2 Parent, int xSize, int ySize) : this(xSize, ySize) => Parent.AddConnection(this);
		public Entity2(IConnectable2 Parent, int xSize, int ySize, double xPos, double yPos) : this(xSize, ySize, xPos, yPos) => Parent.AddConnection(this);

		public Entity2(Vec2i Size) : this(Size.X, Size.Y) { }
		public Entity2(Vec2i Size, Vec2i Pos) : this(Size.X, Size.Y, Pos.X, Pos.Y) { }
		public Entity2(Vec2i Size, Vec2d Pos) : this(Size.X, Size.Y, Pos.X, Pos.Y) { }
		public Entity2(Reg2 Reg) : this(Reg.GetSize(), Reg.GetPosition()) { }

		public Entity2(IConnectable2 Parent, Vec2i Size) : this(Parent, Size.X, Size.Y) { }
		public Entity2(IConnectable2 Parent, Vec2i Size, Vec2i Pos) : this(Parent, Size.X, Size.Y, Pos.X, Pos.Y) { }
		public Entity2(IConnectable2 Parent, Vec2i Size, Vec2d Pos) : this(Parent, Size.X, Size.Y, Pos.X, Pos.Y) { }
		public Entity2(IConnectable2 Parent, Reg2 Reg) : this(Parent, Reg.GetSize(), Reg.GetPosition()) { }
	}
}
