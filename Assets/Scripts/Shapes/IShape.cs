using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IShape
{
    void AddPoint(Vector3 point);
    bool IsShape();

    void SetPositions(List<Vector3> positions);
    List<Vector3> GetPositions();

}
