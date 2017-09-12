using System;
using System.Collections;
using UnityEngine;

namespace Assets.Code
{
    public class ArrowController : MonoBehaviour
    {
        private Rigidbody2D _rigidbody;

        public Action OnArrowHit;

        private void Start()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
        }

        private void FixedUpdate()
        {
            if (_rigidbody.velocity != Vector2.zero)
            {
                Vector3 vel = _rigidbody.velocity;
                float angleZ = Mathf.Atan2(vel.y, vel.x) * Mathf.Rad2Deg;
                float angleY = Mathf.Atan2(vel.z, vel.x) * Mathf.Rad2Deg;
                transform.eulerAngles = new Vector3(0, -angleY, angleZ);
            }
        }

        private void OnCollisionEnter2D(Collision2D coll)
        {
            if (coll != null)
            {
                _rigidbody.angularVelocity = 0;
                _rigidbody.velocity = Vector2.zero;
                _rigidbody.isKinematic = true;

                transform.SetParent(coll.transform);

                StartCoroutine(ShowEffect());
            }
            
        }

        private IEnumerator ShowEffect()
        {
            yield return new WaitForSeconds(2f);
            if (OnArrowHit != null)
            {
                OnArrowHit.Invoke();
            }
        }
    }
}