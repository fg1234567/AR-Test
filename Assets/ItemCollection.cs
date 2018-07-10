using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Firebase;
using Firebase.Database;
using Firebase.Unity.Editor;
using System.IO;
using UnityEngine.SceneManagement;
using Vuforia;

public class ItemCollection : MonoBehaviour {

		// Use this for initialization

	DatabaseReference reference;
	//string path;
	//string jsonString;
	string availability;
	public Animator fadeOutAnim;
	public GameObject ARCamera;
	int numOfItems = 0; 
	public Text itemNumber_Text;

	void Awake(){

		if(SceneManager.GetActiveScene().buildIndex == 2){
			ARCamera = GameObject.Find("ARCamera");
			ARCamera.GetComponent<VuforiaBehaviour>().enabled = true;

		}	
	}


	
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


	    //path = Application.dataPath + "/JsonData/items.json";
	    //jsonString = File.ReadAllText(path);
	}


	// Update is called once per frame
	private void Update () {
		if(Input.GetMouseButtonDown(0)){

			print("Mouse clicked!");

			RaycastHit hit;
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

			if(Physics.Raycast(ray, out hit, 100.0f)){
				if(hit.transform != null ){

					GameObject touchedObj = hit.transform.gameObject;

					PrintName(touchedObj);
					print("An object touched!");

					fadeOutAnim = (Animator)touchedObj.GetComponent(typeof(Animator));
					if(fadeOutAnim){

						reference.GetValueAsync().ContinueWith(task => {
					        
					        if (task.IsFaulted) {
					        	// Handle the error...
					        	print("Database check error!");
					        }
					        else if (task.IsCompleted) {
								DataSnapshot snapshot = task.Result;

								availability = (string)snapshot.Child(touchedObj.name).Value;
								print("Availibility: " + availability);

								if(availability == "available"){
									fadeOutAnim.enabled = true;

									Dictionary<string, object> collectionUpdate = new Dictionary<string, object>(); 
									collectionUpdate.Add( touchedObj.name, "collected");

									//reference.SetRawJsonValueAsync(jsonString);
									reference.UpdateChildrenAsync(collectionUpdate);

									numOfItems += 1;
									itemNumber_Text.text = numOfItems.ToString();

								}else{
									print("Item already collected!");
									fadeOutAnim.enabled = false;
								}
					        }
					    });




					}else{
						print("No animator defined for this object!");

					}

				}

			}

		}

    }

    private void PrintName(GameObject go){
    	print(go.name);
    }

}
