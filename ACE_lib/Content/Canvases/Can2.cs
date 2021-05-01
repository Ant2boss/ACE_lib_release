using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ACE_lib.Vectors;
using ACE_lib.Content;
using ACE_lib.Args;
using ACE_lib.Regions;

namespace ACE_lib.Content.Canvases
{
	public class Can2 : absConnectableBase, IModifiable2<char>, IReg2_readonly
	{
		public static Can2 CreateCanvasSingleton(string Title, int xSize, int ySize)
		{
			if (Instance != null) throw new Exception("Canvas singleton already exists!");

			Instance = new Can2(Title, xSize, ySize);

			return Instance;
		}
		public static Can2 CreateCanvasSingleton(string Title, Vec2i Size) => Can2.CreateCanvasSingleton(Title, Size);
		public static Can2 GetCanvasSingleton()
		{
			if (Instance == null) throw new Exception("Canvas singleton has not yet been created!");

			return Instance;
		}

		public event EventHandler<OnModifiedArgs<char>> OnContentModified;
		public event EventHandler OnContentCleared;

		public event EventHandler OnPreDrawn;
		public event EventHandler OnPostDrawn;

		public event EventHandler OnPreConnectionColorsDrawn;
		public event EventHandler OnPostConnectionColorsDrawn;

		public char this[Vec2i Index] { get => this.GetAt(Index); set => this.SetAt(value, Index); }
		public char this[int x, int y] { get => this.GetAt(x, y); set => this.SetAt(value, x, y); }

		public void SetAt(char el, int x, int y)
		{
			if (this.pCheckIndex(x, y)) throw new IndexOutOfRangeException();

			this.pBuffer[this.pIndexOf(x + this.pOffset.X, y + this.pOffset.Y)] = el;

			this.OnContentModified?.Invoke(this, new OnModifiedArgs<char> { ValueAt = el, Index = new Vec2i(x, y)});
		}
		public void SetAt(char el, Vec2i Index) => this.SetAt(el, Index.X, Index.Y);

		public char GetAt(int x, int y)
		{
			if (this.pCheckIndex(x, y)) throw new IndexOutOfRangeException();

			return this.pBuffer[this.pIndexOf(x + this.pOffset.X, y + this.pOffset.Y)];
		}
		public char GetAt(Vec2i Index) => this.GetAt(Index.X, Index.Y);

		public void Clear(char ClearWith)
		{
			for (int y = 0; y < this.pSize.Y; ++y)
			{
				for (int x = 0; x < this.pSize.X; ++x)
				{
					this.SetAt(ClearWith, x, y);
				}
			}

			this.OnContentCleared?.Invoke(this, new EventArgs());
		}

		public Vec2i GetSize() => this.pSize.Clone() as Vec2i;
		public Vec2d GetPosition() => new Vec2d(0, 0);
		public Reg2 GetRegion() => new Reg2(this.pSize, new Vec2d(0, 0));

		public void Draw()
		{
			this.AppendConnectionsToSelf();

			this.OnPreDrawn?.Invoke(this, new EventArgs());

			Console.SetCursorPosition(0, 0);
			Console.Write(this.pBuffer);

			this.OnPostDrawn?.Invoke(this, new EventArgs());
		}
		public void DrawColorAt(ConsoleColor Col, int x, int y)
		{
			Console.ForegroundColor = Col;

			Console.SetCursorPosition(x + this.pOffset.X, y + this.pOffset.Y);
			Console.Write(this.GetAt(x, y));

			Console.ResetColor();
		}
		public void DrawConnectionsColors()
		{
			this.OnPreConnectionColorsDrawn?.Invoke(this, new EventArgs());

			for (int i = 0; i < this.ConnectedCount; ++i)
			{
				this.GetConnectedDetails(i).DrawColorsToCan(this);
			}

			this.OnPostConnectionColorsDrawn?.Invoke(this, new EventArgs());
		}

		public string DrawAndReadLine(ConsoleColor TextColor, int xCursor, int yCursor)
		{
			this.Draw();

			Console.SetCursorPosition(xCursor + this.pOffset.X, yCursor + this.pOffset.Y);

			Console.ForegroundColor = TextColor;
			Console.CursorVisible = true;

			string result = Console.ReadLine();

			Console.CursorVisible = false;
			Console.ResetColor();

			return result;
		}
		public string DrawAndReadLine(int xCursor, int yCursor) => this.DrawAndReadLine(ConsoleColor.Gray, xCursor, yCursor);
		public string DrawAndReadLine(Vec2i CursorPos) => this.DrawAndReadLine(ConsoleColor.Gray, CursorPos.X, CursorPos.Y);
		public string DrawAndReadLine(ConsoleColor TextColor, Vec2i CursorPos) => this.DrawAndReadLine(TextColor, CursorPos.X, CursorPos.Y);

		public string DrawColorsAndReadLine(ConsoleColor TextColor, int xCursor, int yCursor)
		{
			this.Draw();
			this.DrawConnectionsColors();

			Console.SetCursorPosition(xCursor + this.pOffset.X, yCursor + this.pOffset.Y);

			Console.ForegroundColor = TextColor;
			Console.CursorVisible = true;

			string result = Console.ReadLine();

			Console.CursorVisible = false;
			Console.ResetColor();

			return result;
		}
		public string DrawColorsAndReadLine(int xCursor, int yCursor) => this.DrawColorsAndReadLine(ConsoleColor.Gray, xCursor, yCursor);
		public string DrawColorsAndReadLine(Vec2i CursorPos) => this.DrawColorsAndReadLine(ConsoleColor.Gray, CursorPos.X, CursorPos.Y);
		public string DrawColorsAndReadLine(ConsoleColor TextColor, Vec2i CursorPos) => this.DrawColorsAndReadLine(TextColor, CursorPos.X, CursorPos.Y);

		private bool pCheckIndex(int x, int y) => x < 0 || y < 0 || x >= this.GetSize().X || y >= this.GetSize().Y;

		private Can2(string Title, int xSize, int ySize)
		{ 
			this.pInitBuffer(xSize, ySize);
			this.pInitConsole(Title);
		}
		private void pInitConsole(string title)
		{
			Console.SetWindowSize(this.pBufferSize.X, this.pBufferSize.Y);
			Console.SetBufferSize(this.pBufferSize.X, this.pBufferSize.Y);
			Console.SetWindowSize(this.pBufferSize.X, this.pBufferSize.Y);

			Console.Clear();

			Console.CursorVisible = false;

			Console.Title = title;
		}
		private void pInitBuffer(int xSize, int ySize)
		{
			this.pSize = new Vec2i(xSize, ySize);
			this.pBufferSize = new Vec2i(xSize + 4, ySize + 2);

			this.pOffset = new Vec2i(2, 1);

			this.pBuffer = new char[(this.pBufferSize.X * this.pBufferSize.Y) - 1];

			for (int y = 0; y < this.pBufferSize.Y; ++y)
			{
				if (y == 0 || y == this.pBufferSize.Y - 1)
				{
					for (int x = 0; x < this.pSize.X; ++x)
					{
						this.pBuffer[this.pIndexOf(x + this.pOffset.X, y)] = '-';
					}
				}
				else
				{
					this.pBuffer[this.pIndexOf(1, y)] = '|';
					this.pBuffer[this.pIndexOf(this.pBufferSize.X - 2, y)] = '|';
				}

				if(y != this.pBufferSize.Y - 1) this.pBuffer[this.pIndexOf(this.pBufferSize.X - 1, y)] = '\n';
			}
		}
		private int pIndexOf(int x, int y) => y * this.pBufferSize.X + x;
		internal override void abs_HandleConnectionAppending(IContent2 Con, int Index)
		{
			Con.AppendTo(this);
		}

		private char[] pBuffer;

		private Vec2i pSize;
		private Vec2i pBufferSize;

		private Vec2i pOffset;

		private static Can2 Instance;
	}
}
