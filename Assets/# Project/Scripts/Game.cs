using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Game : MonoBehaviour {
	[Header("Assets")]
	public GameObject asteroidPrefab;

	private List<GameObject> asteroids = new List<GameObject>();

	public void OnPlayerDeath() {
		while (asteroids.Count > 0) {
			DestroyImmediate(asteroids.First().gameObject);
			asteroids.RemoveAt(0);
		}
		asteroids.Clear();
		var rotation = Quaternion.Euler(45f, 0f, 0f);
		Random.InitState(1);
		for (var i = 0; i < 64; ++i) {
			var deltaRotation = Quaternion.Euler(Random.Range(-15f, 15f), Random.Range(-15f, 15f), 0f);
			var asteroid = Instantiate(asteroidPrefab, transform.position, rotation * deltaRotation);
			asteroids.Add(asteroid);
		}
	}

	private void Start() {
		OnPlayerDeath();
	}
}
