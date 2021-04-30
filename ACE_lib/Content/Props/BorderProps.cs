using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACE_lib.Content.Props
{
	public class BorderProps
	{
		public enum BorderSide { Top = 0, Bottom = 1, Left = 2, Right = 3 };

		public BorderProps()
		{
			this.SetBorderAtSide('|', BorderSide.Left);
			this.SetBorderAtSide('|', BorderSide.Right);
			this.SetBorderAtSide('-', BorderSide.Top);
			this.SetBorderAtSide('-', BorderSide.Bottom);
		}

		public ConsoleColor BorderColor { get; set; }

		public void SetBorderAtSide(char el, BorderSide Side)
		{
			this.pBuffer[(int)Side] = el;
		}
		public void SetBorderAtCorner(char el, BorderSide TB, BorderSide LR)
		{
			this.pBuffer[this.pIndexOf(TB, LR)] = el;
		}

		public char GetBorderAtSide(BorderSide Side) => this.pBuffer[this.pIndexOf(Side)];
		public char GetBorderAtCorner(BorderSide TB, BorderSide LR) => this.pBuffer[this.pIndexOf(TB, LR)];

		public void SetSides(char el) => this.pFill(el, 0, 4);
		public void SetCorners(char el) => this.pFill(el, 4, 8);
		public void SetAll(char el) => this.pFill(el, 0, 8);

		private void pFill(char el, int Start, int End)
		{
			for (int i = Start; i < End; ++i)
			{
				this.pBuffer[i] = el;
			}
		}

		private int pIndexOf(BorderSide S) => (int)S;
		private int pIndexOf(BorderSide TB, BorderSide LR)
		{
			switch (TB)
			{
				case BorderSide.Top:
					{
						switch (LR)
						{
							case BorderSide.Left:
								return 4;
							case BorderSide.Right:
								return 5;
							default:
								throw new Exception("No such corner!");
						}
					}
				case BorderSide.Bottom:
					{
						switch (LR)
						{
							case BorderSide.Left:
								return 6;
							case BorderSide.Right:
								return 7;
							default:
								throw new Exception("No such corner!");
						}
					}
				default:
					throw new Exception("No such corner!");
			}
		}

		private char[] pBuffer = new char[BORDER_SIZE];
		private const int BORDER_SIZE = 8;

	}
}
