using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Hazard") || other.CompareTag("DeathZone"))
        {
            AudioManager.Instance.PlayDamage();
            GameManager.Instance.DamagePlayer();
        }
    }
}