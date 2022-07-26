using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PhotonControl : MonoBehaviourPun
{
    public float speed;
    public float angleSpeed;

    public int health = 100;
    public LayerMask layer;

    public GameObject effect;
    public Camera cam;
    RaycastHit hit;

    public Texture2D cursorImage;
    void Start()
    {
        if (photonView.IsMine)
        {
            Camera.main.gameObject.SetActive(false);
        }
        else {
            cam.enabled = false;
            GetComponentInChildren<AudioListener>().enabled = false;
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (!photonView.IsMine) return;

        if (transform.position.y < 0)
        {
            transform.position = new Vector3(Random.Range(0, 4), 1, Random.Range(0, 4));
        }

        Vector3 dir = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

        transform.Translate(dir * speed * Time.deltaTime);

        transform.eulerAngles += new Vector3(0, Input.GetAxis("Mouse X") * angleSpeed * Time.deltaTime, 0);


        if (Input.GetButtonDown("Fire1"))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);


            if (Physics.Raycast(ray, out hit, Mathf.Infinity, layer))
            {
                PhotonControl control = hit.transform.GetComponent<PhotonControl>();

                if (control == null) return;
                control.photonView.RPC("Damage", RpcTarget.All);

            }
        }

        if (health <= 0)
        {
            PhotonNetwork.LeaveRoom();
            PhotonNetwork.Disconnect();
        }
    }


    [PunRPC]


    public void Damege()
    {

        GameObject hitEffect = Instantiate(effect);

        hitEffect.transform.position = hit.point;

        

        Destroy(hitEffect, 0.1f);
        health -= 100;
        

    }
}
