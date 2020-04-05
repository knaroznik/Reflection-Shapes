using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Square : Shape
{
    public override bool IsShape()
    {
        if (points.Count != 4) return false;

        if (IsSquare())
        {
            return true;
        }

        return false;
    }

    protected bool IsSquare()
    {
        Vector2 A = points[0];
        Vector2 B = points[1];
        Vector2 C = points[2];
        Vector2 D = points[3];

        if (SameDistancesFromCenter(A, B, C, D) && SameEdges(A,B,C,D))
        {
            return true;
        }
        return false;
    }

    protected bool SameDistancesFromCenter(Vector2 A, Vector2 B, Vector2 C, Vector2 D)
    {
        float cx, cy;

        cx = (A.x + B.x + C.x + D.x) / 4;
        cy = (A.y + B.y + C.y + D.y) / 4;

        Vector2 center = new Vector2(cx, cy);

        float dd1 = Vector2.Distance(A, center);
        float dd2 = Vector2.Distance(B, center);
        float dd3 = Vector2.Distance(C, center);
        float dd4 = Vector2.Distance(D, center);
        return dd1 == dd2 && dd1 == dd3 && dd1 == dd4;
    }

    protected bool SameEdges(Vector2 A, Vector2 B, Vector2 C, Vector2 D)
    {
        float dist1 = Vector2.Distance(A, B);
        float dist2 = Vector2.Distance(B, C);
        float dist3 = Vector2.Distance(C, D);
        float dist4 = Vector2.Distance(D, A);
        return dist1 == dist2 && dist3 == dist4 && dist2 == dist3 && dist4 == dist1;
    }
}
