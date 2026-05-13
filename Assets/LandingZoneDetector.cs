using UnityEngine;

public class LandingZoneDetector : MonoBehaviour
{
    private bool detected = false;

    [Header("Audio Settings")]
    public AudioSource audioSource;
    public AudioClip successSound;

    void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Ball") || detected) return;
        if (!BallLauncher.ballThrown) return;
        detected = true;

        if (audioSource != null && successSound != null)
            audioSource.PlayOneShot(successSound);

        Rigidbody rb = other.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            rb.isKinematic = true;
        }

        GameManager.Instance.BallLandedInZone();
    }

    public void Reset() => detected = false;
}