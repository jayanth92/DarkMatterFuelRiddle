using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class Tile : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void OnMouseDown()
    {
        Move.instance.transform.DOMove(transform.position, 0.5f).SetEase(Ease.InOutQuad);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
