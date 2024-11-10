using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Trees;

public class GatheringWoodState : PlayerBaseState
{
    [SerializeField] private AnimationClip _clip;
    [SerializeField] private GameObject _axe;
    [SerializeField] private float _raycastDistance = 2f; 
    [SerializeField] private LayerMask _treeLayer;

    private Inventory _inventory;

    private void Start()
    {
        base.InitializeComponents();
        _inventory = AttachedEntity.GetComponent<Inventory>();
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

            RaycastHit hit;
            var position = AttachedEntity.transform.position;
            Vector3 origin = position + new Vector3(0, _characterController.height / 2, 0);
            Vector3 direction = AttachedEntity.transform.forward;

            Trees.Tree tree = null;

            Debug.DrawRay(origin, direction * _raycastDistance, Color.red);

            if (Physics.Raycast(origin, direction, out hit, _raycastDistance, _treeLayer))
                tree = hit.collider.GetComponent<Trees.Tree>() ?? hit.collider.GetComponentInParent<Trees.Tree>();

            yield return new WaitForSeconds(_clip.length / 2);

            int quantityTree = 0;

            if (tree != null)
                quantityTree = tree.GetHit();

            if (quantityTree > 0)
                _inventory.AddItem(tree.TreeItem, quantityTree);

            yield return new WaitForSeconds(_clip.length / 2);

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

