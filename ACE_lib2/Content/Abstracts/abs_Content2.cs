using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ACE_2D_Base.Regions;
using ACE_2D_Base.Vectors;
using ACE_lib2.Args;
using ACE_lib2.Content.Canvases;
using ACE_lib2.Content.Interfaces;

namespace ACE_lib2.Content.Abstracts
{
	public abstract class abs_Content2 : abs_Modifiable2<char>, IContent2
	{
		public Reg2 Region { get; set; }

		public event EventHandler<OnChangedArgs<Reg2>> OnRegionChanged;

		public event EventHandler OnColorsCleared;
		public event EventHandler OnPreAppend;
		public event EventHandler OnPostAppend;

		public override void SetAt(char el, int x, int y)
		{
			this._DoChecks(x, y);

			this._ChBuffer[this._IndexOf(x, y, this._Width)] = el;
		}

		public void SetColorAt(ConsoleColor Col, int x, int y)
		{
			this._DoChecks(x, y);

			this._ColBuffer[this._IndexOf(x, y, this._Width)] = Col;
		}
		public void SetColorAt(ConsoleColor Col, Vec2i Index) => this.SetColorAt(Col, Index.X, Index.Y);

		public override char GetAt(int x, int y)
		{
			this._DoChecks(x, y);

			return this._ChBuffer[this._IndexOf(x, y, this._Width)];
		}

		public ConsoleColor GetColorAt(int x, int y)
		{
			this._DoChecks(x, y);

			return this._ColBuffer[this._IndexOf(x, y, this._Width)];
		}
		public ConsoleColor GetColorAt(Vec2i Index) => this.GetColorAt(Index.X, Index.Y);

		public void ClearColors(ConsoleColor ClearWith)
		{
			for (int i = 0; i < this._ColBuffer.Length; ++i)
			{
				this._ColBuffer[i] = ClearWith;
			}

			this.OnColorsCleared?.Invoke(this, new EventArgs());
		}

		public Vec2d GetPosition() => this.Region.GetPosition();
		public override Vec2i GetSize() => this.Region.GetSize();

		public void AppendTo(IModifiable2<char> Parent)
		{
			this._Attacher((x, y, px, py) => { Parent.SetAt(this.GetAt(x, y), px, py); }, Parent.GetSize());
		}
		public void AppendTo(IContent2 Parent)
		{
			this._Attacher((x, y, px, py) => { 
				Parent.SetAt(this.GetAt(x, y), px, py);
				Parent.SetColorAt(this.GetColorAt(x, y), px, py);
			}, Parent.GetSize());
		}

		public void DrawColorsTo(Canvas2 Can)
		{
			this._Attacher((x, y, px, py) => {
				Can.DrawColorAt(this.GetColorAt(x, y), px, py);
			}, Can.GetSize());
		}

		public override string ToString() => $"Content2{{ {this.Region} }}";

		private void _Attacher(Action<int, int, int, int> HowTo, Vec2i ParentSize)
		{
			this.OnPreAppend?.Invoke(this, new EventArgs());

			Vec2i Pos = this.GetPosition().CloneAsVec2i();
			Vec2i Siz = this.GetSize();

			Vec2i Index = new Vec2i();

			for (int y = 0; y < Siz.Y; ++y)
			{
				for (int x = 0; x < Siz.X; ++x)
				{
					Index.X = Pos.X + x;
					Index.Y = Pos.Y + y;

					if (Index.X < 0 || Index.Y < 0 || Index.X >= ParentSize.X || Index.Y >= ParentSize.Y)
					{
						continue;
					}

					HowTo?.Invoke(x, y, Index.X, Index.Y);
				}
			}

			this.OnPostAppend?.Invoke(this, new EventArgs());
		}

		private void _DoChecks(int x, int y)
		{
			if (x < 0 || y < 0 || x >= this.Region.GetSize().X || y >= this.Region.GetSize().Y)
			{
				throw new IndexOutOfRangeException("Index out of bounds!");
			}

			this._CheckRegion();
		}
		private void _CheckRegion()
		{
			if (this._LastRegion == null)
			{
				this._ResizeBuffers(this.Region.GetSize(), new Vec2i(0, 0));
				this._LastRegion = this.Region.Clone() as Reg2;
				this._Width = this._LastRegion.GetSize().X;
			}
			//If region changed
			else if (this._LastRegion != this.Region)
			{
				//If size was changed
				if (this._LastRegion.GetSize() != this.Region.GetSize())
				{
					this._ResizeBuffers(this.Region.GetSize(), this._LastRegion.GetSize());
				}

				this.OnRegionChanged?.Invoke(this, new OnChangedArgs<Reg2> { OldValue = this._LastRegion.Clone() as Reg2, NewValue = this.Region.Clone() as Reg2 });

				this._LastRegion = this.Region.Clone() as Reg2;
				this._Width = this._LastRegion.GetSize().X;
			}
		}
		private void _ResizeBuffers(Vec2i NewSize, Vec2i OldSize)
		{
			int xBound = Math.Min(NewSize.X, OldSize.X);
			int yBound = Math.Min(NewSize.Y, OldSize.Y);

			char[] tChars = new char[NewSize.X * NewSize.Y];
			ConsoleColor[] tColors = new ConsoleColor[NewSize.X * NewSize.Y];

			for (int y = 0; y < yBound; ++y)
			{
				for (int x = 0; x > xBound; ++x)
				{
					int OldIndex = this._IndexOf(x, y, OldSize.X);
					int NewIndex = this._IndexOf(x, y, NewSize.X);

					tChars[NewIndex] = this._ChBuffer[OldIndex];
					tColors[NewIndex] = this._ColBuffer[OldIndex];
				}
			}

			this._ChBuffer = tChars;
			this._ColBuffer = tColors;
		}

		private int _IndexOf(int x, int y, int Width) => y * Width + x;

		private char[] _ChBuffer;
		private ConsoleColor[] _ColBuffer;

		private Reg2 _LastRegion;
		private int _Width;
	}
}
