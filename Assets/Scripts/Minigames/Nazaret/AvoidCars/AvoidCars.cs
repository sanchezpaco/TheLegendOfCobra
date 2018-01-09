using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;
using Managers;
using UnityEngine.SceneManagement;

public class AvoidCars : MonoBehaviour, IMinigame
{

	#region Public variables

	public GameObject player;
	public ConversationManager conversationManager;
	public RoadBehaviour roadBehaviour;
	public bool minigameStarted;

	#endregion

	#region Private variables

	private StarMovement playerMovement;
	private PlayerBehaviour playerBehaviour;
	private MoveTo playerMoveTo;

	#endregion


	#region Monobehaviour Components

	void Awake ()
	{
		
		this.playerMovement = this.player.GetComponent<StarMovement> ();
		this.playerBehaviour = this.player.GetComponent<PlayerBehaviour> ();
		this.playerMoveTo = this.player.GetComponent<MoveTo> ();

	}

	void Start ()
	{
		
		this.Intro ();

	}

	#endregion

	#region Public Methods

	public void Intro ()
	{
		
		//		this.conversationManager.StartConversation ();
		this.playerMoveTo.MoveToPosition (new Vector3 (0f, 14.4f, 0), 5f);
		this.playerMoveTo.OnMove += EnableGame;

	}

	public void EnableGame ()
	{
		
		this.playerMoveTo.OnMove -= EnableGame;
		this.playerBehaviour.OnDie += this.GameOver;
		this.playerBehaviour.OnWin += this.GameWin;

		this.playerMovement.canMove = true;
		this.minigameStarted = true;
		this.roadBehaviour.ActivateLanes ();

	}

	public void GameWin ()
	{
		
		this.playerBehaviour.OnDie -= this.GameOver;
		this.roadBehaviour.DeactivateLanes ();
		this.roadBehaviour.ThrowPrizeCar ();

	}

	public void GameOver ()
	{
		
		//Game over effects
		this.playerBehaviour.OnDie -= this.GameOver;
		this.playerBehaviour.OnWin -= this.GameWin;
		SceneManager.LoadScene (0);

	}

	public void GameFinish ()
	{
		
		this.playerBehaviour.OnWin -= this.GameWin;
		SceneManager.LoadScene (0);

	}

	#endregion




}
