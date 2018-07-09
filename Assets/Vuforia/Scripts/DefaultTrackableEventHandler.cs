/*==============================================================================
Copyright (c) 2017 PTC Inc. All Rights Reserved.

Copyright (c) 2010-2014 Qualcomm Connected Experiences, Inc.
All Rights Reserved.
Confidential and Proprietary - Protected under copyright and other laws.
==============================================================================*/

using UnityEngine;
using Vuforia;

using Firebase;
using Firebase.Database;
using Firebase.Unity.Editor;

/// <summary>
///     A custom handler that implements the ITrackableEventHandler interface.
/// </summary>
public class DefaultTrackableEventHandler : MonoBehaviour, ITrackableEventHandler
{
    #region PROTECTED_MEMBER_VARIABLES

    protected TrackableBehaviour mTrackableBehaviour;

    #endregion // PROTECTED_MEMBER_VARIABLES

    #region UNITY_MONOBEHAVIOUR_METHODS

    public DatabaseReference reference;
    string availability;
    //bool isAvailable;

    protected virtual void Start()
    {
        mTrackableBehaviour = GetComponent<TrackableBehaviour>();
        if (mTrackableBehaviour){
            mTrackableBehaviour.RegisterTrackableEventHandler(this);
        }

        // *********************************** FIREBASE OPERATIONS
        
        // Set up the Editor before calling into the realtime database.
        FirebaseApp.DefaultInstance.SetEditorDatabaseUrl("https://ar-test-firebase.firebaseio.com/");

        // Get the root reference location of the database.
        reference = FirebaseDatabase.DefaultInstance.RootReference; 



        // *********************************** FIREBASE OPERATIONS



    }

    protected virtual void OnDestroy()
    {
        if (mTrackableBehaviour)
            mTrackableBehaviour.UnregisterTrackableEventHandler(this);
    }

    #endregion // UNITY_MONOBEHAVIOUR_METHODS

    #region PUBLIC_METHODS

    /// <summary>
    ///     Implementation of the ITrackableEventHandler function called when the
    ///     tracking state changes.
    /// </summary>
    public void OnTrackableStateChanged(
        TrackableBehaviour.Status previousStatus,
        TrackableBehaviour.Status newStatus)
    {
        if (newStatus == TrackableBehaviour.Status.DETECTED ||
            newStatus == TrackableBehaviour.Status.TRACKED ||
            newStatus == TrackableBehaviour.Status.EXTENDED_TRACKED)
        {
            Debug.Log("Trackable " + mTrackableBehaviour.TrackableName + " found");
            OnTrackingFound();
        }
        else if (previousStatus == TrackableBehaviour.Status.TRACKED &&
                 newStatus == TrackableBehaviour.Status.NOT_FOUND)
        {
            Debug.Log("Trackable " + mTrackableBehaviour.TrackableName + " lost");
            OnTrackingLost();
        }
        else
        {
            // For combo of previousStatus=UNKNOWN + newStatus=UNKNOWN|NOT_FOUND
            // Vuforia is starting, but tracking has not been lost or found yet
            // Call OnTrackingLost() to hide the augmentations
            OnTrackingLost();
        }
    }

    #endregion // PUBLIC_METHODS

    #region PROTECTED_METHODS

    protected virtual void OnTrackingFound()
    {
        var rendererComponents = GetComponentsInChildren<Renderer>(true);
        var colliderComponents = GetComponentsInChildren<Collider>(true);
        var canvasComponents = GetComponentsInChildren<Canvas>(true);

        //Checking if the item is already collected or not

        reference = FirebaseDatabase.DefaultInstance.RootReference; 


        reference.GetValueAsync().ContinueWith(task => { //Be carefull about asyncronization, task is the return of asyncronized function -> ContinueWith() takes a callback function as argument
            
            if (task.IsFaulted) {
                // Handle the error...
                print("Database check error!");
            }
            else if (task.IsCompleted) {
                DataSnapshot snapshot = task.Result;
                print("ITEM NAME: " + colliderComponents[0].name);

                availability = (string)snapshot.Child(colliderComponents[0].name).Value;
                //print("Availibility: " + availability);

                if(availability != null ){

                    if(availability == "collected"){
                        print("Item is COLLECTED!");
                        foreach (var component in rendererComponents){ //we render each component "after we check" the availibility in the firebase realtime database
                            component.enabled = true;
                            Color color = component.GetComponent<Renderer>().material.color;
                            color.a = 0.40f;// This number equals approximately to 100/255, transparency in the last frame of fade out animation. 
                            component.GetComponent<Renderer>().material.color = color;           
                        
                        } 
                    }else{
                        print("Item is AVAILABLE!");
                        foreach (var component in rendererComponents){ 
                            component.enabled = true;
                            Color color = component.GetComponent<Renderer>().material.color;
                            color.a = 1.00f;// ATTENTION! IF ITEM IS AVAILABLE we render the component with %100 alpha value, 
                            component.GetComponent<Renderer>().material.color = color;                
                        } 
                    }
                }else{ //error catching
                    print("Item availability information could not be reached!");
                }   

                /*
                // Enable rendering:
                foreach (var component in rendererComponents){ //we render each component "after we check" the availibility in the firebase realtime database
                    component.enabled = true;
                    if(availability != null && availability == "collected"){
                        Color color = component.GetComponent<Renderer>().material.color;
                        color.a = 0.40f;// This number equals approximately to 100/255, transparency in the last frame of fade out animation. 
                        component.GetComponent<Renderer>().material.color = color;                
                    } 
                }
                */

            }
        });     


        // Enable colliders:
        foreach (var component in colliderComponents){
            component.enabled = true;
        }

        // Enable canvas':
        foreach (var component in canvasComponents){
            component.enabled = true;
        }
    }


    protected virtual void OnTrackingLost()
    {
        var rendererComponents = GetComponentsInChildren<Renderer>(true);
        var colliderComponents = GetComponentsInChildren<Collider>(true);
        var canvasComponents = GetComponentsInChildren<Canvas>(true);

        // Disable rendering:
        foreach (var component in rendererComponents)
            component.enabled = false;

        // Disable colliders:
        foreach (var component in colliderComponents)
            component.enabled = false;

        // Disable canvas':
        foreach (var component in canvasComponents)
            component.enabled = false;
    }

    #endregion // PROTECTED_METHODS
}
