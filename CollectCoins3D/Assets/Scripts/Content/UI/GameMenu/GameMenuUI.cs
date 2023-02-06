using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Content.UI.GameMenu
{
	/// <summary>
	/// This class just defines the behaviours of the Game Menu Buttons
	/// </summary>
	public class GameMenuUI : MonoBehaviour
	{
		// ### Name of the scene where is the main menu
		[SerializeField] string mainMenuSceneName;

		// ### Button to go to main menu scene
		[SerializeField] Button exitToMainMenuButton;

		// ### Button to exit the game to the desktop
		[SerializeField] Button exitGameButton;

		/// <summary>
		/// Configures the buttons from the game menu to their correspondent behaviours
		/// Exit To Main Menu Button -> To load the scene where is the main menu
		/// Exit Game Button -> To Quit the game to the desktop
		/// </summary>
		private void OnEnable()
		{
			exitToMainMenuButton.onClick.RemoveAllListeners();
			exitToMainMenuButton.onClick.AddListener(() =>
			{
				SceneManager.LoadScene(mainMenuSceneName, LoadSceneMode.Single);
			});

			exitGameButton.onClick.RemoveAllListeners();
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
		/// If menu is opened then closes, if it's not then open it
		/// </summary>
		public void ToggleMenu()
		{
			if(gameObject.activeSelf)
			{
				gameObject.SetActive(false);
				return;
			}

			gameObject.SetActive(true);
		}
	}
}