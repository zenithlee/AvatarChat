using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net.Sockets;
using UnityEngine;
using UnityEngine.UI;

// "##ANNOUNCE|ID|AVATAR1|HAIR1|FACE1|SKIN1|"

public class Client : MonoBehaviour {

  public string ClientID;
  public string AvatarID;
  private bool socketReady;
  TcpClient socket;
  NetworkStream stream;
  StreamReader reader;
  StreamWriter writer;

  public GameObject MessageContainer;
  public GameObject Parent;
  public InputField input;

  public string Host = "10.0.0.6";
  public int Port = 6321;

  bool Debugging = true;

  // Use this for initialization
  void Start()
  {
    SetupChat();
    InvokeRepeating("ConnectToServer", 1, 10);    
  }

  void SetupChat()
  {
    for ( int i=0; i< 10; i++ ) { 
    GameObject go = Instantiate(MessageContainer);
    Text t = go.transform.FindChild("Text").GetComponent<Text>();
    t.text = "";    
    go.transform.parent = Parent.transform;
    }
  }

  public void ConnectToServer()
  {
    if (socketReady) return;  

    try
    {
      socket = new TcpClient(Host, Port);
      stream = socket.GetStream();
      writer = new StreamWriter(stream);
      reader = new StreamReader(stream);
      socketReady = true;
      Send("##ANNOUNCE|" + ClientID +"|" + AvatarID + "|HAIR1|FACE1|SKIN1|");
      SendMessage("SetID", ClientID);      
    }
    catch ( Exception e )
    {
      Debug.Log(e.Message);
    }
  }

  void Send(string s)
  {
    if (!socketReady) return;
    writer.WriteLine(s);
    writer.Flush();
  }

  public void OnSend()
  {
    string t = "##CHAT|"+ClientID + "|" + AvatarID + "|" + input.text;
    Send(t);
  }
  
  public void SendEmote(string s)
  {
    Send("##EMOTE|" + ClientID + "|" + AvatarID + "|" + s);
  }

  void CloseSocket()
  {
    if ( !socketReady)
    {
      return;
    }
    writer.Close();
    reader.Close();
    socket.Close();
    socketReady = false;
  }

  private void OnApplicationQuit()
  {
    CloseSocket();
  }

  private void OnDisable()
  {
    CloseSocket();
  }

  // "##ANNOUNCE|ID|AVATAR1|HAIR1|FACE1|SKIN1|"
  void DoAnnounce(string data)
  {
    string user = data.Split('|')[1];
    string avatar = data.Split('|')[2];
    string[] box = new string[] { user, avatar };
    SendMessageUpwards("ShowAvatar", box);
    if ( Debugging == true )
    {
      DoChat("##CHAT|"+ClientID + "|" + AvatarID + "|"+ ClientID + " Joined");
    }
    
  }

  void DoEmote(string data)
  {
    string user = data.Split('|')[1];
    string avatar = data.Split('|')[2];
    string emote = data.Split('|')[3];
    string[] box = new string[] { user, avatar, emote };
    SendMessageUpwards("EmoteAvatar", box);
  }

  void DoChat(string data)
  {
    string user = data.Split('|')[1];
    string avatar = data.Split('|')[2];
    string text = data.Split('|')[3];

    //Debug.Log("Server:" + data);
    GameObject go = Instantiate(MessageContainer);
    Text t = go.transform.FindChild("Text").GetComponent<Text>();
    t.text = text;
    Transform tr = Parent.transform.GetChild(0);
    GameObject.Destroy(tr.gameObject, 0.1f);
    go.transform.parent = Parent.transform;
  }

  void OnIncomingData(string data)
  {

    if ( data == "##NAME")
    {
      Send("##MYNAME=" + ClientID);
  //    return;
    }

    if ( data.Contains("##EMOTE"))
    {
      DoEmote(data);      
//      return;
    }

    if ( data.Contains("##ANNOUNCE"))
    {
      DoAnnounce(data);
    }

    if ( data.Contains("##CHAT"))
    {
      DoChat(data);
    }
    
  }
  // Update is called once per frame
  void Update () {
		if ( socketReady )
    {
      if ( stream.DataAvailable )
      {
        string data = reader.ReadLine();
        if ( data != null )
        {          
          OnIncomingData(data);
        }
      }
    }

	}
}
