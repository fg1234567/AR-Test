//====================================================================
// Initialized :13.7.2018  12.30
// Last edited : 
//====================================================================

using System.Collections;
using System.Collections.Generic;
using System.IO;

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

using Firebase;
using Firebase.Database;
using Firebase.Unity.Editor;

using Vuforia;

public class ItemCollection : MonoBehaviour {

	DatabaseReference reference;
	string availability;
	public Animator fadeOutAnim;
	public GameObject ARCamera;
	int numOfItems = 0; 
	public Text itemNumber_Text;
    public Animator endGameAnim;
    public Animator itemNumberAnim;

	void Awake(){

		if(SceneManager.GetActiveScene().buildIndex == 2){
			ARCamera = GameObject.Find("ARCamera");
			ARCamera.GetComponent<VuforiaBehaviour>().enabled = true;

		}	
	}


    void Start () {

        //===================== This function checks whether a connection between the program and firabase established =====================//

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
        //====================================================== Function ends here ======================================================//


        // Set up the Editor before calling into the realtime database.
        FirebaseApp.DefaultInstance.SetEditorDatabaseUrl("https://ar-test-firebase.firebaseio.com/");

	    // Get the root reference location of the database.
	    reference = FirebaseDatabase.DefaultInstance.RootReference;		

	}


    private void Update () {
        //Checks whether the mouse left button is pressed
		if(Input.GetMouseButtonDown(0)){

			print("Mouse clicked!");

			RaycastHit hit;
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            //Checks whether the mouse click hits a mesh collider
			if(Physics.Raycast(ray, out hit, 100.0f)){
				if(hit.transform != null ){

                    //Stores the GameObject of the touched object
					GameObject touchedObj = hit.transform.gameObject;

					PrintName(touchedObj);
					print("An object touched!");

                    //Reads the Animator component of the touched object if it exists
					fadeOutAnim = (Animator)touchedObj.GetComponent(typeof(Animator));
					if(fadeOutAnim){

                        numOfItems += 1;

                        if(numOfItems == 1)
                        {
                            itemNumber_Text.text = "You have collected " + numOfItems.ToString() + " item";
                        }
                        else if(numOfItems > 1)
                        {
                            itemNumber_Text.text = "You have collected " + numOfItems.ToString() + " items";
                        }
                        if (numOfItems == 7)
                        {
                            itemNumber_Text.text = "You have collected all items";
                            itemNumberAnim.enabled = true;
                            endGameAnim.enabled = true;
                        }

                        /*reference.GetValueAsync().ContinueWith(task => {
					        
					        if (task.IsFaulted) {
					        	// Handle the error...
					        	print("Database check error!");
					        }
					        else if (task.IsCompleted) {
								DataSnapshot snapshot = task.Result;

                                //Reads the value of the touched object from Firebase
								availability = (string)snapshot.Child(touchedObj.name).Value;
								print("Availibility: " + availability);

                                //Checks wether the object is collected and activates the animation of the touched object
								if(availability == "available"){
									fadeOutAnim.enabled = true;

                                    //Sets the new value to Firebase
									Dictionary<string, object> collectionUpdate = new Dictionary<string, object>(); 
									collectionUpdate.Add( touchedObj.name, "collected");

									//reference.SetRawJsonValueAsync(jsonString);
									reference.UpdateChildrenAsync(collectionUpdate);

                                    //numOfItem is incremented each time an object is collected
									numOfItems += 1;

                                    //Updates the text on the screen with the number of the collected items
                                    if(numOfItems == 1)
                                    {
                                        itemNumber_Text.text = "You have collected" + numOfItems.ToString() + "item";
                                    }
                                    else if(numOfItems > 1)
                                    {
                                        itemNumber_Text.text = "You have collected" + numOfItems.ToString() + "items";
                                    }

                                    //When all objects are collected end game animation is collected
                                    if (numOfItems == 7)
                                    {
                                        itemNumber_Text.text = "You have collected all item";
                                        itemNumberAnim.enabled = true;
                                        endGameAnim.enabled = true;
                                    }

								}else{
									print("Item already collected!");
									fadeOutAnim.enabled = false;
								}
					        }
					    });*/

                    }
                    else
                    {
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
