using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BottleFadeOut : MonoBehaviour {

	public Animator fadeOutAnimation;

	public void startFading(){

		print("Bottle START FADING");
		fadeOutAnimation.enabled = true;	

	}
}
