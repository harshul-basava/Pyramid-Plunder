using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
using UnityEngine;

public class HUDUpdate : MonoBehaviour
{
    public GameObject healthDisplay;
    public GameObject defenseDisplay;
    public GameObject attackDisplay;

    public HealthManager HealthManager;
    TextMeshProUGUI healthText;
    TextMeshProUGUI defenseText;
    TextMeshProUGUI attackText;

    void Start()
    {
        healthText = healthDisplay.GetComponent<TextMeshProUGUI>();
        defenseText = defenseDisplay.GetComponent<TextMeshProUGUI>();
        attackText = attackDisplay.GetComponent<TextMeshProUGUI>();

    }

    void Update()
    {
        UpdateStats();
    }

    private void UpdateStats()
    {
        healthText.text = "" + (int) HealthManager.playerHealth;
        defenseText.text = "" + (int) HealthManager.playerDefense;
        attackText.text = "" + (int) HealthManager.playerAttack;
    }
}
