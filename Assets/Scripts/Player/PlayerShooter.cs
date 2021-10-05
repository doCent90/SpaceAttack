using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(StateMachinePlayer))]
public class PlayerShooter : MonoBehaviour
{
    [SerializeField] private PlayerBullet _bulletTamplate;
    [SerializeField] private Transform _shootPosition;

    public event UnityAction ShotFired;

    public void Shoot()
    {
        var bullet = Instantiate(_bulletTamplate, _shootPosition.position, Quaternion.Euler(_shootPosition.eulerAngles));

        ShotFired?.Invoke();
    }
}
