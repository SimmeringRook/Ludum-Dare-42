using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIBuildingButton : MonoBehaviour
{
    private MouseManager MouseManager;
    public GameObject BuildingPrefab;
    public UIBuildingPanel[] UIBuildingPanels;

    public void Initialize(UIBuildingPanel[] buildingPanels, MouseManager mouseManager)
    {
        UIBuildingPanels = buildingPanels;
        this.MouseManager = mouseManager;
    }

	// Use this for initialization
	void Start ()
    {
        
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnButtonClicked()
    {
        if (this.gameObject.name.Equals("PowerBuildingsButton"))
        {
            foreach (UIBuildingPanel panel in this.UIBuildingPanels)
            {
                panel.TogglePanel("PowerBuildingsPanel");
            }
        }
        else if (this.gameObject.name.Equals("ResourceBuildingsButton"))
        {
            foreach (UIBuildingPanel panel in this.UIBuildingPanels)
            {
                panel.TogglePanel("ResourceBuildingsPanel");
            }
        }
        else
        {
            this.HideAll();
            this.MouseManager.SetSelectedBuildingGhost(this.BuildingPrefab);
        }
    }

    private void HideAll()
    {
        foreach (UIBuildingPanel panel in this.UIBuildingPanels)
        {
            panel.gameObject.SetActive(false);
        }
    }
}
