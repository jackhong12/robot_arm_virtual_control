﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateAxis5 : MonoBehaviour {

    private GameObject robotArm;
    private RobotArmControl robotArmScript;
    private float preAngle;

    // Use this for initialization
    void Start()
    {
        robotArm = GameObject.Find("Epson5S");
        robotArmScript = robotArm.GetComponent<RobotArmControl>();
        preAngle = (float)robotArmScript.theta5;
    }

    // Update is called once per frame
    void Update()
    {
        float angle = (float)robotArmScript.theta5;
        transform.Rotate(0, (angle - preAngle), 0);
        preAngle = angle;
    }
}
