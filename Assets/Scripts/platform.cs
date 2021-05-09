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
    void Update()
    {
       if (mapcontroller.Instance.players.Count == 2)
        {


            if (gameObject.GetComponent<SpriteRenderer>().enabled == false && gameObject.GetComponent<BoxCollider2D>().IsTouching(mapcontroller.Instance.players[0].GetComponent<BoxCollider2D>()))
            {
                StartCoroutine(mapcontroller.Instance.dieagain(0));
            }
            if (gameObject.GetComponent<SpriteRenderer>().enabled == false &&  gameObject.GetComponent<BoxCollider2D>().IsTouching(mapcontroller.Instance.players[1].GetComponent<BoxCollider2D>()))
            {
               StartCoroutine(mapcontroller.Instance.dieagain(1));
            }
        }
    }

    public void OnTriggerEnter2D(Collider2D coll)
    {
       // if (PhotonNetwork.IsMasterClient)
       // {
            currentg = gameObject;
            if (gameObject.GetComponent<SpriteRenderer>().enabled == false)
            {
            StartCoroutine(mapcontroller.Instance.dieplayer(coll.gameObject));
           Debug.Log(1);
        }
         //   Debug.Log(coll.name);

            if (gameObject != mapcontroller.Instance.first && gameObject != mapcontroller.Instance.second && mapcontroller.Instance.ready == true)
            {
         
                
                StartCoroutine(mapcontroller.Instance.DestroyPlatform(coll.gameObject, currentg));
               
                
                mapcontroller.Instance.first = null;
                mapcontroller.Instance.second = null;
            }
      //  }
     
    }
   
   public void OnTriggerStay2D(Collider2D coll)
    {
    //    if (gameObject.GetComponent<SpriteRenderer>().enabled == false) { StartCoroutine(mapcontroller.Instance.dieagain(1)); }
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
