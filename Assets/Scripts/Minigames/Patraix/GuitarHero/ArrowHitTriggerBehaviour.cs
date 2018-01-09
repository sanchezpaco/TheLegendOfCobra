using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowHitTriggerBehaviour : MonoBehaviour
{

	#region Public properties

	public GuitarHero guitarHero;

	#endregion

	#region Private properties

	private bool canCheckKeys = false;

	private ArrowBehaviour currentLeftArrow;
	private ArrowBehaviour currentDownArrow;
	private ArrowBehaviour currentUpArrow;
	private ArrowBehaviour currentRightArrow;

	#endregion


	#region MonoBehaviour Components

	void Update ()
	{

		if (this.canCheckKeys) {
			this.CheckKeys ();
		}
		
	}

	private void OnTriggerEnter2D (Collider2D col)
	{
		
		if (this.IsAnArrow (col.tag)) {

			this.SetArrow (col, false);

		}

	}

	private void OnTriggerExit2D (Collider2D col)
	{

		if (this.IsAnArrow (col.tag)) {

			this.SetArrow (col, true);

		}

	}

	#endregion

	#region Public methods

	public void EnableKeyChecking() {

		this.canCheckKeys = true;
	}

	public void DisableKeyChecking() {

		this.canCheckKeys = false;
	}

	#endregion

	#region Private methods

	private void CheckKeys ()
	{

		if (Input.GetKeyDown (KeyCode.LeftArrow)) {

			if (this.currentLeftArrow != null) {
				this.currentLeftArrow.Hit ();
			} else {
				this.NotifyError ();
			}
		}

		if (Input.GetKeyDown (KeyCode.DownArrow)) {
			
			if (this.currentDownArrow != null) {
				this.currentDownArrow.Hit ();
			} else {
				this.NotifyError ();
			}
		}

		if (Input.GetKeyDown (KeyCode.UpArrow)) {

			if (this.currentUpArrow != null) {
				this.currentUpArrow.Hit ();
			} else {
				this.NotifyError ();
			}
		}

		if (Input.GetKeyDown (KeyCode.RightArrow)) {

			if (this.currentRightArrow != null) {
				this.currentRightArrow.Hit ();
			} else {
				this.NotifyError ();
			}
		}

	}

	private void SetArrow (Collider2D col, bool unset)
	{

		ArrowBehaviour arrow = col.GetComponent<ArrowBehaviour> ();

		int arrowType = arrow.arrowType;

		switch (arrowType) {
			
		case 0: //left
			this.currentLeftArrow = (!unset)?arrow:null;
			break;

		case 1: //down
			this.currentDownArrow = (!unset)?arrow:null;
			break;

		case 2: //up
			this.currentUpArrow = (!unset)?arrow:null;
			break;

		case 3: //right
			this.currentRightArrow = (!unset)?arrow:null;
			break;

		}

	}

	private bool IsAnArrow (string tag)
	{

		return tag == "Arrow";

	}

	private void NotifyError() {

		this.guitarHero.HitError ("key");
	}

	#endregion
}
