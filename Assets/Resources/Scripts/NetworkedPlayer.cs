using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class NetworkedPlayer : NetworkBehaviour {

	[SyncVar] public string playerName;
	[SyncVar] public Color playerColor;

	public override void OnStartLocalPlayer() {
		Debug.Log ( string.Format("local player is '{0}'", playerName) );
		PlayerManager.instance.localNetworkedPlayer = this;
	}

	[ClientRpc]
	public void RpcClaimSendSpots() {
		ColorInitialization.instance.Run ();
	}
}
