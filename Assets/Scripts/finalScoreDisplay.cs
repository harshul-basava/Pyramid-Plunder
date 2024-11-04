using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class finalScoreDisplay : MonoBehaviour
{
    void Start()
    {
        Scorekeeper scorekeeper = GameObject.Find("Scorekeeper").GetComponent<Scorekeeper>();
        gameObject.GetComponent<TextMeshProUGUI>().text = "Score: " + scorekeeper.score;
        
        Destroy(GameObject.Find("Scorekeeper"));
    }
}
