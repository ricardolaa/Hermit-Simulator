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
        StartCoroutine(AttackCoroutine());
    }

    public override void OnExit()
    {
        _sword.SetActive(false);
    }

    private IEnumerator AttackCoroutine()
    {
        while (true)
        {
            _animator.SetBool("Attack", true);

            yield return new WaitForSeconds(_clip.length);

            if (Input.GetMouseButton(0))
            {
                continue;
            }
            else
            {
                break;
            }
        }

        _animator.SetBool("Attack", false);
        TransitionTo(nameof(PlayerIdle));
    }
}
