﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldGeneratorScript : Building
{
    public GameObject ShieldDome;
    private Map Map;
    private float MapRadius;


	// Use this for initialization
	void Start ()
    {
        foreach (Transform child in this.gameObject.GetComponentsInChildren<Transform>())
        {
            if (child.gameObject.name.Equals("ShieldDomePrefab"))
            {
                this.ShieldDome = child.gameObject;
            }
        }
        this.Map = GameObject.FindObjectOfType<Map>();

        this.MapRadius = Mathf.Ceil((this.Map.Width / 2f) - 1);

	}
	
	// Update is called once per frame
	void Update ()
    {
        UpdateShieldRadius();
	}

    void UpdateShieldRadius()
    {
        float shieldPercent = this.GetShieldPercent();
        float shieldPercentThreshold = 100f / this.MapRadius;
        float shieldRadius = Mathf.Ceil(shieldPercent / shieldPercentThreshold) + 0.5f;

        this.ShieldDome.transform.localScale = new Vector3(shieldRadius, 4, shieldRadius);
    }

    public float GetShieldPercent()
    {
        return (this.ActualResourceConsumpedPerSec.Amount / this.ResourceConsumptionRate.Amount) * 100;
    }

    private float stormLevel = 0f;
    private float defaultShieldPowerRate = 1f;

    public void UpdateStormLevel(float stormLevel)
    {
        this.stormLevel = stormLevel;
        float powerRequirementModifer = 1f;

        if (this.stormLevel > 8f)
        {
            powerRequirementModifer = 4f;
        }
        if (this.stormLevel > 6f)
        {
            powerRequirementModifer = 2f;
        }
        else if (this.stormLevel > 4f)
        {
            powerRequirementModifer = 1.5f;
        }
        else if (this.stormLevel > 2f)
        {
            powerRequirementModifer = 1.25f;
        }

        this.ResourceConsumptionRate.Amount = (this.defaultShieldPowerRate + this.stormLevel) * powerRequirementModifer;
    }
}
