using UnityEngine;
using System.Collections;

public class Test_LoadBin : MonoBehaviour {

	// Use this for initialization
	void Start () {
        StartCoroutine(LoadConfigManager.LoadConfigData());
	}
	
	// Update is called once per frame
	void Update () {
        if (LoadConfigManager.allConfigLoaded)
        {
            LoadConfigManager.allConfigLoaded = false;
            ConfigDataManager.GetInstance().TestPrint();
        }
	}
}
