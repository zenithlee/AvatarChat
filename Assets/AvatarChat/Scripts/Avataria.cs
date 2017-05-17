using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using MORPH3D;


// VERTABRAT

public class Avataria : MonoBehaviour {  
  public MORPH3D.M3DCharacterManager man;
  public string AvatarID = "AVATAR1";
  public string ClientID = "000";

  public Expression Smile;
  public Expression Frown;
  public Expression Laugh;

  public void SetID(string newID)
  {
    ClientID = newID;
  }

  public void ResetFace()
  {
    //man.SetBlendshapeValue("PHMMouthSmileOpen", 0);
    Smile.Reset();
    Frown.Reset();
    Laugh.Reset();
  }

  public void DoSmile()
  {
    ResetFace();    
    Smile.Trigger();
  }

  public void DoFrown()
  {
    ResetFace();    
    Frown.Trigger();
  }

  public void DoLaugh()
  {
    ResetFace();
    Laugh.Trigger();
  }

  public IEnumerator Blink() {
  //  Debug.Log("Blink");
    for (int f= 0; f < 100; f+=20) {
      man.SetBlendshapeValue("eCTRLEyesClosed", f);
      //man.SetBlendshapeValue("PHMEyesClosedL", f);
      yield return new WaitForSeconds(0.01f);
    }
    man.SetBlendshapeValue("eCTRLEyesClosed", 0);
    //man.SetBlendshapeValue("PHMEyesClosedL", 0);
   // Debug.Log("/Blink");
  //  HeadMotion1();
    
  }
  

  public void DoBlink()
  {
    StartCoroutine(Blink());
  }

  Expression Setup(string s)
  {
    Expression e = transform.FindChild(s).GetComponent<Expression>();
    e.man = man;
    return e;
  }

  void SetupExpressions()
  {
    Smile = Setup("Smile");
    Frown = Setup("Frown");
    Laugh = Setup("Laugh");
  }

  // Use this for initialization
  void Start () {
    InvokeRepeating("DoBlink", 5, 5);
    SetupExpressions();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
