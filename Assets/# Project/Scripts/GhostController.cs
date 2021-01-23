using System.Collections.Generic;
using UnityEngine;

public class GhostController : MonoBehaviour {
	[Header("Assets")]
	public GameObject bulletPrefab;

	public List<Snapshot> snapshots;

	private float timestamp;
	private Queue<Snapshot> snapshotQueue;

	public void OnPlayerDeath() {
		snapshotQueue = new Queue<Snapshot>(snapshots);
		timestamp = Time.time;
		GetComponentInChildren<Renderer>().enabled = true;
	}

	private void Start() {
		timestamp = Time.time;
		snapshotQueue = new Queue<Snapshot>(snapshots);
	}

	private void Update() {
		while (snapshotQueue.Count > 0 && snapshotQueue.Peek().timestamp < Time.time - timestamp) {
			var snapshot = snapshotQueue.Dequeue();
			transform.rotation = snapshot.rotation;
			if (snapshot.fireRotation != Quaternion.identity) {
				Instantiate(bulletPrefab, transform.position, snapshot.fireRotation);
			}
			if (snapshot.death) {
				GetComponentInChildren<Renderer>().enabled = false;
			}
		}
	}
}
