using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CoreSystems.Player.Camera
{
	/// <summary>
	/// This class consists in making the camera follow the position of a desired game object
	/// </summary>
	public class CameraFollower : MonoBehaviour
	{
		// ### The desired game object that the camera have to follow
		[SerializeField] Transform cameraPosition;

		/// <summary>
		/// Handles the logic to make the camera follow a desired game object
		/// </summary>
		void Update()
		{
			transform.position = cameraPosition.position;
		}
	}
}