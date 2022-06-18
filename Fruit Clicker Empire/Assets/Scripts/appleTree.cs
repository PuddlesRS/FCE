using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class appleTree : MonoBehaviour
{
    public Controller controller;
    private GameObject AppleTree;
    private Animation ATA;

    private void Start()
    {
        AppleTree = GameObject.Find("TreeOverlay");
        //Fetch Rigidbody2D of apppleTree so that it can be clicked on
        GetComponent<Rigidbody2D>();
        ATA = AppleTree.GetComponent<Animation>();
    }


    private void OnMouseDown()
    {
        ATA.Play();
    }

    //On mouse up increment $
    private void OnMouseUp()
    {
        controller.HarvestApples();
    }


}
