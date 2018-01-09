using UnityEngine;
using System.Collections;

namespace Common
{
	public class ShakeTo : MonoBehaviour
	{

		#region Private Properties

		private Vector3 originalPosition;
		private float shakeAmt = 0;
		private bool shaking;
		#endregion

		#region Public Methods

		public void StartShaking (float magnitude)
		{
			if (!this.shaking) {
				this.originalPosition = this.transform.position;
				shakeAmt = magnitude * .0025f;
				InvokeRepeating ("Shake", 0, .01f);
				Invoke ("StopShaking", 0.3f);
				this.shaking = true;
			}
		}

		#endregion

		#region Private Methods

		void Shake ()
		{
			if (shakeAmt > 0) {
				float quakeAmt = Random.value * shakeAmt * 2f - shakeAmt;
				Vector3 pp = this.transform.position;
				pp.x += quakeAmt; // can also add to x and/or z
				this.transform.position = pp;
			}
		}

		void StopShaking ()
		{
			CancelInvoke ("Shake");
			this.transform.position = this.originalPosition;
			this.shaking = false;
		}

		#endregion

	}
}