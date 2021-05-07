using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
public class platform : MonoBehaviour
{
    public GameObject currentg;
    void Start()
    {
        
    }

    public void OnTriggerEnter2D(Collider2D coll)
    {
       // if (PhotonNetwork.IsMasterClient)
       // {
            currentg = gameObject;
            if (gameObject.GetComponent<SpriteRenderer>().enabled == false)
            {
                coll.gameObject.SetActive(false);
            }
         //   Debug.Log(coll.name);

            if (gameObject != mapcontroller.Instance.first && gameObject != mapcontroller.Instance.second && mapcontroller.Instance.ready == true)
            {
         
                //mapcontroller.Instance.StartCoroutine(DestroyPlatform( coll.gameObject, currentg));
                StartCoroutine(mapcontroller.Instance.DestroyPlatform(coll.gameObject, currentg));
                //StartCoroutine(DestroyPlatform(coll.gameObject, currentg));
                
                mapcontroller.Instance.first = null;
                mapcontroller.Instance.second = null;
            }
      //  }
     
    }
    void Update()
    {

    }

   
   

   /* public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(ManagerOfGame.Instance.platform_sprites);
            //  stream.SendNext(nam);
        }
        else
        { 
            ManagerOfGame.Instance.platform_sprites = (gameObject.GetComponent<SpriteRenderer>())stream.ReceiveNext();
            // nam = (string)stream.ReceiveNext();
        }
    }*/
}
