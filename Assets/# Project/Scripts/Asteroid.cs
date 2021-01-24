using UnityEngine;

public class Asteroid : MonoBehaviour {
	private const float MinSpeed = 24f;
	private const float MaxSpeed = 32f;

	[Header("References")]
	public Transform model;

	private float speed;
	private float health = 4;

	private void Start() {
		transform.Rotate(0f, 0f, Random.Range(0f, 2f * Mathf.PI) * Mathf.Rad2Deg, Space.Self);
		speed = Random.Range(MinSpeed, MaxSpeed);
	}

	private void Update() {
		transform.Rotate(Time.deltaTime * speed, 0f, 0f, Space.Self);

		var colliders = Physics.OverlapSphere(model.position, model.localScale.x * 0.5f);
		foreach (var collider in colliders) {
			if (collider.GetComponentInParent<Bullet>()) {
				Destroy(collider.transform.parent.gameObject);
				if (--health <= 0) {
					Destroy(gameObject);
					break;
				}
			}
		}
	}
}
