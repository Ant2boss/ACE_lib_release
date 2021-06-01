using ACE_lib2.Args;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACE_lib2.Content.Interfaces
{
	public interface IConnectable2
	{
		event EventHandler<OnInfoArgs<IContent2>> OnConnectionAdded;
		event EventHandler<OnInfoArgs<IContent2>> OnConnectionDropped;

		event EventHandler OnPreAppendingToSelf;
		event EventHandler OnPostAppendingToSelf;

		bool AddConnection(IContent2 Content);
		bool DropConnection(IContent2 Content);

		void DropAllConnections();

		bool IsConnected(IContent2 Content);

		int ConnectedCount { get; }

		IContent2 GetConnected(int ConnectedIndex);

		void AppendConnectionsToSelf();
	}
}
