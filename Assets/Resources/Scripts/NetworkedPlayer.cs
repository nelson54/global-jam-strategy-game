using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class NetworkedPlayer : NetworkBehaviour {

	[SyncVar] public string playerName;
	[SyncVar] public Color playerColor;

	public override void OnStartLocalPlayer() {
		Debug.Log ("attaching networked player...");
		PlayerManager.instance.localNetworkedPlayer = this;
	}
}
