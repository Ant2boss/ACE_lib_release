using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ACE_lib.Args;

namespace ACE_lib.Vectors
{
	public sealed class Vec2d : ICloneable
	{
		public Vec2d() { }
		public Vec2d(double x, double y)
		{
			this.pX = x;
			this.pY = y;
		}

		public double X
		{
			get => this.pX;
			set
			{
				double oldVal = this.pX;

				this.pX = value;

				this.OnXChanged?.Invoke(this, new OnValueChanged<double> { OldValue = oldVal, NewValue = this.pX });
			}
		}
		public double Y
		{
			get => this.pY;
			set
			{
				double oldVal = this.pY;

				this.pY = value;

				this.OnYChanged?.Invoke(this, new OnValueChanged<double> { OldValue = oldVal, NewValue = this.pY });
			}
		}

		public event EventHandler<OnValueChanged<double>> OnXChanged;
		public event EventHandler<OnValueChanged<double>> OnYChanged;

		public void Init(double x, double y)
		{
			this.X = x;
			this.Y = y;
		}

		public static Vec2d operator +(Vec2d V1, Vec2d V2) => new Vec2d(V1.X + V2.X, V1.Y + V2.Y);
		public static Vec2d operator -(Vec2d V1, Vec2d V2) => new Vec2d(V1.X - V2.X, V1.Y - V2.Y);
		public static Vec2d operator *(Vec2d V, double Val) => new Vec2d(V.Y * Val, V.Y * Val);
		public static Vec2d operator /(Vec2d V, double Val) => new Vec2d(V.Y / Val, V.Y / Val);

		public static bool operator ==(Vec2d V1, Vec2d V2) => V1.Equals(V2);
		public static bool operator !=(Vec2d V1, Vec2d V2) => !V1.Equals(V2);

		public object Clone() => new Vec2d(this.X, this.Y);
		public Vec2i CloneAsVec2i() => new Vec2i((int)this.X, (int)this.Y);

		public override string ToString() => $"Vec2d({this.X}, {this.Y})";
		public override bool Equals(object obj) => (obj is Vec2d tvec) ? (tvec.X == this.X && tvec.Y == this.Y) : (false);
		public override int GetHashCode()
		{
			int hashCode = 1538397407;
			hashCode = hashCode * -1521134295 + pX.GetHashCode();
			hashCode = hashCode * -1521134295 + pY.GetHashCode();
			return hashCode;
		}

		private double pX;
		private double pY;
	}
}
