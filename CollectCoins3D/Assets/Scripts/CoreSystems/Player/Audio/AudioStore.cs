using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CoreSystems.Audio
{
	public class AudioStore : MonoBehaviour
	{
		// ### Reference for the Audio Source where it will execute the sounds and music
		AudioSource audioSource = null;

		/// <summary>
		/// Gets the reference of the Audio Source
		/// </summary>
		private void Awake()
		{
			audioSource = GetComponent<AudioSource>();
		}

		/// <summary>
		/// Plays the music/sound executing the Audio Source's Play method
		/// </summary>
		/// <param name="audioClip"></param>
		public void Play(AudioClip audioClip)
		{
			audioSource.PlayOneShot(audioClip);
		}
	}
}