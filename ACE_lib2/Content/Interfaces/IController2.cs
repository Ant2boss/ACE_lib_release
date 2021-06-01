using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ACE_lib2.Content.Props;

namespace ACE_lib2.Content.Interfaces
{
	public interface IController2 : IConnectable2, IContent2
	{
		BorderProps Border { get; set; }
		TitleProps Title { get; set; }
	}
}
