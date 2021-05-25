using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ACE_2D_Base.Vectors;
using ACE_lib2.Content.Abstracts;
using ACE_lib2.Content.Interfaces;

namespace ACE_lib2.Content.Canvases
{
	public class Canvas2 : abs_Modifiable2<char>
	{
		private Canvas2(string Title, int xSize, int ySize)
		{
			this._InitBuffer(xSize, ySize);
			this._InitConsole(Title);

			this._FillBuffer();
		}

		private static Canvas2 _CanSingleton;

		public static Canvas2 CreateCanvasSingleton(string Title, int xSize, int ySize)
		{
			if (_CanSingleton != null)
			{
				throw new Exception("Canvas2 already exists");
			}

			_CanSingleton = new Canvas2(Title, xSize, ySize);
			return _CanSingleton;
		}
		public static Canvas2 CreateCanvasSingleton(string Title, Vec2i Size) => CreateCanvasSingleton(Title, Size.X, Size.Y);

		public event EventHandler OnPreCanvasDraw;
		public event EventHandler OnPostCanvasDraw;

		public override void SetAt(char el, int x, int y)
		{
			this._CheckIndex(x, y);

			this._Buffer[_MappedIndexOf(x, y)] = el;
		}
		public override char GetAt(int x, int y)
		{
			this._CheckIndex(x, y);

			return this._Buffer[this._MappedIndexOf(x, y)];
		}

		public override Vec2i GetSize() => this._Size.Clone() as Vec2i;

		public void Draw()
		{
			this.OnPreCanvasDraw?.Invoke(this, new EventArgs());

			Console.SetCursorPosition(0, 0);
			Console.Write(this._Buffer);

			this.OnPostCanvasDraw?.Invoke(this, new EventArgs());
		}

		public void DrawColorAt(ConsoleColor Col, int x, int y)
		{
			this._CheckIndex(x, y);

			Console.SetCursorPosition(x + this._Offset.X, y + this._Offset.Y);

			Console.ForegroundColor = Col;
			Console.Write(this.GetAt(x, y));
			Console.ResetColor();
		}
		public void DrawColorAt(ConsoleColor Col, Vec2i Index) => this.DrawColorAt(Col, Index.X, Index.Y);

		private void _CheckIndex(int x, int y)
		{
			if (x < 0 || y < 0 || x >= this._Size.X || y >= this._Size.Y)
			{
				throw new IndexOutOfRangeException("Index out of region!");
			}
		}
		private int _MappedIndexOf(int x, int y) => this._IndexOf(x + this._Offset.X, y + this._Offset.Y);
		private int _IndexOf(int x, int y) => y * this._BuffSize.X + x;

		private void _InitBuffer(int xSize, int ySize)
		{
			this._Size = new Vec2i(xSize, ySize);
			this._BuffSize = new Vec2i(xSize + 4, ySize + 2);

			this._Offset = new Vec2i(2, 1);

			this._Buffer = new char[this._BuffSize.X * this._BuffSize.Y - 1];
		}
		private void _InitConsole(string title)
		{
			Console.Title = title;

			Console.SetWindowSize(this._BuffSize.X, this._BuffSize.Y);
			Console.SetBufferSize(this._BuffSize.X, this._BuffSize.Y);
			Console.SetWindowSize(this._BuffSize.X, this._BuffSize.Y);

			Console.CursorVisible = false;
		}
		private void _FillBuffer()
		{
			for (int y = 0; y < this._BuffSize.Y; ++y)
			{
				if (y == 0 || y == this._BuffSize.Y - 1)
				{
					for (int x = 0; x < this._Size.X; ++x)
					{
						this._Buffer[this._IndexOf(x + 2, y)] = '-';
					}
				}
				else
				{
					this._Buffer[this._IndexOf(1, y)] = '|';
					this._Buffer[this._IndexOf(this._BuffSize.X - 2, y)] = '|';
				}

				if (y != this._BuffSize.Y - 1)
				{
					this._Buffer[this._IndexOf(this._BuffSize.X - 1, y)] = '\n';
				}
			}
		}

		private char[] _Buffer;

		private Vec2i _BuffSize;
		private Vec2i _Size;

		private Vec2i _Offset;

	}
}
