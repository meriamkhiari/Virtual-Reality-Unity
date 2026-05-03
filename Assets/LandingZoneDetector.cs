using UnityEngine;

public class LandingZoneDetector : MonoBehaviour
{
    private bool detected = false;

    void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Ball") || detected) return;
        if (!BallLauncher.ballThrown) return;
        detected = true;

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