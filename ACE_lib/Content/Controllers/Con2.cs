using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ACE_lib.Content;
using ACE_2D_Base.Vectors;
using ACE_2D_Base.Regions;

namespace ACE_lib.Content.Controllers
{
	public class Con2 : absControllerBase
	{
		public Con2() : base() { }
		public Con2(int xSize, int ySize) : base() { this.SetSize(xSize, ySize); }
		public Con2(int xSize, int ySize, double xPos, double yPos) : this(xSize, ySize) { this.SetPosition(xPos, yPos); }

		public Con2(IConnectable2 Parent) : this() { Parent.AddConnection(this); }
		public Con2(IConnectable2 Parent, int xSize, int ySize) : this(xSize, ySize) { Parent.AddConnection(this); }
		public Con2(IConnectable2 Parent, int xSize, int ySize, double xPos, double yPos) : this(xSize, ySize, xPos, yPos) { Parent.AddConnection(this); }

		public Con2(Vec2i Size) : this(Size.X, Size.Y) { }
		public Con2(Vec2i Size, Vec2i Position) : this(Size.X, Size.Y, Position.X, Position.Y) { }
		public Con2(Vec2i Size, Vec2d Position) : this(Size.X, Size.Y, Position.X, Position.Y) { }
		public Con2(Reg2 Region) : this(Region.GetSize(), Region.GetPosition()) { }

		public Con2(IConnectable2 Parent, Vec2i Size) : this(Parent, Size.X, Size.Y) { }
		public Con2(IConnectable2 Parent, Vec2i Size, Vec2i Position) : this(Parent, Size.X, Size.Y, Position.X, Position.Y) { }
		public Con2(IConnectable2 Parent, Vec2i Size, Vec2d Position) : this(Parent, Size.X, Size.Y, Position.X, Position.Y) { }
		public Con2(IConnectable2 Parent, Reg2 Region) : this(Parent, Region.GetSize(), Region.GetPosition()) { }

		internal override void abs_HandleConnectionAppending(IContent2 Con, int Index)
		{
			Con.AppendTo(this);
		}
	}
}
