using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScraulShitBehaviour : MonoBehaviour
{

	#region Public properties

	public float hitsNeededToBeTaken = 1;
	public bool taken;
	public float sizeToScale = 0.2f;
	public float timesHit;

	public delegate void OnDestroyEvent (ScraulShitBehaviour scraulShit);
	public event OnDestroyEvent OnDestroy;

	#endregion

	#region Private properties

	private bool canBeHit = true;

	#endregion

	#region Monobehaviour Components

	// Use this for initialization
	void Awake ()
	{
		
		this.ScaleByNeededHits ();
	}

	#endregion

	#region Public methods

	public void Hit ()
	{
		
		if (this.canBeHit) {
			
			this.timesHit++;
			this.canBeHit = false;

			if (this.timesHit >= this.hitsNeededToBeTaken) {
				this.Destroy ();
			} else {
				StartCoroutine (this.EnableHit ());
			}
		}
	}

	#endregion

	#region Private methods

	private void ScaleByNeededHits ()
	{

		if (this.hitsNeededToBeTaken > 1) {
			
			Vector2 currentScale = this.transform.localScale;

			this.transform.localScale = new Vector2 (
				currentScale.x + (this.sizeToScale * this.hitsNeededToBeTaken),
				currentScale.y + (this.sizeToScale * this.hitsNeededToBeTaken)
			);
		}
	}

	private void Destroy ()
	{
		
		this.OnDestroy (this);
		this.gameObject.SetActive (false);
	}

	private IEnumerator EnableHit ()
	{
		
		yield return new WaitForSeconds (0.05f);
		this.canBeHit = true;
	}

	#endregion
}
