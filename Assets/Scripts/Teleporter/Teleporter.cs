using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Teleporter : MonoBehaviour
{
    [SerializeField] private Transform destination;
    private Boolean isActive=true;
    public GameObject anotherDoor;
    public Transform GetDestination()//用于获取目标传送点位置
    {
        return destination;
    }
    public void Appear()
    {
        gameObject.SetActive(isActive);
        isActive = !isActive;
    }
    public void TurnOnAnother(GameObject anotherDoor)
    {
        gameObject.SetActive(!isActive);
    }
   
}
