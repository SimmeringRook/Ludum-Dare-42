using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseManager : MonoBehaviour {

    public BuildingManager BuildingManager;

    public Material InvalidLocationMaterial;
    public Material ValidLocationMaterial;
    private Material BuildingPrefabMaterial;

    public GameObject GhostOfBuildingPrefab = null;
    private Vector3 mousePositionOnTile;
    private bool isValidLocation = true;
	// Use this for initialization
	void Start ()
    {
	}
	
	// Update is called once per frame
	void Update ()
    {
		if (this.GhostOfBuildingPrefab != null)
        {
            UpdateGhostPosition();

            if (Input.GetMouseButtonDown(0) && isValidLocation)
            {
                this.GhostOfBuildingPrefab.GetComponentInChildren<Renderer>().material = this.BuildingPrefabMaterial;
                this.BuildingManager.SpawnBuildingAt(this.GhostOfBuildingPrefab, mousePositionOnTile);

                GameObject.Destroy(this.GhostOfBuildingPrefab);

                this.GhostOfBuildingPrefab = null;
                this.BuildingPrefabMaterial = null;
            }
        }
	}

    private void UpdateGhostPosition()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 40f))
        {
            this.GhostOfBuildingPrefab.GetComponentInChildren<MeshRenderer>().enabled = true;
            mousePositionOnTile = hit.collider.gameObject.transform.position;
            mousePositionOnTile.y += 0.5f;

            this.GhostOfBuildingPrefab.transform.position = mousePositionOnTile;
            
            if (hit.collider.gameObject.tag.Equals("Building"))
            {
                isValidLocation = false;
                this.GhostOfBuildingPrefab.GetComponentInChildren<Renderer>().material = InvalidLocationMaterial;
            }
            else
            {
                isValidLocation = true;
                this.GhostOfBuildingPrefab.GetComponentInChildren<Renderer>().material = ValidLocationMaterial;
            }
        }
        else
        {
            this.GhostOfBuildingPrefab.GetComponentInChildren<MeshRenderer>().enabled = false;
        }
    }

    public void SetSelectedBuildingGhost(GameObject buildingPrefab)
    {
        this.GhostOfBuildingPrefab = Instantiate(buildingPrefab).GetComponentInChildren<Transform>().gameObject;
        this.GhostOfBuildingPrefab.GetComponentInChildren<Collider>().enabled = false;
        this.BuildingPrefabMaterial = this.GhostOfBuildingPrefab.GetComponentInChildren<Renderer>().material;
    }
}
