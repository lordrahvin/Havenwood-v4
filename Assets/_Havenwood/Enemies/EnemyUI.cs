using UnityEngine;
using UnityEngine.UI;

// Add a UI Socket transform to your enemy
// Attack this script to the socket
// Link to a canvas prefab that contains NPC UI
public class EnemyUI : MonoBehaviour {

    // Works around Unity 5.5's lack of nested prefabs
    [Tooltip("The UI canvas prefab")] [SerializeField] GameObject enemyCanvasPrefab = null;

    Camera cameraToLookAt;
	RectTransform enemyCanvasTransform;
	[SerializeField]float midHeight = 2f;

    // Use this for initialization 
    void Start()
    {
        cameraToLookAt = Camera.main;
        GameObject enemyCanvas = Instantiate(enemyCanvasPrefab, new Vector3(transform.position.x, transform.position.y+midHeight, transform.position.z), transform.rotation, transform) as GameObject;
		enemyCanvasTransform = enemyCanvas.GetComponent<RectTransform>();
    }

    // Update is called once per frame 
    void LateUpdate()
    {
		enemyCanvasTransform.sizeDelta = new Vector2(30, midHeight*4);
		enemyCanvasTransform.position = new Vector3(transform.position.x, transform.position.y + midHeight, transform.position.z);
		enemyCanvasTransform.LookAt(cameraToLookAt.transform, Vector3.up);
		
	}
}