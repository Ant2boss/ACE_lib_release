using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ACE_lib.Args;
using ACE_2D_Base.Regions;
using ACE_2D_Base.Vectors;

namespace ACE_lib.Content.Controllers
{
	public class LayoutCon2 : Con2
	{
		public LayoutCon2() => this.InitClass();
		public LayoutCon2(IConnectable2 Parent) : base(Parent) => this.InitClass();
		public LayoutCon2(Vec2i Size) : base(Size) => this.InitClass();
		public LayoutCon2(Reg2 Region) : base(Region) => this.InitClass();
		public LayoutCon2(int xSize, int ySize) : base(xSize, ySize) => this.InitClass();
		public LayoutCon2(Vec2i Size, Vec2i Position) : base(Size, Position) => this.InitClass();
		public LayoutCon2(Vec2i Size, Vec2d Position) : base(Size, Position) => this.InitClass();
		public LayoutCon2(IConnectable2 Parent, Vec2i Size) : base(Parent, Size) => this.InitClass();
		public LayoutCon2(IConnectable2 Parent, Reg2 Region) : base(Parent, Region) => this.InitClass();
		public LayoutCon2(IConnectable2 Parent, int xSize, int ySize) : base(Parent, xSize, ySize) => this.InitClass();
		public LayoutCon2(IConnectable2 Parent, Vec2i Size, Vec2i Position) : base(Parent, Size, Position) => this.InitClass();
		public LayoutCon2(IConnectable2 Parent, Vec2i Size, Vec2d Position) : base(Parent, Size, Position) => this.InitClass();
		public LayoutCon2(int xSize, int ySize, double xPos, double yPos) : base(xSize, ySize, xPos, yPos) => this.InitClass();
		public LayoutCon2(IConnectable2 Parent, int xSize, int ySize, double xPos, double yPos) : base(Parent, xSize, ySize, xPos, yPos) => this.InitClass();

		public enum LayDir { TopToBottom, LeftToRight }

		public int SpaceBetweenContent { get; set; }

		public Vec2i ContentInitialOffset { get; set; }

		public LayDir LayoutDirection { get; set; }

		internal override void abs_HandleConnectionAppending(IContent2 Con, int Index)
		{
			this._RepositionContent(Con, Index);

			base.abs_HandleConnectionAppending(Con, Index);
		}

		private void _RepositionContent(IContent2 con, int index)
		{
			Vec2i tIndex = this.ContentInitialOffset.Clone() as Vec2i;

			int count = Math.Min(index, this.ConnectedCount);

			for(int i = 0; i < count; ++i)
			{
				IContent2 TC = this.GetConnectedDetails(i);

				switch (this.LayoutDirection)
				{
					case LayDir.TopToBottom:
						tIndex.Y += TC.GetSize().Y + this.SpaceBetweenContent;
						break;
					case LayDir.LeftToRight:
						tIndex.X += TC.GetSize().X + this.SpaceBetweenContent;
						break;
				}
			}

			con.SetPosition(tIndex);
		}

		private void InitClass()
		{
			this.SpaceBetweenContent = 1;

			this.LayoutDirection = LayDir.TopToBottom;
		}
	}
}
