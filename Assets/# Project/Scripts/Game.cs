using UnityEngine;

public class Game : MonoBehaviour {
	[Header("Assets")]
	public GameObject asteroidPrefab;

	public void OnPlayerDeath() {
		Debug.Log("Player Death");
	}

	private void Start() {
		var rotation = Quaternion.Euler(45f, 0f, 0f);
		for (var i = 0; i < 16; ++i) {
			var deltaRotation = Quaternion.Euler(Random.Range(-15f, 15f), Random.Range(-15f, 15f), 0f);
			Instantiate(asteroidPrefab, transform.position, rotation * deltaRotation);
		}
	}
}
