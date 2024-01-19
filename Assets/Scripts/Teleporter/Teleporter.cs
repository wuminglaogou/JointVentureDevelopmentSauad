using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporter : MonoBehaviour
{
    [SerializeField] private Transform destination;
    public Transform GetDestination()//用于获取目标传送点位置
    {
        return destination;
    }
}
