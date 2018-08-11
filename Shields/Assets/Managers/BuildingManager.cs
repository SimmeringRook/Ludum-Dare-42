using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingManager : MonoBehaviour
{
    #region Prefabs

    #endregion
    public List<GameObject> PowerBuildingPrefabs;
    public List<GameObject> ResourceBuildingPrefabs;
    public List<GameObject> StorageBuildingPrefabs;
    public List<GameObject> ScenarioBuildingPrefabs;

    private Map Map;

    public List<Building> Buildings;

    public void Initialize()
    {
        this.Map = this.gameObject.transform.parent.GetComponentInChildren<Map>();
        this.Buildings = new List<Building>();

        this.SpawnBuildingAt(ScenarioBuildingPrefabs[0], this.Map.GetMapCenter());
    }

    // Use this for initialization
    void Start ()
    {
        
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void SpawnBuildingAt(GameObject buildingPrefab, Vector3 location)
    {
        Building spawnedBuilding = Instantiate
            (
                buildingPrefab,
                location,
                Quaternion.identity
            ).GetComponent<Building>();

        spawnedBuilding.gameObject.transform.SetParent(this.gameObject.transform);

        this.Buildings.Add(spawnedBuilding);
    }

    public void SetSelectedBuildingGhost(GameObject buildingPrefab)
    {
        throw new NotImplementedException();
    }
}
