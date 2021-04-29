using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ACE_lib.Vectors;
using ACE_lib.Content.Canvases;
using ACE_lib.Args;

namespace ACE_lib.Content
{
	public interface IContent2 : IModifiable2<char>, IReg2
	{
		event EventHandler<OnModifiedArgs<ConsoleColor>> OnColorsModified;
		event EventHandler OnColorsCleared;

		event EventHandler OnPreAppend;
		event EventHandler OnPostAppend;

		event EventHandler OnPreColorDraw;
		event EventHandler OnPostColorDraw;

		void SetColorAt(ConsoleColor Col, int x, int y);
		void SetColorAt(ConsoleColor Col, Vec2i Index);

		ConsoleColor GetColorAt(int x, int y);
		ConsoleColor GetColorAt(Vec2i Index);

		void ClearColors(ConsoleColor ClearWith);

		void AppendTo(IContent2 Content);
		void AppendTo(IModifiable2<char> Content);

		void DrawColorsToCan(Can2 Canvas);
	}
}
