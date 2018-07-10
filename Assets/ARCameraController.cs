using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Vuforia;

public class ARCameraController : MonoBehaviour {

	//public GameObject ARCamera; 

	void Awake(){
/*
		print("initialization of AR Camera!");

		ARCamera = GameObject.Find("ARCamera");

		VuforiaRuntime.Instance.InitVuforia();

		ARCamera.GetComponent<VuforiaBehaviour>().enabled = true;
*/
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {


		if(Input.GetKeyDown(KeyCode.Escape)){

			SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);

		}


		//ARCamera = GameObject.Find("ARCamera");
		//VuforiaRuntime.Instance.InitVuforia();
		//ARCamera.GetComponent<VuforiaBehaviour>().enabled = true;
	}


	public void initializeArCamera(){



	


		print("Initialization COMPLETED!");

		//Vuforia.VuforiaConfiguration.GenericVuforiaConfiguration.DelayedInitialization = false;

	}


}
