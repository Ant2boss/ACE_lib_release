using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ACE_2D_Base.Regions;
using ACE_2D_Base.Vectors;
using ACE_lib2.Args;
using ACE_lib2.Content.Interfaces;

namespace ACE_lib2.Content.Abstracts
{
	/// <summary>
	/// Simple implementation of IEntity2, where the entity is treated as a simple content buffer with a width (x) and a height (y)
	/// </summary>
	public abstract class absEntity2 : IEntity2
	{
		public event EventHandler<OnChangedArgs<Vec2d>> OnPositionChanged;
		public event EventHandler OnPreAppend;
		public event EventHandler OnPostAppend;

		public void SetPosition(double xPos, double yPos)
		{
			Vec2d Old = this.GetPosition();

			this._Position = new Vec2d(xPos, yPos);

			this.OnPositionChanged?.Invoke(this, new OnChangedArgs<Vec2d> { OldValue = Old, NewValue = this.GetPosition() });
		}
		public void SetPosition(Vec2i Position) => this.SetPosition(Position.X, Position.Y);
		public void SetPosition(Vec2d Position) => this.SetPosition(Position.X, Position.Y);

		public Reg2 GetRegion() => new Reg2(this._BuffSize.Clone() as Vec2i, this._Position.Clone() as Vec2d);

		public void AppendTo(ITextContent ParentContent)
		{
			this.OnPreAppend?.Invoke(this, new EventArgs());

			Vec2i ParentSize = ParentContent.GetSize();

			Vec2i Index = new Vec2i();
			Vec2i MyPos = this.GetPosition().CloneAsVec2i();

			for (int y = 0; y < ParentSize.Y; ++y)
			{
				for (int x = 0; x < ParentSize.X; ++x)
				{
					Index.X = MyPos.X + x;
					Index.Y = MyPos.Y + y;

					if (ParentContent.CheckIndex(Index))
					{
						ParentContent.SetAt(this._iGetAt(x, y), Index);
					}
				}
			}

			this.OnPostAppend?.Invoke(this, new EventArgs());
		}

		public void AppendColorsTo(IColorContent ParentContent)
		{
			this.OnPreAppend?.Invoke(this, new EventArgs());

			Vec2i ParentSize = ParentContent.GetSize();

			Vec2i Index = new Vec2i();
			Vec2i MyPos = this.GetPosition().CloneAsVec2i();

			for (int y = 0; y < ParentSize.Y; ++y)
			{
				for (int x = 0; x < ParentSize.X; ++x)
				{
					Index.X = MyPos.X + x;
					Index.Y = MyPos.Y + y;

					if (ParentContent.CheckIndex(Index))
					{
						ParentContent.SetColorAt(this._iGetColorAt(x, y), Index);
					}
				}
			}

			this.OnPostAppend?.Invoke(this, new EventArgs());
		}


		//-------------------
		//	Internals
		//-------------------

		internal void _iSetSize(int xSize, int ySize)
		{
			int xBound = Math.Min(xSize, this._BuffSize.X);
			int yBount = Math.Min(ySize, this._BuffSize.Y);

			char[] charTemp = new char[xSize * ySize];
			ConsoleColor[] colTemp = new ConsoleColor[xSize * ySize];

			for (int y = 0; y < this._BuffSize.Y; ++y)
			{
				for (int x = 0; x < this._BuffSize.X; ++x)
				{
					charTemp[y * xSize + x] = this._CharBuffer[this._IndexOf(x, y)];
					colTemp[y * xSize + x] = this._ColBuffer[this._IndexOf(x, y)];
				}
			}

			this._CharBuffer = charTemp;
			this._ColBuffer = colTemp;

			this._BuffSize = new Vec2i(xSize, ySize);
		}
		internal void _iSetSize(Vec2i Size) => this._iSetSize(Size.X, Size.Y);

		internal void _iSetAt(char el, int x, int y) => this._CharBuffer[this._IndexOf(x, y)] = el;
		internal void _iSetAt(char el, Vec2i Index) => this._iSetAt(el, Index.X, Index.Y);

		internal void _iSetColorAt(ConsoleColor el, int x, int y) => this._ColBuffer[this._IndexOf(x, y)] = el;
		internal void _iSetColorAt(ConsoleColor el, Vec2i Index) => this._iSetColorAt(el, Index.X, Index.Y);

		internal char _iGetAt(int x, int y) => this._CharBuffer[this._IndexOf(x, y)];
		internal char _iGetAt(Vec2i Index) => this._iGetAt(Index.X, Index.Y);

		internal ConsoleColor _iGetColorAt(int x, int y) => this._ColBuffer[this._IndexOf(x, y)];
		internal ConsoleColor _iGetColorAt(Vec2i Index) => this._iGetColorAt(Index.X, Index.Y);

		internal void _iClear(char el = ' ')
		{
			for (int i = 0; i < this._CharBuffer.Length; ++i)
			{
				this._CharBuffer[i] = el;
			}
		}
		internal void _iClearColors(ConsoleColor el = ConsoleColor.Gray)
		{
			for (int i = 0; i < this._ColBuffer.Length; ++i)
			{
				this._ColBuffer[i] = el;
			}
		}

		private int _IndexOf(int x, int y) => y * this._BuffSize.X + x;

		private char[] _CharBuffer;
		private ConsoleColor[] _ColBuffer;

		private Vec2i _BuffSize;
		private Vec2d _Position;

	}
}
