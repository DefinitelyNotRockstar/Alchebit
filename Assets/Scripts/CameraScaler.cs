using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CameraScaler : MonoBehaviour
{

	public float orthographicSize = 5f;//set this to your camera size
	public float aspect = 4 / 3;//set this to your aspect ratio
	void Start()
	{
		Camera.main.projectionMatrix = Matrix4x4.Ortho(
				-orthographicSize * aspect, orthographicSize * aspect,
				-orthographicSize, orthographicSize,
				Camera.main.nearClipPlane, Camera.main.farClipPlane);
	}

}
