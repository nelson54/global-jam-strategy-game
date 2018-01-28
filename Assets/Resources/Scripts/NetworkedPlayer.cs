using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class NetworkedPlayer : NetworkBehaviour {

	public delegate void ReceivedTowerDelegate(TowerType TowerType, NetworkInstanceId sender);
	public delegate void PlayerLostDelegate(NetworkInstanceId player); //TODO this should be a shared object?
	public delegate void PlayerHealthChangedDelegate(float curr, float total, NetworkInstanceId player);

	[SyncEvent] public event ReceivedTowerDelegate EventReceivedTower;
	[SyncEvent] public event PlayerLostDelegate EventPlayerLost;
	[SyncEvent] public event PlayerHealthChangedDelegate EventPlayerHealthChanged;

	[SyncVar] public string playerName;
	[SyncVar] public Color playerColor;
	[SyncVar] public bool isAlive;

	public override void OnStartLocalPlayer() {
		Debug.Log ( string.Format("local player is '{0}'", playerName) );
		PlayerManager.instance.localNetworkedPlayer = this;
		EventReceivedTower += ReceiveTower;
	}

	[Command]
	public void CmdSendTower(TowerType towerType, NetworkInstanceId receiver) {
		var players = FindObjectsOfType<NetworkedPlayer> ();
		foreach (var player in players) {
			if (player.netId == receiver) {
				EventReceivedTower (towerType, netId);
				break;
			}
		}
	}

	[Command]
	public void CmdUpdateHealth(float amount, float total) {
		EventPlayerHealthChanged (amount, total, netId);
		if (amount <= 0) {
			isAlive = false;
			EventPlayerLost (netId);
		}
	}

	protected void ReceiveTower(TowerType towerType, NetworkInstanceId sender) {
		Debug.Log (string.Format ("received a tower from {0}", sender.Value) );
	}
}
