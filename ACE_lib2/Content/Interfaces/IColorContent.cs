using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ACE_2D_Base.Vectors;

namespace ACE_lib2.Content.Interfaces
{
	public interface IColorContent : IContent2
	{
		void SetColorAt(ConsoleColor el, int x, int y);
		void SetColorAt(ConsoleColor el, Vec2i Index);

		ConsoleColor GetColorAt(int x, int y);
		ConsoleColor GetColorAt(Vec2i Index);

		event EventHandler OnColorClear;

		void ClearColors(ConsoleColor ClearWith = ConsoleColor.Gray);
	}
}
