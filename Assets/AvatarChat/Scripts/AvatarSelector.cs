using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvatarSelector : MonoBehaviour {

  Avataria[] avatars;

  Avataria GetAvatarFromClientID(string id)
  {
    foreach (Avataria a in avatars)
    {
      if (a.ClientID== id)
      {
        return a;
      }
    }
    Debug.Log("No avatar for " + id);
    return null;
  }

  public void EmoteAvatar( string[] data)
  {
    string ClientID = data[0];
    string AvatarID = data[1];
    string Emote  = data[2];

    Avataria a = GetAvatarFromClientID(ClientID);
    if (Emote == "NEUTRAL") a.ResetFace();
    if (Emote == "SMILE") a.DoSmile();
    if (Emote == "LAUGH") a.DoLaugh();
    if (Emote == "FROWN") a.DoFrown();

  }

 public void ShowAvatar(string[] data)
  {
    string ClientID = data[0];
    string AvatarID = data[1];    
        
    foreach( Avataria a in avatars )
    {
      if ( a.AvatarID == AvatarID)
      {
        a.gameObject.SetActive(true);
        a.ClientID = ClientID;
      }
    }
  }

  // Use this for initialization
  void Start()
  {
    avatars = GetComponentsInChildren<Avataria>();
    foreach (Avataria a in avatars)
    {
     // a.gameObject.SetActive(false);
    }
  }
	
	// Update is called once per frame
	void Update () {
		
	}
}
