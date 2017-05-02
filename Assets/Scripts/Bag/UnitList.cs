using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitList : MonoBehaviour {

    private static NetController netController;
    private static AuthController authController;
    private static DataController dataController;
    private static RetrieveUnitsWrapper wrapper;
    

	// Use this for initialization
	void Start () {
        netController = GameObject.Find("NetController").GetComponent<NetController>();
        authController = GameObject.Find("AuthController").GetComponent<AuthController>();
        dataController = GameObject.Find("DataController").GetComponent<DataController>();

        netController.UnitsReceivedEvent += OnUnitsReceived;

        //User user = authController.user;

        // testing... delete when done and uncomment above line
        User user = new User();
        user.name = "sr_fallenangel@hotmail.com";
        user.id = 17;
        user.bag = 83;

        netController.retrieveBag(user);

    }
	
	// Update is called once per frame
	void Update () {
		
	}

    static void OnUnitsReceived(string str)
    {
        Debug.Log(str);
        //userReceived = true;
        wrapper = JsonUtility.FromJson<RetrieveUnitsWrapper>(str);
      
        Debug.Log("To json: " + JsonUtility.ToJson(wrapper));

        for (int i = 0; i < wrapper.units.Count; i++)
        {
            //Debug.Log(wrapper.units[i].name);

            dataController.unitlist.Add(wrapper.units[i]);
        }
        //advanceScene();
    }
}
