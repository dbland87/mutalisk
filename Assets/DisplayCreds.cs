using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayCreds : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Text usernameText = GameObject.Find ("UsernameText").GetComponent<Text>();
		Text idText = GameObject.Find ("UserIdText").GetComponent<Text>();
		Text tokenText = GameObject.Find ("TokenText").GetComponent<Text>();

//		usernameText.text = GameObject.Find ("DataController").GetComponent<DataController> ().userModel.username;
//		idText.text = GameObject.Find ("DataController").GetComponent<DataController> ().userModel.id.ToString();
//		tokenText.text = GameObject.Find ("DataController").GetComponent<DataController> ().userModel.fcmToken;
	}
}
