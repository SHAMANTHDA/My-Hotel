using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;  // Reference to the player's transform
    public Vector3 offset = new Vector3(10f, 10f, -10f);  // Offset distance between the camera and the player
    public float smoothSpeed = 0.5f;  // Speed at which the camera catches up to the player

    private void Start()
    {
        if (player == null)
        {
            // Automatically try to find the player by tag if not set
            GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
            if (playerObj != null)
            {
                player = playerObj.transform;
            }
            else
            {
                Debug.LogError("Player object not found. Please assign the Player Transform manually or tag the player GameObject correctly.");
            }
        }
    }

    void LateUpdate()
    {
        if (player != null)
        {
            Vector3 desiredPosition = player.position + offset;
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
            transform.position = smoothedPosition;

            // Adjust the camera to look at the player
            transform.LookAt(player.position);
        }
    }
}
