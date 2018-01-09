using UnityEngine;
using System.Collections;

namespace Common
{
	public class ResizeTo : MonoBehaviour
	{

		#region Public Properties
		public delegate void OnResizeEvent();
		public event OnResizeEvent OnResize;
		public float speed;
		public bool onStartup;
		public Vector3 toSize;
		public bool withFinalBounce;
		public bool withStartBounce;
		public float bounceSize = 1.01f;
		#endregion

		#region Private Properties
		private Vector3 finalSize;
		private Vector3 originSize;
		private bool resizing;
		private bool rollback;
		#endregion

		#region MonoBehaviour Components

		void Start() {
			if (this.onStartup) {
				if (this.withFinalBounce) {
					this.ResizeToSizeWithBounce (this.toSize, this.speed, this.bounceSize, this.withStartBounce);
				} else {
					this.ResizeToSize (this.toSize, this.speed);
				}
			}
		}

		void Update ()
		{
			if (this.resizing) {
				Vector3 cSize = this.transform.localScale;
				this.transform.localScale = Vector3.MoveTowards (cSize, this.toSize, speed * Time.deltaTime);
				if (cSize == this.toSize) {
					if (this.rollback) {
						this.rollback = false;
						this.resizing = false;
						this.ResizeToSize (this.originSize, this.speed);
					} else {
						if (!this.withFinalBounce) {
							this.resizing = false;
							if (this.OnResize != null) {
								this.OnResize ();
							}
						} else {
							this.resizing = false;
							this.withFinalBounce = false;
							this.ResizeToSize (this.finalSize, this.speed / 4);
						}
					}
				}
			}
		}
		#endregion

		#region Public Methods
		public void WaitAndResize(float timeToWait, Vector3 size, float speed) {
			if (!this.resizing) {
				this.toSize = size;
				this.speed = speed;
				StartCoroutine (this.WaitToResize(timeToWait));
			}
		}

		public void ResizeToSize(Vector3 size, float speed) {
			if (!this.resizing) {
				this.toSize = size;
				this.speed = speed;
				this.resizing = true;
			}
		}

		public void ResizeToSizeWithBounce(Vector3 size, float speed, float bounceSize, bool withStartBounce) {
			if (!this.resizing) {
				this.finalSize = size;
				this.toSize = size * bounceSize;
				this.speed = speed;
				this.resizing = true;
				this.withFinalBounce = true;
				this.withStartBounce = withFinalBounce;
			}
		}

		public void ResizeAndRollback(Vector3 size, float speed) {
			if (!this.resizing) {
				this.rollback = true;
				this.originSize = this.transform.localScale;
				this.ResizeToSize (size, speed);
			}
		}
		#endregion

		#region Private Methods
		private IEnumerator WaitToResize(float time) {
			yield return new WaitForSeconds (time);
			this.resizing = true;
		}
		#endregion
	}
}
