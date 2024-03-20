using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerNetworkController : MonoBehaviour
{
    [SerializeField] float _speed;
    [SerializeField] PhotonView _photonView;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (_photonView != null && _photonView.IsMine)
        {
            this.transform.Translate(Input.GetAxis("Horizontal") * Time.deltaTime * _speed, 0, Input.GetAxis("Vertical") * Time.deltaTime * _speed);
        }   
    }
}
