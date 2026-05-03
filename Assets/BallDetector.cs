using UnityEngine;

public class BallDetector : MonoBehaviour
{
    private bool detected = false;

    void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Ball") || detected) return;
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

    void OnCollisionEnter(Collision other)
    {
        if (!other.gameObject.CompareTag("Ball") || detected) return;
        detected = true;

        Rigidbody rb = other.gameObject.GetComponent<Rigidbody>();
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