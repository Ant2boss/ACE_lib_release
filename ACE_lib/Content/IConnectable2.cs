using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACE_lib.Content
{
	public interface IConnectable2
	{
		event EventHandler OnConnectionAdded;
		event EventHandler OnConnectionDropped;
		event EventHandler OnAllConnectionsDropped;

		event EventHandler OnPreConnectionsAppend;
		event EventHandler OnPostConnectionsAppend;


		bool AddConnection(IContent2 Con);
		bool DropConnection(IContent2 Con);

		void DropAllConnections();

		bool IsConnected(IContent2 Con);

		IContent2 this[int ContentIndex] { get; }
		IContent2 GetConnectedDetails(int ContentIndex);

		int ConnectedCount { get; }

		void AppendConnectionsToSelf();

	}
}
