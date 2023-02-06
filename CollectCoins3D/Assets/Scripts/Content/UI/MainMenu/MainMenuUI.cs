using CoreSystems.Audio;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Content.UI.MainMenu
{
	/// <summary>
	/// This class just defines the behaviours of the Main Menu Buttons
	/// </summary>
	public class MainMenuUI : MonoBehaviour
	{
		// ### Name of the scene where is the main game
		[SerializeField] string gameSceneName;

		// ### Button to start the game in the main menu
		[SerializeField] Button startGameButton;

		// ### Button to exit the game in the main menu
		[SerializeField] Button exitGameButton;

		// ### Reference of the audio store component so it can play sounds
		[SerializeField] AudioStore audioStore;

		// ### BGM (BackGround Music) to play the main menu music
		[SerializeField] AudioClip bgmClip;

		// ### BGS (BackGround Sound) to play the sound when selecting an option
		[SerializeField] AudioClip bgsClip;

		/// <summary>
		/// Configures the buttons from the main menu to their correspondent behaviours
		/// Start Game Button -> To load the scene where is the main game
		/// Exit Game Button -> To Quit the game to the desktop
		/// </summary>
		private void OnEnable()
		{
			// ### Start Game Button
			startGameButton.onClick.AddListener(() =>
			{
				audioStore.Play(bgsClip);
				SceneManager.LoadScene(gameSceneName, LoadSceneMode.Single);
			});

			// ### Exit Game Button
			exitGameButton.onClick.AddListener(() =>
			{
#if UNITY_STANDALONE
				Application.Quit();
#endif
#if UNITY_EDITOR
				UnityEditor.EditorApplication.isPlaying = false;
#endif
			});
		}

		/// <summary>
		/// Starts to play the music for the main menu
		/// </summary>
		private void Start()
		{
			audioStore.Play(bgmClip);
		}
	}
}