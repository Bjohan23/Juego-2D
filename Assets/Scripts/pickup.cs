using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pickup : MonoBehaviour
{
    public enum pickupType { coin, gem, health, life }

    public pickupType pt;
    [SerializeField] GameObject PickupEffect;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            switch (pt)
            {
                case pickupType.coin:
                    GameManager.instance.IncrementCoinCount();
                    Debug.Log("Coin checkpoint saved!");
                    break;

                case pickupType.gem:
                    GameManager.instance.IncrementGemCount();
                    break;

                case pickupType.health:
                    if (HealthManager.instance != null && !HealthManager.instance.IsAtFullHealth())
                    {
                        HealthManager.instance.HealPlayer();
                    }
                    else
                    {
                        // Don't destroy if player is at full health
                        return;
                    }
                    break;

                case pickupType.life:
                    GameManager.instance.AddLife();
                    break;
            }

            // Create pickup effect
            if (PickupEffect != null)
            {
                Instantiate(PickupEffect, transform.position, Quaternion.identity);
            }

            // Destroy the pickup
            Destroy(this.gameObject, 0.2f);
        }
    }
}