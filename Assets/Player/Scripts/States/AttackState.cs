using System.Collections;
using UnityEngine;

public class AttackState : PlayerBaseState
{
    [SerializeField] private AnimationClip _clip;
    [SerializeField] private GameObject _sword;

    private void Start()
    {
        base.InitializeComponents();
    }

    public override void OnEnter()
    {
        _sword.SetActive(true);
        _animator.SetBool("Attack", true);
        StartCoroutine(WaitForAnimationToComplete());
    }

    public override void OnExit()
    {
        _sword.SetActive(false);
    }

    private IEnumerator WaitForAnimationToComplete()
    {
        float timer = _clip.length;
        while (timer > 0)
        {
            yield return null;
            timer -= Time.deltaTime;
        }

        _animator.SetBool("Attack", false);
        TransitionTo(nameof(PlayerIdle));
    }

}
