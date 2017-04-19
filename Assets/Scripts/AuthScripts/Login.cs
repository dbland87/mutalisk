using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Login : MonoBehaviour {

	private string usernameText;
	private string fcmToken;
	private bool tokenReceived;

	void Start(){
		//Initialize firebase
		Firebase.Messaging.FirebaseMessaging.TokenReceived += OnTokenReceived;
		Debug.Log ("Start");
	}

	public void onClickLogin(){
		usernameText = GameObject.Find("UnInput").GetComponent<InputField>().text;

		//TODO Authenticate against api

		if (usernameText.Length != 0 && tokenReceived) {
			//TODO Shouldn't hard code id, need to get from API
			UserModel userModel = new UserModel (1, usernameText, fcmToken);
			DataController dataController = GameObject.Find("DataController").GetComponent<DataController>();
			dataController.userModel = userModel;

			SceneManager.LoadScene ("MainScene");
		} else {
			//TODO Tell 'em nope
		}
	}

	public void OnTokenReceived(object sender, Firebase.Messaging.TokenReceivedEventArgs token) {
		tokenReceived = true;
		fcmToken = token.Token;
	}

}
