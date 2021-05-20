using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ACE_lib.Content.Canvases;
using ACE_2D_Base.Regions;
using ACE_2D_Base.Vectors;

namespace ACE_lib.Content.Entities
{
	public class TextEnt2 : Ent2
	{
		public TextEnt2() => this.pInitClass();
		public TextEnt2(IConnectable2 Parent) : base(Parent) => this.pInitClass();
		public TextEnt2(Vec2i Size) : base(Size) => this.pInitClass();
		public TextEnt2(Reg2 Region) : base(Region) => this.pInitClass();
		public TextEnt2(int xSize, int ySize) : base(xSize, ySize) => this.pInitClass();
		public TextEnt2(Vec2i Size, Vec2i Position) : base(Size, Position) => this.pInitClass();
		public TextEnt2(Vec2i Size, Vec2d Position) : base(Size, Position) => this.pInitClass();
		public TextEnt2(IConnectable2 Parent, Vec2i Size) : base(Parent, Size) => this.pInitClass();
		public TextEnt2(IConnectable2 Parent, Reg2 Region) : base(Parent, Region) => this.pInitClass();
		public TextEnt2(IConnectable2 Parent, int xSize, int ySize) : base(Parent, xSize, ySize) => this.pInitClass();
		public TextEnt2(IConnectable2 Parent, Vec2i Size, Vec2i Position) : base(Parent, Size, Position) => this.pInitClass();
		public TextEnt2(IConnectable2 Parent, Vec2i Size, Vec2d Position) : base(Parent, Size, Position) => this.pInitClass();
		public TextEnt2(int xSize, int ySize, double xPos, double yPos) : base(xSize, ySize, xPos, yPos) => this.pInitClass();
		public TextEnt2(IConnectable2 Parent, int xSize, int ySize, double xPos, double yPos) : base(Parent, xSize, ySize, xPos, yPos) => this.pInitClass();

		public Vec2i CursorPosition { get; set; }

		public ConsoleColor TextColor { get; set; }

		public bool TextWrap { get; set; }
		public bool TextBreak { get; set; }

		public int TabLenght { get; set; }
		public int SpaceLenght { get; set; }
		public int NewlineLenght { get; set; }

		public void Write(string str)
		{
			if (this.TextBreak)
			{
				string[] words = str.Split(' ');

				for (int i = 0; i < words.Length; ++i)
				{
					this.pMakeItFit(words[i]);
					this.pWriteStr(words[i]);

					if (this.CursorPosition.X != 0 && i != words.Length - 1) this.pWriteChar(' ');
				}

			}
			else
			{
				this.pWriteStr(str);
			}
		}

		public void Write(object obj) => this.Write(obj.ToString());
		public void WriteLine() => this.Write("\n");
		public void WriteLine(string str) => this.Write($"{str}\n");
		public void WriteLine(object obj) => this.Write($"{obj.ToString()}\n");

		public string ReadLineOn(Can2 Canvas)
		{
			string Result = Canvas.DrawAndReadLine(this.CursorPosition);
			this.WriteLine(Result);
			return Result;
		}
		public string ReadLineOnWithColors(Can2 Canvas)
		{
			string Result = Canvas.DrawColorsAndReadLine(this.TextColor, this.CursorPosition);
			this.WriteLine(Result);
			return Result;
		}

		private void pWriteStr(string str)
		{
			for (int i = 0; i < str.Length; ++i)
			{
				this.pWriteChar(str[i]);
			}
		}

		private void pWriteChar(char C)
		{
			this.pUpdateCursor();

			switch (C)
			{
				case '\n':
					this.pNewLine();
					break;
				case '\t':
					this.pTab();
					break;
				case ' ':
					this.pAdvance(this.SpaceLenght);
					break;
				default:
					if (!char.IsWhiteSpace(C) && this.pCheckCursor())
					{
						this.SetAt(C, this.CursorPosition);
						this.SetColorAt(this.TextColor, this.CursorPosition);
					}
					this.pAdvance(1);
					break;
			}
		}

		private bool pCheckCursor() => this.CursorPosition.X >= 0 && this.CursorPosition.Y >= 0 && this.CursorPosition.X < this.GetSize().X && this.CursorPosition.Y < this.GetSize().Y;
		private void pUpdateCursor()
		{
			if (!this.TextWrap) return;

			if (CursorPosition.X >= this.GetSize().X)
			{
				this.pNewLine();
			}
			if (CursorPosition.Y >= this.GetSize().Y)
			{ 
				this.CursorPosition.Init(0, 0);
			}
		}

		private void pMakeItFit(string str)
		{
			int strlen = this.pCount(str);

			if (this.CursorPosition.X + strlen > this.GetSize().X)
			{
				this.pNewLine();
			}
		}
		private int pCount(string str)
		{
			int counter = 0;
			for (int i = 0; i < str.Length; ++i)
			{
				if (str[i] == '\t')
				{
					counter += this.TabLenght - (this.CursorPosition.X % this.TabLenght);
				}
				else
				{
					counter += 1;
				}
			}
			return counter;
		}

		private void pAdvance(int AdvanceBy)
		{
			this.CursorPosition.X += AdvanceBy;
			this.pUpdateCursor();
		}
		private void pTab()
		{
			this.CursorPosition.X += this.TabLenght - (this.CursorPosition.X % this.TabLenght);
			this.pUpdateCursor();
		}
		private void pNewLine()
		{
			this.CursorPosition.X = 0;
			this.CursorPosition.Y += this.NewlineLenght;
			this.pUpdateCursor();
		}

		private void pInitClass()
		{
			this.CursorPosition = new Vec2i();

			this.TextColor = ConsoleColor.Gray;

			this.TextWrap = true;
			this.TextBreak = false;

			this.TabLenght = 4;
			this.SpaceLenght = 1;
			this.NewlineLenght = 1;
		}
	}
}
