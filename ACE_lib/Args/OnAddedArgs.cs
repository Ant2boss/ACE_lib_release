using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACE_lib.Args
{
	public class OnAddedArgs<T> : EventArgs
	{
		public T ContentAdded { get; set; }
	}
}
