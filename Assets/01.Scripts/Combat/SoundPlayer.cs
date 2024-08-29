using UnityEngine;

public class SoundPlayer : MonoBehaviour
{
    private AudioSource _audioSource;

	private void Awake()
	{
		_audioSource = GetComponent<AudioSource>();
	}

	public void AudioPlay(AudioClip clip)
	{
		_audioSource.clip = clip;
		_audioSource.Play();
	}
}