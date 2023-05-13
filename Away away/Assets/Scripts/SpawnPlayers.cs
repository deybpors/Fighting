using UnityEngine;
public class SpawnPlayers : MonoBehaviour
{
    [SerializeField] private FollowTarget followTarget;
    [SerializeField] private GameObject playerPrefab;

    private void Start()
    {
        var player = Instantiate(playerPrefab, transform.position, Quaternion.identity);
        followTarget.target = player.transform;
    }
}
