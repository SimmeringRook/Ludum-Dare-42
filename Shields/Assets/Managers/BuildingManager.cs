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
    private LevelManager LevelManager;

    public List<Building> Buildings;

    public void Initialize(LevelManager levelManager)
    {
        this.Map = this.gameObject.transform.parent.GetComponentInChildren<Map>();
        this.Buildings = new List<Building>();

        this.LevelManager = levelManager;

        this.SpawnBuildingAt(ScenarioBuildingPrefabs[0], this.Map.GetMapCenter());
    }

    // Use this for initialization
    void Start ()
    {
        
	}

    float resourceTick = 1f;

	// Update is called once per frame
	void Update ()
    {
        resourceTick -= Time.deltaTime;
        if (resourceTick < 0)
        {
            ResourcesSuppliedAndDemanded resourcesSuppliedAndDemanded = this.CalculateSupplyAndDemandForTick();
            this.LevelManager.UIManager.TriggerTickUpdate(resourcesSuppliedAndDemanded);
        }
	}

    private ResourcesSuppliedAndDemanded CalculateSupplyAndDemandForTick()
    {

        Dictionary<ResourceType, float> ResourcesGeneratedPerSecond = new Dictionary<ResourceType, float>()
        {
            {ResourceType.Power, 0f },
            {ResourceType.Coal, 0f},
            {ResourceType.Oil, 0f }
        };

        Dictionary<ResourceType, float> ResourcesDemandedPerSecond = new Dictionary<ResourceType, float>()
        {
            {ResourceType.Power, 0f },
            {ResourceType.Coal, 0f},
            {ResourceType.Oil, 0f }
        };

        Dictionary<ResourceType, float> AvailableResourcesForTick = new Dictionary<ResourceType, float>()
        {
            {ResourceType.Power, 0f },
            {ResourceType.Coal, 10f},
            {ResourceType.Oil, 0f }
        };


        //Tally everything up
        foreach (Building building in this.Buildings)
        {
            ResourcesGeneratedPerSecond[building.ActualResourceGeneratedPerSec.Type] += building.ActualResourceGeneratedPerSec.Amount;
            AvailableResourcesForTick[building.ActualResourceGeneratedPerSec.Type] += building.ActualResourceGeneratedPerSec.Amount;
            ResourcesDemandedPerSecond[building.ResourceConsumptionRate.Type] += building.ResourceConsumptionRate.Amount;
            
            building.AttemptSatisifyDemandOnTick();
        }
        
        //Note, right now
        //I'm not accounting for any resources stored in tanks/warehouses
        //nor am I adding resources to tanks/warehouses

        foreach (Building b in this.Buildings)
        {
            if (b.ActualResourceConsumpedPerSec.Amount < b.ResourceConsumptionRate.Amount)
            {
                float differenceBetweenActualAndFull = b.ResourceConsumptionRate.Amount - b.ActualResourceConsumpedPerSec.Amount;
                float generatedPerSecond = AvailableResourcesForTick[b.ResourceConsumptionRate.Type];

                if (generatedPerSecond - differenceBetweenActualAndFull >= 0)
                {
                    AvailableResourcesForTick[b.ResourceConsumptionRate.Type] -= differenceBetweenActualAndFull;
                    b.ActualResourceConsumpedPerSec.Amount = b.ResourceConsumptionRate.Amount;
                    b.ActualResourceGeneratedPerSec.Amount = b.ResourceGeneratedPerSec.Amount;
                }
                else
                {
                    float percentSatisfied = b.ActualResourceConsumpedPerSec.Amount / b.ResourceConsumptionRate.Amount;
                    b.ActualResourceGeneratedPerSec.Amount = b.ResourceGeneratedPerSec.Amount * percentSatisfied;
                }
            }
        }

        return new ResourcesSuppliedAndDemanded
        {
            AvailableResourcesForTick = AvailableResourcesForTick,
            ResourcesGeneratedPerSecond = ResourcesGeneratedPerSecond,
            ResourcesDemandedPerSecond = ResourcesDemandedPerSecond
        };
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
        spawnedBuilding.GetComponentInChildren<Collider>().enabled = true;
        this.Buildings.Add(spawnedBuilding);
    }
}

public struct ResourcesSuppliedAndDemanded
{
    public Dictionary<ResourceType, float> AvailableResourcesForTick;
    public Dictionary<ResourceType, float> ResourcesGeneratedPerSecond;
    public Dictionary<ResourceType, float> ResourcesDemandedPerSecond;
}