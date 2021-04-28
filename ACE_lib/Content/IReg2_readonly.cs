using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ACE_lib.Regions;

namespace ACE_lib.Content
{
	public interface IReg2_readonly : ISize2_readonly, IPosition2_readonly
	{
		Reg2 GetRegion();
	}
}
