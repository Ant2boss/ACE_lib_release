using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACE_lib2.Args
{
	public class OnChangedArgs<T> : EventArgs
	{
		public T OldValue { get; set; }
		public T NewValue { get; set; }
	}
}
