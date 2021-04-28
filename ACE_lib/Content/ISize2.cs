using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ACE_lib.Vectors;
using ACE_lib.Args;

namespace ACE_lib.Content
{
	public interface ISize2 : ISize2_readonly
	{
		void SetSize(int xSize, int ySize);
		void SetSize(Vec2i Size);

		event EventHandler<OnValueChangedArgs<Vec2i>> OnSizeChanged;
	}
}
