using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using Prototype.NetworkLobby;

public class NetworkLobbyHook : LobbyHook {

	public override void OnLobbyServerSceneLoadedForPlayer(
		NetworkManager manager, 
		GameObject lobbyPlayerGO, 
		GameObject gamePlayerGO) 
	{ 
		var lobbyPlayer = lobbyPlayerGO.GetComponent<LobbyPlayer> ();
		var gamePlayer = gamePlayerGO.GetComponent<NetworkedPlayer> ();

		gamePlayer.playerColor = lobbyPlayer.playerColor;
	}
}