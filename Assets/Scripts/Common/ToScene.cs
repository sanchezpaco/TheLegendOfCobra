using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;


namespace Common {
	public class ToScene : MonoBehaviour {
		#region Public Properties
		public int sceneToLoad;
		public string triggerLayer;
		public bool withFade;
		public bool isTrigger;
		public FadeTo fadeScreen;
		#endregion

		#region MonoBehaviour Components
		void OnTriggerEnter2D(Collider2D col) {
			if (this.isTrigger) {
				if (col.gameObject.layer == LayerMask.NameToLayer (this.triggerLayer)) {
					if (this.withFade) {
						this.fadeScreen.OnFinishFade += this.LoadScene;
						this.fadeScreen.Fade ("fadeIn", 1, 1);
					} else {
						this.LoadScene ();
					}
				}
			}
		}
		#endregion

		#region Public Methods
		public void LoadScene() {
			SceneManager.LoadScene (this.sceneToLoad);
			PlayerPrefs.SetInt ("lastLoadedScene", SceneManager.GetActiveScene ().buildIndex);
		}

		public void LoadSceneAfterSeconds(int scene, float time) {
			this.sceneToLoad = scene;
			StartCoroutine (this.LoadScene (time));
		}

		public void FadeAndLoadScene(int scene) {
			this.sceneToLoad = scene;
			this.fadeScreen.OnFinishFade += this.LoadScene;
			this.fadeScreen.Fade ("fadeIn", 1, 1);
		}
		#endregion

		#region Private Methods
		IEnumerator LoadScene(float time) {
			yield return new WaitForSeconds (time);
			this.LoadScene ();
		}
		#endregion
	}
}
