using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class GuidBuild : MonoBehaviour
{
    public string guid=null;
    private void OnValidate()
    {
        if (guid == null)
        {
            guid = Guid.NewGuid().ToString();
        }
    }
    private void OnEnable()
    {
        
    }
}
