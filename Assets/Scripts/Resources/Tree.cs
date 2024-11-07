using UnityEngine;

namespace Trees
{
    public class Tree : MonoBehaviour
    {
        [SerializeField] private int _strength = 3;
        [SerializeField] private Item _treeItem;

        private int _health;

        public Item TreeItem => _treeItem;

        private void Start()
        {
            _health = _strength;
        }

        public int GetHit()
        {
            _health -= 1;

            if ( _health <= 0)
            {
                Destroy(gameObject);
                return _strength;
            }

            return 0;
        }
        

    }
}