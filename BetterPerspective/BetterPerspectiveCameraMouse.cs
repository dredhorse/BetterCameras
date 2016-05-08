using System;
using UnityEngine;

namespace BetterCameras.BetterPerspective
{
	public class BetterPerspectiveCameraMouse : MonoBehaviour
	{
		public KeyCode MouseOrbitButton = KeyCode.Mouse2;


		public bool ScreenEdgeMoveBreaksFollow = true;
		public int ScreenEdgeBorderWidth = 4;
		public float MoveSpeed;

		public bool AllowPan = true;
		public bool PanBreaksFollow = true;
		public float PanSpeed;

		public bool AllowRotate = true;
		public float RotateSpeed;

		public bool AllowTilt = true;
		public float TiltSpeed;

		public bool AllowZoom = true;
		public float ZoomSpeed;

		public string RotateInputAxis = "Mouse X";
		public string TiltInputAxis = "Mouse Y";
		public string ZoomInputAxis = "Mouse ScrollWheel";
		public KeyCode PanKey1 = KeyCode.LeftShift;
		public KeyCode PanKey2 = KeyCode.RightShift;

		public BetterCamerasSettings BCSettings = Main.BCSettings;

		//

		private BetterPerspectiveCamera _BPCamera;

		//

		public void Reset()
		{
			ScreenEdgeMoveBreaksFollow = true;
			ScreenEdgeBorderWidth = 4;
			AllowPan = true;
			PanBreaksFollow = true;
			AllowRotate = true;
			AllowTilt = true;
			AllowZoom = true;
			RotateInputAxis = "Mouse X";
			TiltInputAxis = "Mouse Y";
			ZoomInputAxis = "Mouse ScrollWheel";
		}

		protected void Start()
		{
			_BPCamera = gameObject.GetComponent<BetterPerspectiveCamera>();
			RefreshSettings ();
		}

		public void RefreshSettings()
		{
			MoveSpeed = BCSettings.CameraMoveSpeed;
			RotateSpeed = BCSettings.CameraMouseRotateSpeed;
			ZoomSpeed = BCSettings.CameraZoomSpeed;
			TiltSpeed = BCSettings.CameraTiltSpeed;
			PanSpeed= BCSettings.CameraPanSpeed;
			PanKey1 = BCSettings.KeyboardMouseDrag;
			PanKey2 = BCSettings.KeyboardMouseDrag;
			MouseOrbitButton = BCSettings.KeyboardMouseOrbit;   
		}

		protected void Update()
		{
			float num = 0.02f;
		
			if (_BPCamera == null)
				return; 

			MoveSpeed = BCSettings.CameraMoveSpeed;
			RotateSpeed = BCSettings.CameraMouseRotateSpeed;
			ZoomSpeed = BCSettings.CameraZoomSpeed;
			TiltSpeed = BCSettings.CameraTiltSpeed;
			PanSpeed= BCSettings.CameraPanSpeed;
			PanKey1 = BCSettings.KeyboardMouseDrag;
			PanKey2 = BCSettings.KeyboardMouseDrag;
			MouseOrbitButton = BCSettings.KeyboardMouseOrbit;  

			if (AllowZoom && !UIUtility.isMouseOverUIElement())
			{

				var scroll = -Input.GetAxisRaw(ZoomInputAxis);
				_BPCamera.Distance -= scroll * ZoomSpeed * num;
			}

			if (Input.GetKey(MouseOrbitButton))
			{
				if (AllowPan && (Input.GetKey(PanKey1) || Input.GetKey(PanKey2)))
				{

					var panX = -1 * Input.GetAxisRaw("Mouse X") * PanSpeed * num;
					var panZ = -1 * Input.GetAxisRaw("Mouse Y") * PanSpeed * num;

					_BPCamera.AddToPosition(panX, 0, panZ);

					if (PanBreaksFollow && (Mathf.Abs(panX) > 0.001f || Mathf.Abs(panZ) > 0.001f))
					{
						_BPCamera.EndFollow();
					}
				}
				else
				{


					if (AllowTilt)
					{
						var tilt = Input.GetAxisRaw(TiltInputAxis);
						_BPCamera.Tilt -= tilt * TiltSpeed * num;
					}

					if (AllowRotate)
					{
						var rot = Input.GetAxisRaw(RotateInputAxis);
						_BPCamera.Rotation += rot * RotateSpeed * num;
					}
				}
			}

			if (Settings.Instance.controlsEdgeScrolling && (!_BPCamera.IsFollowing || ScreenEdgeMoveBreaksFollow))
			{
				var hasMovement = false;

				if (Input.mousePosition.y > (Screen.height - ScreenEdgeBorderWidth))
				{
					hasMovement = true;
					_BPCamera.AddToPosition(0, 0, MoveSpeed * num);
				}
				else if (Input.mousePosition.y < ScreenEdgeBorderWidth)
				{
					hasMovement = true;
					_BPCamera.AddToPosition(0, 0, -1 * MoveSpeed * num);
				}

				if (Input.mousePosition.x > (Screen.width - ScreenEdgeBorderWidth))
				{
					hasMovement = true;
					_BPCamera.AddToPosition(MoveSpeed * num, 0, 0);
				}
				else if (Input.mousePosition.x < ScreenEdgeBorderWidth)
				{
					hasMovement = true;
					_BPCamera.AddToPosition(-1 * MoveSpeed * num, 0, 0);
				}

				if (hasMovement && _BPCamera.IsFollowing && ScreenEdgeMoveBreaksFollow)
				{
					_BPCamera.EndFollow();
				}
			}
		}
	}
}

