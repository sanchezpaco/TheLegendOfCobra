using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class RoadBehaviour : MonoBehaviour {

	#region Public properties
	public List<LaneBehaviour> lanes;

	public delegate void EnableLanesEvent();
	public event EnableLanesEvent EnableLanes;
	public delegate void DisableLanesEvent();
	public event DisableLanesEvent DisableLanes;
	#endregion

	#region Private properties

	#endregion



	#region Monobehaviour Components
	void Awake() {
		this.lanes = new List<LaneBehaviour> ();
	}

	void Start () {
		this.GetLanes ();
	}
	#endregion

	#region Public methods

	public void ActivateLanes() {
		this.EnableLanes();
	}

	public void DeactivateLanes() {
		this.DisableLanes();
	}

	public void ThrowPrizeCar() {
		Debug.Log ("throwing prize car");
		//Second lane
		//Prefab with the prize behind
		//The car detects itself if its a prize car and when it reach the middle of the screen, drops the prize
	}

	#endregion

	#region Private methods

	private void GetLanes() {
		Transform lanesObject = transform.Find ("Lanes");
		int lanesCount = lanesObject.transform.childCount;
		LaneBehaviour lane = null;
		for (int i = 0; i < lanesCount; i++) {
			lane = lanesObject.transform.GetChild (i).GetComponent<LaneBehaviour> ();
			this.EnableLanes += lane.EnableLane;
			this.DisableLanes += lane.DisableLane;
			this.lanes.Add(lane);
		}
	}

	#endregion
}
