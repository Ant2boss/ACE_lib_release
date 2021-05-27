using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACE_lib2.Args
{
	public class OnInfoArgs<T> : EventArgs
	{
		public T Content { get; set; }
	}
}
