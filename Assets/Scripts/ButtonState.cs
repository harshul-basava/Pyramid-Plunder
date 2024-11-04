using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class ButtonState : MonoBehaviour
{

    private GameObject[] inventory_objects;
    public InventoryManagement InventoryManager;
    public Button button;

    public Timer timer;

    // Start is called before the first frame update
    void Start()
    {
        button.interactable = enable();
    }

    // Update is called once per frame
    void Update()
    {
        button.interactable = enable(); 
        if (timer != null)
        {
            if (Input.GetKeyDown(KeyCode.E) && timer.timeRemaining > 0)
            { 
                if (!InventoryManager.grid.activeInHierarchy) {
                    InventoryManager.OpenInventory();
                } else if (enable() && !Input.GetMouseButton(0)) {
                    InventoryManager.CloseInventory();
                }
            }
        } else {
            if (Input.GetKeyDown(KeyCode.E))
            { 
                if (!InventoryManager.grid.activeInHierarchy) {
                    InventoryManager.OpenInventory();
                } else if (enable() && !Input.GetMouseButton(0)) {
                    InventoryManager.CloseInventory();
                }
            }
        }
    }


    public bool enable() // checking if the player is able to leave the inventory
    {
        inventory_objects = GameObject.FindGameObjectsWithTag("Inventory");

        foreach (GameObject inventory_object in inventory_objects)
        {
            if (!inventory_object.GetComponent<DraggableItem>().snapped)
            {
                return false;
            }

            if (inventory_object.GetComponent<DraggableItem>().colliding)
            {
                return false;
            }
        }

        return true;
    }

}
