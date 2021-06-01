using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACE_lib2.Content.Props
{
	public class BorderProps
	{
		public enum BorderSide { Top = 0, Left = 1, Bottom = 2, Right = 3 }

		public BorderProps()
		{
			this.SetBorderAtSide('-', BorderSide.Top);
			this.SetBorderAtSide('-', BorderSide.Bottom);

			this.SetBorderAtSide('|', BorderSide.Left);
			this.SetBorderAtSide('|', BorderSide.Right);

			this.SetAllColorSidesTo(ConsoleColor.Gray);

			this.Visible = true;
		}

		public void SetBorderAtSide(char el, BorderSide Side)
		{
			this._SideBuffer[this._IndexOf(Side)] = el;
		}
		public void SetBorderAtCorner(char el, BorderSide TopBottom, BorderSide LeftRight)
		{
			this._SideBuffer[this._IndexOf(TopBottom, LeftRight)] = el;
		}

		public char GetBorderAtSide(BorderSide Side) => this._SideBuffer[this._IndexOf(Side)];
		public char GetBorderAtCorner(BorderSide TopBottom, BorderSide LeftRight) => this._SideBuffer[this._IndexOf(TopBottom, LeftRight)];

		public void SetAllBorderTo(char el) => this._Fill(el, 0, 8);
		public void SetAllBorderSidesTo(char el) => this._Fill(el, 0, 4);
		public void SetAllBorderCornersTo(char el) => this._Fill(el, 4, 8);


		public void SetColorsAtSide(ConsoleColor Col, BorderSide Side)
		{
			this._ColorBuffer[this._IndexOf(Side)] = Col;
		}
		public void SetColorsAtCorner(ConsoleColor Col, BorderSide TopBottom, BorderSide LeftRight)
		{
			this._ColorBuffer[this._IndexOf(TopBottom, LeftRight)] = Col;
		}

		public ConsoleColor GetColorAtSide(BorderSide Side) => this._ColorBuffer[this._IndexOf(Side)];
		public ConsoleColor GetColorAtCorner(BorderSide TopBottom, BorderSide LeftRight) => this._ColorBuffer[this._IndexOf(TopBottom, LeftRight)];

		public void SetAllColorTo(ConsoleColor el) => this._FillColors(el, 0, 8);
		public void SetAllColorSidesTo(ConsoleColor el) => this._FillColors(el, 0, 4);
		public void SetAllColorCornersTo(ConsoleColor el) => this._FillColors(el, 4, 8);

		public bool Visible { get; set; }

		private void _Fill(char el, int start, int end)
		{
			for (int i = start; i < end; ++i)
			{
				this._SideBuffer[i] = el;
			}
		}
		private void _FillColors(ConsoleColor el, int start, int end)
		{
			for (int i = start; i < end; ++i)
			{
				this._ColorBuffer[i] = el;
			}
		}

		private int _IndexOf(BorderSide side) => (int)side;
		private int _IndexOf(BorderSide topBottom, BorderSide leftRight)
		{
			switch (topBottom)
			{
				case BorderSide.Top:
					{
						switch (leftRight)
						{
							case BorderSide.Left: return 4;
							case BorderSide.Right: return 5;
							default: throw new Exception("No such side!");
						}
					}
				case BorderSide.Bottom:
					{
						switch (leftRight)
						{
							case BorderSide.Left: return 6;
							case BorderSide.Right: return 7;
							default: throw new Exception("No such side!");
						}
					}
				default: throw new Exception("No such side!");
			}
		}

		private char[] _SideBuffer = new char[8];
		private ConsoleColor[] _ColorBuffer = new ConsoleColor[8];
	}
}
