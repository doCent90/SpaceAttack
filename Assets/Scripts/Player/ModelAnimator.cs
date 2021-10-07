using UnityEngine;

public class ModelAnimator : MonoBehaviour
{
    [SerializeField] private Animator _animator;

    private Player _player;
    private PlayerMover _mover;
    private AttackState _attack;

    private const string RunAnimation = "Run";
    private const string DieAnimation = "Die";
    private const string ShootAnimation = "Shoot";
    private const string TargetAnimation = "LockTarget";

    private void OnEnable()
    {
        _player = GetComponent<Player>();
        _mover = GetComponent<PlayerMover>();
        _attack = GetComponent<AttackState>();

        _player.Died += Die;
        _mover.Moved += Move;
        _attack.Shoted += Shot;
        _attack.Attacked += LockTarget;
    }

    private void Move(bool isMoving)
    {
        _animator.SetBool(RunAnimation, isMoving);
    }

    private void LockTarget(bool isLockedTarget)
    {
        _animator.SetBool(TargetAnimation, isLockedTarget);
    }

    private void Shot()
    {
        _animator.SetTrigger(ShootAnimation);
    }

    private void Die()
    {
        _animator.SetTrigger(DieAnimation);
    }
}
