using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ACE_lib.Vectors;
using ACE_lib.Args;

namespace ACE_lib.Content
{
	public interface IPosition2 : IPosition2_readonly
	{
		void SetPosition(double xPos, double yPos);
		void SetPosition(Vec2i Pos);
		void SetPosition(Vec2d Pos);

		event EventHandler<OnValueChangedArgs<Vec2d>> OnPositionChanged;

	}
}
