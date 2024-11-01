using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GatheringWoodState : PlayerBaseState
{
    [SerializeField] private AnimationClip _clip;
    [SerializeField] private GameObject _axe;

    private void Start()
    {
        base.InitializeComponents();
    }

    public override void OnEnter()
    {
        _axe.SetActive(true);
        _animator.SetBool("Axe", true);
        StartCoroutine(GatheringCoroutine());
    }

    public override void OnExit()
    {
        _axe.SetActive(false);
    }

    private IEnumerator GatheringCoroutine()
    {
        while (true)
        {
            _animator.SetBool("Axe", true);

            yield return new WaitForSeconds(_clip.length);

            if (Input.GetMouseButton(1))
            {
                continue;
            }
            else
            {
                break;
            }
        }

        _animator.SetBool("Axe", false);
        TransitionTo(nameof(PlayerIdle));
    }
}
