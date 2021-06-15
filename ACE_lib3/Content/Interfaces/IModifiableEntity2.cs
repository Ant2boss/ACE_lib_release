using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ACE_2D_Base.Vectors;

namespace ACE_lib3.Content.Interfaces
{
	public interface IModifiableEntity2 : ITextModifiable2, IColorModifiable2
	{
		void SetSize(int xSize, int ySize);
		void SetSize(Vec2i Size);
	}
}
