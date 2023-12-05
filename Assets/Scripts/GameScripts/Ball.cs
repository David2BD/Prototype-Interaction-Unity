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
            if (collision.gameObject.CompareTag("Soldier"))
            {
                soldier = collision.gameObject;
                soldier.GetComponent<Soldier>().removeHealth(20);
            }

            if (collision.gameObject.CompareTag("Ground"))
            {
                Destroy(gameObject);
            }
        }

    }
}
