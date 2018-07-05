using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BottleFadeOut : MonoBehaviour {

	public Animator fadeOutAnimaton;

	public void startFading(){

		print("Bottle START FADING");
		fadeOutAnimaton.enabled = true;	

	}
}
