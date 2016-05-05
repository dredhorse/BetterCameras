using System;
using UnityEngine;

namespace BetterCameras.BetterPerspective
{
	public class BetterPerspectiveCameraKeys : MonoBehaviour
	{
		public bool AllowMove;
		public float MoveSpeed;
		public float MoveSpeedVar;
		public bool AllowFastMove;
		public float FastMoveSpeed;
		public KeyCode FastMoveKeyCode1;
		public KeyCode FastMoveKeyCode2;

		public bool AllowRotate;
		public float RotateSpeed;
		public float RotateSpeedVar;

		public bool AllowZoom;
		public float ZoomSpeed;
		public float ZoomSpeedVar;

		public bool AllowTilt;
		public float TiltSpeed;

		public KeyCode ResetKey;
		public bool IncludePositionOnReset;

		public bool MovementBreaksFollow;

		public string HorizontalInputAxis = "Horizontal";
		public string VerticalInputAxis = "Vertical";

		public bool RotateUsesInputAxis = false;
		public string RotateInputAxis = "KbCameraRotate";
		public KeyCode RotateLeftKey = KeyCode.Q;
		public KeyCode RotateRightKey = KeyCode.E;

		public bool ZoomUsesInputAxis = false;
		public string ZoomInputAxis = "KbCameraZoom";
		public KeyCode ZoomOutKey = KeyCode.Z;
		public KeyCode ZoomInKey = KeyCode.X;

		public bool TiltUsesInputAxis = false;
		public string TiltInputAxis = "KbCameraTilt";
		public KeyCode TiltUpKey = KeyCode.R;
		public KeyCode TiltDownKey = KeyCode.F;

		//

		private BetterPerspectiveCamera _BPCamera;

		//

		public void Reset()
		{
			AllowMove = true;
			MoveSpeedVar = 15f;
			MoveSpeed = MoveSpeedVar;

			AllowFastMove = true;
			FastMoveSpeed = 40f;
			FastMoveKeyCode1 = KeyCode.LeftShift;
			FastMoveKeyCode2 = KeyCode.RightShift;

			AllowRotate = true;
			RotateSpeedVar = 180f;

			AllowZoom = true;
			ZoomSpeedVar = 15f;

			AllowTilt = true;
			TiltSpeed = 90f;

			ResetKey = KeyCode.Home;
			IncludePositionOnReset = true;

			MovementBreaksFollow = true;
		}

		protected void Start()
		{

			_BPCamera = gameObject.GetComponent<BetterPerspectiveCamera>();
		}

		protected void Update()
		{
			float num = 0.02f;

			FastMoveSpeed = MoveSpeedVar * 2;
			MoveSpeed = MoveSpeedVar;
			RotateSpeed = RotateSpeedVar;
			ZoomSpeed = ZoomSpeedVar;
			TiltSpeed = 90f;

			if (_BPCamera == null)
				return; // no camera, bail!

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

			//


		}
	}
}

