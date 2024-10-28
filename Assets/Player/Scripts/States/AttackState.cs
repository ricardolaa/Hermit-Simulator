using System.Collections;
using UnityEngine;

public class AttackState : State
{
    [SerializeField] private AnimationClip _clip;
    [SerializeField] private GameObject _sword;

    private Animator _animator;

    private void Start()
    {
        _animator = AttachedEntity.GetComponent<Animator>();    
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
