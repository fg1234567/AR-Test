using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Vuforia;


public class SceneController : MonoBehaviour {

	public GameObject ARCamera;

	void Start(){

		ARCamera = GameObject.Find("ARCamera");

		//ARCamera.GetComponent<VuforiaBehaviour>().enabled = false;
	}

	public void loadLevel(string sceneName){

		SceneManager.LoadScene(sceneName);
		print("NEW SCENE LOADED!");

	}
}
