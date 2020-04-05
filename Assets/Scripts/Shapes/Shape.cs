using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shape : IShape
{
    protected List<Vector3> points;

    public Shape()
    {
        points = new List<Vector3>();
    }

    public void AddPoint(Vector3 point)
    {
        points.Add(point);
    }

    public List<Vector3> GetPositions()
    {
        return points;
    }

    public virtual bool IsShape()
    {
        return true;
    }

    public void SetPositions(List<Vector3> positions)
    {
        points = positions;
    }

}
