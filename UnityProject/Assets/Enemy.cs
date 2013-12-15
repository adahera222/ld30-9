using UnityEngine;

public class Enemy : MonoBehaviour {
	public Transform target;
	public float speed;
	public GameObject deathFX;
	void Awake(){
		target = Hero.instance.transform;
	}

	public void Kill ()
	{
		Destroy(Instantiate(deathFX,transform.position,Quaternion.identity),1f);
		Destroy(gameObject);
	}

	void Update () {
		transform.localScale = new Vector3(transform.position.x > 0 ? 1 : -1,1f,1f);
		transform.position += (target.position-transform.position).normalized*Time.deltaTime*speed;
		if(Vector3.Distance(target.position,transform.position)<0.1f){
			Kill();
			Hero.instance.Hit();
		}
	}


}
