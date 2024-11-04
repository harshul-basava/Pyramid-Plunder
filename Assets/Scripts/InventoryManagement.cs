using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System.Linq;
using UnityEngine;

public class InventoryManagement : MonoBehaviour
{
    public List<GameObject> inventory;
    public List<string> inventoryNames;
    public HealthManager healthManager;

    public GameObject canvas;
    public GameObject grid;
    public GameObject close;

    void Start()
    {
        inventory = new List<GameObject>();
    }

    public void UpdateInventory(GameObject item)
    {  
        ItemData profile = item.GetComponent<ItemData>();

        healthManager.playerHealth += profile.healthBuff;
        healthManager.playerDefense += profile.defenseBuff;
        healthManager.playerAttack += profile.attackBuff;

        // doing the grid placement stuff
        grid.SetActive(true);
        close.SetActive(true);

        GameObject imgObject = new GameObject(item.name + "_Icon"); // creating the image object
        imgObject.tag = "Inventory";

        RectTransform tf = imgObject.AddComponent<RectTransform>(); // adding the rect transform
        DraggableItem DWS = imgObject.AddComponent<DraggableItem>(); // making it draggable

        Rigidbody2D rb = imgObject.AddComponent<Rigidbody2D>();
        BoxCollider2D bc = imgObject.AddComponent<BoxCollider2D>(); // adding a box collider & rigidbody
        
        rb.isKinematic = false;
        rb.gravityScale = 0;
        rb.freezeRotation = true;

        bc.offset = new Vector2(profile.width / 2, -1 * profile.height / 2); // dimensions for the collider
        bc.size = new Vector2(profile.width, profile.height);

        DWS.assignVars(canvas, close);

        tf.transform.SetParent(GameObject.Find("Grid").transform); // setting parent
        tf.localScale = Vector3.one;
        tf.anchoredPosition = new Vector2(-50f, -280); // setting position, will be on center
        tf.sizeDelta= new Vector2(profile.width, profile.height); // custom size
        tf.pivot = new Vector2(0, 1);

        Image img = imgObject.AddComponent<Image>();
        img.sprite = item.GetComponent<SpriteRenderer>().sprite;
        imgObject.transform.SetParent(GameObject.Find("Grid").transform);

        // adding to the inventory list

        inventory.Add(item);
        inventoryNames.Add(item.name + "_Icon");
    }

    public int GetInventorySize()
    {
        if (inventory.Count == 0)
        {
            return 1;
        }
        
        int total = 0;

        foreach (GameObject item in inventory)
        {   
            ItemData profile = item.GetComponent<ItemData>();
            total += profile.size;
        }

        return total;
    }

    public void CloseInventory()
    {
        grid.SetActive(false);
    }

    public void OpenInventory()
    {
        grid.SetActive(true);
    }

    public void RemoveFromInventory(string objectName)
    {
        int index = inventoryNames.IndexOf(objectName);
        GameObject obj = inventory[index];
        ItemData profile = obj.GetComponent<ItemData>();

        obj.SetActive(true);
        obj.GetComponent<SpriteRenderer>().sortingLayerName = "Items";
        obj.transform.SetParent(GameObject.Find("Items").transform);
        obj.transform.localScale = new Vector3(1, 1, 0); // TODO: figure out why this isn't working properly

        inventory.RemoveAt(index);
        inventoryNames.RemoveAt(index);
        GameObject.Destroy(GameObject.Find(objectName));

        healthManager.playerHealth -= profile.healthBuff;
        healthManager.playerDefense -= profile.defenseBuff;
        healthManager.playerAttack -= profile.attackBuff;
    }

    public int CalculateFinalScore()
    {
        int score = 0;

        foreach (GameObject item in inventory)
        {
            ItemData profile = item.GetComponent<ItemData>();

            score += profile.score;
        }

        return score;
    }
}