using System;
using System.Collections.Generic;
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

		public static void AppendLine(IModifiable2<char> Con, char LineElement, double x1, double y1, double x2, double y2)
		{
			_LineFunction((x, y) => Con.SetAt(LineElement, x, y), Con.GetSize(), x1, y1, x2, y2);
		}
		public static void AppendLine(IContent2 Con, char LineElement, ConsoleColor LineColor, double x1, double y1, double x2, double y2)
		{
			_LineFunction((x, y) =>
			{
				Con.SetAt(LineElement, x, y);
				Con.SetColorAt(LineColor, x, y);
			}, Con.GetSize(), x1, y1, x2, y2);
		}

		public static void AppendLine(IModifiable2<char> Con, char LineElement, Vec2i Start, Vec2i End) => AppendLine(Con, LineElement, Start.X, Start.Y, End.X, End.Y);
		public static void AppendLine(IModifiable2<char> Con, char LineElement, Vec2d Start, Vec2d End) => AppendLine(Con, LineElement, Start.X, Start.Y, End.X, End.Y);
		public static void AppendLine(IContent2 Con, char LineElement, ConsoleColor LineColor, Vec2i Start, Vec2i End) => AppendLine(Con, LineElement, LineColor, Start.X, Start.Y, End.X, End.Y);
		public static void AppendLine(IContent2 Con, char LineElement, ConsoleColor LineColor, Vec2d Start, Vec2d End) => AppendLine(Con, LineElement, LineColor, Start.X, Start.Y, End.X, End.Y);



		public static void AppendElipse(IModifiable2<char> Con, char ElipseElement, double x1, double y1, double x2, double y2)
		{
			_ElipseFunction((x, y) => Con.SetAt(ElipseElement, x, y), null, Con.GetSize(), x1, y1, x2, y2);
		}
		public static void AppendElipse(IContent2 Con, char ElipseElement, ConsoleColor ElipseColor, double x1, double y1, double x2, double y2)
		{
			_ElipseFunction((x, y) =>
			{
				Con.SetAt(ElipseElement, x, y);
				Con.SetColorAt(ElipseColor, x, y);
			}, null, Con.GetSize(), x1, y1, x2, y2);
		}

		public static void AppendElipse(IModifiable2<char> Con, char ElipseElement, Vec2i Start, Vec2i End) => AppendElipse(Con, ElipseElement, Start.X, Start.Y, End.X, End.Y);
		public static void AppendElipse(IModifiable2<char> Con, char ElipseElement, Vec2d Start, Vec2d End) => AppendElipse(Con, ElipseElement, Start.X, Start.Y, End.X, End.Y);
		public static void AppendElipse(IContent2 Con, char ElipseElement, ConsoleColor ElipseColor, Vec2i Start, Vec2i End) => AppendElipse(Con, ElipseElement, ElipseColor, Start.X, Start.Y, End.X, End.Y);
		public static void AppendElipse(IContent2 Con, char ElipseElement, ConsoleColor ElipseColor, Vec2d Start, Vec2d End) => AppendElipse(Con, ElipseElement, ElipseColor, Start.X, Start.Y, End.X, End.Y);

		public static void AppendElipseAndFill(IModifiable2<char> Con, char ElipseOutline, char ElipseFill, double x1, double y1, double x2, double y2)
		{
			_ElipseFunction((x, y) => Con.SetAt(ElipseOutline, x, y), (x, y) => Con.SetAt(ElipseFill, x, y), Con.GetSize(), x1, y1, x2, y2);
		}
		public static void AppendElipseAndFill(IContent2 Con, char ElipseOutline, char ElipseFill, ConsoleColor OutlineColor, ConsoleColor FillColor, double x1, double y1, double x2, double y2)
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

		public static void AppendElipseAndFill(IModifiable2<char> Con, char ElipseElement, double x1, double y1, double x2, double y2) => AppendElipseAndFill(Con, ElipseElement, ElipseElement, x1, y1, x2, y2);
		public static void AppendElipseAndFill(IContent2 Con, char ElipseElement, ConsoleColor ElipseColor, double x1, double y1, double x2, double y2) => AppendElipseAndFill(Con, ElipseElement, ElipseElement, ElipseColor, ElipseColor, x1, y1, x2, y2);

		public static void AppendElipseAndFill(IModifiable2<char> Con, char ElipseElement, Vec2i Start, Vec2i End) => AppendElipseAndFill(Con, ElipseElement, Start.X, Start.Y, End.X, End.Y);
		public static void AppendElipseAndFill(IModifiable2<char> Con, char ElipseElement, Vec2d Start, Vec2d End) => AppendElipseAndFill(Con, ElipseElement, Start.X, Start.Y, End.X, End.Y);
		public static void AppendElipseAndFill(IContent2 Con, char ElipseElement, ConsoleColor ElipseColor, Vec2i Start, Vec2i End) => AppendElipseAndFill(Con, ElipseElement, ElipseColor, Start.X, Start.Y, End.X, End.Y);
		public static void AppendElipseAndFill(IContent2 Con, char ElipseElement, ConsoleColor ElipseColor, Vec2d Start, Vec2d End) => AppendElipseAndFill(Con, ElipseElement, ElipseColor, Start.X, Start.Y, End.X, End.Y);
		public static void AppendElipseAndFill(IModifiable2<char> Con, char ElipseOutline, char ElipseFill, Vec2i Start, Vec2i End) => AppendElipseAndFill(Con, ElipseOutline, ElipseFill, Start.X, Start.Y, End.X, End.Y);
		public static void AppendElipseAndFill(IModifiable2<char> Con, char ElipseOutline, char ElipseFill, Vec2d Start, Vec2d End) => AppendElipseAndFill(Con, ElipseOutline, ElipseFill, Start.X, Start.Y, End.X, End.Y);
		public static void AppendElipseAndFill(IContent2 Con, char ElipseOutline, char ElipseFill, ConsoleColor OutlineColor, ConsoleColor FillColor, Vec2i Start, Vec2i End) => AppendElipseAndFill(Con, ElipseOutline, ElipseFill, OutlineColor, FillColor, Start.X, Start.Y, End.X, End.Y);
		public static void AppendElipseAndFill(IContent2 Con, char ElipseOutline, char ElipseFill, ConsoleColor OutlineColor, ConsoleColor FillColor, Vec2d Start, Vec2d End) => AppendElipseAndFill(Con, ElipseOutline, ElipseFill, OutlineColor, FillColor, Start.X, Start.Y, End.X, End.Y);
	}
}
