using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ballPosition : MonoBehaviour {
    private GameObject robotArm;
    private RobotArmControl robotArmScript;
    // Use this for initialization
    void Start () {
        robotArm = GameObject.Find("Epson5S");
        robotArmScript = robotArm.GetComponent<RobotArmControl>();
    }
	
	// Update is called once per frame
	void Update () {
        transform.position = new Vector3(robotArmScript.endPoint[0], robotArmScript.endPoint[2] + 330, robotArmScript.endPoint[1]);
	}
}
