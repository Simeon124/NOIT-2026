using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UnlockedCheck : MonoBehaviour
{
    public GravityScript[] pins;
    List<Vector3> defaultPositions = new List<Vector3>();
    public bool unlocked; // when this is true the lock can be unlocked. this serves as a WIN CONDITION
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        unlocked = false;
        foreach (var pin in pins)
        {
            defaultPositions.Add(pin.transform.position);
        }
    }

    // Update is called once per frame
    private void Update()
    {
        /*/unlocked = true;
        foreach (var pin in pins)
        {
            if (!pin.locker)
            {
                unlocked = false;
                //Debug.Log("Cannot Unlock");
            }

        }/*/
        
        unlocked = pins.All(x => x.locker);
        Debug.Log(pins.All(x => x.locker));

    }
    
    public void ResetPositions()
    {
        for (int i = 0; i < pins.Length; i++)
        {
            var pin =  pins[i];
            pin.transform.position = defaultPositions[i];
        }
    }
}
