using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Diamond : MonoBehaviour,ICollectable
{
    [SerializeField]
    PlayerController playerController;

    private void Awake()
    {
        playerController.OnCollectDiamond += OnCollect;
    }

    public void OnCollect(GameObject go)
    {
        go.SetActive(false);
    }   
}
