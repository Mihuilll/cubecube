using UnityEngine;

public class DeathZoneChecker : MonoBehaviour
{
    public float minHeight = -10f;
    public Transform player;
    public TeleportWithEffect particleController;

    public AudioClip sounds; 

    private void Update()
    { 
        if (player.position.y < minHeight)
        {
            AudioManager.instance.PlayEventSound(sounds);

            HandlePlayerOutOfBounds();
        }
    }

    private void HandlePlayerOutOfBounds()
    {

        particleController.CreateExplosion(player.position);

        TeleportPlayer();

        particleController.CreateExplosion(particleController.GetCurrentCheckpointPosition());
    }

    private void TeleportPlayer()
    {
        Vector3 checkpointPosition = particleController.GetCurrentCheckpointPosition();
        player.position = checkpointPosition;
    }
}
