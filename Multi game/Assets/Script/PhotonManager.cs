using Photon.Pun;
using UnityEngine.UI;
using Photon.Realtime;

public class PhotonManager : MonoBehaviourPunCallbacks
{
    public Button connect;
    public Text currentregion;
    public Text currentLobby;

    public void Connect()
    {
        PhotonNetwork.ConnectUsingSettings();
    }

    public void Update()
    {
        currentregion.text = PhotonNetwork.PhotonServerSettings.AppSettings.FixedRegion;

        switch (Data.count)
        {
            case 0:
                currentLobby.text = "First Lobby";
                break;
            case 1:
                currentLobby.text = "Second Lobby";
                break;
            case 2:
                currentLobby.text = "Three Lobby";
                break;
        }
    }

    public override void OnConnectedToMaster()
    {
        switch (Data.count)
        {
            case 0:
                PhotonNetwork.JoinLobby(new TypedLobby("Lobby 1", LobbyType.Default));
                break;
            case 1:
                PhotonNetwork.JoinLobby(new TypedLobby("Lobby 2", LobbyType.Default));
                break;
            case 2:
                PhotonNetwork.JoinLobby(new TypedLobby("Lobby 3", LobbyType.Default));
                break;
        }
    }

    public override void OnJoinedLobby()
    {
        PhotonNetwork.LoadLevel("Photon Room");
    }
}
