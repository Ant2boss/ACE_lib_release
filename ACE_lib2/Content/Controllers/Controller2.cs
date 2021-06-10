using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ACE_2D_Base.Regions;
using ACE_2D_Base.Vectors;
using ACE_lib2.Content.Abstracts;
using ACE_lib2.Content.Interfaces;
using ACE_lib2.Content.Props;

namespace ACE_lib2.Content.Controllers
{
	public class Controller2 : abs_Controller2
	{
		public Controller2() 
		{
			this.Border = new BorderProps();
		}
		public Controller2(int xSize, int ySize) : this()
		{
			this.Region.SetSize(xSize, ySize);
		}
		public Controller2(int xSize, int ySize, double xPos, double yPos) : this(xSize, ySize)
		{
			this.Region.SetPosition(xPos, yPos);
		}

		public Controller2(IConnectable2 Parent) : this()
		{
			Parent.AddConnection(this);
		}
		public Controller2(IConnectable2 Parent, int xSize, int ySize) : this(xSize, ySize)
		{
			Parent.AddConnection(this);
		}
		public Controller2(IConnectable2 Parent, int xSize, int ySize, double xPos, double yPos) : this(xSize, ySize, xPos, yPos)
		{
			Parent.AddConnection(this);
		}

		public Controller2(Vec2i Size) : this(Size.X, Size.Y) { }
		public Controller2(Vec2i Size, Vec2i Position) : this(Size.X, Size.Y, Position.X, Position.Y) { }
		public Controller2(Vec2i Size, Vec2d Position) : this(Size.X, Size.Y, Position.X, Position.Y) { }
		public Controller2(Reg2 Region) : this(Region.GetSize(), Region.GetPosition()) { }

		public Controller2(IConnectable2 Parent, Vec2i Size) : this(Parent, Size.X, Size.Y) { }
		public Controller2(IConnectable2 Parent, Vec2i Size, Vec2i Position) : this(Parent, Size.X, Size.Y, Position.X, Position.Y) { }
		public Controller2(IConnectable2 Parent, Vec2i Size, Vec2d Position) : this(Parent, Size.X, Size.Y, Position.X, Position.Y) { }
		public Controller2(IConnectable2 Parent, Reg2 Region) : this(Parent, Region.GetSize(), Region.GetPosition()) { }

		internal override void _iHandleConnection(IContent2 ContentToHandle, int Index)
		{
			ContentToHandle.AppendTo(this);
		}
		internal override void _iUpdateConnectionProps(IContent2 ContentToUpdate, int Index)
		{
			return;
		}
	}
}
