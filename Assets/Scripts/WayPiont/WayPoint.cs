using UnityEngine;

public class WayPoint : MonoBehaviour
{
    private int _numberOfPoint = 0;

    public int NumberPoint => _numberOfPoint;

    public void SetNumberPoint(int number)
    {
        _numberOfPoint = number;
    }
}
