using System.Collections;
using System.Collections.Generic;
using System;
//using System.Numerics.Matrix4x4;
using UnityEngine;

public class Matrix4_4{
    public float[,] matrix;
    public Matrix4_4()
    {
        matrix = new float[4, 4];
        identity();
        //matrix = new Matrix4x4(1.0, 0.0, 0.0, 0.0, 0.0, 1.0, 0.0, 0.0, 0.0, 0.0, 1.0, 0.0, 0.0, 0.0, 0.0);
    }

    //單位矩陣
    public void identity()
    {
        for(int i = 0; i < 4; i++)
        {
            for(int j = 0; j < 4; j++)
            {
                if (i == j)
                    matrix[i, j] = 1;
                else
                    matrix[i, j] = 0;
            }
        }
        //matrix[0, 0] = 0;
    }

    //設置舉陣 1 <= i <= 4; 1 <= j <= 4 
    public void setM(int row, int column, float value)
    {
        if (row < 1 || column < 1)
            return;
        if (row > 4 || column > 4)
            return;

        matrix[row - 1, column - 1] = value;
    }

    public void setM(float[,] m)
    {
        for(int i = 0; i < 4; i++)
        {
            for(int j = 0; j < 4; j++)
            {
                matrix[i, j] = m[i, j];
            }
        }
    }

    //x軸轉換 (角度, 距離)
    public void Rx(float angle, float alpha = 0)
    {
        identity();
        matrix[0, 3] = alpha;
        matrix[1, 1] = cosd(angle);
        matrix[1, 2] = -sind(angle);
        matrix[2, 1] = -matrix[1, 2];
        matrix[2, 2] = matrix[1, 1];
    }

    //z軸轉換 (角度, 距離)
    public void Rz(float angle, float dis = 0)
    {
        identity();
        matrix[0, 0] = cosd(angle);
        matrix[0, 1] = -sind(angle);
        matrix[1, 0] = -matrix[0, 1];
        matrix[1, 1] = matrix[0, 0];
        matrix[2, 3] = dis;
    }

    //相乘
    public Matrix4_4 x(Matrix4_4 matA)
    {
        Matrix4_4 output = new Matrix4_4();
        for(int i = 0; i < 4; i++)
        {
            for(int j = 0; j < 4; j++)
            {
                float sum = 0;
                for(int k = 0; k < 4; k++)
                {
                    sum += matrix[i, k] * matA.matrix[k, j];
                }
                //Debug.Log(sum);
                output.matrix[i, j] = sum;
            }
        }
        return output;
    }

    //反矩陣
    public Matrix4_4 invert()
    {
        Matrix4_4 output = new Matrix4_4();
        for(int i = 1; i <= 3; i++)
        {
            for(int j = 1; j <= 3; j++)
            {
                output.setM(i, j, matrix[j - 1, i - 1]);
            }
            output.setM(i, 4, -matrix[i - 1, 3]);
        }
        return output;
    }


    //顯示矩陣
    public void show(string strIn = "")
    {
        string str = strIn;
        for(int i = 0; i < 4; i++)
        {
            str = str + "row" + (i + 1) + ": ";
            for(int j = 0; j < 4; j++)
            {
                str += matrix[i, j] + " ";
            }
            str += "|";
        }
        Debug.Log(str);
    }

    
    //三角函數
    private float cosd(float angle)
    {
        return (float)Math.Cos(angle * Math.PI / 180);
    }
    private float sind(float angle)
    {
        return (float)Math.Sin(angle * Math.PI / 180);
    }
}
