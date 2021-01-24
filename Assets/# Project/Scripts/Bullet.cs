using UnityEngine;

public class Bullet : MonoBehaviour {
	private const float Speed = 100f;
	private const float Lifetime = 2f;

	public float speed;
	public bool dontDestroy = false;

	private float life;

	public void OnPlayerDeath() {
		Destroy(gameObject);
	}

	private void Update() {
		transform.Rotate(Time.deltaTime * Speed + Time.deltaTime * speed, 0f, 0f, Space.Self);
		life += Time.deltaTime;
		if (life >= Lifetime) {
			if(!dontDestroy) Destroy(gameObject);
		}
	}
}
