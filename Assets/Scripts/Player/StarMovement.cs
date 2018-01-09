using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class StarMovement : MonoBehaviour
{
	#region Public Properties
	public string movingSide;
	[HideInInspector]
	public Vector2 movingXY;
	[HideInInspector]
	public bool rolling;
	[HideInInspector]
	public Vector2 facingDirection;
	public float speed;
	public bool canMove;
	public float rollTime = 0.3f;
	public bool attacking;
	#endregion

	#region Private Properties
	private Transform audios;
	private AudioSource stepSound;
	private AudioSource rollSound;
	private bool canSoundStep;
	private Rigidbody2D rigidBody;
	private float moveX;
	private float moveY;
	private string lastDirection;
	private IDictionary<string, string> moveDirections = new Dictionary<string, string>();
	#endregion

	#region MonoBehaviour Components
	void Awake()
	{
//		this.audios = transform.Find ("Audio");
//		this.stepSound = this.audios.Find ("Step").GetComponent<AudioSource> ();
//		this.rollSound = this.audios.Find ("Roll").GetComponent<AudioSource> ();
		this.rigidBody = GetComponent<Rigidbody2D>();

		this.CreateMoveDirections ();
		this.facingDirection = new Vector2(0, -1);
		this.canSoundStep = true;
	}

	void FixedUpdate()
	{
		if (!rolling) {
			if (canMove) {
				moveY = Input.GetAxisRaw ("Vertical");
				moveX = Input.GetAxisRaw ("Horizontal");

				if (moveY > 0.1f) {
					moveY = 1;
				} else if (moveY < -0.1f) {
					moveY = -1;
				} 

				if (moveX > 0.1f) {
					moveX = 1;
				} else if (moveX < -0.1f) {
					moveX = -1;
				}

				movingSide = moveDirections [moveY + "," + moveX];


				if (!movingSide.Contains ("stop")) {
					lastDirection = movingSide;
				}

				movingSide += (movingSide == "stopped") ? lastDirection : "";

				if (!movingSide.Contains ("stopped")) {
					this.facingDirection = this.movingXY;
				}

				movingXY = new Vector2 (moveX, moveY);

				this.rigidBody.velocity = new Vector2 (moveX, moveY).normalized * speed;
			} else {
				this.rigidBody.velocity = new Vector2 (0, 0);
				movingSide = "stopped";
				movingSide += (movingSide == "stopped") ? lastDirection : "";
			}

		} else {
			this.movingSide = "rolling";
			this.movingSide += (this.movingSide == "rolling") ? lastDirection : "";
		}
	}

	void Update() {
		if (Input.GetButtonDown ("Jump") && !this.rolling && this.canMove) {
			this.Roll ();
		}

		if ((!this.movingSide.Contains("stop") && !this.movingSide.Contains("roll")) && this.canSoundStep)
		{
			this.canSoundStep = false;
			StartCoroutine(this.PlayStep());
		}
	}
	#endregion

	#region Public Methods
	public void StopMoving() {
		this.canMove = false;
	}

	public void StartMoving() {
		this.canMove = true;
	}

	public void Attack(string side) {		
		this.attacking = true;
		this.canMove = false;
		StartCoroutine (this.StopAttacking ());
	}
	#endregion

	#region Private Methods
	void Roll() {

		this.rolling = true;
		this.rigidBody.velocity = this.facingDirection.normalized * speed;
		this.rigidBody.AddForce (this.movingXY.normalized * 8, ForceMode2D.Impulse);
//		this.rollSound.Play ();
		StartCoroutine (this.StopRolling ());
	}

	IEnumerator StopRolling() {
		yield return new WaitForSeconds (this.rollTime);
		this.rolling = false;

	}

	IEnumerator StopAttacking() {
		yield return new WaitForSeconds(0.3f);
		this.attacking = false;
		this.canMove = true;
	}

	IEnumerator PlayStep()
	{
		yield return new WaitForSeconds(0.3f);
		this.canSoundStep = true;
		if ((!this.movingSide.Contains("stop") && !this.movingSide.Contains("roll"))) {
//			this.stepSound.Play ();
		}
	}

	void CreateMoveDirections() {
		this.moveDirections["0,1"] = "right";
		this.moveDirections["1,1"] = "upright";
		this.moveDirections["-1,1"] = "downright";

		this.moveDirections["0,-1"] = "left";
		this.moveDirections["1,-1"] = "upleft";
		this.moveDirections["-1,-1"] = "downleft";

		this.moveDirections["1,0"] = "up";
		this.moveDirections["-1,0"] = "down";
		this.moveDirections["0,0"] = "stopped";
	}
	#endregion
}
