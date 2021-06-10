using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ACE_2D_Base.Vectors;

using ACE_lib2.Args;
using ACE_lib2.Content.Canvases;
using ACE_lib2.Content.Interfaces;
using ACE_lib2.Content.Props;

namespace ACE_lib2.Content.Abstracts
{
	public abstract class abs_Controller2 : abs_Content2, IController2
	{
		public BorderProps Border { get; set; }
		public TitleProps Title { get; set; }

		public int ConnectedCount => this._Connected.Count;

		public event EventHandler<OnInfoArgs<IContent2>> OnConnectionAdded;
		public event EventHandler<OnInfoArgs<IContent2>> OnConnectionDropped;
		public event EventHandler OnPreAppendingToSelf;
		public event EventHandler OnPostAppendingToSelf;

		public bool AddConnection(IContent2 Content)
		{
			if (this.IsConnected(Content)) return false;

			this._Connected.Add(Content);
			this.OnConnectionAdded?.Invoke(this, new OnInfoArgs<IContent2> { Content = Content });
			return true;
		}
		public bool DropConnection(IContent2 Content)
		{
			if(this._Connected.Remove(Content))
			{
				this.OnConnectionDropped?.Invoke(this, new OnInfoArgs<IContent2> { Content = Content });
				return true;
			}
			return false;
		}
		public void DropAllConnections()
		{
			this._Connected.Clear();
		}

		public IContent2 GetConnected(int ConnectedIndex)
		{
			this._iUpdateConnectionProps(this._Connected[ConnectedIndex], ConnectedIndex);
			return this._Connected[ConnectedIndex];
		}

		public bool IsConnected(IContent2 Content)
		{
			if (Content == this) return true;

			if (this._Connected.Contains(Content)) return true;

			if (Content is IConnectable2 tconnect && tconnect.IsConnected(Content)) return true;

			return false;
		}

		public void AppendConnectionsToSelf()
		{
			this.OnPreAppendingToSelf?.Invoke(this, new EventArgs());

			for (int i = 0; i < this.ConnectedCount; ++i)
			{
				this._iHandleConnection(this.GetConnected(i), i);
			}

			this.OnPostAppendingToSelf?.Invoke(this, new EventArgs());
		}

		public override void AppendTo(IContent2 Parent)
		{
			this.AppendConnectionsToSelf();

			base.AppendTo(Parent);

			this._AppendBorderTo(Parent);
			this._AppendBorderColorsTo(Parent);
		}
		public override void AppendTo(IModifiable2<char> Parent)
		{
			this.AppendConnectionsToSelf();

			base.AppendTo(Parent);

			this._AppendBorderTo(Parent);
		}
		public override void DrawColorsTo(Canvas2 Can)
		{
			this.AppendConnectionsToSelf();

			base.DrawColorsTo(Can);

			this._BorderFunc((el, Index) => Can.SetAt(el, Index), (col, Index) => Can.DrawColorAt(col, Index), Can.GetSize());
		}

		private void _AppendBorderTo(IModifiable2<char> parent)
		{
			this._BorderFunc((el, Index) => parent.SetAt(el, Index), null, parent.GetSize());
		}
		private void _AppendBorderColorsTo(IContent2 parent)
		{
			this._BorderFunc((el, Index) => parent.SetAt(el, Index), (col, Index) => parent.SetColorAt(col, Index), parent.GetSize());
		}

		private void _BorderFunc(Action<char, Vec2i> CharPlacer, Action<ConsoleColor, Vec2i> ColorPlacer, Vec2i ParentSize)
		{
			if (this.Border == null || this.Border.Visible == false) return;

			Vec2i Position = this.GetPosition().CloneAsVec2i();
			Vec2i Size = this.GetSize();

			Vec2i Index = new Vec2i();

			for (int y = -1; y < Size.Y + 1; ++y)
			{
				Index.Y = Position.Y + y;

				if (y == -1 || y == Size.Y)
				{
					char BorderElement = (y == -1) ? (this.Border.GetBorderAtSide(BorderProps.BorderSide.Top)) : (this.Border.GetBorderAtSide(BorderProps.BorderSide.Bottom));
					ConsoleColor BorderColor = (y == -1) ? (this.Border.GetColorAtSide(BorderProps.BorderSide.Top)) : (this.Border.GetColorAtSide(BorderProps.BorderSide.Bottom));

					for (int x = 0; x < Size.X; ++x)
					{
						Index.X = Position.X + x;

						if (this._IsOutOfBounds(Index, ParentSize))
						{
							continue;
						}

						CharPlacer?.Invoke(BorderElement, Index);
						ColorPlacer?.Invoke(BorderColor, Index);
					}

					if (y == -1)
					{
						Index.X = Position.X - 1;
						if (!this._IsOutOfBounds(Index, ParentSize))
						{
							CharPlacer?.Invoke(this.Border.GetBorderAtCorner(BorderProps.BorderSide.Top, BorderProps.BorderSide.Left), Index);
							ColorPlacer?.Invoke(this.Border.GetColorAtCorner(BorderProps.BorderSide.Top, BorderProps.BorderSide.Left), Index);
						}

						Index.X = Position.X + Size.X;
						if (!this._IsOutOfBounds(Index, ParentSize))
						{
							CharPlacer?.Invoke(this.Border.GetBorderAtCorner(BorderProps.BorderSide.Top, BorderProps.BorderSide.Right), Index);
							ColorPlacer?.Invoke(this.Border.GetColorAtCorner(BorderProps.BorderSide.Top, BorderProps.BorderSide.Right), Index);
						}
					}
					else
					{
						Index.X = Position.X - 1;
						if (!this._IsOutOfBounds(Index, ParentSize))
						{
							CharPlacer?.Invoke(this.Border.GetBorderAtCorner(BorderProps.BorderSide.Bottom, BorderProps.BorderSide.Left), Index);
							ColorPlacer?.Invoke(this.Border.GetColorAtCorner(BorderProps.BorderSide.Bottom, BorderProps.BorderSide.Left), Index);
						}

						Index.X = Position.X + Size.X;
						if (!this._IsOutOfBounds(Index, ParentSize))
						{
							CharPlacer?.Invoke(this.Border.GetBorderAtCorner(BorderProps.BorderSide.Bottom, BorderProps.BorderSide.Right), Index);
							ColorPlacer?.Invoke(this.Border.GetColorAtCorner(BorderProps.BorderSide.Bottom, BorderProps.BorderSide.Right), Index);
						}
					}
				}
				else
				{
					Index.X = Position.X - 1;
					if (!this._IsOutOfBounds(Index, ParentSize))
					{
						CharPlacer?.Invoke(this.Border.GetBorderAtSide(BorderProps.BorderSide.Left), Index);
						ColorPlacer?.Invoke(this.Border.GetColorAtSide(BorderProps.BorderSide.Left), Index);
					}

					Index.X = Position.X + Size.X;
					if (!this._IsOutOfBounds(Index, ParentSize))
					{
						CharPlacer?.Invoke(this.Border.GetBorderAtSide(BorderProps.BorderSide.Right), Index);
						ColorPlacer?.Invoke(this.Border.GetColorAtSide(BorderProps.BorderSide.Right), Index);
					}
				}
			}

			//Title drawing

			if (this.Title == null || this.Title.Visible == false) return;

			Index.Y = Position.Y - 1;
			Index.X = Position.X + 1;

			this._PlaceText(this.Title.LeftDecorator, this.Title.DecoratorColor, CharPlacer, ColorPlacer, Index);
			this._PlaceText(this.Title.Title, this.Title.TitleColor, CharPlacer, ColorPlacer, Index);
			this._PlaceText(this.Title.RightDecorator, this.Title.DecoratorColor, CharPlacer, ColorPlacer, Index);
		}

		private void _PlaceText(string Text, ConsoleColor Col, Action<char, Vec2i> CharPlacer, Action<ConsoleColor, Vec2i> ColorPlacer, Vec2i Index)
		{
			if (Text == null) return;

			for (int i = 0; i < Text.Length; ++i)
			{
				CharPlacer?.Invoke(Text[i], Index);
				ColorPlacer?.Invoke(Col, Index);
				Index.X += 1;
			}
		}

		private bool _IsOutOfBounds(Vec2i index, Vec2i size) => index.X < 0 || index.Y < 0 || index.X >= size.X || index.Y >= size.Y;


		internal abstract void _iHandleConnection(IContent2 ContentToHandle, int Index);
		internal abstract void _iUpdateConnectionProps(IContent2 ContentToUpdate, int Index);

		private IList<IContent2> _Connected = new List<IContent2>();
	}
}
