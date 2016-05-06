using System;
using UnityEngine;

namespace BetterCameras.BetterRide
{
	public class BetterRideMouse : MonoBehaviour
	{
		public enum RotationAxes
		{
			MouseXAndY = 0, MouseX = 1, MouseY = 2
		}

		// Configured Lookaround to X and Y axis
		public RotationAxes Axes = RotationAxes.MouseXAndY;


		public float _sensitivityX = 10F;
		public float _sensitivityY = 10F;
		private float _minimumX = -135F;
		private float _maximumX = 135F;
		private float _minimumY = -60F;
		private float _maximumY = 60F;


		private float _rotationY;
		private float _rotationX;

		void Update()
		{
			// Allows Lookaround in X and Y
			if (Axes == RotationAxes.MouseXAndY)
			{
				_rotationX = transform.localEulerAngles.y + Input.GetAxis("Mouse X") * _sensitivityX / 4;
				//_rotationX = Mathf.Clamp (_rotationX, _minimumX, _maximumX);
				_rotationY += Input.GetAxis("Mouse Y") * _sensitivityY / 4;
				_rotationY = Mathf.Clamp(_rotationY, _minimumY, _maximumY);

				transform.localEulerAngles = new Vector3(-_rotationY, _rotationX, 0);
			}
			// Allows Lookaround only in X
			else if (Axes == RotationAxes.MouseX)
			{
				_rotationX = Input.GetAxis ("Mouse X") * _sensitivityX / 2;
				_rotationX = Mathf.Clamp (_rotationX, _minimumX, _maximumX);
				transform.Rotate(0, _rotationX, 0);
			}
			else
				// Allows Lookaround ony in Y
			{
				_rotationY += Input.GetAxis("Mouse Y") * _sensitivityY / 2;
				_rotationY = Mathf.Clamp(_rotationY, _minimumY, _maximumY);

				transform.localEulerAngles = new Vector3(-_rotationY, transform.localEulerAngles.y, 0);
			}
		}
	}
}

