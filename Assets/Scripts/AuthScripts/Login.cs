using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Login : MonoBehaviour {

	private static string SCENE_MAIN = "MainScene";
	private static string SCENE_CREATE_ACCOUNT = "CreateAccountScene";
	private static DataController dataController;
	private static NetController netController;
	private static AuthController authController;

	private static bool userReceived;
	private static bool fTokenReceived;
	private static bool fUserAuthenticated;
	private static bool fTokenPosted;

	private string emailText;
	private string passwordText;
	private string fcmToken;

	void Start(){
		//Get components
		dataController = GameObject.Find("DataController").GetComponent<DataController>();
		netController = GameObject.Find ("NetController").GetComponent<NetController> ();
		authController = GameObject.Find ("AuthController").GetComponent<AuthController> ();

		//Initialize firebase, auto requests token
		Firebase.Messaging.FirebaseMessaging.TokenReceived += OnTokenReceived;

		//Event Subscriptions
		netController.UserReceivedEvent += OnUserReceived;
		netController.TokenPostedEvent += onFTokenPosted;

		//If user is already authenticated, advance
//		if (authController.user != null) {
//			SceneManager.LoadScene (SCENE_MAIN);
//		}
	}

	public void onClickLogin(){
		emailText = GameObject.Find("EmailInput").GetComponent<InputField>().text;
		passwordText = GameObject.Find("PwInput").GetComponent<InputField>().text;

		authController.auth.SignInWithEmailAndPasswordAsync (emailText, passwordText).ContinueWith (task => {
			if (task.IsCanceled) {
				Debug.LogError ("SignInWithEmailAndPasswordAsync was canceled.");
				return;
			}
			if (task.IsFaulted) {
				Debug.LogError ("SignInWithEmailAndPasswordAsync encountered an error: " + task.Exception);
				return;
			}

			fUserAuthenticated = true;
			Firebase.Auth.FirebaseUser newUser = task.Result;
			netController.retrieveUser (newUser.DisplayName);

			Debug.LogFormat ("User signed in successfully: {0} ({1})",
				newUser.DisplayName, newUser.UserId);

		});
	}

	public void onClickCreateAccount(){
		SceneManager.LoadScene (SCENE_CREATE_ACCOUNT);
	}

	public void OnTokenReceived(object sender, Firebase.Messaging.TokenReceivedEventArgs token) {
		fTokenReceived = true;
		fcmToken = token.Token;
		advanceScene ();
	}

	static void OnUserReceived(string str){
		Debug.Log (str);
		userReceived = true;
		authController.user = JsonUtility.FromJson<User> (str);
		advanceScene ();
	}

	static void onFTokenPosted(string str){
		fTokenPosted = true;
		Debug.Log (str);
		advanceScene ();
	}

	static void advanceScene(){
		if (fUserAuthenticated && fTokenPosted && userReceived) {
			SceneManager.LoadScene (SCENE_MAIN);
		}
	}
}
