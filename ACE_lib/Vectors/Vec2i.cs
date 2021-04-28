using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ACE_lib.Args;

namespace ACE_lib.Vectors
{
	public class Vec2i
	{
		public Vec2i() { }
		public Vec2i(int x, int y)
		{
			this.pX = x;
			this.pY = y; 
		}

		public int X 
		{ 
			get => this.pX;
			set
			{
				int oldVal = this.pX;

				this.pX = value;

				this.OnXChanged?.Invoke(this, new OnValueChanged<int> { OldValue = oldVal, NewValue = this.pX });
			}
		}
		public int Y 
		{ 
			get => this.pY;
			set
			{
				int oldVal = this.pY;

				this.pY = value;

				this.OnYChanged?.Invoke(this, new OnValueChanged<int> { OldValue = oldVal, NewValue = this.pY });
			}
		}

		public event EventHandler<OnValueChanged<int>> OnXChanged;
		public event EventHandler<OnValueChanged<int>> OnYChanged;

		public void Init(int x, int y)
		{
			this.X = x;
			this.Y = y;
		}

		public static Vec2i operator +(Vec2i V1, Vec2i V2) => new Vec2i(V1.X + V2.X, V1.Y + V2.Y);
		public static Vec2i operator -(Vec2i V1, Vec2i V2) => new Vec2i(V1.X - V2.X, V1.Y - V2.Y);
		public static Vec2i operator *(Vec2i V, int Val) => new Vec2i(V.Y * Val, V.Y * Val);
		public static Vec2i operator /(Vec2i V, int Val) => new Vec2i(V.Y / Val, V.Y / Val);

		public static bool operator ==(Vec2i V1, Vec2i V2) => V1.Equals(V2);
		public static bool operator !=(Vec2i V1, Vec2i V2) => !V1.Equals(V2);

		public override string ToString() => $"Vec2i({this.X}, {this.Y})";
		public override bool Equals(object obj) => (obj is Vec2i tvec) ? (tvec.X == this.X && tvec.Y == this.Y) : (false);
		public override int GetHashCode()
		{
			int hashCode = 1538397407;
			hashCode = hashCode * -1521134295 + pX.GetHashCode();
			hashCode = hashCode * -1521134295 + pY.GetHashCode();
			return hashCode;
		}

		private int pX;
		private int pY;
	}
}
