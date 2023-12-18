using UnityEngine;

namespace GameScripts
{
    public class Ball : MonoBehaviour
    {
        private Rigidbody rb;
        private Vector3 initialPosition;

        public GameObject soldier;
        private bool hit;
    
        void Start()
        {
            rb = GetComponent<Rigidbody>();
            initialPosition = transform.position;
            hit = false;
        }
    
        void Update()
        {
            if (transform.position.y <= -5)
            {
                Destroy(gameObject);
            }
        }
    
        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.CompareTag("Soldier"))
            {
                GameManager.Instance.ballHitIncrease();
                hit = true;
                
                soldier = collision.gameObject;
                soldier.GetComponent<Soldier>().removeHealth(20);
            }

            if (!hit)
            {
                GameManager.Instance.ballMissIncrease();
            }

            if (collision.gameObject.CompareTag("Ground"))
            {
                Destroy(gameObject);
            }
        }

    }
}
