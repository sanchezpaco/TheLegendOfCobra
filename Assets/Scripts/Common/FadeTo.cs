using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace Common
{
	public class FadeTo : MonoBehaviour
	{

		#region Public Properties
		public string type = "fadeOut";
		public float fadeSpeed = 1;
		public float toFade = 1;
		public float waitTimeToFade;
		public bool atStartup;
		public bool uiImage;
		public bool textMesh;
		public bool uiText;
		public delegate void OnFinishFadeEvent();
		public event OnFinishFadeEvent OnFinishFade;
		#endregion

		#region Private properties
		private Text text;
		private TextMesh textM;
		private Image image;
		private SpriteRenderer spriteRenderer;
		private Color newColor;
		private bool stopFading;
		private float fadeDirection;
		#endregion

		#region MonoBehaviour Components
		void Awake ()
		{
			if (this.uiImage) {
				this.image = GetComponent<Image> ();
			} else if (this.textMesh) {
				this.textM = GetComponent<TextMesh> ();
			} else if (this.uiText) {
				this.text = GetComponent<Text> ();
			} else {
				this.spriteRenderer = GetComponent<SpriteRenderer> ();
			}
			this.SetColor ();
			this.fadeDirection = (this.type == "fadeOut") ? -1 : 1;
			this.stopFading = true;

			if (this.atStartup) {
				StartCoroutine (this.StartFading (this.waitTimeToFade));
			}
		}

		void Update ()
		{
			if (!this.stopFading) {
				this.newColor.a += Time.fixedDeltaTime * this.fadeDirection * this.fadeSpeed;
				if ((this.type == "fadeOut" && this.newColor.a <= this.toFade) || 
					(this.type == "fadeIn" && this.newColor.a >= this.toFade)) {

					this.StopFading ();
				}

				if (this.uiImage) {
					this.image.color = this.newColor;
				} else if (this.textMesh) {
					this.textM.color = this.newColor;
				} else if (this.uiText) {
					this.text.color = this.newColor;
				}
				else {
					this.spriteRenderer.color = this.newColor;
				}
			}
		}
		#endregion

		#region Public Methods

		public void Fade (string type, float toFade, float fadeSpeed)
		{
			this.fadeSpeed = fadeSpeed;
			this.toFade = toFade;
			this.type = type;
			this.fadeDirection = (this.type == "fadeOut") ? -1 : 1;
			this.stopFading = false;
			this.SetColor ();
		}

		public void WaitAndFade(float timeToWait, string type, float toFade, float fadeSpeed) {
			this.fadeSpeed = fadeSpeed;
			this.toFade = toFade;
			this.type = type;
			this.fadeDirection = (this.type == "fadeOut") ? -1 : 1;
			StartCoroutine (this.StartFading(timeToWait));
		}
		#endregion

		#region Private Methods
		IEnumerator StartFading (float time)
		{
			yield return new WaitForSeconds (time);
			this.SetColor ();
			this.stopFading = false;
		}

		void StopFading() {
			this.stopFading = true;
			if (this.OnFinishFade != null) {
				this.OnFinishFade ();
			}
		}

		void SetColor() {
			if (this.uiImage) {
				this.newColor = this.image.color;
			} else if (this.textMesh) {
				this.newColor = this.textM.color;
			} else if (this.uiText) {
				this.newColor = this.text.color;
			} else {
				this.newColor = this.spriteRenderer.color;
			}
		}
		#endregion
	}
}