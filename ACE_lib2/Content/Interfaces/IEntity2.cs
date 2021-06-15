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
	public interface IEntity2
	{
		event EventHandler<OnChangedArgs<Vec2d>> OnPositionChanged;

		void SetPosition(double xPos, double yPos);
		void SetPosition(Vec2i Position);
		void SetPosition(Vec2d Position);

		Reg2 GetRegion();

		event EventHandler OnPreAppend;
		event EventHandler OnPostAppend;

		void AppendTo(ITextContent ParentContent);
		void AppendColorsTo(IColorContent ParentContent);

	}
}
