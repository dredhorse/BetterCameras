using UnityEngine;
using BetterCameras.BetterPerspective;
using System;
using BetterCameras.BetterRide;
using BetterCameras.BetterGuest;
using UnityEngine.Events;

namespace BetterCameras
{
	public class Main : IMod, IModSettings
    {
		public string Name { get { return "BetterCameras"; } }
		public string Description { get { return "Cameras Galore"; } }
		public string Identifier { get; set; }

		public Main BetterCamerasMain;

		public GameObject _go;

		public static BetterCamerasSettings BCSettings = null;
		public bool PerspectiveCameraRunning = false;
		public bool GuestCameraRunning = false;
		public bool RideCameraRunning = false;
		public bool WalkCameraRunning = false;

		public BetterPerspectiveCamera PerspectiveCamera;
		public BetterPerspectiveCameraKeys PerspectiveCameraKeys;
		public BetterPerspectiveCameraMouse PerspectiveCameraMouse;
		public BetterRideCamera RideCamera;
		public BetterRideMouse RideMouse;


		private GameObject RideCameraGameObject;
		private GameObject GuestCameraGameObject;
		private BetterGuestCamera GuestCamera;

		public Main()
		{
			BetterCamerasMain = this;
		}

		public void onEnabled()
		{
			_go = new GameObject ();
			_go.AddComponent<BetterCamerasSettings> ();
			if (BCSettings == null)
			{
				BCSettings = _go.GetComponent<BetterCamerasSettings>();
			}
			BCSettings.BetterCamerasMain = BetterCamerasMain;
			BCSettings.onEnable ();
			if (BCSettings.PerspectiveCameraEnabled && !PerspectiveCameraRunning)
			{
				enableBetterPerspectiveCamera();
			}
			if (BCSettings.RideCameraEnabled && !RideCameraRunning)
			{
				enableBetterRideCamera ();
			}	
			if (BCSettings.GuestCameraEnabled && !GuestCameraRunning)
			{
				enableBetterGuestCamera ();
			}

		}

		public void onDisabled()
		{
			BCSettings.Save();
			if (PerspectiveCameraRunning)
			{
				disableBetterPerspectiveCamera();
			}
			if (RideCameraRunning)
			{
				disableBetterRideCamera ();
			}
			if (GuestCameraRunning)
			{
				disableBetterGuestCamera ();
			}
			UnityEngine.Object.Destroy (_go);
		}


		public void onDrawSettingsUI()
		{
			BCSettings.onDrawSettingsUI ();

		}

		public void onSettingsOpened()
		{
			BCSettings.onSettingsOpened ();
		}

		public void onSettingsClosed()
		{
			BCSettings.onSettingsClosed ();
			if (BCSettings.PerspectiveCameraEnabled && !PerspectiveCameraRunning)
			{
				enableBetterPerspectiveCamera ();
			}
			if (!BCSettings.PerspectiveCameraEnabled && PerspectiveCameraRunning)
			{
				disableBetterPerspectiveCamera ();
			}
			if (BCSettings.RideCameraEnabled && !RideCameraRunning)
			{
				enableBetterRideCamera ();
			}
			if (!BCSettings.RideCameraEnabled && RideCameraRunning)
			{
				disableBetterRideCamera ();
			}
			if (BCSettings.GuestCameraEnabled && !GuestCameraRunning)
			{
				enableBetterGuestCamera ();
			}
			if (!BCSettings.GuestCameraEnabled && GuestCameraRunning)
			{
				disableBetterGuestCamera ();
			}
		}

		public void RefreshSettings()
		{
			if (PerspectiveCameraRunning)
			{
				PerspectiveCamera.RefreshSettings ();	
				PerspectiveCameraMouse.RefreshSettings ();
				PerspectiveCameraKeys.RefreshSettings ();
			}
			if (RideCameraRunning)
			{
				RideMouse._sensitivityX = BCSettings.CameraMouseRotateSpeed;
				RideMouse._sensitivityY = BCSettings.CameraMouseRotateSpeed;
			}
			if (GuestCameraRunning)
			{
				GuestCamera.RefreshSettings ();
			}
		}

		public void Reset()
		{
			BCSettings.Reset ();
			if (PerspectiveCameraRunning)
			{
				PerspectiveCamera.ResetToInitialValues(true, false); // need to figure out the difference between the two
				PerspectiveCamera.Reset ();
				PerspectiveCameraMouse.Reset ();
			}
			if (GuestCameraRunning)
			{
				GuestCamera.ApplySettings ();
			}

		}

		#region Better Guest Camera
		public void enableBetterGuestCamera()
		{
			GuestCameraGameObject = new GameObject ();
			GuestCamera = GuestCameraGameObject.AddComponent<BetterGuestCamera> ();
			Debug.Log ("Guest Camera Enabled");
			Debug.Log (GuestCamera);
			GuestCamera.GuestEnter = BCSettings.KeyboardGuest;
			GuestCameraRunning = true;
		}

		public void disableBetterGuestCamera()
		{
			UnityEngine.Object.DestroyImmediate (GuestCameraGameObject);
			GuestCameraRunning = false;
		}

		#endregion

		#region Better Ride Camera
		public void enableBetterRideCamera()
		{
			RideCameraGameObject = new GameObject ();
			RideCamera = RideCameraGameObject.AddComponent<BetterRideCamera> ();
			RideMouse = RideCameraGameObject.GetComponent<BetterRideMouse> ();
			RideCamera.RideKey = BCSettings.KeyboardRide;
			RideMouse._sensitivityX = BCSettings.CameraMouseRotateSpeed;
			RideMouse._sensitivityY = BCSettings.CameraMouseRotateSpeed;
			RideCameraRunning = true;
		}

		public void disableBetterRideCamera()
		{
			UnityEngine.Object.DestroyImmediate (RideCameraGameObject);
			RideCameraRunning = false;
		}
		#endregion

		#region BetterPerspectiveCamera
		private void enableBetterPerspectiveCamera()
		{
			GameObject go = new GameObject();
			Camera cam2 = go.AddComponent<Camera>();

			Camera cam = Camera.main;
			cam.aspect = cam2.aspect;
			cam.backgroundColor = cam2.backgroundColor;
			cam.clearFlags = cam2.clearFlags;
			cam.clearStencilAfterLightingPass = cam2.clearStencilAfterLightingPass;
			cam.cullingMask = cam2.cullingMask;
			cam.depth = cam2.depth;
			cam.depthTextureMode = cam2.depthTextureMode;
			cam.farClipPlane = cam2.farClipPlane;
			cam.fieldOfView = cam2.fieldOfView;
			cam.hdr = cam2.hdr;
			cam.layerCullDistances = cam2.layerCullDistances;
			cam.layerCullSpherical = cam2.layerCullSpherical;
			cam.nearClipPlane = cam2.nearClipPlane;
			cam.pixelRect = cam2.pixelRect;
			cam.projectionMatrix = cam2.projectionMatrix;
			cam.rect = cam2.rect;
			cam.renderingPath = cam2.renderingPath;
			cam.stereoConvergence = cam2.stereoConvergence;
			cam.stereoMirrorMode = cam2.stereoMirrorMode;
			cam.stereoSeparation = cam2.stereoSeparation;
			cam.targetDisplay = cam2.targetDisplay;
			cam.targetTexture = cam2.targetTexture;
			cam.transparencySortMode = cam2.transparencySortMode;
			cam.useOcclusionCulling = cam2.useOcclusionCulling;


			UnityEngine.Object.DestroyImmediate(cam.gameObject.GetComponent<CameraController>());

			PerspectiveCamera = Camera.main.gameObject.AddComponent<BetterPerspectiveCamera>();
			PerspectiveCameraKeys =Camera.main.gameObject.AddComponent<BetterPerspectiveCameraKeys>();
			PerspectiveCameraMouse = Camera.main.gameObject.AddComponent<BetterPerspectiveCameraMouse>();
			Camera.main.gameObject.AddComponent<AudioListener>();
			UnityEngine.Object.Destroy(go);
			PerspectiveCamera.RefreshSettings ();	
			PerspectiveCameraMouse.RefreshSettings ();
			PerspectiveCameraRunning = true;
		}

		private void disableBetterPerspectiveCamera()
		{
			UnityEngine.Object.DestroyImmediate(Camera.main.gameObject.GetComponent<BetterPerspectiveCamera>());
			UnityEngine.Object.DestroyImmediate(Camera.main.gameObject.GetComponent<BetterPerspectiveCameraKeys>());
			UnityEngine.Object.DestroyImmediate(Camera.main.gameObject.GetComponent<BetterPerspectiveCameraMouse>());
			Camera.main.gameObject.AddComponent<CameraController>();
			PerspectiveCameraRunning = false;
		}
		#endregion

    }
}
