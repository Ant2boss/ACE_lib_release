using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ACE_lib.Vectors;
using ACE_lib.Regions;
using ACE_lib.Args;

namespace ACE_lib.Content
{
	public interface IReg2 : IReg2_readonly, ISize2, IPosition2
	{
		void SetRegion(int xSize, int ySize);
		void SetRegion(int xSize, int ySize, double xPos, double yPos);
		void SetRegion(Vec2i Size);
		void SetRegion(Vec2i Size, Vec2i Pos);
		void SetRegion(Vec2i Size, Vec2d Pos);
		void SetRegion(Reg2 Reg);

		event EventHandler<OnValueChangedArgs<Reg2>> OnRegionChanged;
	}
}
