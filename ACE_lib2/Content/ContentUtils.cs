using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ACE_2D_Base.Vectors;
using ACE_lib2.Content.Interfaces;

namespace ACE_lib2.Content
{
	public static class ContentUtils
	{
		private static bool _InBounds(Vec2i ParentSize, int x, int y)
		{
			return !(x < 0 || y < 0 || x >= ParentSize.X || y >= ParentSize.Y);
		}

		private static void _LineFunction(Action<int, int> DecoratorAction, Vec2i ParentSize, double x1, double y1, double x2, double y2)
		{
			int xLoc;
			int yLoc;

			if ((x2 - x1) == 0)
			{
				int yStraight = (int)Math.Min(y1, y2);
				int yStraightEnd = (int)Math.Max(y1, y2);

				for (int y = yStraight; y <= yStraightEnd; ++y)
				{
					xLoc = (int)Math.Round(x1);
					yLoc = y;

					if (_InBounds(ParentSize, xLoc, yLoc))
					{
						DecoratorAction.Invoke(xLoc, yLoc);
					}

				}

				return;
			}

			double K = (y2 - y1) / (x2 - x1);

			int xStart = (int)Math.Min(x1, x2);
			int xEnd = (int)Math.Max(x1, x2);

			for (int x = xStart; x <= xEnd; ++x)
			{
				double yTemp = K * (x - x1) + y1;

				xLoc = x;
				yLoc = (int)Math.Round(yTemp);

				if (_InBounds(ParentSize, xLoc, yLoc))
				{
					DecoratorAction.Invoke(xLoc, yLoc);
				}

			}

			if (K == 0)
			{
				return;
			}

			int yStart = (int)Math.Min(y1, y2);
			int yEnd = (int)Math.Max(y1, y2);

			for (int y = yStart; y <= yEnd; ++y)
			{
				double xTemp = (y - y1 + K * x1) / K;

				xLoc = (int)Math.Round(xTemp);
				yLoc = y;

				if (_InBounds(ParentSize, xLoc, yLoc))
				{
					DecoratorAction.Invoke(xLoc, yLoc);
				}
			}
		}
		private static void _ElipseFunction(Action<int, int> OutlineDecorator, Action<int, int> FillDecorator, Vec2i ParentSize, double x1, double y1, double x2, double y2)
		{
			Vec2d Start = new Vec2d(Math.Min(x1, x2), Math.Min(y1, y2));
			Vec2d End = new Vec2d(Math.Max(x1, x2), Math.Max(y1, y2));

			Vec2d Size = End - Start;

			double P = End.X - ((Size.X) / 2.0);
			double Q = End.Y - ((Size.Y) / 2.0);

			double A = Size.X / 2.0;
			double B = Size.Y / 2.0;

			int xLoc;
			int yLoc;

			for (int y = (int)Start.Y; y <= (int)End.Y; ++y)
			{
				double xCalc1 = Math.Sqrt(Math.Pow(A, 2) * ((Math.Pow(B, 2) - Math.Pow(y - Q, 2)) / Math.Pow(B, 2))) + P;
				double xCalc2 = -1 * (Math.Sqrt(Math.Pow(A, 2) * ((Math.Pow(B, 2) - Math.Pow(y - Q, 2)) / Math.Pow(B, 2)))) + P;

				if (FillDecorator != null)
				{
					_LineFunction(FillDecorator, ParentSize, Math.Round(xCalc1), y, Math.Round(xCalc2), y);
				}

				yLoc = y;
				xLoc = (int)Math.Round(xCalc1);

				if (_InBounds(ParentSize, xLoc, yLoc))
				{
					OutlineDecorator.Invoke(xLoc, yLoc);
				}

				xLoc = (int)Math.Round(xCalc2);

				if (_InBounds(ParentSize, xLoc, yLoc))
				{
					OutlineDecorator.Invoke(xLoc, yLoc);
				}
			}

			for (int x = (int)Start.X; x <= (int)End.X; ++x)
			{
				double yCalc1 = Math.Sqrt(Math.Pow(B, 2) * ((Math.Pow(A, 2) - Math.Pow(x - P, 2)) / Math.Pow(A, 2))) + Q;
				double yCalc2 = -1 * (Math.Sqrt(Math.Pow(B, 2) * ((Math.Pow(A, 2) - Math.Pow(x - P, 2)) / Math.Pow(A, 2)))) + Q;

				xLoc = x;
				yLoc = (int)Math.Round(yCalc1);

				if (_InBounds(ParentSize, xLoc, yLoc))
				{
					OutlineDecorator.Invoke(xLoc, yLoc);
				}

				yLoc = (int)Math.Round(yCalc2);

				if (_InBounds(ParentSize, xLoc, yLoc))
				{
					OutlineDecorator.Invoke(xLoc, yLoc);
				}
			}

		}
		private static void _RectFunction(Action<int, int> OutlineDecorator, Action<int, int> FillDecorator, Vec2i ParentSize, double x1, double y1, double x2, double y2)
		{
			Vec2i Start = (new Vec2d(Math.Min(x1, x2), Math.Min(y1, y2))).CloneAsVec2i();
			Vec2i End = (new Vec2d(Math.Max(x1, x2), Math.Max(y1, y2))).CloneAsVec2i();

			for (int y = Start.Y; y <= End.Y; ++y)
			{
				for (int x = Start.X; x <= End.X; ++x)
				{
					if (_InBounds(ParentSize, x, y))
					{
						if (x == Start.X || y == Start.Y || x == End.X || y == End.Y)
						{
							OutlineDecorator?.Invoke(x, y);
						}
						else
						{
							FillDecorator?.Invoke(x, y);
						}
					}
				}
			}
		}

		//---------------------------------
		// [Line]
		//---------------------------------
		public static void AppendLine(IModifiable2<char> AppendTo, char LineElement, double x1, double y1, double x2, double y2)
		{
			_LineFunction((x, y) => AppendTo.SetAt(LineElement, x, y), AppendTo.GetSize(), x1, y1, x2, y2);
		}
		public static void AppendLine(IModifiable2<char> AppendTo, char LineElement, Vec2i Start, Vec2i End) => AppendLine(AppendTo, LineElement, Start.X, Start.Y, End.X, End.Y);
		public static void AppendLine(IModifiable2<char> AppendTo, char LineElement, Vec2d Start, Vec2d End) => AppendLine(AppendTo, LineElement, Start.X, Start.Y, End.X, End.Y);

		public static void AppendLineWithColor(IContent2 AppendTo, char LineElement, ConsoleColor LineColor, double x1, double y1, double x2, double y2)
		{
			_LineFunction((x, y) =>
			{
				AppendTo.SetAt(LineElement, x, y);
				AppendTo.SetColorAt(LineColor, x, y);
			}, AppendTo.GetSize(), x1, y1, x2, y2);
		}
		public static void AppendLineWithColor(IContent2 AppendTo, char LineElement, ConsoleColor LineColor, Vec2i Start, Vec2i End) => AppendLineWithColor(AppendTo, LineElement, LineColor, Start.X, Start.Y, End.X, End.Y);
		public static void AppendLineWithColor(IContent2 AppendTo, char LineElement, ConsoleColor LineColor, Vec2d Start, Vec2d End) => AppendLineWithColor(AppendTo, LineElement, LineColor, Start.X, Start.Y, End.X, End.Y);

		//---------------------------------
		// [Elipse]
		//---------------------------------
		public static void AppendElipse(IModifiable2<char> AppendTo, char ElipseElement, double x1, double y1, double x2, double y2)
		{
			_ElipseFunction((x, y) => AppendTo.SetAt(ElipseElement, x, y), null, AppendTo.GetSize(), x1, y1, x2, y2);
		}
		public static void AppendElipse(IModifiable2<char> AppendTo, char ElipseElement, Vec2i Start, Vec2i End) => AppendElipse(AppendTo, ElipseElement, Start.X, Start.Y, End.X, End.Y);
		public static void AppendElipse(IModifiable2<char> AppendTo, char ElipseElement, Vec2d Start, Vec2d End) => AppendElipse(AppendTo, ElipseElement, Start.X, Start.Y, End.X, End.Y);
		
		public static void AppendElipseWithColor(IContent2 AppendTo, char ElipseElement, ConsoleColor ElipseColor, double x1, double y1, double x2, double y2)
		{
			_ElipseFunction((x, y) =>
			{
				AppendTo.SetAt(ElipseElement, x, y);
				AppendTo.SetColorAt(ElipseColor, x, y);
			}, null, AppendTo.GetSize(), x1, y1, x2, y2);
		}
		public static void AppendElipseWithColor(IContent2 AppendTo, char ElipseElement, ConsoleColor ElipseColor, Vec2i Start, Vec2i End) => AppendElipseWithColor(AppendTo, ElipseElement, ElipseColor, Start.X, Start.Y, End.X, End.Y);
		public static void AppendElipseWithColor(IContent2 AppendTo, char ElipseElement, ConsoleColor ElipseColor, Vec2d Start, Vec2d End) => AppendElipseWithColor(AppendTo, ElipseElement, ElipseColor, Start.X, Start.Y, End.X, End.Y);

		public static void AppendElipseAndFill(IModifiable2<char> AppendTo, char ElipseOutline, char ElipseFill, double x1, double y1, double x2, double y2)
		{
			_ElipseFunction((x, y) => AppendTo.SetAt(ElipseOutline, x, y), (x, y) => AppendTo.SetAt(ElipseFill, x, y), AppendTo.GetSize(), x1, y1, x2, y2);
		}
		public static void AppendElipseAndFill(IModifiable2<char> AppendTo, char ElipseOutline, char ElipseFill, Vec2d Start, Vec2d End) => AppendElipseAndFill(AppendTo, ElipseOutline, ElipseFill, Start.X, Start.Y, End.X, End.Y);
		public static void AppendElipseAndFill(IModifiable2<char> AppendTo, char ElipseOutline, char ElipseFill, Vec2i Start, Vec2i End) => AppendElipseAndFill(AppendTo, ElipseOutline, ElipseFill, Start.X, Start.Y, End.X, End.Y);
		
		public static void AppendElipseAndFill(IModifiable2<char> AppendTo, char ElipseElement, double x1, double y1, double x2, double y2) => AppendElipseAndFill(AppendTo, ElipseElement, ElipseElement, x1, y1, x2, y2);
		public static void AppendElipseAndFill(IModifiable2<char> AppendTo, char ElipseElement, Vec2i Start, Vec2i End) => AppendElipseAndFill(AppendTo, ElipseElement, Start.X, Start.Y, End.X, End.Y);
		public static void AppendElipseAndFill(IModifiable2<char> AppendTo, char ElipseElement, Vec2d Start, Vec2d End) => AppendElipseAndFill(AppendTo, ElipseElement, Start.X, Start.Y, End.X, End.Y);

		public static void AppendElipseAndFillWithColor(IContent2 Con, char ElipseOutline, char ElipseFill, ConsoleColor OutlineColor, ConsoleColor FillColor, double x1, double y1, double x2, double y2)
		{ 
			_ElipseFunction((x, y) => {
				Con.SetAt(ElipseOutline, x, y);
				Con.SetColorAt(OutlineColor, x, y);
				}, 
				(x, y) => {
					Con.SetAt(ElipseFill, x, y);
					Con.SetColorAt(FillColor, x, y);
				}, Con.GetSize(), x1, y1, x2, y2);
		}
		public static void AppendElipseAndFillWithColor(IContent2 Con, char ElipseOutline, char ElipseFill, ConsoleColor OutlineColor, ConsoleColor FillColor, Vec2i Start, Vec2i End) => AppendElipseAndFillWithColor(Con, ElipseOutline, ElipseFill, OutlineColor, FillColor, Start.X, Start.Y, End.X, End.Y);
		public static void AppendElipseAndFillWithColor(IContent2 Con, char ElipseOutline, char ElipseFill, ConsoleColor OutlineColor, ConsoleColor FillColor, Vec2d Start, Vec2d End) => AppendElipseAndFillWithColor(Con, ElipseOutline, ElipseFill, OutlineColor, FillColor, Start.X, Start.Y, End.X, End.Y);
		
		public static void AppendElipseAndFillWithColor(IContent2 AppendTo, char ElipseElement, ConsoleColor ElipseColor, double x1, double y1, double x2, double y2) => AppendElipseAndFillWithColor(AppendTo, ElipseElement, ElipseElement, ElipseColor, ElipseColor, x1, y1, x2, y2);
		public static void AppendElipseAndFillWithColor(IContent2 AppendTo, char ElipseElement, ConsoleColor ElipseColor, Vec2i Start, Vec2i End) => AppendElipseAndFillWithColor(AppendTo, ElipseElement, ElipseColor, Start.X, Start.Y, End.X, End.Y);
		public static void AppendElipseAndFillWithColor(IContent2 AppendTo, char ElipseElement, ConsoleColor ElipseColor, Vec2d Start, Vec2d End) => AppendElipseAndFillWithColor(AppendTo, ElipseElement, ElipseColor, Start.X, Start.Y, End.X, End.Y);

		//---------------------------------
		// [Rectangle]
		//---------------------------------
		public static void AppendRectangle(IModifiable2<char> AppendTo, char RectElement, double x1, double y1, double x2, double y2)
		{
			_RectFunction((x, y) => { AppendTo.SetAt(RectElement, x, y); }, null, AppendTo.GetSize(), x1, y1, x2, y2);
		}
		public static void AppendRectangle(IModifiable2<char> AppendTo, char RectElement, Vec2i Start, Vec2i End) => AppendRectangle(AppendTo, RectElement, Start.X, Start.Y, End.X, End.Y);
		public static void AppendRectangle(IModifiable2<char> AppendTo, char RectElement, Vec2d Start, Vec2d End) => AppendRectangle(AppendTo, RectElement, Start.X, Start.Y, End.X, End.Y);
		
		public static void AppendRectangleAndFill(IModifiable2<char> AppendTo, char RectOutline, char RectFill, double x1, double y1, double x2, double y2)
		{
			_RectFunction((x, y) => { AppendTo.SetAt(RectOutline, x, y); }, (x, y) => { AppendTo.SetAt(RectFill, x, y); }, AppendTo.GetSize(), x1, y1, x2, y2);
		}
		public static void AppendRectangleAndFill(IModifiable2<char> AppendTo, char RectOutline, char RectFill, Vec2i Start, Vec2i End) => AppendRectangleAndFill(AppendTo, RectOutline, RectFill, Start.X, Start.Y, End.X, End.Y);
		public static void AppendRectangleAndFill(IModifiable2<char> AppendTo, char RectOutline, char RectFill, Vec2d Start, Vec2d End) => AppendRectangleAndFill(AppendTo, RectOutline, RectFill, Start.X, Start.Y, End.X, End.Y);
		
		public static void AppendRectangleAndFill(IModifiable2<char> AppendTo, char RectElement, double x1, double y1, double x2, double y2) => AppendRectangleAndFill(AppendTo, RectElement, RectElement, x1, y1, x2, y2);
		public static void AppendRectangleAndFill(IModifiable2<char> AppendTo, char RectElement, Vec2i Start, Vec2i End) => AppendRectangleAndFill(AppendTo, RectElement, RectElement, Start.X, Start.Y, End.X, End.Y);
		public static void AppendRectangleAndFill(IModifiable2<char> AppendTo, char RectElement, Vec2d Start, Vec2d End) => AppendRectangleAndFill(AppendTo, RectElement, RectElement, Start.X, Start.Y, End.X, End.Y);

		public static void AppendRectangleWithColor(IContent2 AppendTo, char RectElement, ConsoleColor RectColor, double x1, double y1, double x2, double y2)
		{
			_RectFunction((x, y) => { 
				AppendTo.SetAt(RectElement, x, y);
				AppendTo.SetColorAt(RectColor, x, y);
			}, null, AppendTo.GetSize(), x1, y1, x2, y2);
		}
		public static void AppendRectangleWithColor(IContent2 AppendTo, char RectElement, ConsoleColor Col, Vec2i Start, Vec2i End) => AppendRectangleWithColor(AppendTo, RectElement, Col, Start.X, Start.Y, End.X, End.Y);
		public static void AppendRectangleWithColor(IContent2 AppendTo, char RectElement, ConsoleColor Col, Vec2d Start, Vec2d End) => AppendRectangleWithColor(AppendTo, RectElement, Col, Start.X, Start.Y, End.X, End.Y);

		public static void AppendRectangleAndFillWithColor(IContent2 AppendTo, char RectOutline, char RectFill, ConsoleColor OutlineCol, ConsoleColor FillCol, double x1, double y1, double x2, double y2)
		{
			_RectFunction((x, y) => { 
				AppendTo.SetAt(RectOutline, x, y);
				AppendTo.SetColorAt(OutlineCol, x, y);
			}, 
			(x, y) => { 
				AppendTo.SetAt(RectFill, x, y);
				AppendTo.SetColorAt(FillCol, x, y);
			}, AppendTo.GetSize(), x1, y1, x2, y2);
		}
		public static void AppendRectangleAndFillWithColor(IContent2 AppendTo, char RectOutline, char RectFill, ConsoleColor OutlineCol, ConsoleColor FillCol, Vec2i Start, Vec2i End) => AppendRectangleAndFillWithColor(AppendTo, RectOutline, RectFill, OutlineCol, FillCol, Start.X, Start.Y, End.X, End.Y);
		public static void AppendRectangleAndFillWithColor(IContent2 AppendTo, char RectOutline, char RectFill, ConsoleColor OutlineCol, ConsoleColor FillCol, Vec2d Start, Vec2d End) => AppendRectangleAndFillWithColor(AppendTo, RectOutline, RectFill, OutlineCol, FillCol, Start.X, Start.Y, End.X, End.Y);

		public static void AppendRectangleAndFillWithColor(IContent2 AppendTo, char RectElement, ConsoleColor RectCol, double x1, double y1, double x2, double y2) => AppendRectangleAndFillWithColor(AppendTo, RectElement, RectElement, RectCol, RectCol, x1, y1, x2, y2);
		public static void AppendRectangleAndFillWithColor(IContent2 AppendTo, char RectElement, ConsoleColor RectCol, Vec2i Start, Vec2i End) => AppendRectangleAndFillWithColor(AppendTo, RectElement, RectElement, RectCol,RectCol, Start.X, Start.Y, End.X, End.Y);
		public static void AppendRectangleAndFillWithColor(IContent2 AppendTo, char RectElement, ConsoleColor RectCol, Vec2d Start, Vec2d End) => AppendRectangleAndFillWithColor(AppendTo, RectElement, RectElement, RectCol, RectCol, Start.X, Start.Y, End.X, End.Y);

		//---------------------------------
		// [Content mainpulation]
		//---------------------------------
		public static void CopyContentTo(IModifiable2<char> From, IModifiable2<char> To)
		{
			int xBound = Math.Min(From.GetSize().X, To.GetSize().X);
			int yBound = Math.Min(From.GetSize().Y, To.GetSize().Y);

			for (int y = 0; y < yBound; ++y)
			{
				for (int x = 0; x < xBound; ++x)
				{
					To.SetAt(From.GetAt(x, y), x, y);
				}
			}
		}
		public static void CopyContentWithColorTo(IContent2 From, IContent2 To)
		{
			int xBound = Math.Min(From.GetSize().X, To.GetSize().X);
			int yBound = Math.Min(From.GetSize().Y, To.GetSize().Y);

			for (int y = 0; y < yBound; ++y)
			{
				for (int x = 0; x < xBound; ++x)
				{
					To.SetAt(From.GetAt(x, y), x, y);
					To.SetColorAt(From.GetColorAt(x, y), x, y);
				}
			}
		}
	}
}
