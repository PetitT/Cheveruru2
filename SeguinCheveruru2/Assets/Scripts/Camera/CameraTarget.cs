﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraTarget : Singleton<CameraTarget>
{
    public Transform player;
    public Transform boss;
    public Transform cameraLevel;
    public Transform target;
    public CinemachineVirtualCamera cam;

    public float camSizeChangeSpeed;
    public float recenterSpeed;

    private float currentSpeed;

    public Vector2 camSizeRange = new Vector2(18, 32);
    public Vector2 camLevelRange = new Vector2(10, 23);
    public Vector2 distanceRange = new Vector2(15, 45);

    private bool isCentered = false;

    private float normalizedDistance => CalculateNormalizedDistance();

    private void Start()
    {
        currentSpeed = camSizeChangeSpeed;
    }

    private void Update()
    {
        CalculateNormalizedDistance();
        MoveTarget();
        ModifyCamLevel();
        ModifyCamSize();
    }

    private float CalculateNormalizedDistance()
    {
        float distance = Vector2.Distance(player.position, boss.position);
        float normalized = (distance - distanceRange.x) / (distanceRange.y - distanceRange.x);
        normalized = Mathf.Clamp(normalized, 0, 1);
        return normalized;
    }

    private void MoveTarget()
    {
        target.position = Vector2.Lerp(player.position.ModifyY(cameraLevel.position.y), boss.position.ModifyY(cameraLevel.position.y), 0.5f);
    }
    private void ModifyCamLevel()
    {
        float Y = camLevelRange.y;
        if (!isCentered)
        {
            Y = Mathf.Lerp(camLevelRange.x, camLevelRange.y, normalizedDistance);
        }
        cameraLevel.transform.position = new Vector2(cameraLevel.transform.position.x, Y);
    }

    private void ModifyCamSize()
    {
        float size = camSizeRange.y;
        if (!isCentered)
        {
            size = Mathf.Lerp(camSizeRange.x, camSizeRange.y, normalizedDistance);
        }
        cam.m_Lens.OrthographicSize = Mathf.MoveTowards(cam.m_Lens.OrthographicSize, size, currentSpeed * Time.deltaTime);
    }

    public void ToggleRecenter(bool toggle)
    {
        isCentered = toggle;
        currentSpeed = toggle ? recenterSpeed : camSizeChangeSpeed;
    }
}
