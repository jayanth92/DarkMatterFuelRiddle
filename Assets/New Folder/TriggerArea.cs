using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerArea : MonoBehaviour
{
    public int id;
    // Start is called before the first frame update
    void Start()
    {
        transform.GetChild(0).GetComponent<DoorController>().id = id;
    }

    private void OnTriggerEnter(Collider other)
    {
        GameEvents.current.DoorWayTriggerEnter(id);
    }

    private void OnTriggerExit(Collider other)
    {
        GameEvents.current.DoorWayTriggerExit(id);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
