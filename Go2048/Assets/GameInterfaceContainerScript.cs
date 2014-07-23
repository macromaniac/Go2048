using UnityEngine;
using System.Collections;

public class GameInterfaceContainerScript : MonoBehaviour {
	public GameInterface gameInterface;
	void Start(){
	}
	public void init(){
		gameInterface = new GameInterface();
	}

	public static GameInterface getInterface() {
		GameInterface toReturn = GameObject.Find("GameInterfaceContainer").
				GetComponent<GameInterfaceContainerScript>().gameInterface;

		if (toReturn == null) {
			GameObject.Find("GameInterfaceContainer").GetComponent<GameInterfaceContainerScript>().init();
			return getInterface();
		}
		return toReturn;
	}
}
