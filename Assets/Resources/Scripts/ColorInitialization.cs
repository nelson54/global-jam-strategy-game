using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class ColorInitialization : Singleton<ColorInitialization> {

	[SerializeField] SpriteRenderer[] PlayerIns;
	[SerializeField] SpriteRenderer[] PlayerOuts;

	void Start() {
		Invoke("Run", 1f);
	}

	// Use this for initialization
	void Run () {
		// Set colors for each active player
		int i = 0;
		var networkedPlayers = FindObjectsOfType<NetworkedPlayer> ();

		foreach(NetworkedPlayer player in networkedPlayers) {
			if(PlayerManager.instance.localNetworkedPlayer != player) {
				var index = i;
				player.EventPlayerLost += (id) => {
					DisableArea(index);
					DisableTowers(player);
					CheckWinCondition(id);

				};

				player.receiveArea = PlayerOuts [i].gameObject;

				PlayerIns[i].color = player.playerColor;

				var towerSpot = PlayerIns [i].GetComponent<PlaceableTowerSpot> ();
				towerSpot.Unhighlighted = player.playerColor;
				towerSpot.Highlighted = player.playerColor * 1.3f;

				var towerOutSpot = PlayerOuts[i].GetComponent<PlaceableTowerSpot> ();
				towerOutSpot.Unhighlighted = player.playerColor;
				towerOutSpot.Highlighted = player.playerColor * 1.3f;

				var towerSend = PlayerIns [i].GetComponent<TowerSendPlatform> ();
				towerSend.networkedPlayer = player;

				PlayerOuts[i].color = player.playerColor;
				i++;
			}
		}
		// Set the rest of the colors to grey
		for(; i < PlayerIns.Length; i++) {
			DisableArea (i);
		}
	}

	private void DisableArea(int i) {
		PlayerIns[i].color = Color.grey;
		PlayerIns[i].GetComponent<TowerSendPlatform>().enabled = false;
		PlayerIns[i].GetComponent<PlaceableTowerSpot>().enabled = false;
		PlayerOuts[i].color = Color.grey;
		PlayerOuts[i].GetComponent<PlaceableTowerSpot>().enabled = false;
	}

	private void DisableTowers(NetworkedPlayer player) {
		var towers = FindObjectsOfType<Tower> ();
		foreach (var tower in towers) {
			var renderer = tower.GetComponent<SpriteRenderer> ();
			if (renderer.color == player.playerColor) {
				tower.MakeDead ();
			}
		}
	}

	private void CheckWinCondition(NetworkInstanceId id) {
		bool won = true;
		if(!PlayerManager.instance.localNetworkedPlayer.isAlive) return;
		var networkedPlayers = FindObjectsOfType<NetworkedPlayer> ();
		foreach(NetworkedPlayer player in networkedPlayers) {
			if(PlayerManager.instance.localNetworkedPlayer != player && player.netId != id && player.isAlive) {
				won = false;
				break;
			}
		}
		if(won) {
			GameObject.FindWithTag("YouWin").GetComponent<UnityEngine.UI.Text>().enabled = true;
		}
	}
}
