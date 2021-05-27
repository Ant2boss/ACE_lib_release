using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ACE_2D_Base.Regions;
using ACE_2D_Base.Vectors;
using ACE_lib2.Content.Interfaces;

namespace ACE_lib2.Content.Entities
{
	public class TextEntity2 : Entity2
	{
		public TextEntity2() => this._InitClass();
		public TextEntity2(Vec2i Size) : base(Size) => this._InitClass();
		public TextEntity2(Reg2 Reg) : base(Reg) => this._InitClass();
		public TextEntity2(int xSize, int ySize) : base(xSize, ySize) => this._InitClass();
		public TextEntity2(Vec2i Size, Vec2i Pos) : base(Size, Pos) => this._InitClass();
		public TextEntity2(Vec2i Size, Vec2d Pos) : base(Size, Pos) => this._InitClass();
		public TextEntity2(int xSize, int ySize, double xPos, double yPos) : base(xSize, ySize, xPos, yPos) => this._InitClass();
		public TextEntity2(IConnectable2 Parent) : base(Parent) => this._InitClass();
		public TextEntity2(IConnectable2 Parent, int xSize, int ySize) : base(Parent, xSize, ySize) => this._InitClass();
		public TextEntity2(IConnectable2 Parent, int xSize, int ySize, double xPos, double yPos) : base(Parent, xSize, ySize, xPos, yPos) => this._InitClass();
		public TextEntity2(IConnectable2 Parent, Vec2i Size) : base(Parent, Size) => this._InitClass();
		public TextEntity2(IConnectable2 Parent, Vec2i Size, Vec2i Pos) : base(Parent, Size, Pos) => this._InitClass();
		public TextEntity2(IConnectable2 Parent, Vec2i Size, Vec2d Pos) : base(Parent, Size, Pos) => this._InitClass();
		public TextEntity2(IConnectable2 Parent, Reg2 Reg) : base(Parent, Reg) => this._InitClass();

		public bool TextWrap { get; set; }
		public bool TextBreak { get; set; }

		public int TabLenght { get; set; }
		public int SpaceLenght { get; set; }
		public int NewLineLenght { get; set; }

		public Vec2i CursorPosition { get; set; }

		public ConsoleColor TextColor { get; set; }

		public void Write(string str)
		{
			if (this.TextBreak)
			{
				string[] words = str.Split(' ');

				for (int i = 0; i < words.Length; ++i)
				{
					if (this._CanFit(words[i]))
					{
						this._NewLine();
					}

					this._WriteStr(words[i]);

					if (i != words.Length - 1)
					{
						this._Space();
					}
				}
			}
			else
			{
				this._WriteStr(str);
			}
		}
		public void Write(string str, ConsoleColor Col)
		{
			ConsoleColor t = this.TextColor;
			this.TextColor = Col;
			this.Write(str);
			this.TextColor = t;
		}
		
		public void Write(object obj) => this.Write(obj.ToString());
		public void Write(object obj, ConsoleColor Col) => this.Write(obj.ToString(), Col);

		public void WriteLine() => this.Write("\n");
		
		public void WriteLine(string str) => this.Write($"{str}\n");
		public void WriteLine(string str, ConsoleColor Col) => this.Write($"{str}\n", Col);
		
		public void WriteLine(object obj) => this.Write($"{obj}\n");
		public void WriteLine(object obj, ConsoleColor Col) => this.Write($"{obj}\n", Col);

		private bool _CanFit(string word)
		{
			int totalLen = this.CursorPosition.X;

			for (int i = 0; i < word.Length; ++i)
			{
				if (word[i] == '\t')
				{
					totalLen += this._CalcTabLen(this.CursorPosition.X + totalLen);
				}
				else
				{
					totalLen += this.SpaceLenght;
				}
			}

			return totalLen >= this.GetSize().X;
		}

		private void _WriteStr(string str)
		{
			for (int i = 0; i < str.Length; ++i)
			{
				this._WriteChar(str[i]);
			}
		}

		private void _WriteChar(char c)
		{
			switch (c)
			{
				case ' ':
					this._Space();
					break;
				case '\t':
					this._Tab();
					break;
				case '\n':
					this._NewLine();
					break;
				default:
					if (char.IsWhiteSpace(c)) return;

					this._UpdateCursor();

					if (this._CheckCursor())
					{
						this.SetAt(c, this.CursorPosition);
						this.SetColorAt(this.TextColor, this.CursorPosition);
						this._Space();
					}
					break;
			}
		}

		private void _Space()
		{
			this.CursorPosition.X += this.SpaceLenght;

			this._UpdateCursor();
		}
		private void _Tab()
		{
			this.CursorPosition.X += this._CalcTabLen(this.CursorPosition.X);

			this._UpdateCursor();
		}
		private void _NewLine()
		{
			this.CursorPosition.X = 0;
			this.CursorPosition.Y += this.NewLineLenght;

			this._UpdateCursor();
		}

		private bool _CheckCursor() => !(this.CursorPosition.X < 0 || this.CursorPosition.Y < 0 || this.CursorPosition.X >= this.GetSize().X || this.CursorPosition.Y >= this.GetSize().Y);
		private void _UpdateCursor()
		{
			if (!this.TextWrap) return;

			if (this.CursorPosition.X >= this.GetSize().X)
			{
				this.CursorPosition.X = 0;
				this.CursorPosition.Y += 1;
			}

			if (this.CursorPosition.Y >= this.GetSize().Y)
			{
				this.CursorPosition.X = 0;
				this.CursorPosition.Y = 0;
			}

		}

		private int _CalcTabLen(int xPos)
		{
			return this.TabLenght - (xPos % this.TabLenght);
		}

		private void _InitClass()
		{
			this.TextBreak = false;
			this.TextWrap = true;

			this.TabLenght = 4;
			this.SpaceLenght = 1;
			this.NewLineLenght = 1;

			this.CursorPosition = new Vec2i();

			this.TextColor = ConsoleColor.Gray;
		}
	}
}
