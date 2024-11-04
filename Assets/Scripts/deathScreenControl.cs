using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class deathScreenControl : MonoBehaviour
{
    public TextMeshProUGUI InventoryPoints;
    public TextMeshProUGUI EnemyPoints;
    public TextMeshProUGUI HealthPoints;

    public TextMeshProUGUI TotalPoints;

    public void Start()
    {
        gameObject.GetComponent<RectTransform>().localScale = new Vector3(0, 0, 0);
    }

    public void UpdateText(int invPoints, int enemiesKilled, int healthPoints, int totalPoints) 
    {
        InventoryPoints.text = "Inventory Items: " + invPoints;
        EnemyPoints.text = "Enemy Points: " + enemiesKilled + " x 75";
        HealthPoints.text = "Stat Points: " + healthPoints;

        TotalPoints.text = "TOTAL: " + totalPoints;

        gameObject.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 0);
    }
}
