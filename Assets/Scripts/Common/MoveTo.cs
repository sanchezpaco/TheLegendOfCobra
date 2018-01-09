using UnityEngine;
using System.Collections;

namespace Common
{
	public class MoveTo : MonoBehaviour
	{

		#region Public Properties
		public Vector3 toPosition;
		public delegate void OnMoveEvent();
		public event OnMoveEvent OnMove;
		public float speed;
		public float bounceSize = 1;
		public bool atStartup;
		public bool withFinalBounce;
		public bool withStartBounce;
		#endregion

		#region Private Properties
		private delegate void OnBounceEvent();
		private event OnBounceEvent OnBounce;
		private bool moving;
		private Vector3 finalPosition;
		#endregion

		#region MonoBehaviour Components

		void Awake() {
			if (this.atStartup) {
				if (this.withFinalBounce || this.withStartBounce) {
					this.MoveToPositionWithBounce (this.toPosition, this.speed, this.bounceSize, this.withStartBounce);
				} else {
					this.MoveToPosition (this.toPosition, this.speed);
				}
			}
		}
	
		void Update ()
		{
			if (this.moving) {
				Vector3 cPosition = this.transform.localPosition;
				this.transform.localPosition = Vector3.MoveTowards (cPosition, this.toPosition, speed * Time.fixedDeltaTime);
				if (cPosition == this.toPosition) {
					this.moving = false;
					if (this.withFinalBounce || this.withStartBounce) {
						this.OnBounce ();
					} else {
						if (this.OnMove != null) {
							this.OnMove ();
						}
					}

				}
			}
		}
		#endregion

		#region Public Methods
		public void WaitAndMoveToPosition(float timeToWait, Vector3 position, float speed) {
			this.toPosition = position;
			this.speed = speed;
			StartCoroutine (this.WaitToMove (timeToWait));
		}

		public void MoveToPosition(Vector3 position, float speed) {
			this.toPosition = position;
			this.speed = speed;
			this.moving = true;
		}

		public void MoveToPositionWithBounce(Vector3 position, float speed, float bounceSize, bool startBounce) {
			this.withFinalBounce = true;
			this.withStartBounce = startBounce;
			this.finalPosition = position;
			this.bounceSize = bounceSize;
			Vector3 nPosition = Vector3.zero;
			if (startBounce) {
				this.OnBounce += this.StartBounce;
				nPosition = this.transform.localPosition / bounceSize;
			} else {
				this.OnBounce += this.FinalBounce;
				nPosition = position / bounceSize;
			}

			this.MoveToPosition (nPosition, speed);
		}
		#endregion

		#region Private Methods
		private IEnumerator WaitToMove(float timeToWait) {
			yield return new WaitForSeconds (timeToWait);
			this.moving = true;
		}

		void FinalBounce() {
			this.OnBounce -= this.FinalBounce;
			this.withFinalBounce = false;
			this.toPosition = toPosition * this.bounceSize;
			this.MoveToPosition (this.toPosition, this.speed / 6);
		}

		void StartBounce() {
			this.OnBounce -= this.StartBounce;
			this.withStartBounce = false;
			if (this.withFinalBounce) {
				this.OnBounce += this.FinalBounce;
				this.MoveToPosition (this.finalPosition / bounceSize, speed);
			}
		}
		#endregion
	}
}
