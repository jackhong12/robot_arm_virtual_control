using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotArmControl : MonoBehaviour {
    //D&H parameter
    public float theta1, theta2, theta3, theta4, theta5, theta6;
    private readonly float minth1 = -170, minth2 = -150, minth3 = -70, minth4 = -190, minth5 = -135, minth6 = -360; //theta range
    private readonly float maxth1 = 170, maxth2 = 65, maxth3 = 190, maxth4 = 190, maxth5 = 135, maxth6 = 360;
    private readonly float a1 = 0, a2 = 100, a3 = 310, a4 = 40, a5 = 0, a6 = 0;
    private readonly float alp1 = 0, alp2 = 90, alp3 = 0, alp4 = 90, alp5 = -90, alp6 = 90;
    private readonly float d1 = 199, d2 = 0, d3 = 0, d4 = 305, d5 = 0, d6 = 80;

	// Use this for initialization
	void Start () {
        //angle initial
        theta1 = 0;
        theta2 = 0;
        theta3 = 0;
        theta4 = 0;
        theta5 = 0;
        theta6 = 0;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        keyboardControlTest();
	}

    void keyboardControlTest(){
        if (Input.GetKey(KeyCode.A))
            theta1++;
        else if (Input.GetKey(KeyCode.S))
            theta1--;
    }
    public void Onclick()
    {
        theta1++;
    }
}
