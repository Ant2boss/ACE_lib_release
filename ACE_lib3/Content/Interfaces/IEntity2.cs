using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ACE_2D_Base.Regions;
using ACE_2D_Base.Vectors;

namespace ACE_lib3.Content.Interfaces
{
	public interface IEntity2
	{
		void SetPosition(double x, double y);
		void SetPosition(Vec2i Position);
		void SetPosition(Vec2d Position);

		Vec2i GetSize();
		Vec2d GetPosition();
		Reg2 GetRegion();

		event EventHandler OnPreAppend;
		event EventHandler OnPostAppend;

		void AppendTo(ITextModifiable2 Parent);
		void AppendColorsTo(IColorModifiable2 Parent);
	}
}
