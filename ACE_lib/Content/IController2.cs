using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ACE_lib.Content.Props;

namespace ACE_lib.Content
{
	public interface IController2 : IContent2, IConnectable2
	{
		BorderProps BorderProperties { get; set; }
	}
}
