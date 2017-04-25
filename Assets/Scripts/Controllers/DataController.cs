using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DataController : MonoBehaviour {

	public User user;

	// Use this for initialization
	void Start () {
		DontDestroyOnLoad(gameObject);
		SceneManager.LoadScene ("AuthScene");
	}
}
