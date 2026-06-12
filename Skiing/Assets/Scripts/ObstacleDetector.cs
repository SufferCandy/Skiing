using UnityEngine;

public class ObstacleDetector : MonoBehaviour
{
    void OnCollisionEnter(Collision collision)
    {
        
        if (collision.gameObject.CompareTag("Player") ||
            collision.gameObject.GetComponentInParent<PlayerControl>() != null)
        {
            GameManager.Instance?.TriggerObstacleHit();
        }
    }

    void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("Player") || other.GetComponentInParent<PlayerControl>() != null)
        {
            GameManager.Instance?.TriggerObstacleHit();
        }
    }
}