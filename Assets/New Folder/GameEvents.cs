using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEvents : MonoBehaviour
{
    public static GameEvents current;
    public GameObject[] Fuel_Points;


    // Start is called before the first frame update
    private void Awake()
    {
        current = this;
    }
    public event Action<int> onDoorWayTriggerEnter;
    public event Action<int> onDoorWayTriggerExit;

    void Start()
    {
        
    }

    public void DoorWayTriggerEnter(int id)
    {
        if(onDoorWayTriggerEnter != null)
        {
            onDoorWayTriggerEnter(id);
        }
    }

    public void DoorWayTriggerExit(int id)
    {
        if (onDoorWayTriggerExit != null)
        {
            onDoorWayTriggerExit(id);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
