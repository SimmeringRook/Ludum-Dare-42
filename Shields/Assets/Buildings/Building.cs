using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour
{
    #region Shared Building Attributes
    public BuildingName Name;
    public Vector3 Dimensions;

    public float ConstructionTime;

    public GameObject BuildingGraphic;
    #endregion

    #region Resources
    public ResourceType Type;
    public float ResourceGeneratedPerSec;
    public float TotalResourceCapacity;
    public float ResourceConsumpedPerSec;
    #endregion

    public float PowerConsumptionRate;
    public float ActualPowerConsumedPerSec;

    // Use this for initialization
    void Start ()
    {
        this.BuildingGraphic = this.gameObject.GetComponentInChildren<Transform>().gameObject;
    }
	
	// Update is called once per frame
	void Update ()
    {
	}

    public string GetBuildingNameAsString()
    {
        //Todo: Format string
        return this.Name.ToString();
    }

    public void PlaceBuildingAt(Vector3 location)
    {
        //Ensure the building isn't clipping the ground
        //TODO:: Change this if the world isn't flat
        location.y = this.Dimensions.y / 2f;
        this.BuildingGraphic.transform.position = location;
    }
}