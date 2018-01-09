using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MuteAudio : MonoBehaviour {

	public Sprite muteButton;
	public Sprite playButton;

	private bool audioMuted;
	private Image image;

	void Awake() {
		this.image = GetComponent<Image> ();
		this.audioMuted = (PlayerPrefs.GetInt ("AudioMuted",0) == 1)?true:false;
		if (this.audioMuted) {
			this.MuteUnmuteAudio ();
		}


	}

	void OnMouseDown() {
		this.MuteUnmuteAudio ();
	}


	public void MuteUnmuteAudio() {
		if (this.audioMuted) {
			PlayerPrefs.SetInt ("AudioMuted", 1);
			AudioListener.volume = 0.0f;
			this.image.sprite = this.muteButton;
			this.audioMuted = false;
		} else {
			PlayerPrefs.SetInt ("AudioMuted", 0);
			this.image.sprite = this.playButton;
			this.audioMuted = true;
			AudioListener.volume = 1.0f;
		}

	}
}
