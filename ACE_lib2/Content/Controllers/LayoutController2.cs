using ACE_2D_Base.Regions;
using ACE_2D_Base.Vectors;
using ACE_lib2.Content.Abstracts;
using ACE_lib2.Content.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACE_lib2.Content.Controllers
{
	public class LayoutController2 : Controller2
	{
		public LayoutController2() => this._InitClass();
		public LayoutController2(IConnectable2 Parent) : base(Parent) => this._InitClass();
		public LayoutController2(Vec2i Size) : base(Size) => this._InitClass();
		public LayoutController2(Reg2 Region) : base(Region) => this._InitClass();
		public LayoutController2(int xSize, int ySize) : base(xSize, ySize) => this._InitClass();
		public LayoutController2(Vec2i Size, Vec2i Position) : base(Size, Position) => this._InitClass();
		public LayoutController2(Vec2i Size, Vec2d Position) : base(Size, Position) => this._InitClass();
		public LayoutController2(IConnectable2 Parent, Vec2i Size) : base(Parent, Size) => this._InitClass();
		public LayoutController2(IConnectable2 Parent, Reg2 Region) : base(Parent, Region) => this._InitClass();
		public LayoutController2(IConnectable2 Parent, int xSize, int ySize) : base(Parent, xSize, ySize) => this._InitClass();
		public LayoutController2(IConnectable2 Parent, Vec2i Size, Vec2i Position) : base(Parent, Size, Position) => this._InitClass();
		public LayoutController2(IConnectable2 Parent, Vec2i Size, Vec2d Position) : base(Parent, Size, Position) => this._InitClass();
		public LayoutController2(int xSize, int ySize, double xPos, double yPos) : base(xSize, ySize, xPos, yPos) => this._InitClass();
		public LayoutController2(IConnectable2 Parent, int xSize, int ySize, double xPos, double yPos) : base(Parent, xSize, ySize, xPos, yPos) => this._InitClass();

		public enum LayoutDirection { TopToBottom, LeftToRight };

		public Vec2i InitialOffset { get; set; }

		public int SpaceBetweenElements { get; set; }

		public LayoutDirection ElementLayoutDirection { get; set; }

		internal override void _iUpdateConnectionProps(IContent2 ContentToUpdate, int Index)
		{
			Vec2i Pos = this.InitialOffset.Clone() as Vec2i;

			for (int i = 0; i < Index; ++i)
			{
				Pos += this.GetConnected(i).GetSize();
				Pos.X += this.SpaceBetweenElements;
				Pos.Y += this.SpaceBetweenElements;
			}

			switch (this.ElementLayoutDirection)
			{
				case LayoutDirection.TopToBottom:
					ContentToUpdate.Region.SetPosition(this.InitialOffset.X, Pos.Y);
					break;

				case LayoutDirection.LeftToRight:
					ContentToUpdate.Region.SetPosition(Pos.X, this.InitialOffset.Y);
					break;
			}
		}

		private void _InitClass()
		{
			this.SpaceBetweenElements = 1;
			this.InitialOffset = new Vec2i(0, 0);
			this.ElementLayoutDirection = LayoutDirection.TopToBottom;
		}
	}
}
