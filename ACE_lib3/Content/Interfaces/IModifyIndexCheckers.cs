using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ACE_2D_Base.Vectors;

namespace ACE_lib3.Content.Interfaces
{
	public interface IModifyIndexCheckers
	{
		bool CheckIndex(int x, int y);
		bool CheckIndex(Vec2i Index);
	}
}
