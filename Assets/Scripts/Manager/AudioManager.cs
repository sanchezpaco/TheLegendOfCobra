using UnityEngine;
using System.Collections;

public class AudioManager : MonoBehaviour {

	#region Public Properties
	public bool atStartup;
	public bool increaseVolume;
	public bool decreaseVolume;
	public float speed;
	public float toVolume;
	#endregion

	#region Private Properties
	private AudioSource audioSource;
	private float stepSpeed;
	#endregion

	#region MonoBehaviour Components
    void Awake () {
		this.audioSource = GetComponent<AudioSource> ();
    }

	void Update ()
	{

		if (this.increaseVolume) {
			this.stepSpeed = this.speed * Time.fixedDeltaTime;
			this.audioSource.volume += this.stepSpeed;

			if (this.audioSource.volume >= this.toVolume) {
				this.audioSource.volume = this.toVolume;
				this.increaseVolume = false;
			}
		}

		if (this.decreaseVolume) {
			this.stepSpeed = this.speed * Time.fixedDeltaTime;
			this.audioSource.volume -= this.stepSpeed;

			if (this.audioSource.volume <= this.toVolume) {
				this.audioSource.volume = this.toVolume;
				this.decreaseVolume = false;
			}
		}

	}
	#endregion

	#region Public Methods
    public void PlayMusic(float time)
    {
        StartCoroutine(StartPlay(time));
    }

	public void PlayMusic()
	{
		StartCoroutine(StartPlay(0));
	}

	public void PlayWithPitch(float pitch) {
		this.audioSource.pitch = pitch;
		StartCoroutine(StartPlay(0));
	}

    public void StopMusic(float time)
    {
        StartCoroutine(StartStop(time));
    }

	public void VolumeUP (float speed, float to)
	{
		this.speed = speed;
		this.increaseVolume = true;
		this.decreaseVolume = false;
		this.toVolume = to;
	}

	public void VolumeDown (float speed, float to)
	{
		this.speed = speed;
		this.decreaseVolume = true;
		this.increaseVolume = false;
		this.toVolume = to;
	}
	#endregion

	#region Private Methods
    IEnumerator StartPlay(float time)
    {
        yield return new WaitForSeconds(time);
		this.audioSource.Play();
    }

    IEnumerator StartStop(float time)
    {
        yield return new WaitForSeconds(time);
		this.audioSource.Stop();
    }
	#endregion
}
