using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{

	#region Public properties

	public List<string> possibleThreats;

	public delegate void OnDieEvent ();

	public event OnDieEvent OnDie;

	public delegate void OnWinEvent ();

	public event OnWinEvent OnWin;

	public delegate void OnPrizeGetEvent ();

	public event OnPrizeGetEvent OnPrize;

	#endregion

	#region Private properties

	private StarMovement playerMovement;
	private float initPlayerSpeed;
	private bool slowed;

	#endregion

	#region MonoBehaviour Components

	void Awake ()
	{
		this.playerMovement = GetComponent<StarMovement> ();
		this.initPlayerSpeed = this.playerMovement.speed;
	}

	private void OnTriggerEnter2D (Collider2D col)
	{

		string colTag = col.tag;

		if (col.tag == "WinTrigger") {
			if (this.OnWin != null) {
				this.OnWin ();
			}
		}

		if (col.tag == "Prize") {
			if (this.OnPrize != null) {
				this.OnPrize ();
			}
		}

		this.CheckIfThreatCollided (colTag, col.gameObject);

	}

	private void OnCollisionEnter2D (Collision2D col)
	{

		string colTag = col.transform.tag;

		this.CheckIfThreatCollided (colTag, col.gameObject);
	}

	private void OnTriggerStay2D (Collider2D col)
	{
		
		if (this.CheckSlowableEnemy (col.tag, col.gameObject) && !this.slowed) {
			this.SlowPlayer ();
		}
			
	}

	private void OnTriggerExit2D (Collider2D col)
	{

		if (col.tag == "EnemyShit" && this.slowed) {

			this.RemoveSlowPlayer ();

		}

	}

	#endregion

	#region Private methods

	private void Die ()
	{
		this.OnDie ();
	}

	private void CheckIfThreatCollided (string threat, GameObject threatObject)
	{
		if (this.possibleThreats.Contains (threat)) {

			if (threat == "EnemyShit") {
				ShitBehaviour enemyShit = threatObject.GetComponent<ShitBehaviour> ();
				if (!enemyShit.exploded) {
					this.Die ();
				}
			} else {
				this.Die ();
			}
		}
	}

	private bool CheckSlowableEnemy (string threat, GameObject threatObject)
	{

		if (threat == "EnemyShit") {
			ShitBehaviour enemyShit = threatObject.GetComponent<ShitBehaviour> ();
			if (enemyShit.exploded) {
				return true;
			}
		}

		return false;
	}

	private void SlowPlayer ()
	{
		
		this.slowed = true;
		this.playerMovement.speed /= 4;

	}

	private void RemoveSlowPlayer ()
	{

		this.slowed = false;
		this.playerMovement.speed = this.initPlayerSpeed;

	}

	#endregion
}
