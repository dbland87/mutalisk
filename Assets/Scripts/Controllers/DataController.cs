using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DataController : MonoBehaviour {

	public User user;
    public List<Unit> unitlist;

	// Use this for initialization
	void Start () {
		DontDestroyOnLoad(gameObject);
		SceneManager.LoadScene ("temp");
	}
}
