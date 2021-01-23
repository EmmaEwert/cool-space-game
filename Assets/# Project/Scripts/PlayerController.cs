using UnityEngine;

public class PlayerController : MonoBehaviour {
	private const float Speed = 75f;

	[Header("Assets")]
	public GameObject bulletPrefab;

	[Header("References")]
	public Transform model;

	private new Camera camera => Camera.main;
	private float phase;

	private void Update() {
		var input = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

		// Movement
		var movement = new Vector3(input.y, -input.x, 0) * Time.deltaTime * Speed;
		transform.Rotate(movement, Space.Self);

		// Alignment
		var inputPosition = model.position + transform.TransformDirection(input.normalized);
		model.LookAt(inputPosition, -transform.forward);

		// Bullets
		if (phase <= 0f && Input.GetButton("Fire1")) {
			phase = 0.25f;
			var normalizedPosition = (Input.mousePosition - new Vector3(0.5f * Screen.width, 0.5f * Screen.height, 0f)).normalized;
			var angle = Vector2.SignedAngle(Vector2.up, normalizedPosition);
			Instantiate(bulletPrefab, transform.position, transform.rotation * Quaternion.Euler(0f, 0f, angle));
		} else {
			phase -= Time.deltaTime;
		}

		var colliders = Physics.OverlapSphere(model.position, model.localScale.x * 0.5f);
		foreach (var collider in colliders) {
			if (collider.GetComponentInParent<Asteroid>()) {
				Destroy(collider.gameObject);
				Destroy(gameObject);
				break;
			}
		}
	}

	private void OnDestroy() {
		Instantiate(gameObject, Vector3.zero, Quaternion.identity);
	}
}
