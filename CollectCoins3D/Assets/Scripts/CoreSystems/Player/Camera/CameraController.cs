using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

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

		// ### Holds the mouse input axis
		float mouseXInput;
		float mouseYInput;

		/// <summary>
		/// Handles the camera's rotation by input and it's orientation
		/// </summary>
		private void Update()
		{
			if (Input.GetMouseButton(1))
			{
				Cursor.lockState = CursorLockMode.Locked;
				Cursor.visible = false;

				mouseXInput = Input.GetAxisRaw("Mouse X") * Time.deltaTime * sensivityX;
				mouseYInput = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * sensivityY;
			}
			else if (Input.GetMouseButtonUp(1))
			{
				Cursor.lockState = CursorLockMode.None;
				Cursor.visible = true;

				mouseXInput = 0;
				mouseYInput = 0;
			}

			yRotation += mouseXInput;
			xRotation -= mouseYInput;

			xRotation = Mathf.Clamp(xRotation, -90f, 90f);

			// ### Rotate cam and orientation
			transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
			orientation.rotation = Quaternion.Euler(0, yRotation, 0);
		}
	}
}