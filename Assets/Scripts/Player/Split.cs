using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Split : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject PlayerSplit;//¿ËÂ¡½ÇÉ«
    private bool isactive;
    private Playerinputcontrol inputcontrol;
    // Update is called once per frame
    private void Awake()
    {
        isactive = true;
        inputcontrol = new Playerinputcontrol();
        inputcontrol.GamePlay.split.performed +=split ;
    }
    private void OnEnable()
    {
        inputcontrol.Enable();
    }
    private void OnDisable()
    {
        inputcontrol.Disable();
    }
    void Update()
    {
        inputcontrol = new Playerinputcontrol();
    }
    public void split(InputAction.CallbackContext context)
    {
            Debug.Log("1");
        if (isactive == true)
        {
            PlayerSplit.SetActive(true);
            isactive = false;
        }
    }
}
