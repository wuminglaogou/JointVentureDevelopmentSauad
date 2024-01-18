﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTeleporter : MonoBehaviour
{
    private GameObject currentTeleporter;
    public float disableTime;//可设置传送之后的无法传送时间
    private float disableCounter;
    public bool disableTeleporter;//表示能否传送

    void Update()
    {
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
}
