using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text.RegularExpressions;
using UnityEngine.SceneManagement;
using Vuforia;
using Firebase;
using Firebase.Auth;


public class Register : MonoBehaviour {

	public GameObject ARCamera; 
	
	public GameObject username;
	public GameObject email;
	public GameObject password;
	public GameObject confPassword;
	private string Username;
	private string Email;
	private string Password;
	private string ConfPassword;
	private bool isEmailValid = false;


	void Awake(){

		//ARCamera = GameObject.Find("ARCamera");

		//ARCamera.GetComponent<VuforiaBehaviour>().enabled = false;
	}

	// Use this for initialization
	void Start () {
		
	}

	// Update is called once per frame
	void Update () {

		Username = username.GetComponent<InputField>().text;
		Email = email.GetComponent<InputField>().text;
		Password = password.GetComponent<InputField>().text;
		ConfPassword = confPassword.GetComponent<InputField>().text; 

		
	}

	public void loadLevel(){
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
	}

	public void loadLevelOld(string sceneName){

		//SceneManager.LoadScene(sceneName);
		

		print("NEW SCENE LOADED!");
	
	}

	public void RegisterUser(){
		

		if(Password == ConfPassword && Username != "" && Email != ""){ //look for a better string comparison method and validEmail checking method 
			
			Firebase.Auth.FirebaseAuth auth = Firebase.Auth.FirebaseAuth.DefaultInstance;



			print("User will be registered!");

			auth.CreateUserWithEmailAndPasswordAsync(Email, Password).ContinueWith(task => {
			  if (task.IsCanceled) {
			    Debug.LogError("CreateUserWithEmailAndPasswordAsync was canceled.");
			    return;
			  }
			  if (task.IsFaulted) {
			    Debug.LogError("CreateUserWithEmailAndPasswordAsync encountered an error: " + task.Exception);
			    return;
			  }

			  // Firebase user has been created.
			  Firebase.Auth.FirebaseUser newUser = task.Result;
			  Debug.LogFormat("Firebase user created successfully: {0} ({1})",
			      newUser.DisplayName, newUser.UserId);


				print("User registered!");

				//loadLevel();


			});			




		}

		
	}
	
	
}
