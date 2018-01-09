using UnityEngine;
using System.Collections;

namespace Managers
{
	public class TimeManager : MonoBehaviour
	{
        
		#region Public Properties
		public static Vector3 playerSpawnPoint;
		#endregion

		#region Private Properties
		private bool bigPause;
		private bool microPause;
		private float timePaused;
		private float _lastFrameTime;
		private float deltaTime;
		#endregion

		#region MonoBehaviour Components
		void Update ()
		{
			if (microPause && !this.bigPause) {
				timePaused -= deltaTime;
				if (timePaused < 0) {
					this.UnpauseGame ();
				}
			}

			deltaTime = Time.realtimeSinceStartup - _lastFrameTime;
			_lastFrameTime = Time.realtimeSinceStartup;
		}
		#endregion

		#region Public Methods
		public void PauseGame() {
			this.bigPause = true;
			Time.timeScale = 0.0f;
		}

		public void UnpauseGame() {
			Time.timeScale = 1.0f;
			this.microPause = false;
			this.bigPause = false;
		}

		public void Micropause (float time)
		{
			if (Time.timeScale > 0) {
				Time.timeScale = 0.0f;
				this.microPause = true;
				this.timePaused = time;
			}
		}
		#endregion
        
        
	}

}