using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerTeleporter : MonoBehaviour
{
    public AudioData teleportSFX;
    private GameObject currentTeleporter;
    public float disableTime;//可设置传送之后的无法传送时间
    private float disableCounter;
    public bool disableTeleporter;//表示能否传送
    public float findDistance;
    public GameObject[] doors=new GameObject[12];

    void Update()
    {
        FindDoor(doors);
        if (disableTeleporter)
        {
            disableCounter -= Time.deltaTime;
            if (disableCounter <= 0)
                disableTeleporter = false;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Teleporter"))
        {
            currentTeleporter = collision.gameObject;//获得当前传送点目标
            if (!disableTeleporter)
            {
                AudioManager.Instance.PlayAudio(teleportSFX);
                transform.position = currentTeleporter.GetComponent<Teleporter>().GetDestination().position;
                HeleporterAble();
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Teleporter"))
        {
            if (currentTeleporter == collision.gameObject)
            {
                currentTeleporter = null;
            }
        }
    }

    private void HeleporterAble()//控制能否传送的方法
    {
        if (!disableTeleporter)
        {
            disableTeleporter = true;
            disableCounter = disableTime;
        }
    }

    public void FindDoor(params GameObject[] gameObject)
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            for (int i = 0;i<12; i++)
            {
                if (gameObject[i].tag == "TeleporterDoor" && Vector2.Distance(gameObject[i].transform.position, transform.position) <= findDistance)
                {
                    gameObject[i].GetComponent<Teleporter>().Appear();
                    gameObject[i].GetComponent<Teleporter>().TurnOnAnother(gameObject[i].GetComponent<Teleporter>().anotherDoor);
                }
            }
        }
    }
}
