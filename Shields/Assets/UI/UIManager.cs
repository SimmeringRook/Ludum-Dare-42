using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class UIManager : MonoBehaviour
{
    private LevelManager LevelManager;
    public List<UIPanel> UIPanels;

    
    private Text[] PowerStatusTextboxes;

    private Text ShieldStatusTextbox;
    private ShieldGeneratorScript shieldGenerator;

	// Use this for initialization
	void Start ()
    {
        this.UIPanels = this.gameObject.GetComponentsInChildren<UIPanel>(includeInactive: true).ToList();
        this.LevelManager = GameObject.FindObjectOfType<LevelManager>();

        this.shieldGenerator = (ShieldGeneratorScript) this.LevelManager.BuildingManager.Buildings.Where(b => b.Name == BuildingName.ShieldGenerator).FirstOrDefault();
        if (this.shieldGenerator == null)
            throw new System.Exception("UIManager unable to locate Shield Generator (building). The level maybe corrupted or set up incorrectly.");

        this.ShieldStatusTextbox = this.UIPanels[0].gameObject.GetComponentInChildren<Text>();
        this.PowerStatusTextboxes = this.UIPanels[1].gameObject.GetComponentsInChildren<Text>();

        UIBuildingPanel[] uiBuildingPanels = GameObject.FindObjectsOfType<UIBuildingPanel>();
        UIBuildingButton[] uiBuildingButtons = GameObject.FindObjectsOfType<UIBuildingButton>();

        foreach (UIBuildingPanel panel in uiBuildingPanels)
        {
            panel.gameObject.SetActive(false);
        }

        foreach (UIBuildingButton button in uiBuildingButtons)
        {
            button.Initialize(uiBuildingPanels, this.LevelManager.MouseManager);
        }
    }
	
	// Update is called once per frame
	void Update ()
    {

	}

    private void UpdateShieldStatus()
    {
        float shieldPercent = this.shieldGenerator.GetShieldPercent();
        this.ShieldStatusTextbox.text = "Shield Status: " + shieldPercent + "%";
    }

    private void UpdatePowerStatus(ResourcesSuppliedAndDemanded resourcesSuppliedAndDemanded)
    {
        foreach (Text textbox in this.PowerStatusTextboxes)
        {
            if (textbox.gameObject.name.Equals("PowerGeneratedTextbox"))
            {
                textbox.text = "Power Generated: " + resourcesSuppliedAndDemanded.ResourcesGeneratedPerSecond[ResourceType.Power].ToString();
            }
            else if (textbox.gameObject.name.Equals("PowerConsumedTextbox"))
            {
                textbox.text = "Power Consumed: " + resourcesSuppliedAndDemanded.ResourcesDemandedPerSecond[ResourceType.Power].ToString();
            }
            else if (textbox.gameObject.name.Equals("PowerAvailableTextbox"))
            {
                textbox.text = "Power Available: " + resourcesSuppliedAndDemanded.AvailableResourcesForTick[ResourceType.Power].ToString();
            }
        }
    }

    public void TriggerTickUpdate(ResourcesSuppliedAndDemanded resourcesSuppliedAndDemanded)
    {
        UpdatePowerStatus(resourcesSuppliedAndDemanded);
        UpdateShieldStatus();
    }
}
