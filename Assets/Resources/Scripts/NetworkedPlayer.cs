using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class NetworkedPlayer : NetworkBehaviour {

	public delegate void PlayerLostDelegate(NetworkInstanceId player); //TODO this should be a shared object?
	public delegate void PlayerHealthChangedDelegate(float curr, float total, NetworkInstanceId player);

	[SyncEvent] public event PlayerLostDelegate EventPlayerLost;
	[SyncEvent] public event PlayerHealthChangedDelegate EventPlayerHealthChanged;

	[SyncVar] public string playerName;
	[SyncVar] public Color playerColor;
	[SyncVar] public bool isAlive;

	public GameObject receiveArea;
	private float receivePadding = 0.2f;

	public override void OnStartLocalPlayer() {
		Debug.Log ( string.Format("local player is '{0}'", playerName) );
		PlayerManager.instance.localNetworkedPlayer = this;
	}

	[Command]
	public void CmdSendTower(TowerType towerType, NetworkInstanceId receiver) {
		Debug.Log( string.Format("{0} sending to {1}", netId, receiver) );
		var players = FindObjectsOfType<NetworkedPlayer> ();
		foreach (var player in players) {
			if (player.netId == receiver) {
				RpcReceiveTower (towerType, netId);
				break;
			}
		}
	}

	[ClientRpc]
	public void RpcReceiveTower(TowerType towerType, NetworkInstanceId sender) {
		if (this == PlayerManager.instance.localNetworkedPlayer) {
			return;
		}

		Debug.Log (string.Format ("received a tower from {0}", sender));

		//TODO spawn tower
		var bounds = receiveArea.GetComponent<BoxCollider2D>().bounds;
		var x = Random.Range (bounds.min.x + receivePadding, bounds.max.x - receivePadding);
		var y = Random.Range (bounds.min.y + receivePadding, bounds.max.y - receivePadding);

		//TODO move into TowerPlacer
		var color = Color.grey;
		foreach (var player in FindObjectsOfType<NetworkedPlayer>()) {
			if (player.netId == sender) {
				color = player.playerColor;
				break;
			}
		}

		var towerSpot = receiveArea.GetComponent<PlaceableTowerSpot> ();

		var tower = TowerPlacer.instance.PlaceATower (towerType, color);
		tower.transform.position = new Vector3 (x, y, tower.transform.position.z);

		tower.GetComponent<Tower> ().CurrentSpot = towerSpot; //TODO help this
	}

	[Command]
	public void CmdUpdateHealth(float amount, float total) {
		EventPlayerHealthChanged (amount, total, netId);
		if (amount <= 0) {
			isAlive = false;
			EventPlayerLost (netId);
		}
	}
}
