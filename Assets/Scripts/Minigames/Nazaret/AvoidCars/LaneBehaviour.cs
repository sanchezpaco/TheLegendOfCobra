using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Managers;
using Common;

public class LaneBehaviour : MonoBehaviour {

	#region Public properties

	public PoolManager carsPool;
	public float carDelay = 1;
	public float laneDirection = 1;
	public float laneVelocity = 3f;

	public bool canSpawnCars = false;

	#endregion

	#region Private properties

	#endregion

	#region Monobehaviour Components

	void Awake () {
		this.carsPool = this.GetComponent<PoolManager> ();
		this.carsPool.parent = this.transform;
	}

	#endregion

	#region Public Methods
	public void SpawnCar() {
		if (this.canSpawnCars) {
			GameObject carToSpawn = this.carsPool.Get ();
			float xDelay = Random.Range (0, this.carDelay) * this.laneDirection;
			float xPosition = 0.7f * (this.laneDirection * -1) - xDelay;

			carToSpawn.transform.localPosition = new Vector2 (xPosition, 0f);
			carToSpawn.SetActive (true);

			MoveTo carMove = carToSpawn.GetComponent<MoveTo> ();
			carMove.MoveToPosition (new Vector2 (0.7f * this.laneDirection, 0), this.laneVelocity);
			carMove.OnMove += carToSpawn.GetComponent<CarBehaviour> ().SaveCar;
			carMove.OnMove -= this.SpawnCar;
			carMove.OnMove += this.SpawnCar;
		}
	}

	public void EnableLane() {
		this.canSpawnCars = true;
		this.SpawnCar ();
	}

	public void DisableLane() {
		this.canSpawnCars = false;
	}

	#endregion

}
