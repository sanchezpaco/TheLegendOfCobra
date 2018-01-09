using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;
using UnityEngine.SceneManagement;

public class ScraulShits : MonoBehaviour, IMinigame {

	#region Public properties

	public MoveTo cameraMove;
	public GameObject player;
	public FinishSideWalkBehaviour finishSideWalk;
	public ScraulShitsManager scraulShitManager;

	public float maxAngryMeter = 15;
	public float angryMeterVelocity = 1.5f;
	public float angryQuantityToRemove = 1;

	#endregion

	#region Private properties

	private StarMovement playerMovement;
	private float angryMeter;
	private bool gameStarted;

	#endregion


	#region MonoBehaviour Components

	void Awake ()
	{

		this.SetupCobraPlayer ();

	}

	void Start () {
		this.Intro ();
	}

	void Update() {
		if (this.gameStarted) {

			this.angryMeter += Time.fixedDeltaTime * this.angryMeterVelocity;

			if (this.angryMeter > this.maxAngryMeter) {
				this.GameOver ();
			}
		}
	}

	#endregion

	#region Public methods

	public void Intro() {
		//		this.conversationManager.StartConversation("ScraulShits-1")
		//		this.conversationManager.OnFinishConversation += this.MoveCamera
		this.MoveCamera();
	}

	public void EnableGame() {

		this.cameraMove.OnMove -= this.EnableGame;
		this.finishSideWalk.OnGameWin += this.GameWin;
		this.scraulShitManager.OnShitDestroyed += this.ScraulShitDestroyed;

		this.cameraMove.GetComponent<SmoothCamera2D> ().enabled = true;
		this.playerMovement.canMove = true;
		this.gameStarted = true;

	}

	public void GameWin() {
		this.gameStarted = false;
		this.finishSideWalk.OnGameWin -= this.GameWin;
		//		this.conversationManager.StartConversation("ScraulShits-2"), **PENSAR DIALOGO IMMA**
		//		this.conversationManager.OnFinishConversation += this.GameFinish
		this.GameFinish();
	}

	public void GameOver() {
		SceneManager.LoadScene (0);
	}

	public void GameFinish() {
		this.DropScraulShitPrize ();
	}

	#endregion

	#region Private methods

	private void SetupCobraPlayer() {

		this.playerMovement = this.player.GetComponent<StarMovement> ();
		this.player.gameObject.AddComponent<ScraulShitsCobraPlayerBehaviour> ();

	}

	private void ScraulShitDestroyed() {
		this.angryMeter -= this.angryQuantityToRemove;
		Debug.Log (this.angryMeter);
	}

	private void MoveCamera() {
		//		this.conversationManager.OnFinishConversation -= this.MoveCamera
		this.cameraMove.OnMove += this.EnableGame;
		this.cameraMove.MoveToPosition (new Vector3 (0, 0, -10), 8);

	}

	private void DropScraulShitPrize() {
		Debug.Log ("Scraul shitting the prize");
	}

	#endregion
}
