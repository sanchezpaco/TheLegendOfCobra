using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Managers;
using Common;

public class CarBehaviour : MonoBehaviour {

	#region Public properties

	public PoolManager carsPool;
	public LaneBehaviour laneBehaviour;

	#endregion

	#region Private properties

	private MoveTo moveTo;

	#endregion

	#region Monobehaviour Components
	void Start () {
		this.moveTo = this.GetComponent<MoveTo> ();
		this.carsPool = this.GetComponentInParent<PoolManager> ();
		this.laneBehaviour = this.GetComponentInParent<LaneBehaviour> ();
	}

	void OnTriggerEnter2D(Collider2D col) {
		
		string colName = col.name;

		if (colName == "CarSpawnTrigger") {

			if (colName.Contains ("Left") || colName.Contains ("Right")) {
				this.carsPool.Save (this.gameObject);
			} 
			this.laneBehaviour.SpawnCar ();
		}
			
	}
	#endregion

	#region Public methods

	public void SaveCar() {
		this.moveTo.OnMove -= this.SaveCar;
		this.carsPool.Save (this.gameObject);
	}

	#endregion
}
