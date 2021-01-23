using UnityEngine;
using UnityEngine.Events;

public class GameEventListener : MonoBehaviour {
	public GameEvent @event;
	public UnityEvent onRaise;

	private void OnEnable() {
		@event.Register(this);
	}

	private void OnDisable() {
		@event.Unregister(this);
	}
}