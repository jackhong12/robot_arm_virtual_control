using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateAxis6 : MonoBehaviour {

    private GameObject robotArm;
    private RobotArmControl robotArmScript;
    private float preAngle;

    // Use this for initialization
    void Start()
    {
        robotArm = GameObject.Find("Epson5S");
        robotArmScript = robotArm.GetComponent<RobotArmControl>();
        preAngle = (float)robotArmScript.theta6;
    }

    // Update is called once per frame
    void Update()
    {
        float angle = (float)robotArmScript.theta6;
        transform.Rotate(-(angle - preAngle), 0, 0);
        preAngle = angle;
    }
}
