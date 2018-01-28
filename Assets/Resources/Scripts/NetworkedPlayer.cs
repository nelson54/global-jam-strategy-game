using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class NetworkedPlayer : NetworkBehaviour {

	[SyncVar] public string playerName;
	[SyncVar] public Color playerColor;

	public override void OnStartLocalPlayer() {
		Debug.Log (string.Format ("local player name: {0}", playerName) );
	}
}
