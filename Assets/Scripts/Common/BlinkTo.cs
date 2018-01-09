using UnityEngine;
using System.Collections;

namespace Common
{
	public class BlinkTo : MonoBehaviour
	{

		#region Public Properties
		public float maxBlinkFade = 1;
		#endregion
	
		#region Private Properties

		private float minBlinkFade = 0;
		private float blinkTimes;
		private float blinkDirection = 1;
		private float blinkSpeed;
		private float blinkedTimes;
		private bool blinking;
		private SpriteRenderer spriteRenderer;
		private Color spriteColor;

		#endregion

		#region MonoBehaviour Components

		void Awake ()
		{
			this.spriteRenderer = GetComponent<SpriteRenderer> ();
			this.spriteColor = this.spriteRenderer.color;
		}

		void Update ()
		{

			if (this.blinking) {
				spriteColor.a += Time.fixedDeltaTime * this.blinkSpeed * blinkDirection;
				this.spriteRenderer.color = this.spriteColor;

				if (this.spriteColor.a >= this.maxBlinkFade) {
					this.blinkDirection = -1;
				} else if (this.spriteColor.a <= this.minBlinkFade) {
					this.blinkedTimes++;
					if (this.blinkedTimes >= this.blinkTimes) {
						this.blinking = false;
						this.blinkedTimes = 0;
						this.blinkDirection = 1;
					} else {
						this.blinkDirection = 1;
					}
				}
			}
	
		}

		#endregion

		#region Public Methods

		public void Blink (float blinkTimes, float blinkSpeed, float maxBlinkFade, bool fadeToOriginalAlpha)
		{
			if (!this.blinking) {
				this.blinking = true;
				this.blinkTimes = blinkTimes;
				this.blinkSpeed = blinkSpeed;
				this.maxBlinkFade = maxBlinkFade;
				if (fadeToOriginalAlpha) {
					this.minBlinkFade = this.spriteColor.a;
				}
			}
		}

		#endregion
	}
}
