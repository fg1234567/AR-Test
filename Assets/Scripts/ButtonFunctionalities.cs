//====================================================================
// Initialized :13.7.2018  12.30
// Last edited : 
//====================================================================

using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

using UnityEngine;
using UnityEngine.SceneManagement;

using Firebase;
using Firebase.Database;
using Firebase.Unity.Editor;


public class ButtonFunctionalities : MonoBehaviour {

    // =========== This code block creates and object, converts the object to JSON format and stores in a string named "jsonFile" =========== //

    [Serializable]
    public class MyClass
    {
        public string apple;
        public string banana;
        public string bottle;
        public string cup;
        public string orange;
        public string pear;
        public string pumpkin;

        public MyClass (string o1, string o2, string o3, string o4, string o5, string o6, string o7)
        {
            apple = o1;
            banana = o2;
            bottle = o3;
            cup = o4;
            orange = o5;
            pear = o6;
            pumpkin = o7;
        }
    }

    string jsonFile = JsonUtility.ToJson(new MyClass("available", "available", "available", "available", "available", "available", "available"));

    // ======================================== The code block ends here =============================== // 


    DatabaseReference reference;
    string availability;
    //string path;
    //string jsonString;


    void Start () {

        //===================== This function checks whether a connection between the program and firabase established =====================//

        Firebase.FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task => {
            var dependencyStatus = task.Result;
            if (dependencyStatus == Firebase.DependencyStatus.Available)
            {
                // Set a flag here indiciating that Firebase is ready to use by your
                // application.
                Debug.Log("FIREBASE IS READY!");

            }
            else
            {
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

        // === Assigns the directory of the JSON file that exists in the project to a string named "path" 
        // === and assigns the JSON file to a string named "jsonString"
        // === Currenly in comment form because of an issue

        //path = Application.dataPath + "/JsonData/items.json";
        //jsonString = File.ReadAllText(path);

    }

    //  Assings the json file in the project (currently an object defined in this code) to the firebase database

    public void resetFirebase()
    {
        reference.SetRawJsonValueAsync(jsonFile);
    }

    public void loadLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
