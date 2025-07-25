using UnityEngine;

public class CheckpointManager : MonoBehaviour
{
    public static CheckpointManager instance;
    
    [Header("Checkpoint Settings")]
    public Vector3 currentCheckpoint;
    public bool hasCheckpoint = false;
    
    [Header("Visual Feedback")]
    public ParticleSystem checkpointEffect;
    public AudioSource checkpointSound;
    
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    
    private void Start()
    {
        // Set initial checkpoint to player's starting position
        if (GameManager.instance.playerController != null)
        {
            currentCheckpoint = GameManager.instance.playerController.transform.position;
            hasCheckpoint = true;
        }
    }
    
    public void SetCheckpoint(Vector3 newCheckpoint)
    {
        currentCheckpoint = newCheckpoint;
        hasCheckpoint = true;
        
        // Visual feedback
        if (checkpointEffect != null)
        {
            checkpointEffect.transform.position = newCheckpoint;
            checkpointEffect.Play();
        }
        
        // Audio feedback
        if (checkpointSound != null)
        {
            checkpointSound.Play();
        }
        
        Debug.Log("Checkpoint saved at: " + newCheckpoint);
    }
    
    public Vector3 GetRespawnPosition()
    {
        return hasCheckpoint ? currentCheckpoint : Vector3.zero;
    }
}