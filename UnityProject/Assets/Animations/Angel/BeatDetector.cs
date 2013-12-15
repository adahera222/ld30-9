using UnityEngine;
using System.Collections;

public class BeatDetector : MonoBehaviour {
	public static BeatDetector instance;
	public AudioSource source;
	public AudioClip basicClip;
	public string url;
	public bool isLoading;
	float[] sample = new float[64];
	float[] subSamples = new float[64];
	float[] peaks = new float[64];
	public SpriteRenderer[] stars;
	public GameObject starPrefab;
	public SpawnPoint[] points;
	public float bitDelta = 0.1f;
	public float falloffSpeed = 0.25f;
	public SpriteRenderer pentagram;
	public float lifeTime = 0f;
	// Update is called once per frame
	public float progressTime;

	void Awake(){
		instance = this;
		for(int i = 0; i<64; i++){
			Vector3 pos = Camera.main.ScreenToWorldPoint(new Vector2(Random.Range(0f,Screen.width),Random.Range(Screen.height/2f,Screen.height)));
			pos.z = 0f;
			var go = (GameObject)Instantiate(starPrefab,pos,Quaternion.identity);
			stars[i] = go.GetComponent<SpriteRenderer>();
		}
	}

	public float GetProgress(){
		return progressTime/source.clip.length;
	}

	void Update () {
		if(source.clip!= null){
			float m = 0f;
			lifeTime+=Time.deltaTime;
			progressTime += Time.deltaTime;
			source.GetSpectrumData(sample,1,FFTWindow.Rectangular);
			for (int i = 0; i < subSamples.Length; i++) {
				m = Mathf.Max(m,sample[i]);
				subSamples[i] = sample[i];
				stars[i].color = Color.Lerp(Color.white,Color.red,subSamples[i%4]);
				if(subSamples[i] - peaks[i]>bitDelta){
					peaks[i] = subSamples[i];
					points[Random.Range(0,points.Length)].Spawn();
				}else{
					peaks[i] *= falloffSpeed * Time.deltaTime;
				}
			}
			pentagram.color = Color.Lerp(Color.white,Color.red,m);
		}

	}

	void OnDrawGizmos(){
		Gizmos.color = Color.red;
		for(int i = 0; i< subSamples.Length; i++){
			Gizmos.DrawCube(Vector3.right*i,new Vector3(1,subSamples[i]*2f,0f));
		}
	}

	void OnGUI(){
		if(!source.isPlaying && !isLoading){
			GUILayout.Label("Set url to your mp3 file(not in webplayer), or play builtIn track");
#if !UNITY_WEBPLAYER
			url = GUILayout.TextField(url);
			if(GUILayout.Button("Upload")){
				StartCoroutine(LoadTrack());
			}
#endif
			if(GUILayout.Button("Play BuiltIn")){
				source.clip = basicClip;
				source.Play();
				progressTime = 0f;
			}
		}else{
			GUILayout.Label("Lifetime: " + lifeTime + " s");
		}
	}

	IEnumerator LoadTrack(){
		isLoading = true;
		WWW w = new WWW(url);
		yield return w;
		source.clip = w.audioClip;
		isLoading = false;
		source.Play();
		progressTime = 0f;
	}
}
