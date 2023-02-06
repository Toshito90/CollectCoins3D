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
	}
}