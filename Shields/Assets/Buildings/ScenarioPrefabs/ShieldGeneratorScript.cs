using System.Collections;
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

        this.PowerConsumptionRate = 10f;
        this.ActualPowerConsumedPerSec = 10f;
	}
	
	// Update is called once per frame
	void Update ()
    {
        UpdateShieldRadius();
	}

    void UpdateShieldRadius()
    {
        float shieldPercent = this.ActualPowerConsumedPerSec / this.PowerConsumptionRate * 100;
        float shieldPercentThreshold = 100f / this.MapRadius;
        float shieldRadius = Mathf.Ceil(shieldPercent / shieldPercentThreshold) + 0.5f;

        this.ShieldDome.transform.localScale = new Vector3(shieldRadius, 4, shieldRadius);
    }
}
