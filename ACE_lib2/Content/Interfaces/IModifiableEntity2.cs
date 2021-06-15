using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ACE_2D_Base.Regions;
using ACE_2D_Base.Vectors;
using ACE_lib2.Args;

namespace ACE_lib2.Content.Interfaces
{
	public interface IModifiableEntity2 : IEntity2, ITextContent, IColorContent
	{
		event EventHandler<OnChangedArgs<Vec2d>> OnSizeChanged;

		void SetSize(int xSize, int ySize);
		void SetSize(Vec2i Size);

		void SetRegion(Reg2 Region);

	}
}
