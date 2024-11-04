using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
 
public class DraggableItem : MonoBehaviour, IDragHandler
{
    public Canvas canvas;
    public GameObject close;
    public GameObject InventoryManager;
    public RectTransform CANVAS_RTF;

    public List<Vector2> snapPoints;
    private RectTransform rectTransform;

    public bool snapped;
    public bool colliding;
 
    // Start is called before the first frame update
    public void assignVars(GameObject CANVAS, GameObject CLOSE)
    {
        snapped = false;
        colliding = false;

        canvas = CANVAS.GetComponent<Canvas>(); // getting the canvas component
        CANVAS_RTF = CANVAS.GetComponent<RectTransform>(); // getting the canvas transform
        InventoryManager = GameObject.Find("InventoryManager");

        rectTransform = GetComponent<RectTransform>();
        snapPoints = new List<Vector2>();

        var x = new List<int> {-245, -187, -131, -75, -20, 35, 90, 146, 199}; 
        var y = new List<int> {245, 187, 131, 75, 20, -35, -90, -146, -199}; 

        foreach (int i in x) 
        {
            foreach (int j in y)
            {
                var sp = new Vector2(i, j);
                snapPoints.Add(sp);
            }
        }
    }

    public void Update()
    {
        if (!Input.GetMouseButton(0)) 
        {
            SnapGrid();
        }
    }
 
    void IDragHandler.OnDrag(PointerEventData eventData)
    {
        Vector3 mousePos = Input.mousePosition;
        if (mousePos.x < Screen.width && mousePos.y < Screen.height && mousePos.x > 0 && mousePos.y > 0)
        {
            rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor; // dragging w/ mouse
        } else {
            eventData.pointerDrag = null;
        }

        float itemWidth = gameObject.GetComponent<RectTransform>().sizeDelta.x;
        float itemHeight = gameObject.GetComponent<RectTransform>().sizeDelta.y;

        if (rectTransform.anchoredPosition.y > (CANVAS_RTF.sizeDelta.y / 2) ||
            rectTransform.anchoredPosition.x > (CANVAS_RTF.sizeDelta.x / 2) - (itemWidth / 2) ||
            rectTransform.anchoredPosition.y < -1 * (CANVAS_RTF.sizeDelta.y / 2) ||
            rectTransform.anchoredPosition.x < -1 * ((CANVAS_RTF.sizeDelta.y / 2) + (itemWidth / 2)))
        {
            float yPos = Mathf.Clamp(rectTransform.anchoredPosition.y, -1 * (CANVAS_RTF.sizeDelta.y / 2), (CANVAS_RTF.sizeDelta.y / 2)); 
            float xPos = Mathf.Clamp(rectTransform.anchoredPosition.x, -1 * ((CANVAS_RTF.sizeDelta.x / 2) + (itemWidth / 2)), (CANVAS_RTF.sizeDelta.x / 2) - (itemWidth / 2)); 

            rectTransform.anchoredPosition = new Vector2(xPos, yPos);
        }
    }

    // snapping the currently dragged image to a spot on the grid
    private void SnapGrid() { 
        float closestDistance = float.PositiveInfinity;
        Vector2 closestSnapPoint = new Vector2 (float.PositiveInfinity, float.PositiveInfinity);
        var rtf = gameObject.GetComponent<RectTransform>();

        foreach (Vector2 snapPoint in snapPoints) { // finding closest snap point
            float currentDistance = Vector2.Distance(rtf.transform.localPosition, snapPoint);
            if (currentDistance < closestDistance && (snapPoint.x + rtf.sizeDelta.x) < 250 && (snapPoint.y - rtf.sizeDelta.y) > -250) {
                closestSnapPoint = snapPoint;
                closestDistance = currentDistance;
            }
        }

        if (closestSnapPoint != null && closestDistance <= 50) {
            rtf.transform.localPosition = closestSnapPoint;
            snapped = true;
        } else {
            snapped = false;
        }

        float trashDist = Vector2.Distance(new Vector2(rtf.transform.localPosition.x + (rtf.sizeDelta.x/2), rtf.transform.localPosition.y - (rtf.sizeDelta.y/2)), GameObject.Find("DropItem").transform.localPosition);
        
        if (trashDist < 50) 
        {
            InventoryManager.GetComponent<InventoryManagement>().RemoveFromInventory(gameObject.name);
        }
    }

    private void OnCollisionEnter2D(Collision2D curr) {
        if (curr.gameObject.tag == "Inventory")
        {
            colliding = true;
        }
    }

    private void OnCollisionExit2D(Collision2D curr) {
        colliding = false;
    }

}