using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorInitialization : Singleton<ColorInitialization> {

	[SerializeField] SpriteRenderer[] PlayerIns;
	[SerializeField] SpriteRenderer[] PlayerOuts;

	// Use this for initialization
	public void Run () {
		// Set colors for each active player
		int i = 0;
		var networkedPlayers = FindObjectsOfType<NetworkedPlayer> ();

		foreach(NetworkedPlayer player in networkedPlayers) {
			if(PlayerManager.instance.localNetworkedPlayer != player) {
				PlayerIns[i].color = player.playerColor;
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
