using UnityEngine;
using System.Collections;

public class Hero : MonoBehaviour {
	public static Hero instance;
	public bool GameOver;
	public SpriteRenderer[] health;
	public int hp; 
	public GameObject gore;
	// Use this for initialization
	void Awake () {
		instance = this;
		hp = health.Length;
	}

	void OnGUI(){
		if(GameOver){
			if(GUI.Button(new Rect(Screen.width/2f,Screen.height/2,100,100),"Restart?")){
				GameOver = false;
				Application.LoadLevel(0);
			}
		}
	}

	public void Hit(){
		hp--;
		if(hp<0){
			GetComponent<SpriteRenderer>().enabled = false;
			GameObject.Instantiate(gore,transform.position,Quaternion.identity);
			GameOver = true;
		}else{
			//health[hp].enabled = false;
			GameObject.Instantiate(gore,health[hp].transform.position,Quaternion.identity);
		}
	}
}
