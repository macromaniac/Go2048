using UnityEngine;
using System.Collections;

public class GameInterfaceContainerScript : MonoBehaviour {
	public GameInterface gameInterface;
	void Start(){
	}
	public void Init(){
		gameInterface = new GameInterface();
	}

	public static GameInterface GetInterface() {
		GameInterface toReturn = GameObject.Find("GameInterfaceContainer").
				GetComponent<GameInterfaceContainerScript>().gameInterface;

		if (toReturn == null) {
			GameObject.Find("GameInterfaceContainer").GetComponent<GameInterfaceContainerScript>().Init();
			return GetInterface();
		}
		return toReturn;
	}
}
