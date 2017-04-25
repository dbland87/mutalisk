using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Login : MonoBehaviour {

	private DataController dataController;
	private NetController netController;
	private string usernameText;
	private string fcmToken;
	private bool tokenReceived;
	private bool authenticated = true;

	void Start(){
		//Get components
		dataController = GameObject.Find("DataController").GetComponent<DataController>();
		netController = GameObject.Find ("NetController").GetComponent<NetController> ();

		//Initialize firebase, auto requests token
		Firebase.Messaging.FirebaseMessaging.TokenReceived += OnTokenReceived;

		//TODO Delete me
		netController.UserReceivedEvent += OnUserReceived;
		netController.retrieveUser("Dingus");
	}

	public void onClickLogin(){
		usernameText = GameObject.Find("UnInput").GetComponent<InputField>().text;

		//TODO Authenticate against api

		if (usernameText.Length != 0 && authenticated) {
			//Submit supplied user data to get complete user object
			netController.UserReceivedEvent += OnUserReceived;
			netController.retrieveUser(usernameText);

			//UserModel userModel = new UserModel (1, usernameText, fcmToken);

			SceneManager.LoadScene ("MainScene");
		} else {
			//TODO Tell 'em nope
		}
	}

	public void OnTokenReceived(object sender, Firebase.Messaging.TokenReceivedEventArgs token) {
		tokenReceived = true;
		fcmToken = token.Token;
	}

	static void OnUserReceived(string str){
		Debug.Log (str);
	}

	static void onTokenReceived(string str){
		Debug.Log (str);
	}
}
