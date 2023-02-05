using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Content.Points
{
	/// <summary>
	/// This class holds the information of the player's points
	/// </summary>
	public class PointsStore : MonoBehaviour
	{
		// ### Current points of the player
		int points = 0;

		// ### Event that is called when the player adds or subtracts points
		public event Action onPointsUpdated;

		/// <summary>
		/// Handle the logic to add or subtract points from the player's score
		/// </summary>
		/// <param name="points"></param>
		public void AddPoints(int points)
		{
			this.points += points;
			onPointsUpdated?.Invoke();
		}

		/// <summary>
		/// Return the player's current points
		/// </summary>
		/// <returns></returns>
		public int GetPoints()
		{
			return points;
		}
	}
}