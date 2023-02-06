using CoreSystems.Audio;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

namespace Content.Coins
{
	/// <summary>
	/// This class manages all the coins in the game, 
	/// it makes the coins to spawn, despawn and also to position them
	/// </summary>
	public class CoinsManager : MonoBehaviour
	{
		// ### After the coin is spawned this value will be used to lift the coin in the Y axis,
		// so it won't be buried on the ground
		[SerializeField] float liftYCoinPosition;

		// ### The max quantity of coins that should spawn at a given time
		[SerializeField] int maxCoins;

		// ### The prefabs to be used as coins
		[SerializeField] Coin[] coinPrefabs;

		// ### The paths to spawn coins, in the scene it is some planes
		[SerializeField] MeshRenderer[] paths;

		// ### BGM -> BackGround Music
		[SerializeField] AudioClip audioBGM;

		// ### Reference to play music and sounds
		[SerializeField] AudioStore audioStore;

		// ### The current list of spawn coins
		List<Coin> coins = new List<Coin>();

		private void Start()
		{
			RefreshCoins();

			audioStore.Play(audioBGM);
		}

		/// <summary>
		/// Destroy the coins, remove them from the list of current spawned coins and spawn a new coin
		/// </summary>
		/// <param name="coin"></param>
		public void DestroyCoin(Coin coin)
		{
			if(HasCoin(coin))
			{
				RemoveCoinFromList(coin);
				Destroy(coin.gameObject);
				RefreshCoins();
				return;
			}
		}

		/// <summary>
		/// It checks the coin ID that is on the list of spawned coins with the desired coin and then destroys it
		/// after it was found
		/// </summary>
		/// <param name="coin"></param>
		public void RemoveCoinFromList(Coin coin)
		{
			var tempCoinList = new List<Coin>(coins);
			foreach(var c in tempCoinList)
			{
				if(c.GetID() == coin.GetID())
				{
					coins.Remove(c);
					return;
				}
			}
		}

		/// <summary>
		/// Destroys all coins in the Scene and empty the list of the current coins spawned,
		/// then it recreats all of them
		/// </summary>
		public void RestartAllCoins()
		{
			var foundCoins = FindObjectsOfType<Coin>();
			foreach(var coin in foundCoins)
			{
				Destroy(coin.gameObject);
			}

			coins.Clear();
			RefreshCoins();
		}

		/// <summary>
		/// This method makes the coins to spawn in the paths (or the planes in the scene), 
		/// while at the same time it randoms a position inside of bounds
		/// </summary>
		/// <param name="path"></param>
		/// <returns></returns>
		public Vector3 GetRandomPosition(MeshRenderer path)
		{
			float x_dim = path.bounds.size.x / 2;
			float z_dim = path.bounds.size.z / 2;

			var x_rand = UnityEngine.Random.Range(-x_dim, x_dim);
			var z_rand = UnityEngine.Random.Range(-z_dim, z_dim);

			// Random the y position from the smallest bewteen x and z
			z_rand = x_rand > z_rand ? UnityEngine.Random.Range(0, z_rand) : UnityEngine.Random.Range(0, x_rand);

			return new Vector3(path.transform.position.x + x_rand, 0,
				path.transform.position.z + z_rand);
		}

		/// <summary>
		/// Verifies if the desired coin already exists on the spawned coins list
		/// </summary>
		/// <param name="coin"></param>
		/// <returns></returns>
		public bool HasCoin(Coin coin)
		{
			foreach(var c in coins)
			{
				if (c.GetID() == coin.GetID())
					return true;
			}

			return false;
		}

		/// <summary>
		/// Verifies how many coins are left to be spawned, based on the value maxCoins and spawns a new coins
		/// using a random coin prefab
		/// </summary>
		void RefreshCoins()
		{
			var totalCoins = coins.Count;

			for (int i = totalCoins; i < maxCoins; i++)
			{
				int coinsPropertyIndex = UnityEngine.Random.Range(0, coinPrefabs.Length - 1);

				var coin = Instantiate(coinPrefabs[coinsPropertyIndex]);
				coins.Add(coin);

				int index = GetRandomMeshRendererIndex();
				var position = GetRandomPosition(paths[index]);
				coin.transform.position = new Vector3(position.x, position.y + liftYCoinPosition, position.z);
			}
		}

		/// <summary>
		/// Randomizes a path that should be used as a reference to spawn a coin
		/// </summary>
		/// <returns></returns>
		int GetRandomMeshRendererIndex()
		{
			return UnityEngine.Random.Range(0, paths.Length - 1);
		}
	}
}