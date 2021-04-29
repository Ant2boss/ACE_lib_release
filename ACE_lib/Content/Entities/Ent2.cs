using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ACE_lib.Args;
using ACE_lib.Content;
using ACE_lib.Content.Canvases;
using ACE_lib.Regions;
using ACE_lib.Vectors;

namespace ACE_lib.Content.Entities
{
	public class Ent2 : IContent2
	{
		public Ent2()
		{
			this.pSize = new Vec2i();
			this.pPos = new Vec2d();
		}
		public Ent2(int xSize, int ySize) : this()
		{
			this.SetSize(xSize, ySize);
		}
		public Ent2(int xSize, int ySize, double xPos, double yPos) : this(xSize, ySize)
		{
			this.SetPosition(xPos, yPos);
		}
		
		public Ent2(IConnectable2 Parent) : this() { Parent.AddConnection(this); }
		public Ent2(IConnectable2 Parent, int xSize, int ySize) : this() { Parent.AddConnection(this); }
		public Ent2(IConnectable2 Parent, int xSize, int ySize, double xPos, double yPos) : this() { Parent.AddConnection(this); }

		public Ent2(Vec2i Size) : this(Size.X, Size.Y) { }
		public Ent2(Vec2i Size, Vec2i Position) : this(Size.X, Size.Y, Position.X, Position.Y) { }
		public Ent2(Vec2i Size, Vec2d Position) : this(Size.X, Size.Y, Position.X, Position.Y) { }
		public Ent2(Reg2 Region) : this(Region.GetSize(), Region.GetPosition()) {}
		public Ent2(IConnectable2 Parent, Vec2i Size) : this(Parent, Size.X, Size.Y) { }
		public Ent2(IConnectable2 Parent, Vec2i Size, Vec2i Position) : this(Parent, Size.X, Size.Y, Position.X, Position.Y) { }
		public Ent2(IConnectable2 Parent, Vec2i Size, Vec2d Position) : this(Parent, Size.X, Size.Y, Position.X, Position.Y) { }
		public Ent2(IConnectable2 Parent, Reg2 Region) : this(Parent, Region.GetSize(), Region.GetPosition()) { }

		public char this[Vec2i Index] { get => this.GetAt(Index); set => this.SetAt(value, Index); }
		public char this[int x, int y] { get => this.GetAt(x, y); set => this.SetAt(value, x, y); }

		public event EventHandler<OnModifiedArgs<char>> OnContentModified;
		public event EventHandler<OnModifiedArgs<ConsoleColor>> OnColorsModified;

		public event EventHandler<OnValueChangedArgs<Vec2i>> OnSizeChanged;
		public event EventHandler<OnValueChangedArgs<Vec2d>> OnPositionChanged;
		public event EventHandler<OnValueChangedArgs<Reg2>> OnRegionChanged;

		public event EventHandler OnContentCleared;
		public event EventHandler OnColorsCleared;

		public event EventHandler OnPreAppend;
		public event EventHandler OnPostAppend;
		public event EventHandler OnPreColorDraw;
		public event EventHandler OnPostColorDraw;

		public void SetAt(char el, int x, int y)
		{
			if (this.pCheckIndex(x, y)) throw new IndexOutOfRangeException();

			this.pBuffer[this.pIndexOf(x, y)] = el;

			this.OnContentModified?.Invoke(this, new OnModifiedArgs<char> { Index = new Vec2i(x, y), ValueAt = el });
		}
		public void SetAt(char el, Vec2i Index) => this.SetAt(el, Index.X, Index.Y);

		public void SetColorAt(ConsoleColor Col, int x, int y)
		{
			if (this.pCheckIndex(x, y)) throw new IndexOutOfRangeException();

			this.pColorBuffer[this.pIndexOf(x, y)] = Col;

			this.OnColorsModified?.Invoke(this, new OnModifiedArgs<ConsoleColor> { Index = new Vec2i(x, y), ValueAt = Col });
		}
		public void SetColorAt(ConsoleColor Col, Vec2i Index) => this.SetColorAt(Col, Index.X, Index.Y);

		public char GetAt(int x, int y)
		{
			if (this.pCheckIndex(x, y)) throw new IndexOutOfRangeException();

			return this.pBuffer[this.pIndexOf(x, y)];
		}
		public char GetAt(Vec2i Index) => this.GetAt(Index.X, Index.Y);

		public ConsoleColor GetColorAt(int x, int y)
		{
			if (this.pCheckIndex(x, y)) throw new IndexOutOfRangeException();

			return this.pColorBuffer[this.pIndexOf(x, y)];
		}
		public ConsoleColor GetColorAt(Vec2i Index) => this.GetColorAt(Index.X, Index.Y);

		public void SetSize(int xSize, int ySize)
		{
			Vec2i OldSize = this.GetSize();

			this.pSize.Init(xSize, ySize);

			this.pBuffer = new char[xSize * ySize];
			this.pColorBuffer = new ConsoleColor[xSize * ySize];

			this.OnSizeChanged?.Invoke(this, new OnValueChangedArgs<Vec2i> { OldValue = OldSize, NewValue = this.GetSize() });
		}
		public void SetSize(Vec2i Size) => this.SetSize(Size.X, Size.Y);

		public void SetPosition(double xPos, double yPos)
		{
			Vec2d OldPos = this.GetPosition();

			this.pPos.Init(xPos, yPos);

			this.OnPositionChanged?.Invoke(this, new OnValueChangedArgs<Vec2d> { OldValue = OldPos, NewValue = this.GetPosition() });
		}
		public void SetPosition(Vec2i Pos) => this.SetPosition(Pos.X, Pos.Y);
		public void SetPosition(Vec2d Pos) => this.SetPosition(Pos.X, Pos.Y);

		public void SetRegion(int xSize, int ySize, double xPos, double yPos)
		{
			Reg2 OldReg = this.GetRegion();

			this.SetSize(xSize, ySize);
			this.SetPosition(xPos, yPos);

			this.OnRegionChanged?.Invoke(this, new OnValueChangedArgs<Reg2> { OldValue = OldReg, NewValue = this.GetRegion() });
		}
		public void SetRegion(int xSize, int ySize) => this.SetRegion(xSize, ySize, this.pPos.X, this.pPos.Y);
		public void SetRegion(Vec2i Size) => this.SetRegion(Size.X, Size.Y);
		public void SetRegion(Vec2i Size, Vec2i Pos) => this.SetRegion(Size.X, Size.Y, Pos.X, Pos.Y);
		public void SetRegion(Vec2i Size, Vec2d Pos) => this.SetRegion(Size.X, Size.Y, Pos.X, Pos.Y);
		public void SetRegion(Reg2 Reg) => this.SetRegion(Reg.GetSize(), Reg.GetPosition());

		public Vec2i GetSize() => this.pSize.Clone() as Vec2i;
		public Vec2d GetPosition() => this.pPos.Clone() as Vec2d;
		public Reg2 GetRegion() => new Reg2(this.pSize, this.pPos);

		public void Clear(char ClearWith)
		{
			this.pForEach((x, y) => this.SetAt(ClearWith, x, y));

			this.OnContentCleared?.Invoke(this, new EventArgs());
		}
		public void ClearColors(ConsoleColor ClearWith)
		{
			this.pForEach((x, y) => this.SetColorAt(ClearWith, x, y));

			this.OnColorsCleared?.Invoke(this, new EventArgs());
		}

		public void AppendTo(IContent2 Content)
		{
			this.OnPreAppend?.Invoke(this, new EventArgs());

			this.pForEachTransposed(Content.GetSize(), (xParent, yParent, xMe, yMe) => {

				Content.SetAt(this.GetAt(xMe, yMe), xParent, yParent);
				Content.SetColorAt(this.GetColorAt(xMe, yMe), xParent, yParent);
				
			});

			this.OnPostAppend?.Invoke(this, new EventArgs());
		}
		public void AppendTo(IModifiable2<char> Content)
		{
			this.OnPreAppend?.Invoke(this, new EventArgs());

			this.pForEachTransposed(Content.GetSize(), (xParent, yParent, xMe, yMe) => {

				Content.SetAt(this.GetAt(xMe, yMe), xParent, yParent);

			});

			this.OnPostAppend?.Invoke(this, new EventArgs());
		}
		public void DrawColorsToCan(Can2 Canvas)
		{
			this.OnPreColorDraw?.Invoke(this, new EventArgs());

			this.pForEachTransposed(Canvas.GetSize(), (xParent, yParent, xMe, yMe) => {

				Canvas.DrawColorAt(this.GetColorAt(xMe, yMe), xParent, yParent);

			});

			this.OnPostColorDraw?.Invoke(this, new EventArgs());
		}

		private void pForEach(Action<int, int> a)
		{
			for (int y = 0; y < this.pSize.Y; ++y)
			{
				for (int x = 0; x < this.pSize.X; ++x)
				{
					a.Invoke(x, y);
				}
			}
		}
		private void pForEachTransposed(Vec2i ParentSize, Action<int, int, int, int> a)
		{
			Vec2i Index = new Vec2i();

			for (int y = 0; y < this.pSize.Y; ++y)
			{
				for (int x = 0; x < this.pSize.X; ++x)
				{
					Index.X = (int)this.pPos.X + x;
					Index.Y = (int)this.pPos.Y + y;

					if (Index.X < 0 || Index.Y < 0 || Index.X >= ParentSize.X || Index.Y >= ParentSize.Y) continue;

					a.Invoke(Index.X, Index.Y, x, y);
				}
			}
		}
		private int pIndexOf(int x, int y) => y * this.pSize.X + x;
		private bool pCheckIndex(int x, int y) => x < 0 || y < 0 || x >= this.pSize.X || y >= this.pSize.Y;

		private char[] pBuffer;
		private ConsoleColor[] pColorBuffer;

		private Vec2i pSize;
		private Vec2d pPos;

	}
}
