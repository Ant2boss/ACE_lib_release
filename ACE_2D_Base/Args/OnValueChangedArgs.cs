﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACE_2D_Base.Args
{
	public class OnValueChangedArgs<T> : EventArgs
	{
		public T OldValue { get; set; }
		public T NewValue { get; set; }
	}
}
