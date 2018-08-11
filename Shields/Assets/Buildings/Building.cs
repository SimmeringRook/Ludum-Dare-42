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
    //Todo:: Find a way to... condense
    public ResourceTypeToAmountMap ActualResourceGeneratedPerSec;
    public ResourceTypeToAmountMap ResourceGeneratedPerSec;

    public ResourceTypeToAmountMap CurrentResourceStorage;
    public ResourceTypeToAmountMap TotalResourceCapacity;

    public ResourceTypeToAmountMap ResourceConsumptionRate;
    public ResourceTypeToAmountMap ActualResourceConsumpedPerSec;
    
    #endregion

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

    public void AttemptSatisifyDemandOnTick()
    {
        this.ActualResourceConsumpedPerSec.Amount = 0;
        this.ActualResourceGeneratedPerSec.Amount = 0;

        //If this building has onsite storage for the resource it consumes,
        //attempt to consume from storage
        if (this.ResourceConsumptionRate.Type == this.CurrentResourceStorage.Type)
        {
            float remainingStorage = this.CurrentResourceStorage.Amount - this.ResourceConsumptionRate.Amount;
            float partialConsumption = this.ResourceConsumptionRate.Amount - this.CurrentResourceStorage.Amount;

            //If we can actually satisfy the building's needs via storage; do so.
            if (remainingStorage >= 0f)
            {
                this.CurrentResourceStorage.Amount -= this.ResourceConsumptionRate.Amount;
                this.ActualResourceConsumpedPerSec.Amount = this.ResourceConsumptionRate.Amount;

                this.ActualResourceGeneratedPerSec.Amount = this.ResourceGeneratedPerSec.Amount;
            }
            //Otherwise, check to see if we can partially satisfy the building
            else if (partialConsumption < (this.ResourceConsumptionRate.Amount))
            {
                this.ActualResourceConsumpedPerSec.Amount = partialConsumption;
            }
            else
            {
                //There's nothing left in storage to help with the building's consumption
                //There is nothing left to do until the BuildingManager handles transfer from
                //sources or storages to meet demand (if possible)
            }
        }
    }
}

[System.Serializable]
public class ResourceTypeToAmountMap
{
    public ResourceType Type;
    public float Amount;
    //What about:

    /* public float Requested;
     * public float Actual;
     */
}