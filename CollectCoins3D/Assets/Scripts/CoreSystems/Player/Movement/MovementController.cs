using Newtonsoft.Json.Bson;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CoreSystems.Player.Movement
{
	/// <summary>
	/// This class has the logic to move the player and to make it jump
	/// </summary>
	public class MovementController : MonoBehaviour
	{
		[Header("Movement")]
		[SerializeField] float movementSpeed;
		[SerializeField] Transform orientation;

		[Header("Jumping")]
		[SerializeField] float jumpForce;
		[SerializeField] float jumpCooldown;
		[SerializeField] float airMultiplier;

		[Header("Keybinds")]
		[SerializeField] KeyCode jumpKey = KeyCode.Space;

		[Header("Ground Check")]
		[SerializeField] float groundDrag;
		[SerializeField] float playerHeight;
		[SerializeField] LayerMask groundLayer;

		bool readyToJump = true;
		bool isOnGround = true;

		float horizontalInput;
		float verticalInput;

		Vector3 movementDirection;

		Vector3 originalPosition;
		Quaternion originalRotation;

		Rigidbody rb;

		/// <summary>
		/// Get's the Rigidbody component reference and 
		/// set's the original position and original rotation of the player
		/// </summary>
		private void Start()
		{
			rb = GetComponent<Rigidbody>();
			rb.freezeRotation= true;

			originalPosition = transform.localPosition;
			originalRotation = transform.rotation;
		}

		/// <summary>
		/// Checks if the player is on Ground, also manages the player controls, the player speed
		/// </summary>
		private void Update()
		{
			// ground check
			isOnGround = Physics.Raycast(transform.position, Vector3.down, 
				playerHeight * 0.5f + 0.2f, groundLayer);

			MyInput();
			SpeedControl();

			// handle drag
			if (isOnGround)
			{
				rb.drag = groundDrag;
			}
			else
			{
				rb.drag = 0f;
			}
		}

		/// <summary>
		/// Manages the player Movement
		/// </summary>
		private void FixedUpdate()
		{
			MovePlayer();
		}

		/// <summary>
		/// Set the player's position to a desired position in the world
		/// </summary>
		/// <param name="position"></param>
		public void SetPosition(Vector3 position)
		{
			transform.position = position;
		}

		/// <summary>
		/// Set the player's rotation using the euler angles
		/// </summary>
		/// <param name="eulerRotation"></param>
		public void SetRotation(Vector3 eulerRotation)
		{
			transform.eulerAngles = eulerRotation;
		}

		/// <summary>
		/// Set the player's rotation
		/// </summary>
		/// <param name="rotation"></param>
		public void SetRotation(Quaternion rotation)
		{
			transform.rotation = rotation;
		}

		/// <summary>
		/// Resets the player's position back to their 
		/// original position when the game started and their rotation
		/// </summary>
		public void ResetPlayerPositionAndRotation()
		{
			transform.localPosition = originalPosition;
			transform.localRotation = originalRotation;
		}

		/// <summary>
		/// Manages the player input commands
		/// </summary>
		void MyInput()
		{
			horizontalInput = Input.GetAxisRaw("Horizontal");
			verticalInput = Input.GetAxisRaw("Vertical");	
				
			// When to Jump
			if(Input.GetKey(jumpKey) && readyToJump && isOnGround)
			{
				readyToJump = false;
				Jump();
				Invoke(nameof(ResetJump), jumpCooldown);
			}
		}

		/// <summary>
		/// Moves the player to forward direction
		/// </summary>
		void MovePlayer()
		{
			// calculate movement direction

			movementDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;
			
			// On ground
			if(isOnGround)
				rb.AddForce(movementDirection.normalized * movementSpeed * 10f, ForceMode.Force);

			// In air
			else if(!isOnGround)
				rb.AddForce(movementDirection.normalized * movementSpeed * 10f* airMultiplier, ForceMode.Force);
		}

		/// <summary>
		/// Fixes the player movement, applying a limitation, 
		/// so the player won't be slippery and also the movement
		/// speed will be constant after reaching the threshold
		/// </summary>
		void SpeedControl()
		{
			Vector3 flatVelocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

			// limit velocity if needed
			if(flatVelocity.magnitude > movementSpeed)
			{
				Vector3 limitedVelocity = flatVelocity.normalized * movementSpeed;
				rb.velocity = new Vector3(limitedVelocity.x, rb.velocity.y, limitedVelocity.z);
			}
		}

		/// <summary>
		/// Handles the player Jump logic
		/// </summary>
		void Jump()
		{
			// reset y velocity

			rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

			rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
		}

		/// <summary>
		/// After then player jump cooldown has passed it should restart the 
		/// jumping condition so it will be able to jump again
		/// </summary>
		void ResetJump()
		{
			readyToJump = true;
		}
	}
}