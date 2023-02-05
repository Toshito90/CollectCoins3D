using Content.Points;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Content.UI.Points
{
	/// <summary>
	/// This class is only used on the UI (user Interface) to show the player's points
	/// </summary>
	public class PointsUI : MonoBehaviour
	{
		// ### The text that shows on the UI (user Interface) with the player's points
		[SerializeField] TextMeshProUGUI pointsText;

		// ### Uses the player reference from the component PointsStore to get the player's current points
		[SerializeField] PointsStore pointsStore;

		/// <summary>
		/// Subscribe to the event onPointsUpdated and Updates the information for the player's current points
		/// </summary>
		private void OnEnable()
		{
			pointsStore.onPointsUpdated += Refresh;
			Refresh();
		}

		/// <summary>
		/// Usubscribe the event onPointsUpdated
		/// </summary>
		private void OnDisable()
		{
			pointsStore.onPointsUpdated -= Refresh;
		}

		/// <summary>
		/// Updates the player information and also it fix janky UI (User Interface) 
		/// not positioning properly the player points text
		/// </summary>
		void Refresh()
		{
			pointsText.text = $"{pointsStore.GetPoints()}";

			var rectTransform = GetComponent<RectTransform>();
			LayoutRebuilder.ForceRebuildLayoutImmediate(rectTransform);
			foreach(Transform transf in transform)
			{
				var rt = transf.GetComponent<RectTransform>();
				LayoutRebuilder.ForceRebuildLayoutImmediate(rt);
			}
		}
	}
}