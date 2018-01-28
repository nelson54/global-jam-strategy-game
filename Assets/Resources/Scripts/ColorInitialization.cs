using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
				PlayerIns[i].color = player.playerColor;
				PlayerIns[i].GetComponent<PlaceableTowerSpot>().Unhighlighted = player.playerColor;
				PlayerIns[i].GetComponent<PlaceableTowerSpot>().Highlighted = player.playerColor * 1.3f;
				PlayerOuts[i].color = player.playerColor;
				i++;
			}
		}
		// Set the rest of the colors to grey
		for(; i < PlayerIns.Length; i++) {
			PlayerIns[i].color = Color.grey;
			PlayerOuts[i].color = Color.grey;
			PlayerIns[i].GetComponent<TowerSendPlatform>().enabled = false;
			PlayerIns[i].GetComponent<PlaceableTowerSpot>().enabled = false;
		}
	}
}
