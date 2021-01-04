using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;

public class Move : MonoBehaviour
{

    public static Move instance;
 //   [SerializeField] private Vector3 movement = new Vector3();
    public GameObject[] MyTiles;
    public int CurrentID;

    public List<GameObject> Fuel_Points;

    public GameObject Fuel_Point_Fraction_Object;

    public List<GameObject> Fuel_List;

    public ObjectPool objectPool;

    [Header("UI")]
    public GameObject fractionPrefab;
    public Vector3 uiOffset;
    public Transform canvas;

    public int FuelInShip;
    public int ShipMaxFuelCapacity;
    public int MaxFuelPointCapacity;

    private bool Animating= false;


    public GameObject WonCanvas;
    public GameObject LoseCanvas;


    public GameObject ZoomoutCamera;

    public GameObject ZoominCamera;

    public AudioSource audioData;
    public AudioClip NoFuelclip;
    public AudioClip moveclip;
    public AudioClip Fuelclip;
    // Start is called before the first frame update
    void Start()
    {
        GameEvents.current.onDoorWayTriggerEnter += GetTileID;
        audioData = GetComponent<AudioSource>();
      
        Initialize();
    }
    [HideInInspector]
    public GameObject fractionObject;


    public void Initialize()
    {
        fractionObject = Instantiate(fractionPrefab, canvas);
        fractionObject.SetActive(false);
    }
    void Awake()
    {
        instance = this;
    }
    private void CmdRightMove()

    {
        ZoominCamera.SetActive(false);
        ZoomoutCamera.SetActive(true);
        if (!Animating)
        {
            
  if (FuelInShip > 0)
        {Animating = true;
                audioData.clip = moveclip;
                audioData.Play(0);
                FuelInShip -= 1; 
            
            if ((CurrentID + 1) < MyTiles.Length)
            transform.DOMove(MyTiles[CurrentID+1].transform.position, 0.5f).SetEase(Ease.InOutExpo).OnStepComplete(() =>
            {
               // Debug.Log("Complete");
                Animating = false;
            });
            
        }
  else
            {

                audioData.clip = NoFuelclip;
                audioData.Play(0);
            }
        fractionObject.SetActive(true);
        fractionObject.transform.GetChild(0).DOComplete();
        fractionObject.transform.GetChild(0).DOPunchScale(Vector3.one, .3f, 10, 1);
        fractionObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = FuelInShip.ToString();
        fractionObject.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = ShipMaxFuelCapacity.ToString();

        //transform.Translate(movement);
        }
      
    }
   
    private void CmdLeftMove()
    {
        ZoominCamera.SetActive(false);
        ZoomoutCamera.SetActive(true);
        if (!Animating)
        {
           

            if (FuelInShip > 0)
            { Animating = true;
                audioData.clip = moveclip;
                audioData.Play(0);
                audioData.Play(0);
                FuelInShip -= 1;

                if (CurrentID - 1 >= 0)
                    transform.DOMove(MyTiles[CurrentID - 1].transform.position, 0.5f).SetEase(Ease.InOutExpo).OnStepComplete(() =>
                    {
                        //Debug.Log("Complete");
                        Animating = false;
                    });
            }
            else
            {
                audioData.clip = NoFuelclip;
                audioData.Play(0);
            }
            fractionObject.SetActive(true);
            fractionObject.transform.GetChild(0).DOComplete();
            fractionObject.transform.GetChild(0).DOPunchScale(Vector3.one, .3f, 10, 1);
            fractionObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = FuelInShip.ToString();
            fractionObject.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = ShipMaxFuelCapacity.ToString();
        }
        }
    private void CmdUp()
    {
        ZoominCamera.SetActive(true);
        ZoomoutCamera.SetActive(false);
        if (FuelInShip < ShipMaxFuelCapacity&& Fuel_List.Count>0)
        {
 fractionObject.SetActive(true);
        if (FuelInShip >= 0 )
        {
            FuelInShip += 1;
                audioData.clip = Fuelclip;
                audioData.Play(0);
                
        }
        

        Fuel_List[(Fuel_List.Count) - 1].SetActive(false);
        Fuel_List.Remove(Fuel_List[(Fuel_List.Count)-1]);
        fractionObject.SetActive(true);
        fractionObject.transform.GetChild(0).DOComplete();
        fractionObject.transform.GetChild(0).DOPunchScale(Vector3.one, .3f, 10, 1);
        fractionObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = FuelInShip.ToString();
        fractionObject.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = ShipMaxFuelCapacity.ToString();





        Fuel_Point_Fraction_Object.SetActive(true);
        Fuel_Point_Fraction_Object.transform.GetChild(0).DOComplete();
        Fuel_Point_Fraction_Object.transform.GetChild(0).DOPunchScale(Vector3.one, .3f, 10, 1);
        Fuel_Point_Fraction_Object.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = Fuel_List.Count.ToString();
        Fuel_Point_Fraction_Object.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = MaxFuelPointCapacity.ToString();

        }
       

    }
    //public float thrust = Random.Range(-0.5f, -0.1f);
    public Rigidbody rb;
    private void CmdDown()
    {
        ZoominCamera.SetActive(true);
        ZoomoutCamera.SetActive(false);
        if (FuelInShip>0)
        {  
            GameObject Fuel = objectPool.GetObj();
            audioData.clip = Fuelclip;
            audioData.Play(0);
            Fuel_List.Add(Fuel);
        // Fuel.transform.DOMove(MyTiles[CurrentID].transform.position, 0.5f).SetEase(Ease.InOutQuad);
        Fuel.transform.position =  Fuel_Points[Fuel_List.Count].transform.position;
        //  rb = Fuel.GetComponent<Rigidbody>();
        //  rb.AddForce(Random.Range(-0.5f, -0.1f), Random.Range(-0.5f, -0.1f), Random.Range(-0.5f, -0.1f), ForceMode.Impulse);
          FuelInShip -= 1;
        
 

        ////UI
        fractionObject.SetActive(true);
        fractionObject.transform.GetChild(0).DOComplete();
        fractionObject.transform.GetChild(0).DOPunchScale(Vector3.one, .3f, 10, 1);
        fractionObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = FuelInShip.ToString();
        fractionObject.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = ShipMaxFuelCapacity.ToString();



        Fuel_Point_Fraction_Object.SetActive(true);
        Fuel_Point_Fraction_Object.transform.GetChild(0).DOComplete();
        Fuel_Point_Fraction_Object.transform.GetChild(0).DOPunchScale(Vector3.one, .3f, 10, 1);
        Fuel_Point_Fraction_Object.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = Fuel_List.Count.ToString();
        Fuel_Point_Fraction_Object.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = MaxFuelPointCapacity.ToString();
}
    }
    public void GetTileID(int id)
    {
        CurrentID = id;
        //  transform.DOMove((transform.position + new Vector3(1, 1, 1)), 0.5f).SetEase(Ease.InOutQuad);

    }
        //=> transform.Translate(-movement);
        // Update is called once per frame
        void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            CmdLeftMove();
        }

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            CmdRightMove();
        }

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            CmdUp();
        }

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            CmdDown();
        }

       
            if (fractionObject != null)
                fractionObject.transform.position = Camera.main.WorldToScreenPoint(transform.position + uiOffset);
       

    }


}
