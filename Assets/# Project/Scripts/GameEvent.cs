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
		var copy = new GameEventListener[listeners.Count];
		listeners.CopyTo(copy);
		foreach (var listener in copy) {
			listener.onRaise.Invoke();
		}
	}
}
