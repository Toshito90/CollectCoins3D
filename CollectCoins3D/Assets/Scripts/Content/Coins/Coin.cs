using Content.Points;
using CoreSystems.Audio;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Content.Coins
{
	/// <summary>
	/// This class holds the logic with the coins
	/// How much points it should give or take from the player
	/// Also how it should be destroyed once collided with the player
	/// </summary>
	public class Coin : MonoBehaviour
	{
		// ### How many points it should give or take from the player
		[SerializeField] int pointsAward = 1;

		// ### The coin ID so it will be easier to identify this object
		[SerializeField] string coinID = string.Empty;

		[SerializeField] AudioClip audioBGS;

		// ### Reference that should be on the scene of the CoinsManager
		CoinsManager coinsManager = null;

		// ### Reference for the audio store, to start play the BHM music
		AudioStore audioStore;

		/// <summary>
		/// Initializes the coinsManager and coin ID and the reference for the audio store component
		/// </summary>
		private void Start()
		{
			coinsManager = FindObjectOfType<CoinsManager>();

			if(string.IsNullOrEmpty(coinID) || string.IsNullOrWhiteSpace(coinID))
			{
				coinID = Guid.NewGuid().ToString();
			}

			audioStore = FindObjectOfType<AudioStore>();
		}

		/// <summary>
		/// Handles the logic of collision with the player
		/// </summary>
		/// <param name="other"></param>
		private void OnTriggerEnter(Collider other)
		{
			if (other == null) return;

			var pointsStore = other.transform.GetComponent<PointsStore>();
			if (pointsStore == null) return;

			pointsStore.AddPoints(pointsAward);

			audioStore.Play(audioBGS);

			if (coinsManager == null)
				coinsManager= FindObjectOfType<CoinsManager>();

			coinsManager.DestroyCoin(this);
		}

		/// <summary>
		/// Returns the ID of this coin
		/// </summary>
		/// <returns></returns>
		public string GetID()
		{
			return coinID;
		}
	}
}