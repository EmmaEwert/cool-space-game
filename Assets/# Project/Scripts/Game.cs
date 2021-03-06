﻿using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Game : MonoBehaviour {
	[Header("Assets")]
	public GameObject asteroidPrefab;
	public GameObject warningPrefab;
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
		SetupWarnings();
		OnPlayerDeath();
	}

	private void Update(){
		CheckForNextAsteroidWave();
	}


	private void SetupWarnings(){
		//foreach (AsteroidWave item in waves){}{
		for(int i=0;i<waves.Count;i++){
			var item = waves[i];
			var rotation = Quaternion.Euler(item.eulerOrigin);

			//why is this null?
			item.warningIndicator = Instantiate(warningPrefab, transform.position, rotation);
			// its because struct is a data object and not a class object
			// or something
			waves[i] = item;

		}
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
		[HideInInspector]
		public GameObject warningIndicator;
    }

	private bool CheckForNextAsteroidWave(){
		
		//check for incoming warnings first
		for(int i=0;i<waves.Count;i++){
			
			var wave = waves[i];
			float timeUntilSpawn = (Time.time - waveStartTime) - wave.time;
			bool comingSoon = timeUntilSpawn < 1;
			bool hasAppeared = timeUntilSpawn < -2;
			
			//im sleepy and this works
			wave.warningIndicator.SetActive(comingSoon && !hasAppeared);

		}



		// dont even think about out of bounds exception
		if(numWavesSpawned == waves.Count) return false;
		
		AsteroidWave nextWave = waves[numWavesSpawned];
		if(nextWave.time < Time.time - waveStartTime){
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
