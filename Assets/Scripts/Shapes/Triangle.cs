using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Triangle : Shape
{
    public override bool IsShape()
    {
        if (points.Count == 3) return true;
        return false;
    }
}
