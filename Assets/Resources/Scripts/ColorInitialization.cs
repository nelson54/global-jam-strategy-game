using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorInitialization : MonoBehaviour {

	[SerializeField] SpriteRenderer[] PlayerIns;
	[SerializeField] SpriteRenderer[] PlayerOuts;

	// Use this for initialization
	void Awake () {
		// Set colors for each active player
		int i = 0;
		foreach(NetworkedPlayer player in FindObjectsOfType<NetworkedPlayer>()) {
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
			//PlayerIns[i].GetComponent<PlaceableTowerSpot>().enabled = false;
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
