using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

using NavMeshPlus.Components;
using NavMeshPlus.Extensions;
using UnityEngine.AI;

public class itemGenerator : MonoBehaviour
{
    public int itemsPerRoom;

    public GameObject[] itemPrefabs;
    public List<GameObject> rooms;

    void Awake()
    {
        rooms = GameObject.Find("Map").GetComponent<mapGenerator>().rooms;

        foreach (GameObject room in rooms)
        {
            List<Vector2> roomCoords = room.GetComponent<childTilemaps>().coords;
            Vector2 origin = new Vector2(room.transform.position.x, room.transform.position.y);
            
            for (int i = 0; i < itemsPerRoom; i++)
            {
                int index = Random.Range(0, roomCoords.Count);
                Vector2 itemPos = roomCoords[index]; // random spawn coordinate
                roomCoords.RemoveAt(index);

                itemPos = new Vector2(origin.x + itemPos.x + 0.5f, origin.y + itemPos.y + 0.5f);
                GameObject itemPrefab = itemPrefabs[Random.Range(0, itemPrefabs.Length)]; // random item

                Instantiate(itemPrefab, itemPos, Quaternion.identity, gameObject.transform);
            }
        }
    }
}
