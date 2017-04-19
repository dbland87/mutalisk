using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class UserModel {

	public int id;
	public string username;
	public string fcmToken;

	public UserModel(int id, string username, string fcmToken){
		this.id = id;
		this.username = username;
		this.fcmToken = fcmToken;
	}

}
