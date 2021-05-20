using ACE_2D_Base.Vectors;
using ACE_lib2.Content.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACE_lib2.Content.Abstracts
{
	public abstract class abs_Modifiable2<T> : IModifiable2<T>
	{
		public abstract void SetAt(T el, int x, int y);
		public abstract T GetAt(int x, int y);

		public abstract Vec2i GetSize();


		public event EventHandler OnCleared;

		public T this[int x, int y]
		{
			get => this.GetAt(x, y);
			set => this.SetAt(value, x, y);
		}
		public T this[Vec2i Index]
		{
			get => this.GetAt(Index);
			set => this.SetAt(value, Index);
		}

		public void SetAt(T el, Vec2i Index) => this.SetAt(el, Index.X, Index.Y);
		public T GetAt(Vec2i Index) => this.GetAt(Index.X, Index.Y);	
		public void Clear(T ClearWith)
		{
			Vec2i size = this.GetSize();

			for(int y = 0; y < size.Y; ++y)
			{
				for (int x = 0; x < size.X; ++x)
				{
					this.SetAt(ClearWith, x, y);
				}
			}

			this.OnCleared?.Invoke(this, new EventArgs());
		}
	}
}
