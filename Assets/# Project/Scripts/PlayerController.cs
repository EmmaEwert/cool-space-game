using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour {
	private const float Speed = 75f;

	[Header("Assets")]
	public Bullet bulletPrefab;
	public GhostController ghostPrefab;
	public GameEvent playerDeathEvent;

	[Header("References")]
	public Transform model;

	private new Camera camera => Camera.main;
	private float phase;
	private List<Snapshot> snapshots = new List<Snapshot>();
	private float timestamp;
	private Vector3 lastPosition;
	private Quaternion startRotation;

	private void Start() {
		timestamp = Time.time;
		lastPosition = model.position;
		startRotation = transform.rotation;
	}

	private void Update() {
		var input = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
		input = Vector2.ClampMagnitude(input, 1f);

		// Movement
		var movement = new Vector3(input.y, -input.x, 0) * Time.deltaTime * Speed;
		var speed = (180f - Quaternion.Angle(transform.localRotation, Quaternion.Euler(movement))) / 180f * Speed;
		transform.Rotate(movement, Space.Self);

		// Alignment
		var inputPosition = model.position + transform.TransformDirection(input.normalized);
		model.LookAt(inputPosition, -transform.forward);

		// Bullets
		var velocity = model.position - lastPosition;
		lastPosition = model.position;
		if (phase <= 0f && Input.GetButton("Fire1")) {
			phase = 0.1f;
			var normalizedPosition = (Input.mousePosition - new Vector3(0.5f * Screen.width, 0.5f * Screen.height, 0f)).normalized;
			var angle = Vector2.SignedAngle(Vector2.up, normalizedPosition);
			var bullet = Instantiate(bulletPrefab, transform.position, transform.rotation * Quaternion.Euler(0f, 0f, angle));
			bullet.speed = 0f * speed;
			snapshots.Add(new Snapshot {
				timestamp = Time.time - timestamp,
				rotation = transform.rotation,
				fireRotation = transform.rotation * Quaternion.Euler(0f, 0f, angle),
				fireSpeed = speed,
				death = false,
			});
		} else {
			phase -= Time.deltaTime;
		}


		var colliders = Physics.OverlapSphere(model.position, model.localScale.x * 0.5f);
		foreach (var collider in colliders) {
			if (collider.GetComponentInParent<CanKillPlayer>()) {
				snapshots.Add(new Snapshot {
					timestamp = Time.time - timestamp,
					rotation = transform.rotation,
					fireRotation = Quaternion.identity,
					death = true
				});
				var ghost = Instantiate(ghostPrefab, Vector3.zero, Quaternion.identity);
				ghost.snapshots = snapshots;
				snapshots = new List<Snapshot>();
				//transform.rotation = Quaternion.identity;
				transform.rotation = startRotation;
				playerDeathEvent.Raise();
				timestamp = Time.time;
				return;
			}
		}

		snapshots.Add(new Snapshot {
			timestamp = Time.time - timestamp,
			rotation = transform.rotation,
			fireRotation = Quaternion.identity,
			death = false
		});


		// main menu check
		if(Input.GetKeyDown(KeyCode.Escape)){
			SceneManager.LoadScene("main menu", LoadSceneMode.Single);
		}

	}
}
