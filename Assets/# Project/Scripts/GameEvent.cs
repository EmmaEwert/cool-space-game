using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class GameEvent : ScriptableObject {
	public List<GameEventListener> listeners;

	public void Register(GameEventListener listener) {
		listeners.Add(listener);
	}

	public void Unregister(GameEventListener listener) {
		listeners.Remove(listener);
	}

	public void Raise() {
		foreach (var listener in listeners) {
			listener.onRaise.Invoke();
		}
	}
}
