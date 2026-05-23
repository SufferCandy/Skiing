using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioClip hitSound;
    private AudioSource source;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        source = GetComponent<AudioSource>();
    }

    private void OnEnable()
    {
        Obstacle.OnObstacleHit += PlayHitSound;
    }

    private void OnDisable()
    {
        Obstacle.OnObstacleHit -= PlayHitSound;
    }

    private void PlayHitSound()
    {
        source.PlayOneShot(hitSound);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
