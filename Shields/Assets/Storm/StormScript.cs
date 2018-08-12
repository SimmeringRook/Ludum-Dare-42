using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StormScript : MonoBehaviour
{
    private float stormStartTimer;
    private ShieldGeneratorScript shieldGenerator;

    public Transform GraphicTransform;

    public void Initialize(Map map, float SecondsBeforeStormStarts)
    {
        this.GraphicTransform.localScale = new Vector3(map.Width + 2, map.Length + 2);
        Vector3 center = map.GetMapCenter();
        center.x -= 1f;
        center.y -= 0.9f;
        center.z -= 1f;
        this.gameObject.transform.position = center;
        this.stormStartTimer = SecondsBeforeStormStarts;

        this.shieldGenerator = GameObject.FindObjectOfType<ShieldGeneratorScript>();
        this.StormHasStarted = false;
    }

    private float timeTillStormIncreases = 5f;
    public bool StormHasStarted { get; private set; }

	void Update ()
    {
        this.stormStartTimer -= Time.deltaTime;
        if (this.stormStartTimer < 0)
        {
            this.StormHasStarted = true;
        }

        if (this.StormHasStarted)
        {
            this.timeTillStormIncreases -= Time.deltaTime;
            if (this.timeTillStormIncreases < 0)
            {
                UpdateStormLevel();
            }
        }


    }

    private void UpdateStormLevel()
    {
        Vector3 current = this.gameObject.transform.position;
        current.y += 0.25f;

        this.gameObject.transform.position = current;
        this.timeTillStormIncreases = 5f;

        this.shieldGenerator.UpdateStormLevel(current.y);
    }
}
