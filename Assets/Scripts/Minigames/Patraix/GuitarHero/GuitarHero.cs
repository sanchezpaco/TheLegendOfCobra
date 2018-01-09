using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GuitarHero : MonoBehaviour, IMinigame
{


	#region Public properties

	public int pointsToPass = 1000;
	public int pointsByPerfectHit = 2;
	public int pointsMultiplier = 1;
	public int currentPoints = 0;

	public ArrowManager arrowManager;
	public ArrowHitTriggerBehaviour arrowHitTriggerBehaviour;

	#endregion

	#region Private properties

	private int totalPerfectsInARow = 0;

	#endregion

	#region Monobehaviour Components

	// Use this for initialization
	void Start ()
	{
		this.Intro ();	
	}

	#endregion

	#region Public methods

	public void Intro() {
		//Conversation 
		this.EnableGame();
	}

	public void EnableGame() {

		this.arrowManager.StartThrowing ();
		this.arrowHitTriggerBehaviour.EnableKeyChecking ();
	}

	public void GameOver() {
		
		SceneManager.LoadScene (0);
	}

	public void GameWin() {
		
		//Conversation and give piece
	}

	public void GameFinish() {

		this.arrowHitTriggerBehaviour.DisableKeyChecking ();

		//Check points
		if (this.currentPoints >= this.pointsToPass) {
			this.GameWin ();
		} else {
			this.GameOver ();
		}

	}

	public void SumPoints (bool perfect)
	{

		if (perfect) {
			this.totalPerfectsInARow++;
		}

		this.CheckPointsMultiplier ();
		this.currentPoints += ((perfect) ? this.pointsByPerfectHit : this.pointsByPerfectHit / 2) * this.pointsMultiplier;

	}

	public void HitError (string errorType)
	{
		
		this.totalPerfectsInARow = 0;
		this.CheckPointsMultiplier ();

		if (errorType == "key") {
			//Hit error effects! Screenshake? Glitch?
		}

		if (errorType == "floor") {
			//Floor error effects! Floor shake?
		}
	}

	#endregion

	#region Private methods

	private void CheckPointsMultiplier ()
	{
		
		if (this.totalPerfectsInARow < 5 && this.pointsMultiplier > 1) {
			this.pointsMultiplier = 1;
		}

		if (this.totalPerfectsInARow >= 5) {
			this.pointsMultiplier = 2;
		}

		if (this.totalPerfectsInARow >= 10) {
			this.pointsMultiplier = 3;
		}

		if (this.totalPerfectsInARow >= 15) {
			this.pointsMultiplier = 4;
		}

		if (this.totalPerfectsInARow >= 20) {
			this.pointsMultiplier = 5;
		}

	}

	#endregion
}
