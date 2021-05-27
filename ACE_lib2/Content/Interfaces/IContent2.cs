using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ACE_2D_Base.Regions;
using ACE_2D_Base.Vectors;
using ACE_lib2.Args;
using ACE_lib2.Content.Canvases;

namespace ACE_lib2.Content.Interfaces
{
	public interface IContent2 : IModifiable2<char>
	{
		Reg2 Region { get; set; }

		event EventHandler<OnChangedArgs<Reg2>> OnRegionChanged;

		event EventHandler OnColorsCleared;

		event EventHandler OnPreAppend;
		event EventHandler OnPostAppend;

		void SetColorAt(ConsoleColor Col, int x, int y);
		void SetColorAt(ConsoleColor Col, Vec2i Index);

		ConsoleColor GetColorAt(int x, int y);
		ConsoleColor GetColorAt(Vec2i Index);

		void ClearColors(ConsoleColor ClearWith);

		Vec2d GetPosition();

		void AppendTo(IModifiable2<char> Parent);
		void AppendTo(IContent2 Parent);
		void DrawColorsTo(Canvas2 Can);
	}
}
