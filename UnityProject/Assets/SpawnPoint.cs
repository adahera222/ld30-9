using UnityEngine;
using System.Collections;

public class SpawnPoint : MonoBehaviour {
	public Enemy[] enemies;
	public Vector2 maxOffset;
	public float maxSpawndDelta;
	public float minSpawnedDelta;
	public float currD;
	public float lastSpawn;
	public void Spawn(){
		currD = Mathf.Lerp(minSpawnedDelta,maxSpawndDelta,1f-BeatDetector.instance.GetProgress());
		if(Time.timeSinceLevelLoad > lastSpawn+currD){
			lastSpawn = Time.timeSinceLevelLoad;
		Instantiate(enemies[Random.Range(0,enemies.Length)].gameObject,
		            transform.position + new Vector3(Random.Range(-maxOffset.x,maxOffset.x),
					                                 Random.Range(-maxOffset.y,maxOffset.y),
					                                 0f),
		            Quaternion.identity);
		}
	}	
}