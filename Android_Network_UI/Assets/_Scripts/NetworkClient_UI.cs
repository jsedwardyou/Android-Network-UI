using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using UnityEngine.Networking;
using UnityEngine.Networking.NetworkSystem;
using UnityEngine;
using UnityEngine.UI;

public class NetworkClient_UI : MonoBehaviour {

    [SerializeField] private Text status;

    static NetworkClient client;
    private string ipAddress = "192.168.0.2";

    private string receivedMessage = "";

    private int[] surrounding_cars = new int[4];
    public string[] car;
    public string car_message = "";

    void Start () {
        client = new NetworkClient();
        if (!client.isConnected) {
            ConnectIP();
        }

        //RegisterHandle msg sent from Server
        client.RegisterHandler(888, ClientReceiveMessage);


        //LocalIPAddress();
        //NetworkActivity();

        
	}

    // Update is called once per frame
    void Update()
    {
        SendInfo();
    }

    static public void SendInfo() {
        if (client.isConnected) {
            StringMessage msg = new StringMessage();
            //TODO: Alter the msg.value 
            msg.value = "111";

            client.Send(888, msg);
        }
    }

    private void ClientReceiveMessage(NetworkMessage message) {
        StringMessage msg = new StringMessage();
        msg.value = message.ReadMessage<StringMessage>().value;

        string[] main = msg.value.Split('/');
        string general_info;
        string car_info;
        general_info = main[0]; car_info = main[1];
        car_message = main[2].Split('|')[1];

        car = car_info.Split('|');

        receivedMessage = msg.value;
    }

    //Get IP Address
    public string LocalIPAddress()
    {
        IPHostEntry host;
        string localIP = "";
        host = Dns.GetHostEntry(Dns.GetHostName());
        foreach (IPAddress ip in host.AddressList)
        {
            if (ip.AddressFamily == AddressFamily.InterNetwork)
            {
                localIP = ip.ToString();
                break;
            }
        }
        //ipAddress.text = "IP Address: " + localIP;
        return localIP;
    }

    public void NetworkActivity()
    {
        if (NetworkClient.active)
        {
            status.text = "Network Client Status: Active"; 
        }
        else
        {
            status.text = "Network Client Status: Not Active";
        }
    }

    public int GetState(int index) {
        return surrounding_cars[index];
    }

    public void ConnectIP() {
        Debug.Log(ipAddress);
        client.Connect(ipAddress, 25000);
    }

    public void SetIP(Text value) {
        ipAddress = value.text;
    }
}
