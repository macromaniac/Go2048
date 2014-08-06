using UnityEngine;
using System;
using System.Collections;
using System.Threading;
using System.Net;
using System.Net.Sockets;

public class NetManager : MonoBehaviour {
	private NetInterface netInterface;
	void Awake() {
		netInterface = new NetInterface(NetInfo.serverIp, NetInfo.port);
	}
}