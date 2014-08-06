using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class NetInterface {
	public NetInterface(string ip, int port) {
		AsynchronousClient.OnConnect(OnConnect);
		AsynchronousClient.OnDoneSending(OnDoneSending);
		AsynchronousClient.OnDoneReceiving(OnDoneReceiving);
		AsynchronousClient.StartClient(ip, port);
	}
	public void OnConnect(bool wasSuccessful, string errorMessage) {
		if (wasSuccessful)
			Debug.Log("Successful connect");
		else
			Debug.Log("Connection failed: "+ errorMessage);

		AsynchronousClient.Send("HELLO!DSAD");
	}
	public void OnDoneSending(bool wasSuccessful, string errorMessage) {
		if (wasSuccessful)
			Debug.Log("Successful send");
		else
			Debug.Log("Send failed: "+ errorMessage);

	}
	public void OnDoneReceiving(bool wasSuccessful, string information) {
		if (wasSuccessful)
			Debug.Log("Successful recv: " + information);
		else
			Debug.Log("Failed recv: " + information);
	}
}