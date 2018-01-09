using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Managers;
using Common;

public class ShitManager : MonoBehaviour {

	#region Public properties

	public bool canDropShits;
	public float timeBetweenShits = 1;
	public float yLimit;
	public float xLimit;
	public float shitVelocity = 4;
	public float shitVelocityIncrement = 0.3f;
	public Transform player;

	#endregion

	#region Private properties

	private float timeSinceLastShit;
	private PoolManager shitPool;

	#endregion


	#region Monobehaviour Components

	void Awake () {

		this.shitPool = this.GetComponent<PoolManager> ();

	}

	void Update () {

		if (this.canDropShits) {

			if (this.timeSinceLastShit > this.timeBetweenShits) {
				
				this.DropShit ();

				this.timeSinceLastShit = 0;
			}

			this.timeSinceLastShit += Time.fixedDeltaTime;
		}

	}

	#endregion

	#region Public methods

	public void IncrementShitFallVelocity() {
		this.shitVelocity += this.shitVelocityIncrement;
		this.timeBetweenShits -= 0.1f;
	}

	public void EnableShitDrop() {
		this.canDropShits = true;
	}

	public void DisableShitDrop() {
		this.canDropShits = false;
	}

	public void DropPrizeShit() {
		Debug.Log ("Dropping prize shit");
	}

	#endregion

	#region Private methods

	private void DropShit() {

		GameObject shit = this.shitPool.Get ();
		MoveTo shitMove = shit.GetComponent<MoveTo> ();
		ShitBehaviour shitBehaviour = shit.GetComponent<ShitBehaviour> ();

		Vector2 playerPosition = this.player.localPosition;
		Vector2 shitPosition = new Vector2 (playerPosition.x, 6);

		shit.transform.localPosition = shitPosition;

		shit.SetActive (true);
		shitMove.OnMove += shitBehaviour.Explode;
		shitMove.MoveToPosition (new Vector2 (playerPosition.x, playerPosition.y), this.shitVelocity + Random.Range(0f,2f));


	}

	#endregion
}
