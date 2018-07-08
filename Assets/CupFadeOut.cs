using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CupFadeOut : MonoBehaviour {

	public Animator fadeOutAnimation;

	public void startFading(){

		print("Cup START FADING");
		fadeOutAnimation.enabled = true;	

	}

}
