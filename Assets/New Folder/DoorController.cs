using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;
public class DoorController : MonoBehaviour
{
    public int id;
    public List<GameObject> Fuel_Points;

    public List<GameObject> Fuel_List;

    [Header("UI")]
    public GameObject fractionPrefab;
    public Vector3 uiOffset;
    public Transform canvas;
    [HideInInspector]
    public GameObject fractionObject;

    // Start is called before the first frame update
    void Start()
    {
        GameEvents.current.onDoorWayTriggerEnter += OnDoorwayOpen;
        GameEvents.current.onDoorWayTriggerExit += OnDoorwayExit;
        Initialize();
    }
    public void Initialize()
    {
        canvas = GameObject.FindGameObjectWithTag("Canvas").transform;
        fractionObject = Instantiate(fractionPrefab, canvas);
        
        if (id == 0)
        {
            for (int i = 0; i < 45; i++)
            {
                GameObject Fuel = Move.instance.objectPool.GetObj();
                Fuel_List.Add(Fuel);
                Fuel.transform.position = Fuel_Points[i].transform.position;
            }

            Move.instance.Fuel_Point_Fraction_Object.SetActive(true);
            Move.instance.Fuel_Point_Fraction_Object.transform.GetChild(0).DOComplete();
            Move.instance.Fuel_Point_Fraction_Object.transform.GetChild(0).DOPunchScale(Vector3.one, .3f, 10, 1);
            Move.instance.Fuel_Point_Fraction_Object.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = Move.instance.Fuel_List.Count.ToString();
            Move.instance.Fuel_Point_Fraction_Object.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = Move.instance.ShipMaxFuelCapacity.ToString();

        }

            fractionObject.SetActive(false);
    }
    // Update is called once per frame
    void Update()
    {
        if (fractionObject != null)
            fractionObject.transform.position = Camera.main.WorldToScreenPoint(transform.position + uiOffset);

    }

    public void OnDoorwayOpen(int id)
    {
        if (id == this.id)
        {
            Move.instance.Fuel_Points = Fuel_Points;

            Move.instance.Fuel_List = Fuel_List;
            fractionObject.SetActive(true);
            Move.instance.Fuel_Point_Fraction_Object = fractionObject;
            Move.instance.Fuel_Point_Fraction_Object.SetActive(true);
            Move.instance.Fuel_Point_Fraction_Object.transform.GetChild(0).DOComplete();
            Move.instance.Fuel_Point_Fraction_Object.transform.GetChild(0).DOPunchScale(Vector3.one, .3f, 10, 1);
            Move.instance.Fuel_Point_Fraction_Object.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = Move.instance.Fuel_List.Count.ToString();
            Move.instance.Fuel_Point_Fraction_Object.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = Move.instance.MaxFuelPointCapacity.ToString();

            //transform.DOMove((transform.position+new Vector3(1,1,1)), 0.5f).SetEase(Ease.InOutQuad);
           // Debug.Log("WonMove.instance.MyTiles.Length"+Move.instance.MyTiles.Length);
         //   Debug.Log("Wonid" + id);
            if (id == (Move.instance.MyTiles.Length) - 1)
            {
                //Won 
                Debug.Log("Won");
                //Debug.Log("WonMove.instance.MyTiles.Length"+Move.instance.MyTiles.Length);
                //Debug.Log("Wonid" + id);
                Move.instance.WonCanvas.SetActive(true);
            }
            if (Fuel_List.Count== 0 && Move.instance.FuelInShip==0&& id != (Move.instance.MyTiles.Length) - 1)
            {
                Debug.Log("Lose");
                Move.instance.LoseCanvas.SetActive(true);

            }
            }
    }

    public void OnDoorwayExit(int id)
    {
        if (id == this.id)
        {
           

             Fuel_List = Move.instance.Fuel_List ;
            fractionObject.SetActive(true);

            //transform.DOMove((transform.position+new Vector3(1,1,1)), 0.5f).SetEase(Ease.InOutQuad);
        }

    }
}
