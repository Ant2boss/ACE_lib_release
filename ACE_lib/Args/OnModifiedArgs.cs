using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ACE_lib.Vectors;

namespace ACE_lib.Args
{
	public class OnModifiedArgs<T> : EventArgs
	{
		public T ValueAt { get; set; }
		public Vec2i Index { get; set; }
	}
}
