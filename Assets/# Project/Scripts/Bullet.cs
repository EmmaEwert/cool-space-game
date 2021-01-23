using UnityEngine;

public class Bullet : MonoBehaviour {
	private const float Speed = 100f;
	private const float Lifetime = 10f;

	private float life;

	private void Update() {
		transform.Rotate(Time.deltaTime * Speed, 0f, 0f, Space.Self);
		life += Time.deltaTime;
		if (life >= Lifetime) {
			Destroy(gameObject);
		}
	}
}
