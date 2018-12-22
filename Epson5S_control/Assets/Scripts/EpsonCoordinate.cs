using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;


public class EpsonCoordinate{
    public float th1, th2, th3, th4, th5, th6;
    public float[] newTh = new float[6] { 0, 0, 0, 0, 0, 0 };
    public float[] preTh = new float[6] { 0, 0, 0, 0, 0, 0 };
    public float uniX, uniY, uniZ;
    public float realX, realY, realZ;
    public float c4X, c4Y, c4Z;
    private readonly float minth1 = -170, minth2 = -150, minth3 = -70, minth4 = -190, minth5 = -135, minth6 = -360; //theta range
    private readonly float maxth1 = 170, maxth2 = 65, maxth3 = 190, maxth4 = 190, maxth5 = 135, maxth6 = 360;
    private readonly float pi = (float)Math.PI;
    //D&H method parameter
    private readonly float a1 = 0, a2 = 100, a3 = 310, a4 = 40, a5 = 0, a6 = 0;
    private readonly float alp1 = 0, alp2 = 90, alp3 = 0, alp4 = 90, alp5 = -90, alp6 = 90;
    private readonly float d1 = 199, d2 = 0, d3 = 0, d4 = 305, d5 = 0, d6 = 80;
    private Matrix4_4 preM;
    public EpsonCoordinate()
    {
        setAngle();
        uniX = 0;
        uniY = 0;
        uniZ = 0;
        realX = 0;
        realY = 0;
        realZ = 0;
        c4X = 0;
        c4Y = 0;
        c4Z = 0;
        th1 = 0;
        th2 = 0;
        th3 = 0;
        th4 = 0;
        th5 = 0;
        th6 = 0;
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

    public Matrix4_4 T36()
    {
        float[,] m = new float[4, 4]{
            { cos(th4) * cos(th5) * cos(th6) - sin(th4) * sin(th6), -cos(th4) * cos(th5) * sin(th6) - sin(th4) * cos(th5), cos(th4) * sin(th5), a4 + d6 * cos(th4) * sin(th5)},
            { sin(th5) * cos(th6), -sin(th5) * sin(th6), -cos(th5), -d4 - d6 * cos(th5)},
            { sin(th4) * cos(th5) * cos(th6) + cos(th4) * sin(th6), -sin(th4) * cos(th5) * sin(th6) + cos(th4) * cos(th6), sin(th4) * sin(th5), d6 * sin(th4) * sin(th5)},
            { 0, 0, 0, 1}
        };
        Matrix4_4 mat = new Matrix4_4();
        mat.setM(m);

        return mat;
    }

    public bool setUniversalCoordinate()
    {
        Matrix4_4 m = preM;

        realX = m.matrix[0, 3];
        realY = m.matrix[1, 3];
        realZ = m.matrix[2, 3];

        //座標6，z軸向量
        float[] z6 = new float[3] { m.matrix[0, 2], m.matrix[1, 2], m.matrix[2, 2] };
        //座標4的位置
        float[] coor4 = new float[3] { realX - d6 * z6[0], realY - d6 * z6[1], realZ - d6 * z6[2] };


        //1軸角度推導
        th1 = (float)Math.Atan2(coor4[1], coor4[0]) * 180 / pi;
        if((cos(th1) * cos(preTh[0]) + sin(th1) * sin(preTh[0])) > 0)
            newTh[0] = th1;
        else
        {
            th1 = (float)Math.Atan2(-coor4[1], -coor4[0]) * 180 / pi;
            newTh[0] = th1;
        }

        //2、3軸角度推導\
        float c1;
        if ((newTh[0] == 0) || (newTh[0] == 180))
            c1 = coor4[0] / cos(newTh[0]) - a2;
        else
            c1 = coor4[1] / sin(newTh[0]) - a2;

        float l23 = (float)Math.Sqrt(c1 * c1 + coor4[2] * coor4[2]);
        float r4 = (float)Math.Sqrt(a4 * a4 + d4 * d4);

        //檢查是否有解
        float cosThPi2 = (l23 * l23 + a3 * a3 - r4 * r4) / (2 * a3 * l23);
        if ((cosThPi2 > 1) || (cosThPi2 < -1))
            return false;

        float gapR4 = (float)Math.Atan2(a4, d4);
        //th2 th3 可能的角度
        float theta21 = ((float)Math.Acos(cosThPi2) + (float)Math.Atan2(coor4[2], c1)) * 180 / pi;
        float theta22 = (-(float)Math.Acos(cosThPi2) + (float)Math.Atan2(coor4[2], c1)) * 180 / pi;
        float phi31 = (float)Math.Atan2((coor4[2] - a3 * sin(180 - theta21)) / r4, (-c1 - a3 * cos(180 - theta21)) / r4);
        float phi32 = (float)Math.Atan2((coor4[2] - a3 * sin(180 - theta22)) / r4, (-c1 - a3 * cos(180 - theta22)) / r4);
        float theta31 = 270 - (phi31 + gapR4) * 180 / pi - theta21;
        float theta32 = 270 - (phi32 + gapR4) * 180 / pi - theta22;

        //選擇角度
        if((cos(theta21)*cos(preTh[1]) + sin(theta21)*sin(preTh[1])) > (cos(theta22) * cos(preTh[1]) + sin(theta22) * sin(preTh[1])))
        {
            //Debug.Log(theta32);
            newTh[1] = theta21;
            th2 = theta21;
            newTh[2] = theta31;
            th3 = theta31;
        }
        else
        {
            newTh[1] = theta22;
            th2 = theta22;
            newTh[2] = theta32;
            th3 = theta32;
        }


        //th2 th3 角度分類
        float th23case1 = a3 + a4 * cos(theta31) + d4 * sin(theta31) - c1 * cos(theta21) - coor4[2] * sin(theta21);
        float th23case2 = a3 + a4 * cos(theta32) + d4 * sin(theta32) - c1 * cos(theta22) - coor4[2] * sin(theta22);

        //4、5、6軸
        Matrix4_4 t03 = T03();
        Matrix4_4 t36 = t03.invert().x(m);
        Matrix4_4 t36c = T36();

        if(t36.matrix[1,2] == -1)
        {
            float checkValue = t36.matrix[0, 0] - t36.matrix[2, 1];
            if (!((checkValue < 0.0001) && (checkValue > -0.0001)))
            {
                Debug.Log("break");
                return false;

            }
            //t36.show("t03: ");  
            newTh[4] = 0;
            newTh[3] = 0;
            newTh[5] = (float)Math.Atan2(t36.matrix[2, 0], t36.matrix[0, 0]) * 180 / pi;
        }
        else if(t36.matrix[1, 2] == 1)
        {
            float checkValue = t36.matrix[0, 0] + t36.matrix[2, 1];
            if (!((checkValue < 0.0001) && (checkValue > -0.0001)))
            {
                Debug.Log("break");
                return false;
            }
            //t36.show("t03: ");
            newTh[4] = 180;
            newTh[3] = 0;
            newTh[5] = (float)Math.Atan2(t36.matrix[2, 0], -t36.matrix[0, 0]) * 180 / pi;
        }
        else
        {
            //t36.show("fake: ");
            
            newTh[5] = (float)Math.Atan2(-t36.matrix[1, 1], t36.matrix[1, 0]) * 180 / pi;
            newTh[3] = (float)Math.Atan2(t36.matrix[2, 2], t36.matrix[0, 2]) * 180 / pi;
            if ((newTh[3] == 0) || (newTh[3] == 180))
                newTh[4] = (float)Math.Atan2(t36.matrix[0, 2] / cos(newTh[3]), -t36.matrix[1, 2]) * 180 / pi;
            else
                newTh[4] = (float)Math.Atan2(t36.matrix[2, 2] / sin(newTh[3]), -t36.matrix[1, 2]) * 180 / pi;
        }

        uniX = realX;
        uniY = realZ;
        uniZ = realY;

        th4 = newTh[3];
        th5 = newTh[4];
        th6 = newTh[5];
        //preM.show("preState");
        //T06().show("nowState");
        return true;
    }

    //世界座標直線移動
    public bool moveUniX(float [] angle, float step)
    {
        setPreT06(angle);
        preM.matrix[0, 3] += step;
        return setUniversalCoordinate();
    }

    public bool moveUniY(float [] angle, float step)
    {
        setPreT06(angle);
        preM.matrix[1, 3] += step;
        return setUniversalCoordinate();
    }

    public bool moveUniZ(float [] angle, float step)
    {
        setPreT06(angle);
        preM.matrix[2, 3] += step;
        return setUniversalCoordinate();
    }

    //世界座標旋轉
    public bool rotateUniX(float [] angle, float rotateAngle)
    {
        setPreT06(angle);
        Matrix4_4 flag;
        Matrix4_4 rx = new Matrix4_4();
        rx.Rx(rotateAngle);
        flag = rx.x(preM);
        preM.changeRotateMatrix(flag);

        return setUniversalCoordinate();
    }

    public bool rotateUniY(float[] angle, float rotateAngle)
    {
        setPreT06(angle);
        Matrix4_4 flag;
        Matrix4_4 ry = new Matrix4_4();
        ry.Ry(rotateAngle);
        flag = ry.x(preM);
        preM.changeRotateMatrix(flag);

        return setUniversalCoordinate();
    }

    public bool rotateUniZ(float[] angle, float rotateAngle)
    {
        setPreT06(angle);
        Matrix4_4 flag;
        Matrix4_4 rz = new Matrix4_4();
        rz.Rz(rotateAngle);
        flag = rz.x(preM);
        preM.changeRotateMatrix(flag);

        return setUniversalCoordinate();
    }

    //得到previous state的T06轉移矩陣
    private void setPreT06(float [] angle)
    {
        th1 = angle[0];
        th2 = angle[1] + 90;
        th3 = angle[2];
        th4 = angle[3];
        th5 = angle[4];
        th6 = angle[5];

        preM = T06();

        for (int i = 0; i < 6; i++)
            preTh[i] = angle[i];
        preTh[1] += 90;
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
