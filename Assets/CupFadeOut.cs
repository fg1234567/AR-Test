using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CupFadeOut : MonoBehaviour {

	public Animator fadeOutAnimaton;

	public void startFading(){

		print("Cup START FADING");
		fadeOutAnimaton.enabled = true;	

	}

}
