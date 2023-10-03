using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class UnitCardData
{
    public string name;
    public string imgPath;
    public string alignment;
    public string unitClass;
    public int movementCost;
    public List<string> unitModifiers;
    public List<string> family;
    public int rank;
    public int health;
    public int power;
    public List<string> abilities;
}
public class UnitCard : Card
{
    UnitCardData data;
    // UI elements for displaying card properties
    public GameObject powerUI;
    public GameObject healthUI;
    // Override the start function for custom initialization
    protected override void Start()
    {
        base.Start();
    }

    // Initializes the card with properties from a JSON file
    public void Initialize(string link)
    {
        data = new UnitCardData();

        UnityEngine.Debug.Log("Initializing..............." + data.name);

        TextAsset cardJson = Resources.Load<TextAsset>(link);
        if (cardJson != null)
        {
            JsonUtility.FromJsonOverwrite(cardJson.text, data);
            UpdateUI();
        }
        else
        {
            Debug.LogError("Failed to load JSON data from: " + link);
        }
        if (data.power == 0 && data.health == 0)
        {
            Debug.LogError("Power and Health are not initialized.");
        }
    }
    // Updates the UI elements to reflect the card's properties
    private void UpdateUI()
    {
        // Use the material manager to get the material
        Material material = CachedMaterials.GetMaterial(data.imgPath);
        if (material != null)
        {
            GetComponent<Renderer>().material = material;
        }

        // Update power and health UI if not null
        if (powerUI != null && healthUI != null)
        {
            powerUI.GetComponent<NumberBox_Handler>().Initialize(data.power,
                new Color(149 / 255.0f, 40 / 255.0f, 16 / 255.0f, 1f),
                new Color(65 / 255.0f, 51 / 255.0f, 30 / 255.0f, 1f));

            healthUI.GetComponent<NumberBox_Handler>().Initialize(data.health,
                new Color(37 / 255.0f, 149 / 255.0f, 16 / 255.0f, 1f),
                new Color(90 / 255.0f, 70 / 255.0f, 67 / 255.0f, 1f));
        }
        else
        {
            Debug.LogError("powerUI or healthUI is null.");
        }
    }


}
