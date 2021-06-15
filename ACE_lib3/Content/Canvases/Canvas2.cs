using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ACE_2D_Base.Vectors;
using ACE_lib3.Content.Interfaces;

namespace ACE_lib3.Content.Canvases
{
	public class Canvas2 : ITextModifiable2
	{
		public static Canvas2 GetInstance(string Title, int xSize, int ySize)
		{
			return new Canvas2(Title, xSize, ySize);
		}
		public static Canvas2 GetInstance(string Title, Vec2i Size) => GetInstance(Title, Size.X, Size.Y);

		public event EventHandler OnPreDraw;
		public event EventHandler OnPostDraw;

		public bool CheckIndex(int x, int y) => x >= 0 && y >= 0 && x < this._size.X && y < this._size.Y;
		public bool CheckIndex(Vec2i Index) => this.CheckIndex(Index.X, Index.Y);

		public void SetAt(char el, int x, int y)
		{
			if (this.CheckIndex(x, y))
			{
				this._charBuffer[this._IndexOf(x + this._offset.X, y + this._offset.Y)] = el;
			}
		}
		public void SetAt(char el, Vec2i Index) => this.SetAt(el, Index.X, Index.Y);

		public char GetAt(int x, int y)
		{
			if (this.CheckIndex(x, y))
			{
				return this._charBuffer[this._IndexOf(x + this._offset.X, y + this._offset.Y)];
			}

			throw new IndexOutOfRangeException();
		}
		public char GetAt(Vec2i Index) => this.GetAt(Index.X, Index.Y);

		public event EventHandler OnTextCleared;

		public void Clear(char ClearWith = ' ')
		{
			for (int y = 0; y < this._size.Y; ++y)
			{
				for (int x = 0; x < this._size.X; ++x)
				{
					this.SetAt(ClearWith, x, y);
				}
			}

			this.OnTextCleared?.Invoke(this, new EventArgs());
		}

		public void Draw()
		{
			Console.SetCursorPosition(0, 0);

			this.OnPreDraw?.Invoke(this, new EventArgs());

			Console.Write(this._charBuffer);

			this.OnPostDraw?.Invoke(this, new EventArgs());
		}
		public void DrawColorAt(ConsoleColor Col, int x, int y)
		{
			if (this.CheckIndex(x, y))
			{
				Console.SetCursorPosition(x + this._offset.X, y + this._offset.Y);
				Console.ForegroundColor = Col;

				Console.Write(this.GetAt(x, y));

				Console.ResetColor();
			}
		}

		private Canvas2(string Title, int xSize, int ySize)
		{
			this._initBuffers(xSize, ySize);
			this._initConsole(Title);
		}

		private void _initBuffers(int xSize, int ySize)
		{
			this._size = new Vec2i(xSize, ySize);
			this._bufferSize = new Vec2i(xSize + 4, ySize +2);

			this._charBuffer = new char[(this._bufferSize.X * this._bufferSize.Y) - 1];

			for (int y = 0; y < this._bufferSize.Y; ++y)
			{
				if (y == 0 || y == this._bufferSize.Y - 1)
				{
					for (int x = 0; x < this._size.X; ++x)
					{
						this._charBuffer[this._IndexOf(x + 2, y)] = '-';
					}
				}
				else 
				{
					this._charBuffer[this._IndexOf(1, y)] = '|';
					this._charBuffer[this._IndexOf(this._bufferSize.X - 2, y)] = '|';
				}

				if (y != this._bufferSize.Y - 1)
				{
					this._charBuffer[this._IndexOf(this._bufferSize.X - 1, y)] = '\n';
				}
			}

		}
		private void _initConsole(string title)
		{
			Console.Title = title;

			Console.SetWindowSize(this._bufferSize.X, this._bufferSize.Y);
			Console.SetBufferSize(this._bufferSize.X, this._bufferSize.Y);
			Console.SetWindowSize(this._bufferSize.X, this._bufferSize.Y);

			Console.Clear();

			Console.CursorVisible = false;
		}

		private int _IndexOf(int x, int y) => y * this._bufferSize.X + x;

		private char[] _charBuffer;

		private Vec2i _size;
		private Vec2i _bufferSize;

		private Vec2i _offset = new Vec2i(2, 1);

	}
}
