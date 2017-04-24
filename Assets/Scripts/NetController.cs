using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;

public class NetController : MonoBehaviour {

	//URL's
	private static string apiURL = "http://localhost:8000";
	private static string tokenURL = apiURL + "/token/";
	private static string usersURL = apiURL + "/users/";

	//Events
	public delegate void EventHandler(string str);
	public event EventHandler UserReceivedEvent;


	void Start () {
		DontDestroyOnLoad(gameObject);
	}

	public void postFcmToken(FcmTokenModel fcmTokenModel){
		Hashtable headers = new Hashtable ();
		headers.Add("Content-Type", "application/json");
		string data = JsonUtility.ToJson (fcmTokenModel);
		WWW www = new WWW (tokenURL, Encoding.UTF8.GetBytes(data), headers);


		StartCoroutine (PostFcmTokenEnumerator (www));
	}

	IEnumerator PostFcmTokenEnumerator(WWW www) {
		yield return www;
		if (www.error == null) {
			Debug.Log ("Token submitted!");
		} else {
			Debug.Log (www.error);
		}
	}

	public void retrieveUser(string username){
		Hashtable headers = new Hashtable ();
		headers.Add("Content-Type", "application/json");
		UserModel userModel = new UserModel ();
		userModel.username = username;
		string data = JsonUtility.ToJson (userModel);
		WWW www = new WWW (usersURL, Encoding.UTF8.GetBytes(data), headers);

		StartCoroutine (PostUserEnumerator (www));
	}

	IEnumerator PostUserEnumerator(WWW www) {
		yield return www;
		if (www.error == null) {
			UserReceivedEvent(www.text);
		}
	}

}
