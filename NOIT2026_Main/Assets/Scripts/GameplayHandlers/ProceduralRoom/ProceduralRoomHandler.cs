using System.Collections.Generic;
using UnityEngine;

public class ProceduralRoomHandler : MonoBehaviour
{
    [SerializeField] List<GameObject> proceduralRoomsPositions;

    void Start()
    {
        foreach (var position in proceduralRoomsPositions)
        {
            var randomRoom = position.transform.GetChild(Random.Range(0, position.transform.childCount)).gameObject;
            randomRoom.SetActive(true);
        }
    }
}
