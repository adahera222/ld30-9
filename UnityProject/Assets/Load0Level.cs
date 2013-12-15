using UnityEngine;
using System.Collections;

public class Load0Level : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Resources.UnloadUnusedAssets();
		Application.LoadLevel(0);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
