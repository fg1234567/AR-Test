using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text.RegularExpressions;
using UnityEngine.SceneManagement;
using Vuforia;
using Firebase;
using Firebase.Auth;

public class Login : MonoBehaviour {


	public GameObject email;
	public GameObject password;
	private string Email;
	private string Password;
	private bool isEmailValid = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		Email = email.GetComponent<InputField>().text;
		Password = password.GetComponent<InputField>().text;

		
	}


	public void loadLevel(){
		SceneManager.LoadScene(1);
	}

	public void LoginUser(){
		

		
		Firebase.Auth.FirebaseAuth auth = Firebase.Auth.FirebaseAuth.DefaultInstance;


		auth.SignInWithEmailAndPasswordAsync(Email, Password).ContinueWith(task => {
			if (task.IsCanceled) {
			Debug.LogError("SignInWithEmailAndPasswordAsync was canceled.");
			return;
			}
			if (task.IsFaulted) {
			Debug.LogError("SignInWithEmailAndPasswordAsync encountered an error: " + task.Exception);
			return;
			}

			Firebase.Auth.FirebaseUser newUser = task.Result;
			Debug.LogFormat("User signed in successfully: {0} ({1})",
			  newUser.DisplayName, newUser.UserId);

			print("User signed-in!");

			loadLevel();



		});


		print("Sign-in test");


		
	}
	




}
