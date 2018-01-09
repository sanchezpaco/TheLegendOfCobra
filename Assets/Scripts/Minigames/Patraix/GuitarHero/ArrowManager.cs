using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Managers;

public class ArrowManager : MonoBehaviour
{

	#region Public properties

	public PoolManager arrowPool;
	public Color[] arrowColors;
	public List<int> arrowPossibilities;
	public List<float> arrowXPositions;
	public GuitarHero guitarHero;

	public float minigameDuration = 60;
	public float timeBetweenArrows = 1;
	public int arrowsToThrow = 1;

	public bool enabledThrowing;
	public int arrowsThrowed;

	#endregion

	#region Private properties

	private float passedTime;
	private int currentDifficulty = 1;
	private List<int> availableArrowTypes;
	private float passedTimeToThrow;

	#endregion

	#region Monobehaviour Components

	void Awake ()
	{
		
		this.availableArrowTypes = new List<int> ();
		this.ResetAvailableArrows ();
	}

	void Update ()
	{

		if (this.enabledThrowing) {

			if (this.passedTimeToThrow > this.timeBetweenArrows) {

				this.ThrowArrow ();
				this.passedTimeToThrow = 0;
			}

			if (this.passedTime > this.minigameDuration) {
				this.StopThrowing ();
				this.guitarHero.GameFinish ();
			}

			this.CalculateDifficulty ();
			this.passedTimeToThrow += Time.fixedDeltaTime;
			this.passedTime += Time.fixedDeltaTime;
		}
		
	}

	#endregion

	#region Public methods

	public void StartThrowing ()
	{
		
		this.enabledThrowing = true;
	}

	public void StopThrowing() {

		//this.OnFinishThrowing();
		this.enabledThrowing = false;
	}

	#endregion

	#region Private methods

	private void ThrowArrow ()
	{

		ArrowBehaviour arrowToThrow = null;
		int arrowsToThrow = Random.Range (1, this.arrowsToThrow + 1);

		for (int i = 0; i < arrowsToThrow; i++) {

			arrowToThrow = this.GetArrow ();
			arrowToThrow.Move (4, -3);
			this.arrowsThrowed++;
		}
			
		this.ResetAvailableArrows ();
	}

	#region Difficulty methods

	private ArrowBehaviour GetArrow ()
	{

		int arrowTypeToThrow = this.availableArrowTypes [0];
		this.availableArrowTypes.Remove (arrowTypeToThrow);

		Transform arrowToThrow = this.arrowPool.Get ().transform;
		SpriteRenderer arrowSprite = arrowToThrow.GetComponent<SpriteRenderer> ();
		ArrowBehaviour arrowBehaviour = arrowToThrow.GetComponent<ArrowBehaviour> ();
		arrowBehaviour.arrowType = arrowTypeToThrow;
		arrowBehaviour.OnHit += this.ArrowHit;
		arrowBehaviour.OnErrorSave += this.guitarHero.HitError;

		arrowSprite.color = this.arrowColors [arrowTypeToThrow];

		arrowToThrow.localPosition = new Vector2 (this.arrowXPositions [arrowTypeToThrow], 5);

		arrowToThrow.gameObject.SetActive (true);
		return arrowBehaviour;
	}

	private void ResetAvailableArrows ()
	{

		int arrowPossiblitiesLength = this.arrowPossibilities.Count;
		this.availableArrowTypes.Clear ();
		
		for (int i = 0; i < arrowPossiblitiesLength; i++) {
			this.availableArrowTypes.Add (this.arrowPossibilities [i]);
		}

		this.availableArrowTypes.Shuffle ();
	}

	private void CalculateDifficulty ()
	{

		float passedTimePercentage = (this.passedTime / this.minigameDuration) * 100;

		if (this.CanPassToNormalDifficulty (passedTimePercentage)) {
			
			this.UpgradeDifficulty ();
		}

		if (this.CanPassToHardDifficulty (passedTimePercentage)) {
			
			this.UpgradeDifficulty ();
		}

		if (this.CanPassToExtremeDifficulty (passedTimePercentage)) {

			this.UpgradeDifficulty ();
		}
		
	}

	private void UpgradeDifficulty ()
	{
		Debug.Log ("DIFF UPGRADED");

		this.arrowsToThrow++;
		this.arrowsToThrow = (this.arrowsToThrow >= 2) ? 2 : this.arrowsToThrow;
		this.currentDifficulty++;
		this.timeBetweenArrows -= 0.225f;
	}

	private bool CanUpgradeDifficulty (float passedTimePercetage, int expectedDifficulty, int expectedTimePercentage)
	{
		
		return this.currentDifficulty < expectedDifficulty && passedTimePercetage > expectedTimePercentage;
	}

	private bool CanPassToNormalDifficulty (float passedTimePercentage)
	{
		
		return this.CanUpgradeDifficulty (passedTimePercentage, 2, 20);
	}

	private bool CanPassToHardDifficulty (float passedTimePercentage)
	{

		return this.CanUpgradeDifficulty (passedTimePercentage, 3, 40);
	}

	private bool CanPassToExtremeDifficulty (float passedTimePercentage)
	{

		return this.CanUpgradeDifficulty (passedTimePercentage, 4, 70);
	}

	#endregion

	private void ArrowHit(ArrowBehaviour arrow, bool center) {
		
		arrow.OnErrorSave -= this.guitarHero.HitError;
		this.guitarHero.SumPoints (center);
	}

	#endregion
}
