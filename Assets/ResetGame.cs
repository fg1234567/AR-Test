using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Database;
using Firebase.Unity.Editor;
using System.IO;
using UnityEngine.SceneManagement;

public class ResetGame : MonoBehaviour {

    DatabaseReference reference;
    string availability;
    string path;
    string jsonString;


    void Start () {

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


        // Set up the Editor before calling into the realtime database.
        FirebaseApp.DefaultInstance.SetEditorDatabaseUrl("https://ar-test-firebase.firebaseio.com/");

        // Get the root reference location of the database.
        reference = FirebaseDatabase.DefaultInstance.RootReference;


        path = Application.dataPath + "/JsonData/items.json";
        jsonString = File.ReadAllText(path);

    }

    public void ResetFirebase()
    {

        Debug.Log(jsonString);
        reference.SetRawJsonValueAsync(jsonString);


    }
}
