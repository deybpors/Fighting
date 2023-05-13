using Photon.Pun;
using TMPro;

public class CreateJoinRooms : MonoBehaviourPunCallbacks
{
    public TMP_Dropdown actionDropdown;
    public TMP_InputField roomCode;

    public void CreateJoinRoom()
    {
        if (roomCode.text == string.Empty) return;

        if(actionDropdown.captionText.text == "Create")
        {
            PhotonNetwork.CreateRoom(roomCode.text);
        }
        else
        {
            PhotonNetwork.JoinRoom(roomCode.text);
        }
    }

    public override void OnJoinedRoom()
    {
        PhotonNetwork.LoadLevel("Game");
    }
}
