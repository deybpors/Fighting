using UnityEngine;
using Photon.Pun;

public class SpawnPlayers : MonoBehaviour
{
    [SerializeField] private FollowTarget followTarget;
    [SerializeField] private GameObject playerPrefab;

    private void Start()
    {
        var player = PhotonNetwork.Instantiate(playerPrefab.name, transform.position, Quaternion.identity);
        followTarget.target = player.transform;
    }
}
