using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]   
public class Data 
{
    public SerializeVector3 vector3Data;
}
[System.Serializable]
public class SerializeVector3
{
    public float x;
    public float y; 
    public float z;
    public SerializeVector3(Vector3 data)
    {
        x = data.x;
        y = data.y;
        z = data.z;
    }
    public Vector3 ToVector3()
    {
        return new Vector3(x, y,z);
    }
}