using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CoreSystems.Player.Camera
{
	/// <summary>
	/// This class handles the rotation, orientation of the camera and also it's input logic
	/// </summary>
	public class CameraController : MonoBehaviour
	{
		// ### Defines the sensivity of rotation of the camera in the X-axis
		[SerializeField] float sensivityX;

		// ### Defines the sensivity of rotation of the camera in the Y-axis
		[SerializeField] float sensivityY;

		// ### The reference of the game object that it will be used as the orientation for the camera
		[SerializeField] Transform orientation;

		// ### Current X rotation and Y rotation
		float xRotation;
		float yRotation;

		/// <summary>
		/// Lock the cursor and make it invisible
		/// </summary>
		private void Start()
		{
			Cursor.lockState = CursorLockMode.Locked;
			Cursor.visible = false;
		}

		/// <summary>
		/// Handles the camera's rotation by input and it's orientation
		/// </summary>
		private void Update()
		{
			float mouseXInput = Input.GetAxisRaw("Mouse X") * Time.deltaTime * sensivityX;
			float mouseYInput = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * sensivityY;

			yRotation += mouseXInput;
			xRotation -= mouseYInput;

			xRotation = Mathf.Clamp(xRotation, -90f, 90f);

			// rotate cam and orientation
			transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
			orientation.rotation = Quaternion.Euler(0, yRotation, 0);
		}
	}
}