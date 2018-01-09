using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Managers;
using Common;

public class ShitBehaviour : MonoBehaviour {

	#region Public properties

	public bool exploded;
	public float timeToDisappear = 3;

	#endregion

	#region Private properties

	private PoolManager shitPool;
	private ResizeTo resizeTo;
	private Vector2 initSize;

	#endregion

	#region Monobehaviour Components


	void Awake() {
		this.initSize = this.transform.localScale;
	}


	void Start () {
		
		this.resizeTo = this.GetComponent<ResizeTo> ();
		this.shitPool = this.GetComponentInParent<PoolManager> ();

	}

	#endregion

	#region Public methods

	public void Explode() {
		
		this.exploded = true;
		this.resizeTo.ResizeToSize (this.transform.localScale * (2f + Random.Range(0f, 1f)), 2);
		this.StartCoroutine (this.Disappear ());

	}

	#endregion

	#region Private methods

	IEnumerator Disappear ()
	{

		yield return new WaitForSeconds (this.timeToDisappear + Random.Range(0f, 3f));
		this.exploded = false;
		this.transform.localScale = this.initSize;
		this.shitPool.Save (this.gameObject);


	}

	#endregion
}
