using UnityEngine;

namespace Trees
{
    public class Tree : MonoBehaviour
    {
        [SerializeField] private int _strength = 3;

        public int GetHit()
        {
            print(1);
            _strength -= 1;

            if ( _strength <= 0)
            {
                Destroy(gameObject);
                return _strength;
            }

            return 0;
        }
        

    }
}