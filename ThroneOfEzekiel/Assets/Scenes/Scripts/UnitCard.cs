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
    private UnitCardData data;

    // UI elements for displaying card properties
    public GameObject powerUI;
    public GameObject healthUI;

    // Override the start function for custom initialization
    public UnitPropertys.Unit_Class unitClass;

    public override void Initialize(string link, int index)
    {
        // First, instantiate 'data' before trying to access its properties
        data = new UnitCardData();

        // Load and deserialize JSON data
        TextAsset cardJson = Resources.Load<TextAsset>(link);
        if (cardJson != null)
        {
            // Overwrite 'data' with the JSON content
            JsonUtility.FromJsonOverwrite(cardJson.text, data);

            // Now that 'data' has been populated, we can set the base name
            base.Name = data.name;

            // Parse string text into enum
            // Ensure UnitPropertys.Instance is not null and has AssignEnum method correctly implemented
            unitClass = UnitPropertys.Instance.AssignEnum<UnitPropertys.Unit_Class>(data.unitClass);

            // Additional initialization based on 'data'
            UpdateUI();

            if (data.power == 0 && data.health == 0)
            {
                Debug.LogError("Power and Health are not initialized.");
            }
        }
        else
        {
            Debug.LogError("Failed to load JSON data from: " + link);
            return; // Exit if JSON data failed to load
        }

        // Call base.Initialize at the end
        base.Initialize(link, index);
    }


    private void UpdateUI()
    {
        Renderer renderer = GetComponent<Renderer>();
        if (renderer != null)
        {
            Material material = CachedMaterials.GetMaterial(data.imgPath);
            if (material != null)
            {
                renderer.material = material;
            }
        }
        else
        {
            Debug.LogError("Renderer component missing on the card GameObject.");
        }

        if (powerUI != null && healthUI != null)
        {
            UpdateNumberBox(powerUI, data.power, new Color(149 / 255.0f, 40 / 255.0f, 16 / 255.0f, 1f));
            UpdateNumberBox(healthUI, data.health, new Color(37 / 255.0f, 149 / 255.0f, 16 / 255.0f, 1f));
        }
        else
        {
            Debug.LogError("Power UI or Health UI GameObject is not assigned.");
        }
    }

    private void UpdateNumberBox(GameObject uiElement, int number, Color color)
    {
        NumberBox_Handler numberBoxHandler = uiElement.GetComponent<NumberBox_Handler>();
        if (numberBoxHandler != null)
        {
            numberBoxHandler.Initialize(number, color, Color.black);
        }
        else
        {
            Debug.LogError($"NumberBox_Handler component missing on the UI element {uiElement.name}.");
        }
    }
}
