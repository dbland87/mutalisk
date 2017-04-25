using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AuthController : MonoBehaviour {

	public User user;

	public Firebase.Auth.FirebaseAuth auth;

	// Use this for initialization
	void Start () {
		DontDestroyOnLoad(gameObject);
		InitializeFirebase ();
	}

	void InitializeFirebase(){
		auth = Firebase.Auth.FirebaseAuth.DefaultInstance;
		auth.StateChanged += AuthStateChanged;
		AuthStateChanged (this, null);
	}
		
	void AuthStateChanged(object sender, EventArgs eventArgs){

	}
		
}
