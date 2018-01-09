using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AvoidShits : MonoBehaviour, IMinigame
{

	#region Public properties

	public GameObject player;
	public ShitManager shitManager;
	public float minigameDuration = 30;

	#endregion

	#region Private properties

	private PlayerBehaviour playerBehaviour;
	private float passedTime;
	private bool gameStarted;
	private float timeQuarter;
	private float initTimeQuarter;

	#endregion

	#region Monobehaviour Components

	void Awake ()
	{
		this.timeQuarter = this.minigameDuration / 4;
		this.initTimeQuarter = this.timeQuarter;
		this.playerBehaviour = player.GetComponent<PlayerBehaviour> ();
	}

	// Use this for initialization
	void Start ()
	{
		this.Intro ();
	}
	
	// Update is called once per frame
	void Update ()
	{

		if (this.gameStarted) {

			if (this.passedTime >= this.timeQuarter) {
				
				this.UpgradeDifficuly ();

				this.timeQuarter += this.initTimeQuarter;
			}

			if (this.passedTime >= this.minigameDuration) {
				this.GameWin ();
			}

			this.passedTime += Time.fixedDeltaTime;
		}

	}

	#endregion

	#region Public methods

	public void Intro ()
	{
//		this.conversationManager.StartConversation("AvoidShits-1")
//		this.conversationManager.OnFinishConversation += this.EnableGame
//		this.conversationManager.OnFinishConversation += this.HidePeople
		this.EnableGame ();
	}

	public void EnableGame ()
	{

		this.gameStarted = true;
		this.shitManager.EnableShitDrop ();
		this.playerBehaviour.OnDie += this.GameOver;
	}

	public void GameOver ()
	{
		this.playerBehaviour.OnDie -= this.GameOver;
		SceneManager.LoadScene (0);
	}

	public void GameWin ()
	{
		
		this.gameStarted = false;
		this.shitManager.DisableShitDrop ();
		StartCoroutine (this.ShowPeople ());
	}

	public void GameFinish ()
	{
		this.shitManager.DropPrizeShit ();
	}

	#endregion

	#region Private methods

	private void UpgradeDifficuly ()
	{
		this.shitManager.IncrementShitFallVelocity ();
	}

	private void HidePeople ()
	{
		Debug.Log ("Hiding people");
	}

	private IEnumerator ShowPeople ()
	{

		yield return new WaitForSeconds (4f);
		Debug.Log ("Showing people");
		//		this.conversationManager.StartConversation("AvoidShits-2"), Esa de alli no es Carol? Eh! tiene una mierda en la cabeza!
		//		this.conversationManager.OnFinishConversation -= this.HidePeople
		//		this.conversationManager.OnFinishConversation -= this.EnableGame
		//		this.conversationManager.OnFinishConversation += this.GameFinish
		this.GameFinish ();
	}

	#endregion

}
