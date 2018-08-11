using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIBuildingPanel : MonoBehaviour
{

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void TogglePanel(string panelName)
    {
        if (this.gameObject.name.Equals(panelName))
        {
            this.gameObject.SetActive(!this.gameObject.activeInHierarchy);
        }
        else
        {
            this.gameObject.SetActive(false);
        }
    }
}
