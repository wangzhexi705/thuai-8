using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Position 
{
    public double X;
    public double Y;
    public double Z;
    public double Angle;

    public Position(double x, double z, double angle)
    {
        this.X = x + Constants.POS_BIAS;
        this.Y = Constants.YPOS;
        this.Z = z + Constants.POS_BIAS;
        this.Angle = angle;
    }
    public override bool Equals(object obj)
    {
        if (obj is Position other)
        {
            // �Ƚ� X, Y �� Angle �Ƿ����
            return X.Equals(other.X) && Z.Equals(other.Z) && Angle.Equals(other.Angle);
        }
        return false;
    }

    public override int GetHashCode()
    {
        // ����һ������ X, Y �� Angle �Ĺ�ϣ��
        int hashCode = 17; // һ������ĳ���
        hashCode = hashCode * 23 + X.GetHashCode();
        hashCode = hashCode * 23 + Y.GetHashCode();
        hashCode = hashCode * 23 + Angle.GetHashCode();
        return hashCode;
    }
}
