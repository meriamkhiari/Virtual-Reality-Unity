using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class BallLauncher : MonoBehaviour
{
    public GameObject ballPrefab;
    private GameObject currentBall;
    public static bool ballThrown = false;

    public void SpawnBall()
    {
        if (currentBall != null) Destroy(currentBall);

        float randomX = Random.Range(-3f, 3f);
        float randomZ = Random.Range(0f, 4f);
        Vector3 spawnPos = new Vector3(randomX, 1f, randomZ);

        ballThrown = false;
        currentBall = Instantiate(ballPrefab, spawnPos, Quaternion.identity);

        XRGrabInteractable grab = currentBall.GetComponent<XRGrabInteractable>();
        if (grab != null)
            grab.selectExited.AddListener(OnBallReleased);
    }

    void OnBallReleased(SelectExitEventArgs args)
    {
        ballThrown = true;
    }
}