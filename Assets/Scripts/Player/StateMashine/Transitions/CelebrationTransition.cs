using UnityEngine;

public class CelebrationTransition : TransitionPlayer
{
    private PlayerMover _playerMover;

    private void OnEnable()
    {
        _playerMover = GetComponent<PlayerMover>();

        if (_playerMover.IsLastWayPoint)
            NeedTransit = true;
    }
}
