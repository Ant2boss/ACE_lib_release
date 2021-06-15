using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ACE_2D_Base.Vectors;

namespace ACE_lib2.Content.Interfaces
{
	public interface ITextContent : IContent2
	{
		void SetAt(char el, int x, int y);
		void SetAt(char el, Vec2i Index);

		char GetAt(int x, int y);
		char GetAt(Vec2i Index);

		event EventHandler OnTextClear;

		void Clear(char ClearWith = ' ');
	}
}
