using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScraulShitsCobraPlayerBehaviour : MonoBehaviour {

	#region Public properties

	#endregion

	#region Private properties
	#endregion


	#region Monobehaviour Components

	void Start () {
		
	}
		
	private void OnTriggerStay2D(Collider2D col) {

		if (col.tag == "ScraulShit") {

			ScraulShitBehaviour scraulShit = col.GetComponent<ScraulShitBehaviour> ();

			if (Input.GetKeyDown (KeyCode.J)) {
				scraulShit.Hit ();
			}

		}
			
	}

	#endregion

	#region Public methods
	#endregion

	#region Private methods
	#endregion
}
