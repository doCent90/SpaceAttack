using UnityEngine;

public class EnemiesOnPoint : MonoBehaviour
{
    private WayPoint[] _wayPoints;

    public int CountWayPoint { get; private set; }

    private void OnEnable()
    {
        _wayPoints = GetComponentsInChildren<WayPoint>();

        for (int i = 0; i < _wayPoints.Length; i++)
        {
            _wayPoints[i].SetNumberPoint(i + 1);
        }

        CountWayPoint = _wayPoints.Length - 1;
    }
}
