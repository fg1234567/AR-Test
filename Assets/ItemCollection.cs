using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCollection : MonoBehaviour {

	
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
