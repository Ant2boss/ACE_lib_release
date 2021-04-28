using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ACE_lib.Args;
using ACE_lib.Vectors;

namespace ACE_lib.Content
{
	public interface IModifiable2<T> : ISize2_readonly
	{
		T this[int x, int y] { get;set; }
		T this[Vec2i Index] { get;set; }

		event EventHandler<OnModifiedArgs<T>> OnContentModified;
		event EventHandler OnContentCleared;

		void SetAt(T el, int x, int y);
		void SetAt(T el, Vec2i Index);

		T GetAt(int x, int y);
		T GetAt(Vec2i Index);

		void Clear(T ClearWith);
	}
}
