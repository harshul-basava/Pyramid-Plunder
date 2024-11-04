using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public float trueTime = 0;
    public float totalTime;
    public float timeRemaining;

    public GameObject grid;

    GameObject pauseMenu;
    bool paused;

    void Start()
    {
        Time.timeScale = 1.0f;
        timeRemaining = totalTime;
        gameObject.GetComponent<TextMeshProUGUI>().text = Mathf.Floor(totalTime/60).ToString("00") + ":" + (totalTime % 60).ToString("00.00");
        
        pauseMenu = GameObject.Find("pauseMenu");
        pauseMenu.GetComponent<RectTransform>().localScale = new Vector3(0, 0, 1);

        paused = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            paused = !paused;
        }

        if (timeRemaining > 0)
        {
            if (!paused) 
            {
                trueTime += Time.deltaTime;
                timeRemaining = totalTime - trueTime;
                pauseMenu.GetComponent<RectTransform>().localScale = new Vector3(0, 0, 1);
                Time.timeScale = 1.0f;
            } else {
                pauseMenu.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
                Time.timeScale = 0.0f;
            }

            gameObject.GetComponent<TextMeshProUGUI>().text = Mathf.Floor(timeRemaining/60).ToString("0") + ":" + (timeRemaining % 60).ToString("00");

            if (grid.activeInHierarchy)
            {
               gameObject.GetComponent<TextMeshProUGUI>().text = ""; 
            }
        }
        else
        {
            timeRemaining = 0;
            grid.SetActive(false);
            gameObject.GetComponent<TextMeshProUGUI>().text = "0:00";
            Time.timeScale = 0.0f;
        }
    }
}
