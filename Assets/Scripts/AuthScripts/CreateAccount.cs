using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CreateAccount : MonoBehaviour {

	private static string SCENE_AUTH = "AuthScene";

	private static DataController dataController;
	private static NetController netController;
	private static AuthController authController;

	private string emailText;
	private string usernameText;
	private string passwordText;

	void Start () {
		//Get components
		dataController = GameObject.Find("DataController").GetComponent<DataController>();
		netController = GameObject.Find ("NetController").GetComponent<NetController> ();
		authController = GameObject.Find ("AuthController").GetComponent<AuthController> ();

		//Event Subscriptions
		netController.UserReceivedEvent += OnUserReceived;
	}

	public void onClickSubmit(){

		//TODO Need to validate username choice before continuing

		emailText = GameObject.Find("EmailInput").GetComponent<InputField>().text;
		usernameText = GameObject.Find("UnInput").GetComponent<InputField>().text;
		passwordText = GameObject.Find("PwInput").GetComponent<InputField>().text;

		authController.auth.CreateUserWithEmailAndPasswordAsync(emailText, passwordText).ContinueWith(task => {
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

			//Get user object, will trigger onUserReceived()
			netController.retrieveUser(newUser.DisplayName);

			Debug.LogFormat("Firebase user created successfully: {0} ({1})",
				newUser.DisplayName, newUser.UserId);
		});
	}

	static void OnUserReceived(string str){
		Debug.Log (str); 
		authController.user = JsonUtility.FromJson<User> (str);
		SceneManager.LoadScene(SCENE_AUTH);
	}
}
