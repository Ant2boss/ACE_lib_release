using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACE_2D_Base.Vectors
{
	public class Vec2Utils
	{
		public static double GetLenght_Squared(Vec2d Vec) => Math.Pow(Vec.X, 2) + Math.Pow(Vec.Y, 2);
		public static double GetLenght_Squared(Vec2i Vec) => Vec2Utils.GetLenght_Squared(Vec.CloneAsVec2d());

		public static double GetDistance_Squared(Vec2d Vec1, Vec2d Vec2) => Math.Pow(Vec1.X - Vec2.X, 2) + Math.Pow(Vec1.Y - Vec2.Y, 2);
		public static double GetDistance_Squared(Vec2i Vec1, Vec2i Vec2) => Vec2Utils.GetDistance_Squared(Vec1.CloneAsVec2d(), Vec2.CloneAsVec2d());

		public static double GetLenght(Vec2d Vec) => Math.Sqrt(Vec2Utils.GetLenght_Squared(Vec));
		public static double GetLenght(Vec2i Vec) => Vec2Utils.GetLenght(Vec.CloneAsVec2d());

		public static double GetDistance(Vec2d Vec1, Vec2d Vec2) => Math.Sqrt(Vec2Utils.GetDistance_Squared(Vec1, Vec2));
		public static double GetDistance(Vec2i Vec1, Vec2i Vec2) => Vec2Utils.GetDistance(Vec1.CloneAsVec2d(), Vec2.CloneAsVec2d());

		public static double GetDotBetween(Vec2d V1, Vec2d V2)
		{
			double len_prod = Vec2Utils.GetLenght(V1) * Vec2Utils.GetLenght(V2);

			if (double.IsNaN(len_prod)) return 0;

			return (V1.X * V2.X + V1.Y * V2.Y) / (len_prod);
		}
		public static double GetDotBetween(Vec2i V1, Vec2i V2) => Vec2Utils.GetDotBetween(V1.CloneAsVec2d(), V2.CloneAsVec2d());

		public static double GetAngleBetween_Rad(Vec2d V1, Vec2d V2) => Math.Acos(Vec2Utils.GetDotBetween(V1, V2));
		public static double GetAngleBetween_Rad(Vec2i V1, Vec2i V2) => Vec2Utils.GetAngleBetween_Rad(V1.CloneAsVec2d(), V2.CloneAsVec2d());

		public static double GetAngleBetween_Deg(Vec2d V1, Vec2d V2) => (Vec2Utils.GetAngleBetween_Rad(V1, V2) * 180) / Math.PI;
		public static double GetAngleBetween_Deg(Vec2i V1, Vec2i V2) => Vec2Utils.GetAngleBetween_Deg(V1.CloneAsVec2d(), V2.CloneAsVec2d());

		public static Vec2d GetRotatedByRad(Vec2d Vec, double Rad) => new Vec2d(Math.Round(Math.Cos(Rad) * Vec.X - Math.Sin(Rad) * Vec.Y, 3), Math.Round(Math.Sin(Rad) * Vec.X + Math.Cos(Rad) * Vec.Y, 3));
		public static Vec2d GetRotatedByRad(Vec2i Vec, double Rad) => Vec2Utils.GetRotatedByRad(Vec.CloneAsVec2d(), Rad);

		public static Vec2d GetRotatedByDeg(Vec2d Vec, double Deg) => Vec2Utils.GetRotatedByRad(Vec, (Deg * Math.PI) / 180);
		public static Vec2d GetRotatedByDeg(Vec2i Vec, double Deg) => Vec2Utils.GetRotatedByDeg(Vec.CloneAsVec2d(), Deg);

		public static Vec2d GetUnitVec2(Vec2d Vec) => Vec / Vec2Utils.GetLenght(Vec);
		public static Vec2d GetUnitVec2(Vec2i Vec) => Vec2Utils.GetUnitVec2(Vec.CloneAsVec2d());

		public static Vec2d GetDirectionVec2Toward(Vec2d Origin, Vec2d EndPoint) => Vec2Utils.GetUnitVec2(EndPoint - Origin);
		public static Vec2d GetDirectionVec2Toward(Vec2i Origin, Vec2i EndPoint) => Vec2Utils.GetDirectionVec2Toward(Origin.CloneAsVec2d(), EndPoint.CloneAsVec2d());


		public static Vec2d GetClampedVec2(Vec2d Vec, double Lenght) => (Vec2Utils.GetLenght(Vec) > Lenght) ? (Vec2Utils.GetUnitVec2(Vec) * Lenght) : (Vec.Clone() as Vec2d);
		public static Vec2d GetClampedVec2(Vec2i Vec, double Lenght) => Vec2Utils.GetClampedVec2(Vec.CloneAsVec2d(), Lenght);

		public static void SaveVec2iToFile(BinaryWriter bw, Vec2i Vec)
		{
			bw.Write(BitConverter.GetBytes(Vec.X));
			bw.Write(BitConverter.GetBytes(Vec.Y));
		}
		public static void SaveVec2dToFile(BinaryWriter bw, Vec2d Vec)
		{
			bw.Write(BitConverter.GetBytes(Vec.X));
			bw.Write(BitConverter.GetBytes(Vec.Y));
		}

		public static Vec2i LoadVec2iFromFile(BinaryReader br) => new Vec2i(br.ReadInt32(), br.ReadInt32());
		public static Vec2d LoadVec2dFromFile(BinaryReader br) => new Vec2d(br.ReadDouble(), br.ReadDouble());

	}
}
