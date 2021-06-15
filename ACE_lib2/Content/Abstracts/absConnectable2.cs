using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ACE_lib2.Args;
using ACE_lib2.Content.Interfaces;

namespace ACE_lib2.Content.Abstracts
{
	public abstract class absConnectable2 : IConnectable2
	{
		public event EventHandler<OnInfoArgs<IEntity2>> OnConnectionAdded;
		public event EventHandler<OnInfoArgs<IEntity2>> OnConnectionDropped;
		public event EventHandler OnAllConnectionsDropped;
		public event EventHandler OnPreConnectionHandling;
		public event EventHandler OnPostConnectionHandling;

		public int ConnectedCount => this._Connected.Count;

		public bool AddConnection(IEntity2 Entity)
		{
			if (!this.IsConnected(Entity))
			{
				this._Connected.Add(Entity);

				this.OnConnectionAdded?.Invoke(this, new OnInfoArgs<IEntity2> { Content = Entity });

				return true;
			}

			return false;
		}

		public bool DropConnection(IEntity2 Entity)
		{
			if (this._Connected.Remove(Entity))
			{
				this.OnConnectionDropped?.Invoke(this, new OnInfoArgs<IEntity2> { Content = Entity });

				return true;
			}
			return false;
		}

		public bool DropConnectionAt(int Index)
		{
			if (Index < 0 || Index >= this.ConnectedCount) return false;

			this._Connected.RemoveAt(Index);

			return true;
		}

		public void DropAllConnections()
		{
			this._Connected.Clear();

			this.OnAllConnectionsDropped?.Invoke(this, new EventArgs());
		}

		public IEntity2 GetConnectionDetails(int Index) => this._Connected[Index];

		public bool IsConnected(IEntity2 Entity)
		{
			if (this._Connected.Contains(Entity)) 
			{
				return true;
			}

			foreach (IEntity2 ent in this._Connected)
			{
				if (ent is IConnectable2 tcon && tcon.IsConnected(Entity))
				{
					return true;
				}
			}

			if (this is IEntity2 tent && tent == Entity)
			{
				return true;
			}

			return false;
		}

		public void HandleConnections()
		{
			this.OnPreConnectionHandling?.Invoke(this, new EventArgs());

			for (int i = 0; i < this._Connected.Count; ++i)
			{
				this._iUpdatePosition(this._Connected[i], i);
				this._iHandleAppending(this._Connected[i], i);
			}

			this.OnPostConnectionHandling?.Invoke(this, new EventArgs());
		}

		internal abstract void _iHandleAppending(IEntity2 entity2, int i);
		internal abstract void _iUpdatePosition(IEntity2 entity2, int i);

		private IList<IEntity2> _Connected = new List<IEntity2>();

	}
}
