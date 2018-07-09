using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Vuforia;


public class SceneController : MonoBehaviour {

	public GameObject ARCamera;

	void Awake(){

		print("SCENE Controller is awake!");



		//ARCamera.GetComponent<VuforiaBehaviour>().enabled = false;

	}

	void Start(){


	}

	public void loadLevel(string sceneName){

		SceneManager.LoadScene(sceneName);
		print("NEW SCENE LOADED!");
		



	}
}
