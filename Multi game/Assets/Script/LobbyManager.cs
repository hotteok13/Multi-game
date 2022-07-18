using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class LobbyManager : MonoBehaviourPunCallbacks
{
    public InputField RoomName, RoomPerson;
    public Button RoomCreate, RoomJoin;

    public GameObject RoomPrefab;
    public Transform RoomContect;

    Dictionary<string,RoomInfo> RoomCatalog = new Dictionary<string,RoomInfo>();

    private void Update()
    {
        if (RoomName.text.Length > 0)
        {
            RoomJoin.interactable = true;
        }
        else
        {
            RoomJoin.interactable = false;
        }

        if(RoomName.text.Length > 0&& RoomPerson.text.Length > 0)
        {
            RoomCreate.interactable=true;
        }
        else
        {
            RoomCreate.interactable=false;
        }
    }

    public void OnClickCreateRoom()
    {
        RoomOptions Room = new RoomOptions();

        Room.MaxPlayers = byte.Parse(RoomName.text);

        Room.IsOpen = true;

        Room.IsVisible = true;

        PhotonNetwork.CreateRoom(RoomName.text, Room);
    }

    public void OnClickJoinRoom()
    {
        PhotonNetwork.JoinRoom(RoomName.text);
    }

    public override void OnCreatedRoom()
    {
        Debug.Log("Create Room");
    }

    public void AllDeleteRoom()
    {
        foreach (Transform trans in RoomContect)
        {
            Destroy(trans.gameObject);
        }
    }

    public override void OnJoinedRoom()
    {
        PhotonNetwork.LoadLevel("Photon Game");
    }

    public void CreateRoomObjcet()
    {
        foreach(RoomInfo info in RoomCatalog.Values)
        {
            GameObject room = Instantiate(RoomPrefab);

            room.transform.SetParent(RoomContect);

            room.GetComponent<infomation>().SetInfo(info.Name, info.PlayerCount, info.MaxPlayers);
        }
    }
}
