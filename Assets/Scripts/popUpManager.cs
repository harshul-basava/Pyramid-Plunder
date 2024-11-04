using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class popUpManager : MonoBehaviour
{
    public GameObject[] popUps;
    public GameObject grid;

    void Start()
    {
        DeactivateAll();
    }

    void Update()
    {
        if (grid.activeInHierarchy)
        {
            DeactivateAll();
        }
    }

    public void ActivatePopUp(GameObject currItem) 
    {
        foreach (GameObject popUp in popUps)
        {
            if (currItem.name.Contains(popUp.name))
            {
                popUp.SetActive(true);
            }
            else
            {
                popUp.SetActive(false);
            }
        }
    }

    public void DeactivateAll()
    {
        foreach (GameObject popUp in popUps)
        {
            popUp.SetActive(false);
        }
    }
}
