using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class EpsonCoordinate{
    public float th1, th2, th3, th4, th5, th6;
    public float uniX, uniY, uniZ;
    public float realX, realY, realZ;
    private readonly float minth1 = -170, minth2 = -150, minth3 = -70, minth4 = -190, minth5 = -135, minth6 = -360; //theta range
    private readonly float maxth1 = 170, maxth2 = 65, maxth3 = 190, maxth4 = 190, maxth5 = 135, maxth6 = 360;
    //D&H method parameter
    private readonly float a1 = 0, a2 = 100, a3 = 310, a4 = 40, a5 = 0, a6 = 0;
    private readonly float alp1 = 0, alp2 = 90, alp3 = 0, alp4 = 90, alp5 = -90, alp6 = 90;
    private readonly float d1 = 199, d2 = 0, d3 = 0, d4 = 305, d5 = 0, d6 = 80;
    public EpsonCoordinate()
    {
        setAngle();
    }
    public void setAngle(float angle1 = 0, float angle2 = 0, float angle3 = 0, float angle4 = 0, float angle5 = 0, float angle6 = 0)
    {
        th1 = angle1;
        th2 = angle2 + 90;
        th3 = angle3;
        th4 = angle4;
        th5 = angle5;
        th6 = angle6;
    }

    public Matrix4_4 T01()
    {
        Matrix4_4 mat = new Matrix4_4();
        mat.setM(1, 1, cos(th1));
        mat.setM(1, 2, -sin(th1));
        mat.setM(2, 1, sin(th1));
        mat.setM(2, 2, cos(th1));
        return mat;
    }

    public Matrix4_4 T02()
    {
        float[,] m = new float[4, 4]{
            { cos(th1) * cos(th2), -cos(th1) * sin(th2), sin(th1), a2 * cos(th1)},
            { sin(th1) * cos(th2), -sin(th1) * sin(th2), -cos(th1), a2 * sin(th1)},
            { sin(th2), cos(th2), 0, 0},
            { 0, 0, 0, 1}
        };
        Matrix4_4 mat = new Matrix4_4();
        mat.setM(m);

        //check 
        /*
        Matrix4_4 rx2 = new Matrix4_4();
        Matrix4_4 rz2 = new Matrix4_4();
        rx2.Rx(alp2, a2);
        rz2.Rz(th2, d2);
        //T01().show("T01");
        //rx2.show("rx2");
        //rz2.show("rz2");
        Matrix4_4 checkM = T01().x(rx2).x(rz2);
        mat.show("T02: ");
        checkM.show("T02 check: ");
        */

        return mat;
    }

    public Matrix4_4 T03()
    {
        float[,] m = new float[4, 4]{
            { cos(th1) * cos(th2) * cos(th3) - cos(th1) * sin(th2) * sin(th3), -cos(th1) * cos(th2) * sin(th3) - cos(th1) * sin(th2) * cos(th3), sin(th1), a2 * cos(th1) + a3 * cos(th1) * cos(th2)},
            { sin(th1) * cos(th2) * cos(th3) - sin(th1) * sin(th2) * sin(th3), -sin(th1) * cos(th2) * sin(th3) - sin(th1) * sin(th2) * cos(th3), -cos(th1), a2 * sin(th1) + a3 * sin(th1) * cos(th2)},
            { sin(th2) * cos(th3) + cos(th2) * sin(th3), -sin(th2) * sin(th3) + cos(th2)*cos(th3), 0, a3 * sin(th2)},
            { 0, 0, 0, 1}
        };
        Matrix4_4 mat = new Matrix4_4();
        mat.setM(m);

        //check
        /*
        Matrix4_4 rx3 = new Matrix4_4();
        Matrix4_4 rz3 = new Matrix4_4();
        rx3.Rx(alp3, a3);
        rz3.Rz(th3, d3);
        Matrix4_4 checkM = T02().x(rx3).x(rz3);

        mat.show("T03 R:");
        checkM.show("T03 C:");
        */
        return mat;
    }

    public Matrix4_4 T04()
    {
        Matrix4_4 mat = new Matrix4_4();
        mat.setM(1, 1, cos(th1) * cos(th2) * cos(th3) * cos(th4) - cos(th1) * sin(th2) * sin(th3) * cos(th4) + sin(th1) * sin(th4));
        mat.setM(2, 1, sin(th1) * cos(th2) * cos(th3) * cos(th4) - sin(th1) * sin(th2) * sin(th3) * cos(th4) - cos(th1) * sin(th4));
        mat.setM(3, 1, sin(th2) * cos(th3) * cos(th4) + cos(th2) * sin(th3) * cos(th4));

        mat.setM(1, 2, -cos(th1) * cos(th2) * cos(th3) * sin(th4) + cos(th1) * sin(th2) * sin(th3) * sin(th4) + sin(th1) * cos(th4));
        mat.setM(2, 2, -sin(th1) * cos(th2) * cos(th3) * sin(th4) + sin(th1) * sin(th2) * sin(th3) * sin(th4) - cos(th1) * cos(th4));
        mat.setM(3, 2, -sin(th2) * cos(th3) * sin(th4) - cos(th2) * sin(th3) * sin(th4));

        mat.setM(1, 3, cos(th1) * cos(th2) * sin(th3) + cos(th1) * sin(th2) * cos(th3));
        mat.setM(2, 3, sin(th1) * cos(th2) * sin(th3) + sin(th1) * sin(th2) * cos(th3));
        mat.setM(3, 3, sin(th2) * sin(th3) - cos(th2) * cos(th3));

        mat.setM(1, 4, a2 * cos(th1) + a3 * cos(th1) * cos(th2) + a4 * cos(th1) * cos(th2) * cos(th3) - a4 * cos(th1) * sin(th2) * sin(th3) + d4 * cos(th1) * cos(th2) * sin(th3) + d4 * cos(th1) * sin(th2) * cos(th3));
        mat.setM(2, 4, a2 * sin(th1) + a3 * sin(th1) * cos(th2) + a4 * sin(th1) * cos(th2) * cos(th3) - a4 * sin(th1) * sin(th2) * sin(th3) + d4 * sin(th1) * cos(th2) * sin(th3) + d4 * sin(th1) * sin(th2) * cos(th3));
        mat.setM(3, 4, a3 * sin(th2) + a4 * sin(th2) * cos(th3) + a4 * cos(th2) * sin(th3) + d4 * sin(th2) * sin(th3) - d4 * cos(th2) * cos(th3));

        //check
        /*
        Matrix4_4 rx4 = new Matrix4_4();
        Matrix4_4 rz4 = new Matrix4_4();
        rx4.Rx(alp4, a4);
        rz4.Rz(th4, d4);
        Matrix4_4 checkM = T03().x(rx4).x(rz4);

        mat.show("T04 R:");
        checkM.show("T04 C:");
        mat.invert().show("invert");
        mat.invert().x(mat).show("I :");
        */

        return mat;
    }

    public Matrix4_4 T05()
    {
        //check
        Matrix4_4 rx5 = new Matrix4_4();
        Matrix4_4 rz5 = new Matrix4_4();
        rx5.Rx(alp5, a5);
        rz5.Rz(th5, d5);
        Matrix4_4 mat = T04().x(rx5).x(rz5);
        //mat.show();
        return mat;
    }

    public Matrix4_4 T06()
    {
        //check
        Matrix4_4 rx6 = new Matrix4_4();
        Matrix4_4 rz6 = new Matrix4_4();
        rx6.Rx(alp6, a6);
        rz6.Rz(th6, d6);
        Matrix4_4 mat = T05().x(rx6).x(rz6);
        return mat;
    }

    public void setUniversalCoordinate()
    {
        Matrix4_4 m = T06();
        realX = m.matrix[0, 3] + 199;
        realY = m.matrix[1, 3];
        realZ = m.matrix[2, 3];

        uniX = realX;
        uniY = realZ;
        uniZ = realY;
    }

    //三角函數
    private float cos(float angle)
    {
        return (float)Math.Cos(angle * Math.PI / 180);
    }
    private float sin(float angle)
    {
        return (float)Math.Sin(angle * Math.PI / 180);
    }
}
