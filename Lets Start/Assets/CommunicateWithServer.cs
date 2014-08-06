using UnityEngine;
using System;
using System.Collections;
using System.Net.Sockets;

public class CommunicateWithServer : MonoBehaviour {

	string serverMessage="";

	public string GetServerMessage {
		get {
			return serverMessage;
		}
	}

	void Start () {
		Connect("54.84.172.177", " HELLO ");
		//Connect("127.0.0.1", " HELLO ");
		TextMesh mesh = GetComponent<TextMesh>();
		TryMe tryMe = new TryMe();
		tryMe.ex();
		//Debug.Log( typeof(TryMe.)
		Social.localUser.Authenticate(
			success => {
				if (success)
					mesh.text = Social.localUser.userName;
				else
					mesh.text = "Could not authenticate";
			}
		);
	}

	void Update () {
	
	}

static void Connect(String server, String message) 
{
  try 
  {
	Debug.Log("TRYING TO CONNECT");
    Int32 port = 7777;
    TcpClient client = new TcpClient(server, port);

    Byte[] data = System.Text.Encoding.ASCII.GetBytes(message);         

    NetworkStream stream = client.GetStream();

    stream.Write(data, 0, data.Length);

	Debug.Log("Sent: " + message);


    data = new Byte[256];

    String responseData = String.Empty;

    Int32 bytes = stream.Read(data, 0, data.Length);
    responseData = System.Text.Encoding.ASCII.GetString(data, 0, bytes);
	Debug.Log("Received: " + responseData);

    stream.Close();         
    client.Close();         
  } 
  catch (ArgumentNullException e) 
  {
	  Debug.Log("ArgumentNullException: " + e);
  } 
  catch (SocketException e) 
  {
	  Debug.Log("SocketException: " + e);
  }

}
}
