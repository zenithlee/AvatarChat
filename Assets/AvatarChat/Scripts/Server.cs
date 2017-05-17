using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using UnityEngine;
using UnityEngine.UI;

public class Server : MonoBehaviour {

  public int port = 6321;
  private TcpListener server;
  private bool serverStarted;
  private List<ServerClient> clients = new List<ServerClient>();
  private List<ServerClient> disconnectList = new List<ServerClient>();
  public InputField Logger;

  public List<string> log = new List<string>();

  void Log(string s)
  {
    if (Logger == null) return;
    Logger.text += s + Environment.NewLine;

  }

  void StartListening()
  {
    server.BeginAcceptTcpClient(AcceptTCP, server);
  }

  void Broadcast( string s, List<ServerClient> cl)
  {
    foreach( ServerClient c  in cl)
    {
      try
      {
        StreamWriter writer = new StreamWriter(c.tcp.GetStream());
        writer.WriteLine(s);
        writer.Flush();
      }
      catch ( Exception e)
      {
        Debug.Log(e.Message);
      }
    }
  }


  private void AcceptTCP(IAsyncResult result)
  {
    log.Add("New Connection");
    TcpListener listener = (TcpListener)result.AsyncState;
    ServerClient sc = new ServerClient(listener.EndAcceptTcpClient(result));
    clients.Add(sc);
    StartListening();
    //Broadcast(clients[clients.Count - 1].ClientName + " had connected", clients);

    List<ServerClient> rec = new List<ServerClient>();
    rec.Add(sc);
    Broadcast("##NAME", rec);
  }



  bool IsConnected( TcpClient c)
  {
    try
    {
      if (c != null && c.Client != null && c.Client.Connected)
      {
        if (c.Client.Poll(0, SelectMode.SelectRead))
        {
          return !(c.Client.Receive(new byte[1], SocketFlags.Peek) == 0);
        }

        return true;
      }
      else return false;
    }
     catch ( Exception e)
    {
      return false;
    }
  }

  void OnIncomingData(ServerClient c, string data)
  {
    if (data.Contains("##MYNAME")){
      c.ClientName = data.Split('=')[1];
      Broadcast(c.ClientName + ": has connected", clients);
      log.Add("Client Name:" + c.ClientName);
      return;
    }

    log.Add(c.ClientName + ": " + data);
    Broadcast(c.ClientName + ":" +data, clients);
  }

  void ShowLog()
  {
    Logger.text = "";
    while ( log.Count > 20 )
    {
      log.RemoveAt(0);
    }
    foreach ( string s in log)
    {      
      Log(s);
    }
  }

  private void Update()
  {
    ShowLog();

    if (!serverStarted) return;
    foreach( ServerClient c in clients )
    {
      if ( !IsConnected( c.tcp ))
      {
        c.tcp.Close();
        disconnectList.Add(c);
        continue;
      }
      else
      {
        NetworkStream s = c.tcp.GetStream();
        if ( s.DataAvailable )
        {
          StreamReader reader = new StreamReader(s, true);
          string data = reader.ReadLine();
          if ( data != null )
          {
            OnIncomingData(c, data);
          }
        }
      }
    }

    for ( int i=0; i< disconnectList.Count-1; i++ )
    {
      string msg = disconnectList[i].ClientName + " has disconnected";
      log.Add(msg);
      Broadcast(msg, clients);
      clients.Remove(disconnectList[i]);
      disconnectList.RemoveAt(i);
    }


  }

  // Use this for initialization
  void Start()
  {

    try
    {
      server = new TcpListener(IPAddress.Any, port);
      server.Start();

      StartListening();
      serverStarted = true;
      log.Add("Server Started : port " + port);
    }
    catch (Exception e)
    {
      Log(e.Message);
    }

  }
}



public class ServerClient
{
  public TcpClient tcp;
  public string ClientName;
  public ServerClient(TcpClient client)
  {
    ClientName = "Guest";
    tcp = client;
  }
}