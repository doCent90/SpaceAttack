using UnityEngine;

public class CelebrationTransition : TransitionPlayer
{
    private PlayerMover _playerMover;

    private void OnEnable()
    {
        if (_playerMover.IsLastWayPoint)
            NeedTransit = true;
    }

    private void Start()
    {
        _playerMover = GetComponent<PlayerMover>();        
    }
}
