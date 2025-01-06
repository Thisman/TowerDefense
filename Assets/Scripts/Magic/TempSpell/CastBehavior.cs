using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Magic
{
    public class CastBehavior : MonoBehaviour
    {
        public void Start()
        {
            StartCoroutine(CastSpell());
        }

        public void OnDestroy()
        {
            StopAllCoroutines();
        }

        private IEnumerator CastSpell()
        {
            yield return new WaitForSeconds(3);
            GameObject.Destroy(gameObject);
        }
    }
}
