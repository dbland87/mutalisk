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
    private static string unitsURL = apiURL + "/units/";

    //Events
    public delegate void EventHandler(string str);
	public event EventHandler UserReceivedEvent;
	public event EventHandler TokenPostedEvent;
    public event EventHandler UnitsReceivedEvent;


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
			TokenPostedEvent (request.downloadHandler.text);
			Debug.Log (request.downloadHandler.text);
		}
	}

	public void retrieveUser(string username){
		User user = new User();
		user.name = username;
		RetrieveUserWrapper wrapper = new RetrieveUserWrapper ();
		wrapper.user = user;
		string json = JsonUtility.ToJson (wrapper);
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

    // -----------------------  Retrieve Bag Start  ----------------------- //

    public void retrieveBag(User user)
    {
        RetrieveBagWrapper wrapper = new RetrieveBagWrapper();
        wrapper.user = user;
        string json = JsonUtility.ToJson(wrapper);
        UnityWebRequest request = new UnityWebRequest(unitsURL, UnityWebRequest.kHttpVerbPOST);
        byte[] bodyRaw = Encoding.UTF8.GetBytes(json);
        request.uploadHandler = (UploadHandler)new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");

        StartCoroutine(PostUnitsEnumerator(request));
    }

    IEnumerator PostUnitsEnumerator(UnityWebRequest request)
    {
        yield return request.Send();
        if (request.isError)
        {
            Debug.Log(request.error);
        }
        else
        {
            Debug.Log(request.downloadHandler.text);
            UnitsReceivedEvent(request.downloadHandler.text);
        }
    }

    // -----------------------  Retrieve Bag End  ----------------------- //

}
