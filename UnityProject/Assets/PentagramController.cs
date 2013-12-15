using UnityEngine;
using System.Collections;

public class PentagramController : MonoBehaviour {
	public LineRenderer t;
	public Color cDefault;
	public Color c;
	public AudioSource source;
	void OnEnable(){
		t.useWorldSpace = true;
		t.SetPosition(0,transform.position);
		TouchManager.TouchBeganEvent+=OnTap;
	}
	RaycastHit2D info;
	void OnTap (TouchInfo touch)
	{
		Ray r = Camera.main.ScreenPointToRay(touch.position);
		info = Physics2D.Raycast(r.origin,r.direction,100f);
		if(info){
			Enemy en = info.collider.gameObject.GetComponent<Enemy>();
			if(en){
				en.Kill();
			}
		}
		Vector3 pos = Camera.main.ScreenToWorldPoint(touch.position);
		source.Play();
		pos.z = 0f;
		t.SetPosition(1,pos);
		t.SetColors(cDefault,cDefault);
		float d = Vector3.Distance(transform.position,Camera.main.ScreenToWorldPoint(touch.position));
		t.materials[0].SetTextureScale("_MainTex",new Vector2(d,1));

	}

	void Update(){
		c.a -= Time.deltaTime*0.5f;
		t.SetColors(c,c);
	}

	void OnDisable(){
		TouchManager.TouchBeganEvent-=OnTap;
	}
}
