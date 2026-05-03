using UnityEngine;

public class GroundDetector : MonoBehaviour
{
    public Transform landingZone;
    public float landingZoneRadius = 1f;

    void OnCollisionEnter(Collision other)
    {
        if (!other.gameObject.CompareTag("Ball")) return;
        if (!BallLauncher.ballThrown) return;

        Vector3 ballPos = other.transform.position;
        Vector3 zonePos = landingZone.position;

        float distX = Mathf.Abs(ballPos.x - zonePos.x);
        float distZ = Mathf.Abs(ballPos.z - zonePos.z);

        if (distX <= landingZoneRadius && distZ <= landingZoneRadius)
            GameManager.Instance.BallLandedInZone();
        else
            GameManager.Instance.BallLandedOutside();
    }
}