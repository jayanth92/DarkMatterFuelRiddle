using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class number : MonoBehaviour
{
    public GameObject fractionPrefab;

    public GameObject fractionObject;
    public Vector3 uiOffset;
    public Transform canvas;
    public int numbercount;
    // Start is called before the first frame update
    void Start()
    {
        fractionObject = Instantiate(fractionPrefab, canvas);
        fractionObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = numbercount.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        if (fractionObject != null)
            fractionObject.transform.position = Camera.main.WorldToScreenPoint(transform.position + uiOffset);

    }
}
