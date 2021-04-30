using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACE_lib.Content
{
	public abstract class absConnectableBase : IConnectable2
	{
		public event EventHandler OnConnectionAdded;
		public event EventHandler OnConnectionDropped;
		public event EventHandler OnAllConnectionsDropped;

		public event EventHandler OnPreConnectionsAppend;
		public event EventHandler OnPostConnectionsAppend;

		public bool AddConnection(IContent2 Con)
		{
			if (!this.IsConnected(Con))
			{
				this.pConList.Add(Con);
				this.OnConnectionAdded?.Invoke(this, new EventArgs());
				return true;
			}
			return false;
		}
		public bool DropConnection(IContent2 Con)
		{
			if (this.pConList.Remove(Con))
			{
				this.OnConnectionDropped?.Invoke(this, new EventArgs());
				return true;
			}
			return false;
		}

		public void DropAllConnections()
		{
			this.pConList.Clear();
			this.OnAllConnectionsDropped?.Invoke(this, new EventArgs());
		}

		public bool IsConnected(IContent2 Con)
		{
			if (Con == this) return true;
			if (this.pConList.Where((el) => el == Con).Count() > 0) return true;
			if (this.pConList.Where((el) => (el is IConnectable2 tCon) ? (tCon.IsConnected(Con)) : (false)).Count() > 0) return true;

			return false;
		}

		public IContent2 this[int ContentIndex] => this.GetConnectedDetails(ContentIndex);
		public int ConnectedCount => this.pConList.Count;

		public IContent2 GetConnectedDetails(int ContentIndex) => this.pConList[ContentIndex];

		public void AppendConnectionsToSelf()
		{
			this.OnPreConnectionsAppend?.Invoke(this, new EventArgs());

			for (int i = 0; i < this.pConList.Count; ++i)
			{
				this.abs_HandleConnectionAppending(this.pConList[i], i);
			}

			this.OnPostConnectionsAppend?.Invoke(this, new EventArgs());
		}

		internal abstract void abs_HandleConnectionAppending(IContent2 Con, int Index);

		private IList<IContent2> pConList = new List<IContent2>();
	}
}
