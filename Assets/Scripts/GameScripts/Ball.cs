using UnityEngine;

namespace GameScripts
{
    public class Ball : MonoBehaviour
    {
        private Rigidbody rb;
        private Vector3 initialPosition;

        public GameObject soldier;
    
        void Start()
        {
            rb = GetComponent<Rigidbody>();
            initialPosition = transform.position;
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
            int player = soldier.GetComponent<Soldier>().getTeam();
            bool hit = false;
            
            if (collision.gameObject.CompareTag("Soldier"))
            {
                GameManager.Instance.ballHitIncrease(player);
                hit = true;
                
                soldier = collision.gameObject;
                soldier.GetComponent<Soldier>().removeHealth(20);
            }

            if (!hit)
            {
                GameManager.Instance.ballMissIncrease(player);
            }

            if (collision.gameObject.CompareTag("Ground"))
            {
                Destroy(gameObject);
            }
        }

    }
}
