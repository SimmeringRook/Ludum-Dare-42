using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class UIManager : MonoBehaviour
{
    private LevelManager LevelManager;
    public List<UIPanel> UIPanels;

    private Text ShieldStatusTextbox;
    private Building shieldGenerator;

	// Use this for initialization
	void Start ()
    {
        this.UIPanels = this.gameObject.GetComponentsInChildren<UIPanel>(includeInactive: true).ToList();
        this.LevelManager = GameObject.FindObjectOfType<LevelManager>();

        this.shieldGenerator = this.LevelManager.BuildingManager.Buildings.Where(b => b.Name == BuildingName.ShieldGenerator).FirstOrDefault();
        if (this.shieldGenerator == null)
            throw new System.Exception("UIManager unable to locate Shield Generator (building). The level maybe corrupted or set up incorrectly.");

        this.ShieldStatusTextbox = this.UIPanels[0].gameObject.GetComponentInChildren<Text>();

        UIBuildingPanel[] uiBuildingPanels = GameObject.FindObjectsOfType<UIBuildingPanel>();
        UIBuildingButton[] uiBuildingButtons = GameObject.FindObjectsOfType<UIBuildingButton>();

        foreach (UIBuildingPanel panel in uiBuildingPanels)
        {
            panel.gameObject.SetActive(false);
        }

        foreach (UIBuildingButton button in uiBuildingButtons)
        {
            button.Initialize(uiBuildingPanels);
        }
    }
	
	// Update is called once per frame
	void Update ()
    {
        UpdateShieldStatus();
	}

    private void UpdateShieldStatus()
    {
        float shieldPercent = this.shieldGenerator.ActualPowerConsumedPerSec / this.shieldGenerator.PowerConsumptionRate;
        shieldPercent = Mathf.Round(shieldPercent * 100);
        ShieldStatusTextbox.text = "Shield Status: " + shieldPercent + "%";
    }

    private void UpdatePowerStatus()
    {

    }
}
