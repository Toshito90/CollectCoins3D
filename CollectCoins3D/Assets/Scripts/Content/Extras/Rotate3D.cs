using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Content.Extras
{
	/// <summary>
	/// This class makes the object to rotate, used only for animation purposes
	/// </summary>
	public class Rotate3D : MonoBehaviour
	{
		// ### Defines a desired direction to rotate the game object
		[SerializeField] Vector3 rotationDirection;

		// ### Defines the frequency speed of the game object's rotation
		[SerializeField] float smoothTime;

		// ### Converts the time
		private float convertedTime = 200;

		// ### Used only to smooth the game object's animation
		private float smooth;

		void Update()
		{
			smooth = Time.deltaTime * smoothTime * convertedTime;
			transform.Rotate(rotationDirection * smooth);
		}
	}
}