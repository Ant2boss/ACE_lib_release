using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ACE_lib.Vectors;
using ACE_lib.Args;

namespace ACE_lib.Regions
{
	public sealed class Reg2 : ICloneable
	{
		public Reg2()
		{
			this.pSize = new Vec2i();
			this.pPos = new Vec2d();
		}
		public Reg2(int xSize, int ySize) : this()
		{
			this.pSize.Init(xSize, ySize);
		}
		public Reg2(int xSize, int ySize, double xPos, double yPos) : this(xSize, ySize)
		{
			this.pPos.Init(xPos, yPos);
		}

		public Reg2(Vec2i Size) : this(Size.X, Size.Y) { }
		public Reg2(Vec2i Size, Vec2i Position) : this(Size.X, Size.Y, Position.X, Position.Y) { }
		public Reg2(Vec2i Size, Vec2d Position) : this(Size.X, Size.Y, Position.X, Position.Y) { }

		public event EventHandler<OnValueChanged<Vec2i>> OnSizeChanged;
		public event EventHandler<OnValueChanged<Vec2d>> OnPositionChanged;

		public void SetSize(int xSize, int ySize)
		{
			Vec2i oldVal = this.pSize.Clone() as Vec2i;

			this.pSize.Init(xSize, ySize);

			this.OnSizeChanged?.Invoke(this, new OnValueChanged<Vec2i> { OldValue = oldVal, NewValue = this.pSize.Clone() as Vec2i });
		}
		public void SetSize(Vec2i Size) => this.SetSize(Size.X, Size.Y);

		public void SetPosition(double xPos, double yPos)
		{
			Vec2d oldVal = this.pPos.Clone() as Vec2d;

			this.pPos.Init(xPos, yPos);

			this.OnPositionChanged?.Invoke(this, new OnValueChanged<Vec2d> { OldValue = oldVal, NewValue = this.pPos.Clone() as Vec2d });
		}
		public void SetPosition(Vec2i Position) => this.SetPosition(Position.X, Position.Y);
		public void SetPosition(Vec2d Position) => this.SetPosition(Position.X, Position.Y);

		public Vec2i GetSize() => this.pSize.Clone() as Vec2i;
		public Vec2d GetPosition() => this.pPos.Clone() as Vec2d;

		public double GetTop() => this.pPos.Y;
		public double GetBottom() => this.pPos.Y + this.pSize.Y - 1;
		public double GetLeft() => this.pPos.X;
		public double GetRight() => this.pPos.X + this.pSize.X - 1;

		public object Clone() => new Reg2(this.pSize, this.pPos);

		public override string ToString() => $"Reg2(Size: {this.pSize}, Position: {this.pPos})";
		public override bool Equals(object obj) => (obj is Reg2 treg) ? (treg.pSize == this.pSize && treg.pPos == this.pPos) : (false);
		public override int GetHashCode()
		{
			int hashCode = 1284627749;
			hashCode = hashCode * -1521134295 + EqualityComparer<Vec2i>.Default.GetHashCode(pSize);
			hashCode = hashCode * -1521134295 + EqualityComparer<Vec2d>.Default.GetHashCode(pPos);
			return hashCode;
		}

		private Vec2i pSize;
		private Vec2d pPos;
	}
}
