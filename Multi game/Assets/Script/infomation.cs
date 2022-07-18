using Photon.Pun;
using UnityEngine.UI;

public class infomation : MonoBehaviourPunCallbacks
{
    public Text RoomData;


    public void SetInfo(string Name, int Currecnt, int Max)
    {
        RoomData.text = Name + "(" + Currecnt + " / " + Max + ")";

    }
}
