using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Database;
using Firebase.Unity.Editor;
using System.IO;

public class ItemCollection : MonoBehaviour {

		// Use this for initialization

	DatabaseReference reference;
	string path;
	string jsonString;

	void Start () {
		Firebase.FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task => {
		  var dependencyStatus = task.Result;
		  if (dependencyStatus == Firebase.DependencyStatus.Available) {
		    // Set a flag here indiciating that Firebase is ready to use by your
		    // application.
		  	Debug.Log("FIREBASE IS READY!");


		  } else {
		    UnityEngine.Debug.LogError(System.String.Format(
		      "Could not resolve all Firebase dependencies: {0}", dependencyStatus));
		    // Firebase Unity SDK is not safe to use here.
		  	Debug.Log("FIREBASE IS NOT READY!");


		  }
		});


		// Set up the Editor before calling into the realtime database.
	    FirebaseApp.DefaultInstance.SetEditorDatabaseUrl("https://ar-test-firebase.firebaseio.com/");

	    // Get the root reference location of the database.
	    reference = FirebaseDatabase.DefaultInstance.RootReference;		


	    path = Application.dataPath + "/JsonData/items.json";
	    jsonString = File.ReadAllText(path);
	}


	// Update is called once per frame
	private void Update () {
		if(Input.GetMouseButtonDown(0)){

			print("TEST1");

			RaycastHit hit;
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

			if(Physics.Raycast(ray, out hit, 100.0f)){
				if(hit.transform != null ){

					PrintName(hit.transform.gameObject);
					print("TEST2");

					BottleFadeOut BFO = (BottleFadeOut)FindObjectOfType(typeof(BottleFadeOut));
				    if (BFO){
		        	    Debug.Log("BottleFadeOut object found: " + BFO.name);
				    
		        	    // FADEOUT ACTION
		        	    BFO.startFading();

		        	    //ITEM COLLECTION ACTION ON FIREBASE REALTIME DATABASE


		        	    reference.SetRawJsonValueAsync(jsonString);

				    }
		        	else
		            	Debug.Log("No BottleFadeOut object could be found");

				}

			}



		}

    }

    private void PrintName(GameObject go){
    	print(go.name);
    }

}
