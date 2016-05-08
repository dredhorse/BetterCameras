using System;
using System.Collections.Generic;
using UnityEngine;
using System.Security.Principal;

namespace BetterCameras
{
	public class BetterCamerasSettings : MonoBehaviour, IModSettings
	{
		public const string CAMERA_SETTINGS = "Camera Settings";
		public const string CAMERA_SETTING_SMOOTH = "Smooth";
		public const string CAMERA_SETTING_SMOOTHNESS = "Smoothness";
		public const string CAMERA_SETTING_MOVE_SPEED = "Move Speed";
		public const string CAMERA_SETTING_FAST_MOVE_SPEED = "Fast Move Speed" ;
		public const string CAMERA_SETTING_ROTATE_SPEED = "Rotate Speed";
		public const string CAMERA_SETTING_ZOOM_SPEED = "Zoom Speed";
		public const string CAMERA_SETTING_TILT_SPEED = "Tilt Speed" ;
		public const string CAMERA_SETTING_PAN_SPEED = "Pan Speed" ;
		public const string CAMERA_SETTING_GUEST_DISTANCE = "Guest Camera Distance" ;
		public const string CAMERA_SETTING_GUEST_HEIGHT = "Guest Camera Height";
		public const string CAMERA_SETTING_DRAWING_DISTANCE = "Drawing Distance" ;
		public const string CAMERA_SETTING_QUALITY_SETTINGS = "Quality Settings" ;
		public const string CAMERA_SETTING_DEFAULT_SETTINGS = "Default Settings" ;
		public const string CAMERA_SETTING_PUSHKEY = "Press {0} to toggle this window" ;

		public const string KEYBOARD_SETTINGS = "Keyboard Settings" ;
		public const string KEYBOARD_SETTING_ROTATE_LEFT = "Rotate Left Key" ;
		public const string KEYBOARD_SETTING_ROTATE_RIGHT = "Rotate Right Key" ;
		public const string KEYBOARD_SETTING_ZOOM_OUT = "Zoom Out Key" ;
		public const string KEYBOARD_SETTING_ZOOM_IN = "Zoom In Key" ;
		public const string KEYBOARD_SETTING_TILT_UP = "Tilt Up Key" ;
		public const string KEYBOARD_SETTING_TILT_DOWN = "Tilt Down Key" ;
		public const string KEYBOARD_SETTING_FASTMOVE = "Fast Move Key" ;
		public const string KEYBOARD_SETTING_MOUSE_DRAG = "Mouse Drag Key" ;
		public const string KEYBOARD_SETTING_MOUSE_ORBIT_BUTTON = "Mouse Orbit Button" ;
		public const string KEYBOARD_SETTING_RIDE = "Enter / Leave Ride Key" ;
		public const string KEYBOARD_SETTING_GUEST = "Follow Guest Key" ;
		public const string KEYBOARD_SETTING_WALK = "Walk Key" ;
		public const string KEYBOARD_SETTING_RESET = "Reset Key" ;
		public const string KEYBOARD_SETTING_CAMERA_SETTING = "Camera Settings Menu" ;

		public const string COMPONENT_SETTINGS = "Component Settings" ;
		public const string COMPONENT_SETTING_PERSPECTIVE_CAMERA = "Enable Perspective Camera" ;
		public const string COMPONENT_SETTING_RIDE_CAMERA = "Enable Ride Camera" ;
		public const string COMPONENT_SETTING_GUEST_CAMERA = "Enable Guest Camera" ;
		public const string COMPONENT_SETTING_WALK_CAMERA = "Enable Walk the Park Camera" ;

		const string cameraSmoothing = "CameraSmoothing";
		const string cameraMoveSpeed = "CameraMoveSpeed";
		const string cameraSmoothness = "CameraSmoothness";
		const string cameraFastMoveSpeed = "CameraFastMoveSpeed";
		const string cameraMouseRotateSpeed = "CameraMouseRotateSpeed";
		const string cameraKeyRotateSpeed = "CameraKeyRotateSpeed";
		const string cameraZoomSpeed = "CameraZoomSpeed";
		const string cameraTiltSpeed = "CameraTiltSpeed";
		const string cameraPanSpeed = "CameraPanSpeed";
		const string cameraGuestDistance = "CameraGuestDistance";
		const string cameraGuestHeight = "CameraGuestHeight";
		const string cameraDrawingDistance = "CameraDrawingDistance";
		const string cameraQualitySetting = "CameraQualitySetting";
		const string kbRotateLeft = "KbRotateLeft";
		const string kbRotateRight = "KbRotateRight";
		const string kbZoomOut = "KbZoomOut";
		const string kbZoomIn = "KbZoomIn";
		const string kbTiltUp = "KbTiltUp";
		const string kbTiltDown = "KbTiltDown";
		const string kbMouseDrag = "KbMouseDrag";
		const string kbMouseOrbit = "KbMouseOrbit";
		const string kbRide = "KbRide";
		const string kbGuest = "KbGuest";
		const string kbWalk = "KbWalk";
		const string kbReset = "KbReset";
		const string kbFastMove = "KbFastMove";
		const string kbCameraSettings = "KbCameraSettings";
		const string perspectiveCameraEnabled = "PerspectiveCameraEnabled";
		const string rideCameraEnabled = "RideCameraEnabled";
		const string walkCameraEnabled = "WalkCameraEnabled";
		const string guestCameraEnabled = "GuestCameraEnabled";
		const string betterCamerasSettings = "BetterCamerasSettings";


		public bool CameraSmoothing;
		public float CameraSmoothness;
		public float CameraMoveSpeed;
		public float CameraMouseRotateSpeed;
		public float CameraKeyRotateSpeed;
		public float CameraZoomSpeed;
		public float CameraFastMoveSpeed;
		public float CameraTiltSpeed;
		public float CameraPanSpeed;
		public float CameraGuestDistance;
		public float CameraGuestHeight;
		public float CameraDrawingDistance;
		public int CameraQualitySetting;

		public KeyCode KeyboardRotateLeft = KeyCode.Q;
		public KeyCode KeyboardRotateRight = KeyCode.E;
		public KeyCode KeyboardZoomOut = KeyCode.Z;
		public KeyCode KeyboardZoomIn = KeyCode.X;
		public KeyCode KeyboardTiltUp = KeyCode.R;
		public KeyCode KeyboardTiltDown = KeyCode.F;
		public KeyCode KeyboardFastMove = KeyCode.RightShift;
		public KeyCode KeyboardMouseDrag = KeyCode.LeftControl;
		public KeyCode KeyboardMouseOrbit = KeyCode.Mouse2;
		public KeyCode KeyboardRide = KeyCode.T;
		public KeyCode KeyboardGuest = KeyCode.O;
		public KeyCode KeyboardWalk = KeyCode.Tab;
		public KeyCode KeyboardReset = KeyCode.Home;
		public KeyCode KeyboardCameraSettings = KeyCode.P;

		public bool PerspectiveCameraEnabled = true;
		public bool RideCameraEnabled = true;
		public bool GuestCameraEnabled = true;
		public bool WalkCameraEnabled = true;
		public bool ShowCameraSettings = false;

		public Main BetterCamerasMain;
		public BetterCamerasSettings BCSettings;

		private Rect windowRect = new Rect(5, 70, 250, 600);
		private bool SettingsLoaded = false;
		private Dictionary<KeyCode,bool>GuiToggle = new Dictionary<KeyCode, bool>();


		public BetterCamerasSettings ()  
		{
			BCSettings = this;
		}

		void Start()
		{
		}

		public void onEnable()
		{
			Load();
			if (!SettingsLoaded)
			{
				Reset ();
				PerspectiveCameraEnabled = true;
				RideCameraEnabled = true;
				GuestCameraEnabled = true;
				WalkCameraEnabled = true;
				Save ();
			}
		}

		public void onDisable()
		{
			Save ();
		}

		protected void Update()
		{
			if(Input.GetKeyUp(KeyCode.P))
			{
				ShowCameraSettings = !ShowCameraSettings;
			}
			if (ShowCameraSettings && Input.GetKey(KeyCode.Escape))
			{
				ShowCameraSettings = false;
			}
			if (KeyboardReset != KeyCode.None)
			{
				if (Input.GetKeyDown(KeyboardReset))
				{
					BetterCamerasMain.Reset ();
				}
			}
		}

		public void Reset()
		{
			CameraSmoothing = true;
			CameraSmoothness = 7F;
			CameraMoveSpeed = 15F;
			CameraMouseRotateSpeed = 180F;
			CameraKeyRotateSpeed = 80F;
			CameraZoomSpeed = 15F;
			CameraFastMoveSpeed = 40F;
			CameraTiltSpeed = 90F;
			CameraPanSpeed = 50F;
			CameraGuestDistance = -0.13f;
			CameraGuestHeight = -0.09f;
			CameraDrawingDistance = 10F;
		}

		#region Load / Save Settings
		public void Load()
		{
			CameraSmoothing = GetBooleanPlayerPrefs (cameraSmoothing);
			CameraSmoothness = PlayerPrefs.GetFloat (cameraSmoothness);
			CameraMoveSpeed = PlayerPrefs.GetFloat (cameraMoveSpeed);
			CameraFastMoveSpeed = PlayerPrefs.GetFloat (cameraFastMoveSpeed);
			CameraMouseRotateSpeed = PlayerPrefs.GetFloat (cameraMouseRotateSpeed);
			CameraKeyRotateSpeed = PlayerPrefs.GetFloat (cameraKeyRotateSpeed);
			CameraZoomSpeed = PlayerPrefs.GetFloat (cameraZoomSpeed);
			CameraTiltSpeed = PlayerPrefs.GetFloat (cameraTiltSpeed);
			CameraPanSpeed = PlayerPrefs.GetFloat (cameraPanSpeed);
			CameraDrawingDistance = PlayerPrefs.GetFloat (cameraDrawingDistance);
			CameraGuestHeight = PlayerPrefs.GetFloat (cameraGuestHeight);
			CameraGuestDistance = PlayerPrefs.GetFloat (cameraGuestDistance);
			CameraQualitySetting = PlayerPrefs.GetInt (cameraQualitySetting);

			KeyboardRotateLeft = GetKeyCodeFromString (PlayerPrefs.GetString (kbRotateLeft));
			KeyboardRotateRight = GetKeyCodeFromString (PlayerPrefs.GetString (kbRotateRight));
			KeyboardZoomOut = GetKeyCodeFromString (PlayerPrefs.GetString (kbZoomOut));
			KeyboardZoomIn = GetKeyCodeFromString (PlayerPrefs.GetString (kbZoomIn));
			KeyboardTiltUp = GetKeyCodeFromString (PlayerPrefs.GetString (kbTiltUp));
			KeyboardTiltDown = GetKeyCodeFromString (PlayerPrefs.GetString (kbTiltDown));
			KeyboardFastMove = GetKeyCodeFromString (PlayerPrefs.GetString (kbFastMove));
			KeyboardMouseDrag = GetKeyCodeFromString (PlayerPrefs.GetString (kbMouseDrag));
			KeyboardMouseOrbit = GetKeyCodeFromString (PlayerPrefs.GetString (kbMouseOrbit));
			KeyboardRide = GetKeyCodeFromString (PlayerPrefs.GetString (kbRide));
			KeyboardGuest = GetKeyCodeFromString (PlayerPrefs.GetString (kbGuest));
			KeyboardWalk = GetKeyCodeFromString (PlayerPrefs.GetString (kbWalk));
			KeyboardReset = GetKeyCodeFromString (PlayerPrefs.GetString (kbReset));
			KeyboardCameraSettings = GetKeyCodeFromString (PlayerPrefs.GetString (kbCameraSettings));

			PerspectiveCameraEnabled = GetBooleanPlayerPrefs (perspectiveCameraEnabled);
			RideCameraEnabled = GetBooleanPlayerPrefs (rideCameraEnabled);
			WalkCameraEnabled = GetBooleanPlayerPrefs (walkCameraEnabled);
			GuestCameraEnabled = GetBooleanPlayerPrefs (guestCameraEnabled);


			SettingsLoaded = Convert.ToBoolean (PlayerPrefs.GetInt (betterCamerasSettings));
		}

		public void Save()
		{
			PlayerPrefs.SetInt (cameraSmoothing, Convert.ToInt32 (CameraSmoothing));
			PlayerPrefs.SetFloat (cameraSmoothness, CameraSmoothness);
			PlayerPrefs.SetFloat (cameraMoveSpeed, CameraMoveSpeed);
			PlayerPrefs.SetFloat (cameraFastMoveSpeed, CameraFastMoveSpeed);
			PlayerPrefs.SetFloat (cameraMouseRotateSpeed, CameraMouseRotateSpeed);
			PlayerPrefs.SetFloat (cameraKeyRotateSpeed, CameraKeyRotateSpeed);
			PlayerPrefs.SetFloat (cameraZoomSpeed, CameraZoomSpeed);
			PlayerPrefs.SetFloat (cameraTiltSpeed, CameraTiltSpeed);
			PlayerPrefs.SetFloat (cameraPanSpeed, CameraPanSpeed);
			PlayerPrefs.SetFloat (cameraDrawingDistance, CameraDrawingDistance);
			PlayerPrefs.SetFloat (cameraGuestDistance, CameraGuestDistance);
			PlayerPrefs.SetFloat (cameraGuestHeight, CameraGuestHeight);
			PlayerPrefs.SetInt (cameraQualitySetting, CameraQualitySetting);

			PlayerPrefs.SetString (kbRotateLeft, KeyboardRotateLeft.ToString());
			PlayerPrefs.SetString (kbRotateRight, KeyboardRotateRight.ToString());
			PlayerPrefs.SetString (kbZoomOut, KeyboardZoomOut.ToString());
			PlayerPrefs.SetString (kbZoomIn, KeyboardZoomIn.ToString());
			PlayerPrefs.SetString (kbTiltUp, KeyboardTiltUp.ToString());
			PlayerPrefs.SetString (kbTiltDown, KeyboardTiltDown.ToString());
			PlayerPrefs.SetString (kbFastMove, KeyboardFastMove.ToString());
			PlayerPrefs.SetString (kbMouseDrag, KeyboardMouseDrag.ToString ());
			PlayerPrefs.SetString (kbMouseOrbit, KeyboardMouseOrbit.ToString ());
			PlayerPrefs.SetString (kbRide, KeyboardRide.ToString());
			PlayerPrefs.SetString (kbGuest, KeyboardGuest.ToString());
			PlayerPrefs.SetString (kbWalk, KeyboardWalk.ToString());
			PlayerPrefs.SetString (kbReset, KeyboardReset.ToString());
			PlayerPrefs.SetString (kbCameraSettings, KeyboardCameraSettings.ToString());

			PlayerPrefs.SetInt (perspectiveCameraEnabled, Convert.ToInt32 (PerspectiveCameraEnabled));
			PlayerPrefs.SetInt (rideCameraEnabled, Convert.ToInt32 (RideCameraEnabled));
			PlayerPrefs.SetInt (walkCameraEnabled, Convert.ToInt32 (WalkCameraEnabled));
			PlayerPrefs.SetInt (guestCameraEnabled, Convert.ToInt32 (GuestCameraEnabled));

			PlayerPrefs.SetInt (betterCamerasSettings, Convert.ToInt32 (true));


		}
		#endregion

		#region Settings Menu
		public void onDrawSettingsUI()
		{
			GUILayout.Label (KEYBOARD_SETTINGS);
			KeyboardRotateLeft = DrawKeyCode (KEYBOARD_SETTING_ROTATE_LEFT, KeyboardRotateLeft);
			KeyboardRotateRight = DrawKeyCode (KEYBOARD_SETTING_ROTATE_RIGHT, KeyboardRotateRight);
			KeyboardZoomOut = DrawKeyCode (KEYBOARD_SETTING_ZOOM_OUT, KeyboardZoomOut);
			KeyboardZoomIn = DrawKeyCode (KEYBOARD_SETTING_ZOOM_IN, KeyboardZoomIn);
			KeyboardTiltUp = DrawKeyCode (KEYBOARD_SETTING_TILT_UP, KeyboardTiltUp);
			KeyboardTiltDown = DrawKeyCode (KEYBOARD_SETTING_TILT_DOWN, KeyboardTiltDown);
			KeyboardFastMove = DrawKeyCode (KEYBOARD_SETTING_FASTMOVE, KeyboardFastMove);
			KeyboardMouseDrag = DrawKeyCode (KEYBOARD_SETTING_MOUSE_DRAG, KeyboardMouseDrag);
			KeyboardMouseOrbit = DrawKeyCode (KEYBOARD_SETTING_MOUSE_ORBIT_BUTTON, KeyboardMouseOrbit);
			KeyboardRide = DrawKeyCode (KEYBOARD_SETTING_RIDE, KeyboardRide);
			KeyboardGuest = DrawKeyCode (KEYBOARD_SETTING_GUEST, KeyboardGuest);
			KeyboardWalk = DrawKeyCode (KEYBOARD_SETTING_WALK, KeyboardWalk);
			KeyboardReset = DrawKeyCode (KEYBOARD_SETTING_RESET, KeyboardReset);
			KeyboardCameraSettings = DrawKeyCode (KEYBOARD_SETTING_CAMERA_SETTING, KeyboardCameraSettings);
			GUILayout.Label (COMPONENT_SETTINGS);
			PerspectiveCameraEnabled = DrawBoolField (COMPONENT_SETTING_PERSPECTIVE_CAMERA, PerspectiveCameraEnabled);
			GuestCameraEnabled = DrawBoolField (COMPONENT_SETTING_GUEST_CAMERA, GuestCameraEnabled);
			WalkCameraEnabled = DrawBoolField (COMPONENT_SETTING_WALK_CAMERA, WalkCameraEnabled);
			RideCameraEnabled = DrawBoolField (COMPONENT_SETTING_RIDE_CAMERA, RideCameraEnabled);

		}
		public void onSettingsOpened()
		{
			Load ();
		}

		public void onSettingsClosed()
		{
			Save ();
		}
		#endregion



		#region Settings Panel
		public void OnGUI()
		{
			if (ShowCameraSettings) 
			{
				windowRect = GUI.Window (0, windowRect, DoMyWindow, CAMERA_SETTINGS);
			}
		}

		public void DoMyWindow(int windowID)
		{
			GUI.DragWindow(new Rect(0, 0, 10000, 20));
			CameraSmoothing = GUILayout.Toggle(CameraSmoothing, CAMERA_SETTING_SMOOTH);
			CameraSmoothness = DrawFloatSlider (CAMERA_SETTING_SMOOTHNESS, CameraSmoothness, 10F, 1F);
			CameraMoveSpeed = DrawFloatSlider (CAMERA_SETTING_MOVE_SPEED, CameraMoveSpeed, 1.0F, 40.0f);
			CameraFastMoveSpeed = DrawFloatSlider(CAMERA_SETTING_FAST_MOVE_SPEED, CameraFastMoveSpeed, 30.0F, 80.0f);
			CameraMouseRotateSpeed = DrawFloatSlider(CAMERA_SETTING_ROTATE_SPEED,CameraMouseRotateSpeed, 50F, 350F);
			CameraKeyRotateSpeed = CameraMouseRotateSpeed + 50F;
			CameraZoomSpeed = DrawFloatSlider(CAMERA_SETTING_ZOOM_SPEED, CameraZoomSpeed, 1f, 30f);
			CameraTiltSpeed = DrawFloatSlider(CAMERA_SETTING_TILT_SPEED, CameraTiltSpeed, 30.0F, 80.0f);
			CameraPanSpeed = DrawFloatSlider (CAMERA_SETTING_PAN_SPEED, CameraPanSpeed, 10.0F, 60.0F);
			CameraDrawingDistance = DrawFloatSlider(CAMERA_SETTING_DRAWING_DISTANCE,CameraDrawingDistance, 20f, 300f);
			CameraGuestDistance = DrawFloatSlider (CAMERA_SETTING_GUEST_DISTANCE, CameraGuestDistance, -0.5f, 2.5f);
			CameraGuestHeight = DrawFloatSlider (CAMERA_SETTING_GUEST_HEIGHT, CameraGuestHeight, -0.5f, 0.5f);

			string[] names = QualitySettings.names;
			GUILayout.FlexibleSpace();
			GUILayout.BeginVertical();
			GUILayout.Label(CAMERA_SETTING_QUALITY_SETTINGS + ":");
			int i = 0;
			while (i < names.Length)
			{
				if (GUILayout.Button (names [i]))
					CameraQualitySetting = i;

				i++;
			}
			GUILayout.EndVertical();
			GUILayout.FlexibleSpace();
			if(GUILayout.Button(CAMERA_SETTING_DEFAULT_SETTINGS))
			{
				BetterCamerasMain.Reset ();
			}
			GUILayout.Label(String.Format(CAMERA_SETTING_PUSHKEY, KeyboardCameraSettings));
			BetterCamerasMain.RefreshSettings ();

		}
		#endregion

		#region Helper Functions
		private bool GetBooleanPlayerPrefs (string playerPref)
		{
			return Convert.ToBoolean (PlayerPrefs.GetInt (playerPref));
		}

		private KeyCode GetKeyCodeFromString (string keycode)
		{
			return (KeyCode)Enum.Parse (typeof(KeyCode), keycode);
		}

		private KeyCode DrawKeyCode (string fieldName, KeyCode intialKeyCode)
		{
			KeyCode KeyCodeValue;
			GUILayout.BeginHorizontal ();
			GUILayout.Label (fieldName.PadRight(30));
			KeyCodeValue = GetKeyCodeFromGui (intialKeyCode);
			GUILayout.EndHorizontal ();
			return KeyCodeValue;
		}

		private bool DrawBoolField (string fieldName, bool initialValue)
		{
			bool boolValue;
			boolValue = GUILayout.Toggle (initialValue, fieldName.PadRight(30));
			return boolValue;
		}

		private float DrawFloatSlider (string fieldName, float initialValue, float minValue, float maxValue)
		{
			float floatValue;
			GUILayout.BeginHorizontal();
			GUILayout.Label(fieldName.PadRight(30));
			floatValue = GUILayout.HorizontalSlider(initialValue, minValue, maxValue);
			GUILayout.EndHorizontal();
			return floatValue;
		}

		private float DrawFloatValue (string fieldName, float initialValue)
		{
			GUILayout.BeginHorizontal();
			GUILayout.Label(fieldName.PadRight(30));
			string floatString = GUILayout.TextField(initialValue.ToString("F1"));
			float floatValue;
			if (!float.TryParse(floatString, out floatValue))
			{
				floatValue = initialValue;
			}
			else if (floatValue == 0.0f)
			{
				floatValue = initialValue;
			}
			GUILayout.EndHorizontal();
			return floatValue;
		}

		private KeyCode GetKeyCodeFromGui (KeyCode _keyCode)
		{
			bool toggle = false;
			if (GuiToggle.ContainsKey(_keyCode))
			{
				GuiToggle.TryGetValue (_keyCode, out toggle);
				GuiToggle.Remove (_keyCode);
			}
			toggle = GUILayout.Toggle (toggle, _keyCode.ToString().PadRight(30));
			if (toggle) {
				KeyCode key;
				if (FetchKey (out key)) {
					toggle = false;
					_keyCode = key;
				}
			}
			GuiToggle.Add (_keyCode, toggle);
			return _keyCode;
		}

		private bool FetchKey (out KeyCode outKey)
		{
			foreach (KeyCode key in System.Enum.GetValues(typeof(KeyCode)))
			{
				if (Input.GetKeyDown (key))
				{
					outKey = key;
					return true;
				}
			}
			outKey = KeyCode.Break;
			return false;
		}
		#endregion

	}
}

