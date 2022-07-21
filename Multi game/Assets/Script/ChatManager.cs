using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class ChatManager : MonoBehaviourPunCallbacks
{
    public InputField input;
    public GameObject ChatPrefab;
    public Transform ChatContent;

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (input.text.Length == 0) return;

            string chat = PhotonNetwork.NickName + " : " + input.text;

            photonView.RPC("RpcAddChat",RpcTarget.All,chat);
        }
    }
    [PunRPC]

    public void RpcAddChat(string msg)
    {
        GameObject chat = Instantiate(ChatPrefab);
        chat.GetComponent<Text>().text = msg;

        chat.transform.SetParent(ChatContent);

        input.ActivateInputField();

        input.text = "";
    }
}
