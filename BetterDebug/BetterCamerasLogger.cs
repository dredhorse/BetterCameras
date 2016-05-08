using System;
using System.Text;
using UnityEngine;

namespace BetterCameras
{
	public class BetterCamerasLogger
	{
		public BetterCamerasLogger ()
		{
		}
		public static string Log (params object [] data)
		{
			StringBuilder sb = new StringBuilder();
			for (int i = 0; i < data.Length; i++)
			{
				sb.Append(data[i].ToString());
				sb.Append("\t");
			}
			string s = sb.ToString();
			Debug.Log(s);
			return s;
		}
	}
}

