using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraChanger : MonoBehaviour
{
    public GameObject camera0;
    public GameObject camera1;
    public GameObject camerachanger0;
    public GameObject camerachanger1;
    public void OnTriggerEnter2D(Collider2D collision)
    {
        camera0.SetActive(false);
        camera0.tag = "Untagged";
        camera1.tag = "MainCamera";
        CharacterManager.Instance.ClearToOnePlayer();
        camera1.SetActive(true);
    }
    public void OnTriggerExit2D(Collider2D collision)
    {
        camerachanger0.SetActive(false);
        camerachanger1.SetActive(true);
    }
}
