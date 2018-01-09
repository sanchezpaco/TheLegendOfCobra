using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScraulShitsManager : MonoBehaviour {

	#region Public properties

	public delegate void OnShitDestroyedEvent();
	public event OnShitDestroyedEvent OnShitDestroyed;

	#endregion

	#region Private properties

	private List<ScraulShitBehaviour> scraulShits;

	#endregion

	#region Monobehaviour Components

	// Use this for initialization
	void Awake () {

		this.scraulShits = new List<ScraulShitBehaviour> ();

		this.EnableShits ();

	}

	#endregion

	#region Public methods

	public bool AreAllShitsDestroyed() {

		return this.scraulShits.Count == 0;
	}

	#endregion

	#region Private methods

	private void EnableShits() {

		Transform shits = GameObject.Find ("Shits").transform;
		float shitsCount = shits.childCount;
		ScraulShitBehaviour scraulShit = null;;

		for (int i = 0; i < shitsCount; i++) {

			scraulShit = shits.GetChild (i).GetComponent<ScraulShitBehaviour> ();
			scraulShit.OnDestroy += this.ScraulShitDestroyed;
			scraulShits.Add (scraulShit);
		}

	}

	private void ScraulShitDestroyed(ScraulShitBehaviour scraulShit) {

		scraulShit.OnDestroy -= this.ScraulShitDestroyed;
		this.scraulShits.Remove (scraulShit);
		this.OnShitDestroyed ();
	}

	#endregion
}
