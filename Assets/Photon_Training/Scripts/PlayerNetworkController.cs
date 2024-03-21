using Cinemachine;
using Photon.Pun;
using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class PlayerNetworkController : MonoBehaviour
{
    [SerializeField] float _speed;
    [SerializeField] PhotonView _photonView;
    [SerializeField] AssetReference _emojiPrefab;
    [SerializeField] Transform _anchorPoint;
    [SerializeField] ThirdPersonController thirdPersonController;
    // Start is called before the first frame update

    private void Awake()
    {
        if (_photonView.IsMine)
        {
            CinemachineVirtualCamera cinemachineVirtualCamera = FindObjectOfType<CinemachineVirtualCamera>();
            if (cinemachineVirtualCamera != null)
            {
                cinemachineVirtualCamera.Follow = thirdPersonController.CinemachineCameraTarget.transform;
            }
        }     
    }


    [PunRPC]
    public void CreateEmoji()
    {
        _emojiPrefab.InstantiateAsync(_anchorPoint.position, Quaternion.identity).Completed += (async)=> 
        {
            async.Result.transform.SetParent(this.transform);
        };
    }

    private void Update()
    {
        if (_photonView.IsMine)
        {
            if (Input.GetKeyDown(KeyCode.H))
            {
                _photonView.RPC("CreateEmoji", RpcTarget.All);
            }      
        }
    }
}
