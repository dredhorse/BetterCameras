using System;
using UnityEngine;

namespace BetterCameras.BetterPerspective
{
	public class BetterPerspectiveCameraKeys : MonoBehaviour
	{
		public bool AllowMove = true;
		public float MoveSpeed;
		public float MoveSpeedVar;
		public bool AllowFastMove = true;
		public float FastMoveSpeed;
		public KeyCode FastMoveKeyCode1;
		public KeyCode FastMoveKeyCode2;

		public bool AllowRotate = true;
		public float RotateSpeed;
		public float RotateSpeedVar;

		public bool AllowZoom = true;
		public float ZoomSpeed;
		public float ZoomSpeedVar;

		public bool AllowTilt = true;
		public float TiltSpeed;

		public KeyCode ResetKey;
		public bool IncludePositionOnReset;

		public bool MovementBreaksFollow;

		public string HorizontalInputAxis = "Horizontal";
		public string VerticalInputAxis = "Vertical";

		public bool RotateUsesInputAxis = false;
		public string RotateInputAxis = "KbCameraRotate";
		public KeyCode RotateLeftKey;
		public KeyCode RotateRightKey;

		public bool ZoomUsesInputAxis = false;
		public string ZoomInputAxis = "KbCameraZoom";
		public KeyCode ZoomOutKey;
		public KeyCode ZoomInKey;

		public bool TiltUsesInputAxis = false;
		public string TiltInputAxis = "KbCameraTilt";
		public KeyCode TiltUpKey;
		public KeyCode TiltDownKey;

		//

		private BetterPerspectiveCamera _BPCamera;
		public BetterCamerasSettings BCSettings;

		//

		public void Reset()
		{
			AllowMove = true;
			AllowFastMove = true;
			AllowRotate = true;
			AllowZoom = true;
			AllowTilt = true;
			IncludePositionOnReset = true;
			MovementBreaksFollow = true;
			RefreshSettings ();
		}

		public void Awake()
		{
			Start ();
		}

		public void RefreshSettings()
		{
			MoveSpeed = BCSettings.CameraMoveSpeed;
			FastMoveSpeed = BCSettings.CameraFastMoveSpeed;
			RotateSpeed = BCSettings.CameraKeyRotateSpeed;
			ZoomSpeed = BCSettings.CameraZoomSpeed;
			TiltSpeed = BCSettings.CameraTiltSpeed;
			FastMoveKeyCode1 = BCSettings.KeyboardFastMove;
			FastMoveKeyCode2 = BCSettings.KeyboardFastMove;
			ResetKey = BCSettings.KeyboardReset;
			RotateLeftKey = BCSettings.KeyboardRotateLeft;
			RotateRightKey = BCSettings.KeyboardRotateRight;
			ZoomOutKey = BCSettings.KeyboardZoomOut;
			ZoomInKey = BCSettings.KeyboardZoomIn;
			TiltUpKey = BCSettings.KeyboardTiltUp;
			TiltDownKey = BCSettings.KeyboardTiltDown;
		}

		protected void Start()
		{
			_BPCamera = gameObject.GetComponent<BetterPerspectiveCamera>();
			BCSettings = Main.BCSettings;
			RefreshSettings ();
		}

		protected void Update()
		{
			float num = 0.02f;

			if (_BPCamera == null)
				return;

			if (AllowMove && (!_BPCamera.IsFollowing || MovementBreaksFollow))
			{
				var hasMovement = false;

				var speed = MoveSpeed;
				if (AllowFastMove && (Input.GetKey(FastMoveKeyCode1) || Input.GetKey(FastMoveKeyCode2)))
				{
					speed = FastMoveSpeed;
				}

				var h = Input.GetAxisRaw(HorizontalInputAxis);
				if (Mathf.Abs(h) > 0.001f)
				{
					hasMovement = true;
					_BPCamera.AddToPosition(h * speed * num, 0, 0);
				}

				var v = Input.GetAxisRaw(VerticalInputAxis);
				if (Mathf.Abs(v) > 0.001f)
				{
					hasMovement = true;
					_BPCamera.AddToPosition(0, 0, v * speed * num);
				}

				if (hasMovement && _BPCamera.IsFollowing && MovementBreaksFollow)
					_BPCamera.EndFollow();
			}



			if (AllowRotate)
			{
				if (RotateUsesInputAxis)
				{
					var rot = Input.GetAxisRaw(RotateInputAxis);
					if (Mathf.Abs(rot) > 0.001f)
					{
						_BPCamera.Rotation += rot * RotateSpeed * num;
					}
				}
				else
				{
					if (Input.GetKey(RotateLeftKey))
					{
						_BPCamera.Rotation += RotateSpeed * num;
					}
					if (Input.GetKey(RotateRightKey))
					{
						_BPCamera.Rotation -= RotateSpeed * num;
					}
				}
			}

			if (AllowZoom)
			{
				if (ZoomUsesInputAxis)
				{
					var zoom = Input.GetAxisRaw(ZoomInputAxis);
					if (Mathf.Abs(zoom) > 0.001f)
					{
						_BPCamera.Distance += zoom * ZoomSpeed * num;
					}
				}
				else
				{
					if (Input.GetKey(ZoomOutKey))
					{
						_BPCamera.Distance += ZoomSpeed * num;
					}
					if (Input.GetKey(ZoomInKey))
					{
						_BPCamera.Distance -= ZoomSpeed * num;
					}
				}
			}

			if (AllowTilt)
			{
				if (TiltUsesInputAxis)
				{
					var tilt = Input.GetAxisRaw(TiltInputAxis);
					if (Mathf.Abs(tilt) > 0.001f)
					{
						_BPCamera.Tilt += tilt * TiltSpeed * num;
					}
				}
				else
				{
					if (Input.GetKey(TiltUpKey))
					{
						_BPCamera.Tilt += TiltSpeed * num;
					}
					if (Input.GetKey(TiltDownKey))
					{
						_BPCamera.Tilt -= TiltSpeed * num;
					}
				}
			}
		}
	}
}

