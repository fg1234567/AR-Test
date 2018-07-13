//====================================================================
// Initialized :13.7.2018  12.30
// Last edited : 
//====================================================================

using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;

using UnityEngine;
using UnityEngine.UI;
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

    // ======== The texts in the input fields are assigned to strings named Username, Email, Password and Confpassword  ======== // 

    void Update () {

        Username = username.GetComponent<InputField>().text;
		Email = email.GetComponent<InputField>().text;
		Password = password.GetComponent<InputField>().text;
		ConfPassword = confPassword.GetComponent<InputField>().text; 
        		
	}

    // ===== User Authentication is established via Firebase
    // ===== The function does not skip the scene

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

			});			

		}
		
	}
	
}
