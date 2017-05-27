using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(CameraRaycaster))]
public class CursorAffordance : MonoBehaviour {

	[SerializeField] Texture2D walkCursor = null;
	[SerializeField] Texture2D attackCursor = null;
	[SerializeField] Texture2D errorCursor = null;
	[SerializeField] Texture2D mysteryCursor = null;
	[SerializeField] Vector2 cursorHotspot = new Vector2(32, 32);


	CameraRaycaster cameraRaycaster;
	

	void Start () {
		cameraRaycaster = GetComponent<CameraRaycaster>();
		cameraRaycaster.layerChangeObservers += OnLayerChanged;
	}
	
	void OnLayerChanged (Layer newLayer) {
		switch (newLayer)
		{
			case Layer.Walkable:
				Cursor.SetCursor(walkCursor, Vector2.zero, CursorMode.Auto);
				break;
			case Layer.Enemy:
				Cursor.SetCursor(attackCursor, Vector2.zero, CursorMode.Auto);
				break;
			case Layer.Obstacle:
				Cursor.SetCursor(errorCursor, cursorHotspot, CursorMode.Auto);
				break;
			case Layer.Mystery:
				Cursor.SetCursor(mysteryCursor, cursorHotspot, CursorMode.Auto);
				break;
			case Layer.Friendly:
				Cursor.SetCursor(mysteryCursor, cursorHotspot, CursorMode.Auto);
				break;
			case Layer.RaycastEndStop:
				Cursor.SetCursor(errorCursor, cursorHotspot, CursorMode.Auto);
				break;
			default:
				break;
		}
	}
}
