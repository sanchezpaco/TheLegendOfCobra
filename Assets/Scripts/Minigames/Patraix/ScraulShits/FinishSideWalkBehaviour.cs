using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishSideWalkBehaviour : MonoBehaviour {

	#region Public properties

	public ScraulShitsManager scraulShitsManager;
	public delegate void OnGameWinEvent();
	public event OnGameWinEvent OnGameWin;

	#endregion

	#region Private properties

	private bool gameWon;

	#endregion

	#region Monobehaviour Components

	private void OnTriggerEnter2D(Collider2D col) {

		if (col.tag == "Player" && !this.gameWon) {
			if (this.scraulShitsManager.AreAllShitsDestroyed ()) {
				this.gameWon = true;
				this.OnGameWin ();
			}
		}

	}

	#endregion
}
