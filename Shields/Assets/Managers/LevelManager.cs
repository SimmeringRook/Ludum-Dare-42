using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public int Level;

    public GameObject MapPrefab;
    public Map Map;
    public int MapWidth;
    public int MapLength;

    public GameObject BuildingManagerPrefab;
    public BuildingManager BuildingManager;

    public CameraManager CameraManager;

    public GameObject UIManagerPrefab;
    public UIManager UIManager;

	// Use this for initialization
	void Start ()
    {
        InstantiateManagers();

    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void InstantiateManagers()
    {
        InstantiateAndInitialize_Map();
        InstantiateAndInitialize_Buildings();
        InstantiateAndInitialize_Camera();
        InstantiateAndInitialize_UI();
    }

    private void InstantiateAndInitialize_Map()
    {
        GameObject mapGameObject = Instantiate(MapPrefab, Vector3.zero, Quaternion.identity);

        this.Map = mapGameObject.GetComponent<Map>();

        this.Map.Width = MapWidth;
        this.Map.Length = MapLength;

        this.Map.gameObject.transform.SetParent(this.gameObject.transform);
        this.Map.gameObject.name = "Tiles";

        this.Map.GenerateMap();
    }

    private void InstantiateAndInitialize_Buildings()
    {
        this.BuildingManager = Instantiate
            (
                BuildingManagerPrefab, 
                Vector3.zero, 
                Quaternion.identity
            ).GetComponent<BuildingManager>();

        this.BuildingManager.gameObject.transform.SetParent(this.gameObject.transform);
        this.BuildingManager.gameObject.name = "Buildings";

        this.BuildingManager.Initialize();
    }

    private void InstantiateAndInitialize_Camera()
    {
        Camera.main.gameObject.AddComponent<CameraManager>();

        this.CameraManager = Camera.main.gameObject.GetComponent<CameraManager>();
    }

    private void InstantiateAndInitialize_UI()
    {
        this.UIManager = Instantiate
            (
                UIManagerPrefab,
                Vector3.zero,
                Quaternion.identity
            ).GetComponent<UIManager>();
    }
}
