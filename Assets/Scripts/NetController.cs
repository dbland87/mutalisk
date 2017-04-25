using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System.Text;

public class NetController : MonoBehaviour {

	//URL's
	private static string apiURL = "http://54.67.43.54:8000";
	private static string tokenURL = apiURL + "/token/";
	private static string usersURL = apiURL + "/users/";

	//Events
	public delegate void EventHandler(string str);
	public event EventHandler UserReceivedEvent;
	//public event EventHandler TokenReceivedEvent;


	void Start () {
		DontDestroyOnLoad(gameObject);
	}

	public void postFcmToken(FcmTokenModel fcmTokenModel){
		string json = JsonUtility.ToJson (fcmTokenModel);
		UnityWebRequest request = new UnityWebRequest(tokenURL);
		byte[] bodyRaw = Encoding.UTF8.GetBytes (json);
		request.uploadHandler = (UploadHandler)new UploadHandlerRaw(bodyRaw);
		request.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer ();
		request.SetRequestHeader ("Content-Type", "application/json");

		StartCoroutine (PostFcmTokenEnumerator(request));
	}

	IEnumerator PostFcmTokenEnumerator(UnityWebRequest request) {
		
		yield return request.Send();
		if (request.isError) {
			Debug.Log (request.error);
		} else {
			//TokenReceivedEvent (request.downloadHandler.text);
			Debug.Log (request.downloadHandler.text);
		}
	}

	public void retrieveUser(string username){
		User user = new User();
		user.name = username;
		RetrieveUserWrapper wrapper = new RetrieveUserWrapper ();
		wrapper.user = user;
		string json = JsonUtility.ToJson (wrapper);
		Debug.Log (json);
		UnityWebRequest request = new UnityWebRequest(usersURL, UnityWebRequest.kHttpVerbPOST);
		byte[] bodyRaw = Encoding.UTF8.GetBytes(json);
		request.uploadHandler = (UploadHandler)new UploadHandlerRaw(bodyRaw);
		request.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer ();
		request.SetRequestHeader ("Content-Type", "application/json");

		StartCoroutine (PostUserEnumerator (request));
	}

	IEnumerator PostUserEnumerator(UnityWebRequest request) {
		yield return request.Send();
		if (request.isError) {
			Debug.Log (request.error);
		} else {
			UserReceivedEvent(request.downloadHandler.text);
		}
	}

}
