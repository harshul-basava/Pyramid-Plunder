using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    public InventoryManagement InventoryManager;
    private bool atItem;
    private GameObject item;
    public popUpManager pum;

    public Timer timer;

    // Check if hovering over an Item
    private void OnTriggerEnter2D(Collider2D curr) {
        if (curr.gameObject.tag == "ITEM")
        {
            item = curr.gameObject;
            atItem = true;
            pum.ActivatePopUp(item);
        }
    }

    private void OnTriggerStay2D(Collider2D curr) {
        if (curr.gameObject.tag == "ITEM")
        {
            item = curr.gameObject;
            atItem = true;
            pum.ActivatePopUp(item);
        }
    }

    // checks if you stop hovering over an item
    private void OnTriggerExit2D(Collider2D curr)
    {
        atItem = false;
        pum.DeactivateAll();
    }

    private void Update()
    {
        // if hovering over an item, press space to pick up
        if (Input.GetKeyDown(KeyCode.LeftShift) && atItem && timer != null)
        {
            if (timer.timeRemaining > 0)
            {
                InventoryManager.UpdateInventory(item);
                item.transform.SetParent(gameObject.transform);
                item.transform.localPosition = Vector2.zero;
                item.SetActive(false);
            }
        }
        else if (Input.GetKeyDown(KeyCode.LeftShift) && atItem)
        {
            InventoryManager.UpdateInventory(item);
            item.transform.SetParent(gameObject.transform);
            item.transform.localPosition = Vector2.zero;
            item.SetActive(false);
        }
    }

}
