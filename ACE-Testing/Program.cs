using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ACE_lib.Vectors;

namespace ACE_Testing
{
	class Program
	{
		static void Main(string[] args)
		{
			Vec2i vec1 = new Vec2i(3, 9);

			Vec2i vec2 = new Vec2i(7, 2);

			Console.WriteLine($"{vec1} + {vec2} = {vec1 + vec2}");
		}
	}
}
