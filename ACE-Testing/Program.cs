using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ACE_lib.Vectors;
using ACE_lib.Regions;

namespace ACE_Testing
{
	class Program
	{
		static void Main(string[] args)
		{
			Console.WriteLine("--{}--");
			Reg2 r1 = new Reg2(3, 3, 3, 0);
			Reg2 r2 = new Reg2(2, 2, 0, 0);

			r2.SetPosition(2.1, 0);
			Console.WriteLine($"[{r1.GetLeft()}, {r1.GetRight()}] | [{r2.GetLeft()}, {r2.GetRight()}] -> [{Reg2Utils.IsRegOverReg(r1, r2)}]");

			r2.SetPosition(4.9, 0);
			Console.WriteLine($"[{r1.GetLeft()}, {r1.GetRight()}] | [{r2.GetLeft()}, {r2.GetRight()}] -> [{Reg2Utils.IsRegOverReg(r1, r2)}]");

			r2.SetPosition(5, 0);
			Console.WriteLine($"[{r1.GetLeft()}, {r1.GetRight()}] | [{r2.GetLeft()}, {r2.GetRight()}] -> [{Reg2Utils.IsRegOverReg(r1, r2)}]");

			r2.SetPosition(3, 0);
			Console.WriteLine($"[{r1.GetTop()}, {r1.GetBottom()}] | [{r2.GetTop()}, {r2.GetBottom()}] -> [{Reg2Utils.IsRegOverReg(r1, r2)}]");

			r2.SetPosition(3, -1);
			Console.WriteLine($"[{r1.GetTop()}, {r1.GetBottom()}] | [{r2.GetTop()}, {r2.GetBottom()}] -> [{Reg2Utils.IsRegOverReg(r1, r2)}]");
			r2.SetPosition(3, -0.9);
			Console.WriteLine($"[{r1.GetTop()}, {r1.GetBottom()}] | [{r2.GetTop()}, {r2.GetBottom()}] -> [{Reg2Utils.IsRegOverReg(r1, r2)}]");

			r2.SetPosition(3, 1.9);
			Console.WriteLine($"[{r1.GetTop()}, {r1.GetBottom()}] | [{r2.GetTop()}, {r2.GetBottom()}] -> [{Reg2Utils.IsRegOverReg(r1, r2)}]");
			r2.SetPosition(3, 2);
			Console.WriteLine($"[{r1.GetTop()}, {r1.GetBottom()}] | [{r2.GetTop()}, {r2.GetBottom()}] -> [{Reg2Utils.IsRegOverReg(r1, r2)}]");

		}
	}
}
