using System;
using UnityEngine;
using System.Collections;

public class LifeLine : MonoBehaviour {

  public enum estate { DELAY_START, MOVING_UP, DELAY_END, MOVING_DOWN };

  public bool DriveRotation = false;
  public bool RX = false;
  public bool RY = false;
  public bool RZ = false;

  public string _ = "";

  public bool DriveMorphs = false;

  public enum eMorphs { _None, Aged_Body1, Aged_Body2, Aged_Posture, Aged_Posture_Smoother, Basic_Weight1, Bulbous1, Bulbous2, CBM10yearold, CBM5yearold, CBMAdult, CBMAged1, CBMAged2, CBMOlderTeen, CBMYoungerTeen, Chin_Pointed, Eye_Sag_Top, Eye_Size, Eyelid_Size, FBMAndrogynous, FBMBodySize, FBMBodyTone, FBMBodybuilderDetails, FBMBodybuilderSize, FBMEmaciated, FBMExo, FBMFaerie, FBMFitnessDetails, FBMFitnessSize, FBMGoblin, FBMHag, FBMHeavy, FBMHeight, FBMPearFigure, FBMThin, FBMZombie, FHMAlienGrey, FHMAlienGreySmooth, FHMAlienHumanoid, FHMAmphibiousAlien, FHMAquaticAlien, FHMBat, FHMCatGirl, FHMChimp, FHMCoral, FHMDemon, FHMExo, FHMFaerie, FHMGoblin, FHMGogo, FHMHag, FHMHag2, FHMHammerHeadAlien, FHMHellFiend, FHMHobgoblin, FHMLion, FHMOrc, FHMPlatedAlien, FHMSkeletalVampire, FHMSmoothAlien, FHMZombie, FaeEars, Head_Proportion, Mouth_Sag, Neck_Weight, Nose_Age, PBMBelly, PBMBreastsCleavage, PBMBreastsDiameter, PBMBreastsDownwardSlope, PBMBreastsHeavy, PBMBreastsNaturalL, PBMBreastsNaturalR, PBMBreastsPerkSide, PBMBreastsShape03, PBMBreastsShape04, PBMBreastsShape05, PBMBreastsShape08, PBMBreastsSize, PBMBreastsSmall, PBMBreastsUnderCurve, PBMCalvesSize, PBMDevilClaws, PBMFeetBeast, PBMFeetElongated, PBMFeetFroglike, PBMFeetShoeFit, PBMFingernailsClaw, PBMForearmsSize, PBMGlutesSize, PBMHandsBeast, PBMHandsElongated, PBMHandsFroglike, PBMHandsWitch, PBMHunchback, PBMLegsLength, PBMLoveHandles, PBMNailsLength, PBMNeckSize, PBMPregnant, PBMRibcageSize, PBMSternumDepth, PBMStomachLowerDepth, PBMThighsSize, PBMToenailsClaw, PBMWaistNarrowLower, PBMWaistNarrowUpper, PBMWaistWidth, PBMlBreastsHangForward, PBMrBreastsFlatten, PBMrBreastsHangForward, PHMAfraid, PHMAlienScalp, PHMAngry, PHMBrowDownL, PHMBrowDownR, PHMBrowInnerDownL, PHMBrowInnerDownR, PHMBrowInnerUpL, PHMBrowInnerUpR, PHMCheekBonesWidth, PHMCheeksBalloon, PHMCheeksBalloonPucker, PHMCheeksDefine, PHMCheeksDepthL, PHMCheeksDepthR, PHMCheeksDimpleCreaseL, PHMCheeksDimpleCreaseR, PHMCheeksSinkL, PHMCheeksSinkR, PHMChinCleft, PHMChinWidthL, PHMChinWidthR, PHMConfused, PHMContempt, PHMCrowsFeet, PHMDevilEyes, PHMDevilFangs, PHMDisgust, PHMEarAlien, PHMEarBat, PHMEarCat, PHMEarCatBack, PHMEarChimp, PHMEarDeer, PHMEarDeerBack, PHMEarElfElfln, PHMEarElfMid, PHMEarElfShort, PHMEarFin, PHMEarGremlin, PHMEarMouse, PHMEarMouseBack, PHMEarPanther, PHMEarlobesAttached, PHMEarlobesLength, PHMEarsElfLong, PHMEarsGone, PHMEarsPig, PHMEarsRabbit, PHMExcitement, PHMEyeLidsBottomDownL, PHMEyeLidsBottomDownR, PHMEyeLidsBottomUpL, PHMEyeLidsBottomUpR, PHMEyeLidsTopUpL, PHMEyeLidsTopUpR, PHMEyelashesHideLayer1, PHMEyelashesHideLayer2, PHMEyelashesLength, PHMEyelidsFoldDown, PHMEyelidsTopDownL, PHMEyelidsTopDownR, PHMEyelidsTopInHeight, PHMEyelidsTopOutHeight, PHMEyesAlmondInnerL, PHMEyesAlmondInnerR, PHMEyesAlmondOuterL, PHMEyesAlmondOuterR, PHMEyesAngleL, PHMEyesAngleR, PHMEyesCat, PHMEyesClosedL, PHMEyesClosedR, PHMEyesHeightOuter, PHMEyesIrisSize, PHMEyesPuffyLower, PHMEyesSizeL, PHMEyesSizeR, PHMEyesSquintL, PHMEyesSquintR, PHMFaceCenterDepth, PHMFaceFlatL, PHMFaceFlatR, PHMFaceHeartL, PHMFaceHeartR, PHMFaceRoundL, PHMFaceRoundR, PHMFaceSquareL, PHMFaceSquareR, PHMFaceYoung, PHMFear, PHMFlirtingFeminineL, PHMFlirtingFeminineR, PHMFlirtingMasculineR, PHMFrown, PHMGlare, PHMHappy, PHMJawAngle, PHMJawCornerWidthL, PHMJawCornerWidthR, PHMJawCurve, PHMJawHeightL, PHMJawHeightR, PHMJawIn, PHMJawLineDepth, PHMJawOut, PHMLacrimalsPinch, PHMLipBottomDownL, PHMLipBottomInL, PHMLipBottomInR, PHMLipBottomOutL, PHMLipBottomOutR, PHMLipLowerDepth, PHMLipTopUpL, PHMLipTopUpR, PHMLipUpperSize, PHMLipsHeart, PHMLipsPart, PHMLipsPartCenter, PHMLipsPucker, PHMLipsPuckerWide, PHMLipsSquare, PHMMolarsGone, PHMMouthCornerUpDown, PHMMouthCurves, PHMMouthFrown, PHMMouthNarrowL, PHMMouthNarrowR, PHMMouthOpen, PHMMouthOpenWide, PHMMouthSmile, PHMMouthSmileOpen, PHMMouthSmileSimpleL, PHMMouthSmileSimpleR, PHMMouthWidth, PHMNoseBridgeDepth, PHMNoseBridgeSlope, PHMNoseBump, PHMNoseCat, PHMNoseDepth, PHMNoseFaerie, PHMNoseHag, PHMNosePig, PHMNosePinch, PHMNoseRidgeWidth, PHMNoseSize, PHMNoseTipHeight, PHMNoseTipRound, PHMNoseWidth, PHMNoseZombie, PHMNostrilsWidthL, PHMPain, PHMPupilsDialate, PHMPupilsSlit, PHMSad, PHMScalpCrest, PHMScream, PHMShock, PHMSmileFullFace, PHMSmileOpenFullFace, PHMSnarlLeft, PHMSnarlRight, PHMSurprise, PHMTeethBeastBottom, PHMTeethBeastNarrowBottom, PHMTeethBeastNarrowTop, PHMTeethBeastTop, PHMTeethCanineSharpBottom, PHMTeethCanineSharpTop, PHMTeethChip, PHMTeethExCrooked, PHMTeethFangBottom, PHMTeethFangTop, PHMTeethMessy, PHMTeethNosferatu, PHMTeethSharpBottom, PHMTeethSharpLongBottom, PHMTeethSharpLongTop, PHMTeethSharpTop, PHMTeethSlant, PHMTeethVampireBottom, PHMTeethVampireTop, PHMTemplesDefineL, PHMTemplesDefineR, PHMTongueFork, PHMTongueForkShort, PHMlHornFrontHigh, PHMlHornFrontLarge, PHMlHornFrontSmall, PHMlHornSide, PHMrHornFrontHigh, PHMrHornFrontLarge, PHMrHornFrontSmall, PHMrHornSide, SCLArmsLength, SCLChestDepth, SCLChestWidth, SCLFingersLength, SCLFingersLengthL, SCLFingersLengthR, SCLFingersWidth, SCLFingersWidthL, SCLFingersWidthR, SCLNeckLength, SCLPalmScale, SCLPalmScaleL, SCLPalmScaleR, SCLPropagatingChest, SCLPropagatingFoot, SCLPropagatingFootL, SCLPropagatingFootR, SCLPropagatingHand, SCLPropagatingHandL, SCLPropagatingHandR, SCLPropagatingHead, SCLShldrsScale, SCLShldrsWidth, SCLTorsoLength, Shoulder_Drop, Teeth_Hide, Thin_Lips, VSMEH, VSMER, VSMF, VSMIH, VSMIY, VSMK, VSML, VSMM, VSMOW, VSMS, VSMSH, VSMT, VSMTH, VSMUW, VSMW, Youth_Morph, Youth_Posture, Youth_Weight, VSMAA };
  public eMorphs Morph1;
  public eMorphs Morph2;

  public bool DriveSentiment = false;
  public bool DrivePositiveSentiment = true; //1 = happy, -1 = sad  
  public float Sentiment = 0; //1 = happy, -1 = sad  
  public float SentimentDecay = 2;

  public Transform Target;
  Quaternion TargetRotation;
  Quaternion OriginalRotation;
  public float Speed = 1;

  public float Multiplier = 100;
  public float Direction = 1;

  //public float MaxDelay = 4;
  public float DelayStartMaxRandom = 1;
  public float DelayStartMax = 2;
  public float DelayStartCounter = 2;

  public float DelayEndMaxRandom = 1;
  public float DelayEndMax = 2;
  public float DelayEndCounter = 2;

  public float MovingUpCounter = 0;
  public float MovingUpMax = 5;
  public float MovingDownCounter = 0;
  public float MovingDownMax = 5;

  estate state = estate.DELAY_START;
  public System.Random rnd = new System.Random();

  public float Driver = 0;

  // Use this for initialization
  void Start () {
    rnd = new System.Random();
    if ( Target != null )
    {
      OriginalRotation = Target.localRotation;
      TargetRotation = OriginalRotation;
    }
  }

  public void SetSentiment(float in_sentiment)
  {    
      Sentiment = in_sentiment;
  }

  public float Value()
  {    
    return Driver * Multiplier;    
  }

  public void Reset()
  {

  }  

  public void DoDriveSentiment()
  {
    float sent = 0;
    if (Sentiment > 0 && DrivePositiveSentiment)
    {
      sent = Sentiment;
    }
    else if (Sentiment < 0 && !DrivePositiveSentiment)
    {
      sent = Math.Abs(Sentiment);
    }
    else
    {
      sent = 0;
    }
    
    Driver = Mathf.Lerp(Driver, sent, Time.deltaTime * SentimentDecay);
  }

  public void DoDriveRotation()
  {
    TargetRotation *= Quaternion.Euler(
          RX ? (float)rnd.NextDouble() * Multiplier - (Multiplier / 2) : 0,
          RY ? (float)rnd.NextDouble() * Multiplier - (Multiplier / 2) : 0,
          RZ ? (float)rnd.NextDouble() * Multiplier - (Multiplier / 2) : 0);   
  }

  public void Step(float timedelta)
  {

    if ( DriveSentiment )
    {
      DoDriveSentiment();
      return;
    }

    if ( state == estate.DELAY_START)
    {
      DelayStartCounter += timedelta;
      if ( DelayStartCounter > DelayStartMax )
      {        
        state = estate.MOVING_UP;
        MovingUpCounter = 0;
      }
    }

    if ( state == estate.MOVING_UP )
    {
      Driver = MovingUpCounter / MovingUpMax;
      MovingUpCounter += timedelta;
      
      if ( MovingUpCounter > MovingUpMax )
      {
        if (DriveRotation) DoDriveRotation();
        state = estate.DELAY_END;
        DelayEndCounter = (float)rnd.NextDouble()*DelayEndMaxRandom;
      }
    }

    if ( state == estate.DELAY_END )
    {
      DelayEndCounter += timedelta;
      if ( DelayEndCounter > DelayEndMax)
      {
        state = estate.MOVING_DOWN;
        MovingDownCounter = MovingDownMax;
      }
    }

    if ( state == estate.MOVING_DOWN)
    {
      Driver = MovingDownCounter / MovingDownMax;
      MovingDownCounter -= timedelta;
      
      if (MovingDownCounter < 0 )
      {
        if (DriveRotation) DoDriveRotation();
        state = estate.DELAY_START;
        DelayStartCounter = (float)rnd.NextDouble()*DelayStartMaxRandom;
      }
    }

    if (DriveRotation && (Target != null))
    {
      Target.localRotation = Quaternion.Slerp(Target.localRotation, TargetRotation, timedelta * Speed);
    }

   


    /*
    if( state == 1 )
    {
      Delay -= timedelta;

      if ( Direction >0 )
      {

      }

      if (Delay < 0)
      {
        state = 0;
        Delay = MaxDelay * (float)rnd.NextDouble()*MaxDelay;
        TargetRotation = OriginalRotation;
      }
      
    }
    

    if (state == 0)
    {
      Delay -= timedelta;
      Driver += timedelta * Direction * Speed;

      if (Driver > 1)
      {
        Direction = -1;
        state = 1;
        TargetRotation *= Quaternion.Euler(          
          RX?(float)rnd.NextDouble() * Multiplier - (Multiplier / 2):0,
          RY ? (float)rnd.NextDouble() * Multiplier - (Multiplier / 2) : 0,
          RZ ? (float)rnd.NextDouble() * Multiplier - (Multiplier / 2) : 0);
      }
      if (Driver < 0)
      {
        state = 1;
        Direction = 1;
        TargetRotation *= Quaternion.Euler(
          RX?(float)rnd.NextDouble() * Multiplier - (Multiplier / 2):0,
          RY ? (float)rnd.NextDouble() * Multiplier - (Multiplier / 2) : 0,
          RZ ? (float)rnd.NextDouble() * Multiplier - (Multiplier / 2) : 0);
      }      
    }
    */


  }


  // Update is called once per frame
  void Update () {
	
	}
}
