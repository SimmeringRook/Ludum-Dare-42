using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public void Initialize(int level, float secondsBeforeStormBeings)
    {
        this.Level = level;
        this.SecondsBeforeStorm = secondsBeforeStormBeings;

        InstantiateManagers();
    }

    public int Level;
    public float SecondsBeforeStorm = 10f;

    public GameObject MapPrefab;
    public Map Map;
    public int MapWidth;
    public int MapLength;

    public GameObject BuildingManagerPrefab;
    public BuildingManager BuildingManager;
    private ShieldGeneratorScript ShieldGenerator;

    public CameraManager CameraManager;

    public GameObject UIManagerPrefab;
    public UIManager UIManager;

    public GameObject StormPrefab;
    public StormScript Storm;

    public MouseManager MouseManager;
	// Use this for initialization
	void Start ()
    {
        Initialize(1, 10f);
    }

    bool levelFinished = false;

	// Update is called once per frame
	void Update ()
    {
	    if(this.Storm.transform.position.y > 1f 
            && this.ShieldGenerator.GetShieldPercent() <= 0f 
            && levelFinished == false)
        {
            levelFinished = true;
            this.TriggerEnd();
        }
	}

    void InstantiateManagers()
    {
        InstantiateAndInitialize_Map();
        InstantiateAndInitialize_Buildings();
        InstantiateAndInitialize_Camera();

        this.MouseManager = this.gameObject.GetComponent<MouseManager>();
        this.MouseManager.BuildingManager = this.BuildingManager;

        InstantiateAndInitialize_UI();
        StartStorm();
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

        this.BuildingManager.Initialize(this);

        this.ShieldGenerator = GameObject.FindObjectOfType<ShieldGeneratorScript>();
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

    private void StartStorm()
    {
        this.Storm = Instantiate
            (
                StormPrefab,
                Vector3.zero,
                Quaternion.identity
            ).GetComponent<StormScript>();

        this.Storm.Initialize(this.Map, SecondsBeforeStorm);
    }

    private void TriggerEnd()
    {
        float duration = this.Storm.transform.position.y / 0.25f;
        Debug.LogError("You survived for " + (duration - 10f).ToString() + " seconds.");
        //GameObject.FindObjectOfType<GameManager>().EndLevel();
    }
}
