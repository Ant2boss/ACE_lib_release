using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ACE_lib.Args;
using ACE_lib.Content.Canvases;
using ACE_lib.Content.Props;
using ACE_lib.Content.Entities;
using ACE_lib.Regions;
using ACE_lib.Vectors;


namespace ACE_lib.Content
{
	public abstract class absControllerBase : absConnectableBase, IController2
	{
		public char this[int x, int y] { get => this.GetAt(x, y); set => this.SetAt(value, x, y); }
		public char this[Vec2i Index] { get => this.GetAt(Index); set => this.SetAt(value, Index); }
		public BorderProps BorderProperties { get; set; } = new BorderProps();

		public event EventHandler OnPreAppend;
		public event EventHandler OnPostAppend;
		public event EventHandler OnPreColorDraw;
		public event EventHandler OnPostColorDraw;

		public event EventHandler OnContentCleared;
		public event EventHandler OnColorsCleared;

		public event EventHandler<OnValueChangedArgs<Vec2i>> OnSizeChanged;
		public event EventHandler<OnValueChangedArgs<Vec2d>> OnPositionChanged;

		public event EventHandler<OnValueChangedArgs<Reg2>> OnRegionChanged;

		public event EventHandler<OnModifiedArgs<char>> OnContentModified;
		public event EventHandler<OnModifiedArgs<ConsoleColor>> OnColorsModified;

		public void SetAt(char el, int x, int y)
		{
			this.pContent.SetAt(el, x, y);
			this.OnContentModified?.Invoke(this, new OnModifiedArgs<char> { Index = new Vec2i(x, y), ValueAt = el });
		}
		public void SetAt(char el, Vec2i Index) => this.SetAt(el, Index.X, Index.Y);

		public void SetColorAt(ConsoleColor Col, int x, int y)
		{
			this.pContent.SetColorAt(Col, x, y);
			this.OnColorsModified?.Invoke(this, new OnModifiedArgs<ConsoleColor> { Index = new Vec2i(x, y), ValueAt = Col });
		}
		public void SetColorAt(ConsoleColor Col, Vec2i Index) => this.SetColorAt(Col, Index.X, Index.Y);

		public char GetAt(int x, int y) => this.pContent.GetAt(x, y);
		public char GetAt(Vec2i Index) => this.GetAt(Index.X, Index.Y);

		public ConsoleColor GetColorAt(int x, int y) => this.pContent.GetColorAt(x, y);
		public ConsoleColor GetColorAt(Vec2i Index) => this.GetColorAt(Index.X, Index.Y);

		public void Clear(char ClearWith)
		{
			this.pContent.Clear(ClearWith);
			this.OnContentCleared?.Invoke(this, new EventArgs());
		}
		public void ClearColors(ConsoleColor ClearWith)
		{
			this.pContent.ClearColors(ClearWith);
			this.OnColorsCleared?.Invoke(this, new EventArgs());
		}

		public void SetPosition(double xPos, double yPos)
		{
			Vec2d OldPos = this.GetPosition();

			this.pContent.SetPosition(xPos, yPos);

			this.OnPositionChanged?.Invoke(this, new OnValueChangedArgs<Vec2d> { OldValue = OldPos, NewValue = this.GetPosition() });
		}
		public void SetPosition(Vec2i Pos) => this.SetPosition(Pos.X, Pos.Y);
		public void SetPosition(Vec2d Pos) => this.SetPosition(Pos.X, Pos.Y);

		public void ResizeTo(int xSize, int ySize)
		{
			Vec2i OldSize = new Vec2i(xSize, ySize);

			this.pContent.ResizeTo(xSize, ySize);

			this.OnSizeChanged?.Invoke(this, new OnValueChangedArgs<Vec2i> { OldValue = OldSize, NewValue = this.GetSize() });
		}
		public void ResizeTo(Vec2i Size) => this.ResizeTo(Size.X, Size.Y);
		public void SetSize(int xSize, int ySize)
		{
			Vec2i OldSize = new Vec2i(xSize, ySize);

			this.pContent.SetSize(xSize, ySize);

			this.OnSizeChanged?.Invoke(this, new OnValueChangedArgs<Vec2i> { OldValue = OldSize, NewValue = this.GetSize() });
		}
		public void SetSize(Vec2i Size) => this.SetSize(Size.X, Size.Y);

		public void SetRegion(int xSize, int ySize, double xPos, double yPos)
		{
			Reg2 OldReg = this.GetRegion();

			this.pContent.SetRegion(xSize, ySize, xPos, yPos);

			this.OnRegionChanged?.Invoke(this, new OnValueChangedArgs<Reg2> { OldValue = OldReg, NewValue = this.GetRegion() });
		}
		public void SetRegion(int xSize, int ySize) => this.SetRegion(xSize, ySize, this.GetPosition().X, this.GetPosition().Y);
		public void SetRegion(Vec2i Size) => this.SetRegion(Size.X, Size.Y);
		public void SetRegion(Vec2i Size, Vec2i Pos) => this.SetRegion(Size.X, Size.Y, Pos.X, Pos.Y);
		public void SetRegion(Vec2i Size, Vec2d Pos) => this.SetRegion(Size.X, Size.Y, Pos.X, Pos.Y);
		public void SetRegion(Reg2 Reg) => this.SetRegion(Reg.GetSize(), Reg.GetPosition());

		public Vec2d GetPosition() => this.pContent.GetPosition();
		public Vec2i GetSize() => this.pContent.GetSize();
		public Reg2 GetRegion() => this.pContent.GetRegion();

		public void AppendTo(IContent2 Content)
		{
			this.OnPreAppend?.Invoke(this, new EventArgs());

			this.pHowToBorder(Content.GetSize(), (el, col, x, y) => { 
				Content.SetAt(el, x, y);
				Content.SetColorAt(col, x, y);
			});
			this.AppendConnectionsToSelf();
			this.pContent.AppendTo(Content);

			this.OnPostAppend?.Invoke(this, new EventArgs());
		}
		public void AppendTo(IModifiable2<char> Content)
		{
			this.OnPreAppend?.Invoke(this, new EventArgs());

			this.pHowToBorder(Content.GetSize(), (el, col, x, y) => Content.SetAt(el, x, y));
			this.AppendConnectionsToSelf();
			this.pContent.AppendTo(Content);

			this.OnPostAppend?.Invoke(this, new EventArgs());
		}
		public void DrawColorsToCan(Can2 Canvas)
		{
			this.OnPreColorDraw?.Invoke(this, new EventArgs());

			this.pHowToBorder(Canvas.GetSize(), (el, col, x, y) => Canvas.DrawColorAt(col, x, y));
			this.AppendConnectionsToSelf();
			this.pContent.DrawColorsToCan(Canvas);

			this.OnPostColorDraw?.Invoke(this, new EventArgs());
		}

		private void pHowToBorder(Vec2i ParentSize, Action<char, ConsoleColor, int, int> a)
		{
			Vec2i tPos = this.GetPosition().CloneAsVec2i();
			Vec2i tSize = this.GetSize();

			Vec2i Index = new Vec2i();

			for (int y = -1; y < tSize.Y + 1; ++y)
			{
				Index.Y = tPos.Y + y;

				if (y == -1 || y == tSize.Y)
				{
					char fillWith = (y == -1) ? (this.BorderProperties.GetBorderAtSide(BorderProps.BorderSide.Top)) : (this.BorderProperties.GetBorderAtSide(BorderProps.BorderSide.Bottom));

					for (int x = 0; x < tSize.X; ++x)
					{
						Index.X = tPos.X + x;

						if (this.pIsIndexNotOnParent(ParentSize, Index)) continue;

						a.Invoke(fillWith, this.BorderProperties.BorderColor, Index.X, Index.Y);
					}
				}
				else
				{
					Index.X = tPos.X - 1;
					if (!this.pIsIndexNotOnParent(ParentSize, Index)) a.Invoke(this.BorderProperties.GetBorderAtSide(BorderProps.BorderSide.Left), this.BorderProperties.BorderColor, Index.X, Index.Y);

					Index.X = tPos.X + tSize.X;
					if (!this.pIsIndexNotOnParent(ParentSize, Index)) a.Invoke(this.BorderProperties.GetBorderAtSide(BorderProps.BorderSide.Right), this.BorderProperties.BorderColor, Index.X, Index.Y);
				}
			}

			Index.X = tPos.X - 1;
			Index.Y = tPos.Y - 1;
			if (!this.pIsIndexNotOnParent(ParentSize, Index)) a.Invoke(this.BorderProperties.GetBorderAtCorner(BorderProps.BorderSide.Top, BorderProps.BorderSide.Left), this.BorderProperties.BorderColor, Index.X, Index.Y);

			Index.X = tPos.X - 1;
			Index.Y = tPos.Y + tSize.Y;
			if (!this.pIsIndexNotOnParent(ParentSize, Index)) a.Invoke(this.BorderProperties.GetBorderAtCorner(BorderProps.BorderSide.Bottom, BorderProps.BorderSide.Left), this.BorderProperties.BorderColor, Index.X, Index.Y);

			Index.X = tPos.X + tSize.X;
			Index.Y = tPos.Y - 1;
			if (!this.pIsIndexNotOnParent(ParentSize, Index)) a.Invoke(this.BorderProperties.GetBorderAtCorner(BorderProps.BorderSide.Top, BorderProps.BorderSide.Right), this.BorderProperties.BorderColor, Index.X, Index.Y);

			Index.X = tPos.X + tSize.X;
			Index.Y = tPos.Y + tSize.Y;
			if (!this.pIsIndexNotOnParent(ParentSize, Index)) a.Invoke(this.BorderProperties.GetBorderAtCorner(BorderProps.BorderSide.Bottom, BorderProps.BorderSide.Right), this.BorderProperties.BorderColor, Index.X, Index.Y);

		}

		private bool pIsIndexNotOnParent(Vec2i ParentSize, Vec2i Index)
		{
			return Index.X < 0 || Index.Y < 0 || Index.X >= ParentSize.X || Index.Y >= ParentSize.Y;
		}

		private Ent2 pContent = new Ent2();
	}
}
