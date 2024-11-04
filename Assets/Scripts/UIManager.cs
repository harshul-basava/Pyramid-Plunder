using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public GameObject grid;
    public Timer timer;
    public HealthManager HM;

    void Start()
    {
        HM = GameObject.Find("HealthManager").GetComponent<HealthManager>();
    }

    // Update is called once per frame
    void Update()
    {
        CursorUpdate();
    }

    private void CursorUpdate()
    {
        if (grid.activeInHierarchy || timer.timeRemaining == 0 || HM.playerHealth == 0)
        {
            Cursor.visible = true;
        }
        else
        {
            Cursor.visible = false;
        }
    }
}
