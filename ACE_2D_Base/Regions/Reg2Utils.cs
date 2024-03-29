﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ACE_2D_Base.Vectors;

namespace ACE_2D_Base.Regions
{
	public static class Reg2Utils
	{
		public static bool IsRegOverVec(Reg2 Reg, Vec2d Vec) => Vec.X >= Reg.GetLeft() && Vec.Y >= Reg.GetTop() && Vec.X <= Reg.GetRight() && Vec.Y <= Reg.GetBottom();
		public static bool IsRegOverVec(Reg2 Reg, Vec2i Vec) => Reg2Utils.IsRegOverVec(Reg, Vec.CloneAsVec2d());

		public static bool IsRegOverReg(Reg2 R1, Reg2 R2)
		{
			//if (R1.GetRight() <= R2.GetLeft()) return false;
			//if (R1.GetLeft() >= R2.GetRight()) return false;

			//if (R1.GetBottom() <= R2.GetTop()) return false;
			//if (R1.GetTop() >= R2.GetBottom()) return false;

			return !(R1.GetRight() <= R2.GetLeft() || R1.GetLeft() >= R2.GetRight() || R1.GetBottom() <= R2.GetTop() || R1.GetTop() >= R2.GetBottom());
		}

		public static void MoveReg(Reg2 Reg, double x, double y) => Reg.SetPosition(Reg.GetLeft() + x, Reg.GetTop() + y);
		public static void MoveReg(Reg2 Reg, Vec2i Offset) => Reg2Utils.MoveReg(Reg, Offset.X, Offset.Y);
		public static void MoveReg(Reg2 Reg, Vec2d Offset) => Reg2Utils.MoveReg(Reg, Offset.X, Offset.Y);
	}
}
