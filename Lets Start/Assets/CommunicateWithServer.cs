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
		TextMesh mesh = GetComponent<TextMesh>();
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
    // Create a TcpClient. 
    // Note, for this client to work you need to have a TcpServer  
    // connected to the same address as specified by the server, port 
    // combination.
    Int32 port = 7777;
    TcpClient client = new TcpClient(server, port);

    // Translate the passed message into ASCII and store it as a Byte array.
    Byte[] data = System.Text.Encoding.ASCII.GetBytes(message);         

    // Get a client stream for reading and writing. 
   //  Stream stream = client.GetStream();

    NetworkStream stream = client.GetStream();

    // Send the message to the connected TcpServer. 
    stream.Write(data, 0, data.Length);

	Debug.Log("Sent: " + message);

    // Receive the TcpServer.response. 

    // Buffer to store the response bytes.
    data = new Byte[256];

    // String to store the response ASCII representation.
    String responseData = String.Empty;

    // Read the first batch of the TcpServer response bytes.
    Int32 bytes = stream.Read(data, 0, data.Length);
    responseData = System.Text.Encoding.ASCII.GetString(data, 0, bytes);
	Debug.Log("Received: " + responseData);

    // Close everything.
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
