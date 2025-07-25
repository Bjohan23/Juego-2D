using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    [Header("Checkpoint Settings")]
    public bool isActivated = false;
    
    [Header("Visual Elements")]
    public GameObject activeVisual;
    public GameObject inactiveVisual;
    public ParticleSystem activationEffect;
    
    private void Start()
    {
        UpdateVisuals();
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !isActivated)
        {
            ActivateCheckpoint();
        }
    }
    
    private void ActivateCheckpoint()
    {
        isActivated = true;
        
        // Set this as the current checkpoint
        CheckpointManager.instance.SetCheckpoint(transform.position);
        
        // Update visuals
        UpdateVisuals();
        
        // Play activation effect
        if (activationEffect != null)
        {
            activationEffect.Play();
        }
        
        Debug.Log("Checkpoint activated!");
    }
    
    private void UpdateVisuals()
    {
        if (activeVisual != null)
            activeVisual.SetActive(isActivated);
            
        if (inactiveVisual != null)
            inactiveVisual.SetActive(!isActivated);
    }
}