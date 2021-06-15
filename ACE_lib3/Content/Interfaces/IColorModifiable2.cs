using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ACE_2D_Base.Vectors;

namespace ACE_lib3.Content.Interfaces
{
	public interface IColorModifiable2 : IModifyIndexCheckers
	{
		void SetColorAt(ConsoleColor el, int x, int y);
		void SetColorAt(ConsoleColor el, Vec2i Index);

		ConsoleColor GetColorAt(int x, int y);
		ConsoleColor GetColorAt(Vec2i Index);

		event EventHandler OnColorsCleared;

		void ClearColor(ConsoleColor ClearWith = ConsoleColor.Gray);
	}
}
