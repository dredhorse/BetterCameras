using UnityEngine;
using BetterCameras.BetterPerspective;
using System;

namespace BetterCameras
{
	public class Main : IMod, IModSettings
    {
		public string Name { get { return "BetterCameras"; } }
		public string Description { get { return "Cameras Galore"; } }
		public string Identifier { get; set; }

		public Main BetterCamerasMain;

		public GameObject _go;

		public static BetterCamerasSettings Settings = null;
		public bool PerspectiveCameraRunning = false;
		public bool GuestCameraRunning = false;
		public bool RideCameraRunning = false;
		public bool WalkCameraRunning = false;

		public BetterPerspectiveCamera PerspectiveCamera;
		public BetterPerspectiveCameraKeys PerspectiveCameraKeys;
		public BetterPerspectiveCameraMouse PerspectiveCameraMouse;

		public Main()
		{
			BetterCamerasMain = this;
		}

		public void onEnabled()
		{
			_go = new GameObject ();
			_go.AddComponent<BetterCamerasSettings> ();
			if (Settings == null)
			{
				Settings = _go.GetComponent<BetterCamerasSettings>();
			}
			Settings.BetterCamerasMain = BetterCamerasMain;
			Settings.onEnable ();
			if (Settings.PerspectiveCameraEnabled && !PerspectiveCameraRunning)
			{
				enableBetterPerspectiveCamera();
			}
				

		}

		public void onDisabled()
		{
			Settings.Save();
			if (PerspectiveCameraRunning)
			{
				disableBetterPerspectiveCamera();
			}
			UnityEngine.Object.Destroy (_go);
		}


		public void onDrawSettingsUI()
		{
			Settings.onDrawSettingsUI ();

		}

		public void onSettingsOpened()
		{
			Settings.onSettingsOpened ();
		}

		public void onSettingsClosed()
		{
			Settings.onSettingsClosed ();
			if (Settings.PerspectiveCameraEnabled && !PerspectiveCameraRunning)
			{
				enableBetterPerspectiveCamera ();
			}
			if (!Settings.PerspectiveCameraEnabled && PerspectiveCameraRunning)
			{
				disableBetterPerspectiveCamera ();
			}

		}

		public void RefreshSettings()
		{
			if (PerspectiveCameraRunning)
			{
				PerspectiveCamera.RefreshSettings ();	
			}
		}

		public void Reset()
		{
			Settings.Reset ();
			if (PerspectiveCameraRunning)
			{
				PerspectiveCamera.ResetToInitialValues(true, false); // need to figure out the difference between the two
				PerspectiveCamera.Reset ();
			}
		}

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

			Camera.main.gameObject.AddComponent<BetterPerspectiveCamera>();
			Camera.main.gameObject.AddComponent<BetterPerspectiveCameraKeys>();
			Camera.main.gameObject.AddComponent<BetterPerspectiveCameraMouse>();
			Camera.main.gameObject.AddComponent<AudioListener>();
			UnityEngine.Object.Destroy(go);
			PerspectiveCamera = Camera.main.gameObject.GetComponent<BetterPerspectiveCamera>();
			PerspectiveCameraKeys = Camera.main.gameObject.GetComponent<BetterPerspectiveCameraKeys>();
			PerspectiveCameraMouse = Camera.main.gameObject.GetComponent<BetterPerspectiveCameraMouse>();
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
