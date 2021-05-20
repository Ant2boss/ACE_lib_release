using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ACE_2D_Base.Regions;
using ACE_2D_Base.Vectors;

namespace ACE_lib.Content.Controllers
{
	public class GridCon2 : Con2
	{
		public GridCon2(int xGridSize, int yGridSize)
		{
			this.InitClass(xGridSize, yGridSize);
		}
		public GridCon2(int xGridSize, int yGridSize, int xSize, int ySize) : base(xSize, ySize)
		{
			this.InitClass(xGridSize, yGridSize);
		}
		public GridCon2(int xGridSize, int yGridSize, int xSize, int ySize, double xPos, double yPos) : base(xSize, ySize, xPos, yPos)
		{
			this.InitClass(xGridSize, yGridSize);
		}
		public GridCon2(int xGridSize, int yGridSize, IConnectable2 Parent) : base(Parent)
		{
			this.InitClass(xGridSize, yGridSize);
		}
		public GridCon2(int xGridSize, int yGridSize, IConnectable2 Parent, int xSize, int ySize) : base(Parent, xSize, ySize)
		{
			this.InitClass(xGridSize, yGridSize);
		}
		public GridCon2(int xGridSize, int yGridSize, IConnectable2 Parent, int xSize, int ySize, double xPos, double yPos) : base(Parent, xSize, ySize, xPos, yPos)
		{
			this.InitClass(xGridSize, yGridSize);
		}

		public GridCon2(Vec2i GridSize) : this(GridSize.X, GridSize.Y) { }
		public GridCon2(Vec2i GridSize, Vec2i Size) : this(GridSize.X, GridSize.Y, Size.X, Size.Y) { }
		public GridCon2(Vec2i GridSize, Vec2i Size, Vec2i Position) : this(GridSize.X, GridSize.Y, Size.X, Size.Y, Position.X, Position.Y) { }
		public GridCon2(Vec2i GridSize, Vec2i Size, Vec2d Position) : this(GridSize.X, GridSize.Y, Size.X, Size.Y, Position.X, Position.Y) { }
		public GridCon2(Vec2i GridSize, Reg2 Region) : this(GridSize, Region.GetSize(), Region.GetPosition()) { }

		public GridCon2(Vec2i GridSize, IConnectable2 Parent) : this(GridSize.X, GridSize.Y, Parent) { }
		public GridCon2(Vec2i GridSize, IConnectable2 Parent, Vec2i Size) : this(GridSize.X, GridSize.Y, Parent, Size.X, Size.Y) { }
		public GridCon2(Vec2i GridSize, IConnectable2 Parent, Vec2i Size, Vec2i Position) : this(GridSize.X, GridSize.Y, Parent, Size.X, Size.Y, Position.X, Position.Y) { }
		public GridCon2(Vec2i GridSize, IConnectable2 Parent, Vec2i Size, Vec2d Position) : this(GridSize.X, GridSize.Y, Parent, Size.X, Size.Y, Position.X, Position.Y) { }
		public GridCon2(Vec2i GridSize, IConnectable2 Parent, Reg2 Region) : this(GridSize, Parent, Region.GetSize(), Region.GetPosition()) { }

		public Vec2i GridSize { get; set; }

		public int ColumnCount { get; set; }

		//Please finish later...

		private void InitClass(int xGridSize, int yGridSize)
		{
			this.GridSize = new Vec2i(xGridSize, yGridSize);

			this.ColumnCount = -1;
		}
	}
}
