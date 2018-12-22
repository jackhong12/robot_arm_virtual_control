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
    private EpsonCoordinate epc;
    public float[] endPoint;
    public float[] checkAngle = new float[6] { 0, 0, 0, 0, 0, 0 }; 

	// Use this for initialization
	void Start () {
        //angle initial
        epc = new EpsonCoordinate();
        endPoint = new float[3];
        //matrix.show();

        theta1 = 0;
        theta2 = 0;
        theta3 = 0;
        theta4 = 0;
        theta5 = 0;
        theta6 = 0;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        keyboardControlTest(ref theta5);

        float[] angle = new float[6] { theta1, theta2, theta3, theta4, theta5, theta6 };
        epc.moveUniZ(angle, 0);

        //epson calculate


        endPoint[0] = epc.realX;
        endPoint[1] = epc.realY;
        endPoint[2] = epc.realZ;

        checkAngle[0] = epc.newTh[0];
        checkAngle[1] = epc.newTh[1];
        checkAngle[2] = epc.newTh[2];
        checkAngle[3] = epc.newTh[3];
        checkAngle[4] = epc.newTh[4];
        checkAngle[5] = epc.newTh[5];
    }

    void keyboardControlTest(ref float controlAxis){
        float[] angle = new float[6] { theta1, theta2, theta3, theta4, theta5, theta6 };

        bool flag = false;

        if (Input.GetKey(KeyCode.A))
            flag = epc.moveUniZ(angle, 5);
            
        else if (Input.GetKey(KeyCode.S))
            flag = epc.moveUniZ(angle, -5);
        if (Input.GetKey(KeyCode.D))
            flag = epc.moveUniX(angle, 5);
        else if (Input.GetKey(KeyCode.F))
            flag = epc.moveUniX(angle, -5);
        if (Input.GetKey(KeyCode.G))
            flag = epc.moveUniY(angle, 5);
        else if (Input.GetKey(KeyCode.H))
            flag = epc.moveUniY(angle, -5);
        if (flag)
        {
            theta1 = epc.newTh[0];
            theta2 = epc.newTh[1] - 90;
            theta3 = epc.newTh[2];
            theta4 = epc.newTh[3];
            theta5 = epc.newTh[4];
            theta6 = epc.newTh[5];
            flag = false;
        }

        
    }
    public void Onclick()
    {
        theta1++;
    }
}
