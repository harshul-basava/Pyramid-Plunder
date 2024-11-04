using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class loadLevel : MonoBehaviour
{
    public string sceneName;

    public void load()
    {
        SceneManager.LoadScene(sceneName);
    }

    public void UpdateTotalScore()
    {
        GameObject.Find("Scorekeeper").GetComponent<Scorekeeper>().UpdateTotalScore();
    }
}
