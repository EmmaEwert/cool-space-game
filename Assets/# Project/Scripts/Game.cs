using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Game : MonoBehaviour {
	[Header("Assets")]
	public GameObject asteroidPrefab;
	public GameEvent newLifeEvent;
	private List<GameObject> asteroids = new List<GameObject>();

	public void OnPlayerDeath() {
		while (asteroids.Count > 0) {
			DestroyImmediate(asteroids.First().gameObject);
			asteroids.RemoveAt(0);
		}
		asteroids.Clear();
		numWavesSpawned = 0;
		waveStartTime = Time.time;
		NextAsteroidWave();

		newLifeEvent.Raise();
		
	}

	private void Start() {
		OnPlayerDeath();
	}

	private void Update(){
		CheckForNextAsteroidWave();
	}


	public float waveStartTime;
	public int numWavesSpawned = 0;
	public List<AsteroidWave> waves;
	[System.Serializable]
    public struct AsteroidWave
    {
        public Vector3 eulerOrigin;
		public int quantity;
		public float time;
    }

	private bool CheckForNextAsteroidWave(){
		
		// dont even think about out of bounds exception
		if(numWavesSpawned == waves.Count) return false;
		
		AsteroidWave wave = waves[numWavesSpawned];
		if(wave.time < Time.time - waveStartTime){
			NextAsteroidWave();
			return true;
		}
		return false;
	}

	private void NextAsteroidWave(){

		AsteroidWave wave = waves[numWavesSpawned];
		
		var rotation = Quaternion.Euler(wave.eulerOrigin);
		Random.InitState(1);
		for (var i = 0; i < wave.quantity; ++i) {
			var deltaRotation = Quaternion.Euler(Random.Range(-15f, 15f), Random.Range(-15f, 15f), 0f);
			var asteroid = Instantiate(asteroidPrefab, transform.position, rotation * deltaRotation);
			asteroids.Add(asteroid);
		}
		numWavesSpawned++;
	}
}
