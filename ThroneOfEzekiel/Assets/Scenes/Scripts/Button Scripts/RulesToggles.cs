using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine;

public class RulesToggle : Singleton<RulesToggle>
{
    private TextMeshProUGUI rulesText;

    void Awake()
    {
        base.Awake();
        rulesText = GetComponent<TextMeshProUGUI>();
        if (rulesText == null)
        {
            Debug.LogError("TextMeshProUGUI component not found on the GameObject.");
        }
    }

    public void ToggleRulesVisibility()
    {
        // Toggle the enabled state of the TextMeshProUGUI component
        if (rulesText != null)
        {
            rulesText.enabled = !rulesText.enabled;
        }
    }
}

