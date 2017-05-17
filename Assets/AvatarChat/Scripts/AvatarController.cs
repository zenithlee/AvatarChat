using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvatarController : MonoBehaviour {

  public Avataria avatar;

  void SetID(string sID)
  {
   // avatar.SetID(sID);
  }

  void Emote(string s)
  {
    if ( s == "NEUTRAL")
    {
      avatar.ResetFace();
    }
    if( s == "SMILE" )
    {
      avatar.DoSmile();
    }
    if ( s == "FROWN")
    {
      avatar.DoFrown();
    }
    if ( s == "LAUGH" )
    {
      avatar.DoLaugh();
    }
  }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
