using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Expression : MonoBehaviour {

  public string ExpressionMorph;
  public float DefaultExpression;
  public float TargetExpression;
  public float CurrentExpression;
  public float Speed = 5;
  public MORPH3D.M3DCharacterManager man;

  public void Trigger()
  {
    TargetExpression = DefaultExpression;
  }

  public void Reset()
  {
    TargetExpression = 0;
  }

  public void SetTarget(float t, float InSpeed = 5)
  {
    TargetExpression = t;
    Speed = InSpeed;
  }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
    CurrentExpression = Mathf.Lerp(CurrentExpression, TargetExpression, Time.deltaTime * Speed);
    man.SetBlendshapeValue(ExpressionMorph, CurrentExpression);
  }
}
