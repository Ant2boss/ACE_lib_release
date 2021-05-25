using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ACE_2D_Base.Regions;
using ACE_2D_Base.Vectors;

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

		public bool TextWrap { get; set; }
		public bool TextBreak { get; set; }

		public int TabLenght { get; set; }
		public int SpaceLenght { get; set; }
		public int NewLineLenght { get; set; }

		public Vec2i CursorPosition { get; set; }

		public ConsoleColor TextColor { get; set; }

		public void Write(string str)
		{ 
			
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
