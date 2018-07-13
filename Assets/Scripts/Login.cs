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

public class Login : MonoBehaviour {


	public GameObject email;
	public GameObject password;
	private string Email;
	private string Password;
	private bool isEmailValid = false;

    // ======== The texts in the input fields are assigned to strings named Email and Password ======== // 

	void Update () {

		Email = email.GetComponent<InputField>().text;
		Password = password.GetComponent<InputField>().text;
		
	}

    // ===== User Authentication is established via Firebase and loadLevel() is called at the end.

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

}
    public void loadLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }


}
