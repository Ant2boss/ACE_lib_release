using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ACE_2D_Base.Vectors;

namespace ACE_lib2.Content.Interfaces
{
	public interface IMatrix2<T>
	{
		void SetAt(T el, int x, int y);
		void SetAt(T el, Vec2i Index);

		T GetAt(int x, int y);
		T GetAt(Vec2i Index);

		void Clear(T ClearWith);

		Vec2i GetSize();
	}
}
