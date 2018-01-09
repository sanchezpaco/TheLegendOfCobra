using UnityEngine;
using System.Collections;

namespace Common {
	
	public class ToColor : MonoBehaviour {

		#region Public Properties
		public bool cameraColor;
		public bool atStartup;
		public float speed;
		public Color destinationColor;
		public delegate void OnColorChangedEvent ();
		public event OnColorChangedEvent OnColorChange;
		#endregion

		#region Private Properties
		private Color currentColor;
		private SpriteRenderer spriteRenderer;
		private Camera mCamera;
		private bool changingColor;
		#endregion

		#region MonoBehaviour Components

		void Awake () {
			if (!this.cameraColor) {
				this.spriteRenderer = GetComponent<SpriteRenderer> ();
			} else {
				this.mCamera = GetComponent<Camera> ();
			}
			this.SetColor ();

			if (this.atStartup) {
				this.changingColor = true;
			}
		}

		void Update () {

			if (this.changingColor) {
				this.currentColor = Color.Lerp (this.currentColor, this.destinationColor, speed * Time.fixedDeltaTime);
				if (this.cameraColor) {
					this.mCamera.backgroundColor = this.currentColor;
				} else {
					this.spriteRenderer.color = this.currentColor;
				}
				if (this.currentColor == this.destinationColor) {
					if (this.OnColorChange != null) {
						this.OnColorChange ();
					}
					this.changingColor = false;
					this.currentColor = this.destinationColor;
				}
			}

		}
		#endregion

		#region Public Methods
		public void ChangeToColor(Color toColor, float speed) {
			this.destinationColor = toColor;
			this.speed = speed;
			this.changingColor = true;
			this.SetColor ();
		}
		#endregion

		#region Private Methods
		private void SetColor() {
			if (!this.cameraColor) {
				this.currentColor = this.spriteRenderer.color;
			} else {
				this.currentColor = this.mCamera.backgroundColor;
			}
		}
		#endregion
	}

}