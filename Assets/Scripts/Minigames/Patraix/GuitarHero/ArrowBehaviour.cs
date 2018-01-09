using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;
using Managers;

public class ArrowBehaviour : MonoBehaviour {

	#region Public properties

	public delegate void OnHitEvent(ArrowBehaviour arrow, bool perfect);
	public event OnHitEvent OnHit;

	public delegate void OnErroSaveEvent(string errorType);
	public event OnErroSaveEvent OnErrorSave;

	public int arrowType;

	#endregion

	#region Private properties

	private PoolManager arrowPool;
	private MoveTo moveTo;

	#endregion

	#region MonoBehaviour Components

	void Awake() {

		this.moveTo = this.GetComponent<MoveTo> ();
	}

	void Start() {
		
		this.arrowPool = this.GetComponentInParent<PoolManager> ();
	}

	#endregion

	#region Public methods

	public void Move(float speed, float destination) {

		this.moveTo.MoveToPosition (new Vector2 (this.transform.localPosition.x, destination), speed);
		this.moveTo.OnMove += this.SaveArrow;
	}

	public void Hit() {

		float yPosition = this.transform.localPosition.y;
		bool perfect = false;

		if (yPosition < -0.4f && yPosition > -1.2f) {
			perfect = true;
		}

		this.OnHit (this, perfect);

		this.ReturnArrowToPool ();
	}

	#endregion

	#region Private methods

	private void SaveArrow() {

		this.moveTo.OnMove -= this.SaveArrow;
		this.OnErrorSave ("floor");
		this.ReturnArrowToPool ();
	}

	private void ReturnArrowToPool() {
		this.moveTo.OnMove -= this.SaveArrow;
		this.arrowPool.Save (this.gameObject);
	}

	#endregion
}
