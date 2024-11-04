using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class objectiveMenuControl : MonoBehaviour
{
    bool active;

    void Start()
    {
        active = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (active)
        {
            Time.timeScale = 0.0f;
            gameObject.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
        } else {
            Time.timeScale = 1.0f;
            gameObject.GetComponent<RectTransform>().localScale = new Vector3(0, 0, 1);
        }

        if (Input.GetKeyDown(KeyCode.O))
        {
            active = !active;
        }
    }
}
