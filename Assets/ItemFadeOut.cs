using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemFadeOut : MonoBehaviour {



	Renderer rend;

	// Use this for initialization
	void Start () {
		rend = GetComponent<Renderer> ();
		//print(this.GetComponent<MeshRenderer>().material.color.a)
		//this.GetComponent<MeshRenderer>().material.color = new Color(1.0f, 1.0f, 1.0f, 0.5f);
		//print("NAME:");
		//print(rend.name);

	}

	IEnumerator FadeOut(){
		for(float f = 1f; f >= -0.05f; f -= 0.05f){
			Color c = rend.material.color;
			c.a = f;
			rend.material.color = c;
			Debug.Log(rend.material);
			Debug.Log(rend.material.color.a);
			/*print("printing color channels");
			
			print(rend.material.color.r);
			print(rend.material.color.g);
			print(rend.material.color.b);
			print("FadeOut TEST");
			*/
			yield return new WaitForSeconds(0.05f);
		}
	}
	
	public void startFading(){
		//print("startFading test");

		StartCoroutine("FadeOut");

	}
	// Update is called once per frame
	void Update () {
		
	}
}
