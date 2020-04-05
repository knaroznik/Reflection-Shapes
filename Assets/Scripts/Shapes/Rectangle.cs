using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rectangle : Square
{
    public override bool IsShape()
    {
        if (points.Count != 4) return false;

        if (IsSquare())
        {
            return false;
        }
        else
        {
            if (IsRectangle())
            {
                return true;
            }
        }
        return false;
    }

    protected bool IsRectangle()
    {
        Vector2 A = points[0];
        Vector2 B = points[1];
        Vector2 C = points[2];
        Vector2 D = points[3];
        if (SameDistancesFromCenter(A, B, C, D))
        {
            return true;
        }
        return false;
    }
}
