
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;
using Photon.Realtime;
using UnityEngine.UI;
using TMPro;
public class Movement : MonoBehaviour, IPunObservable
{
    Rigidbody2D body;
    private PhotonView photon;
    float horizontal;
    float vertical;
    float moveLimiter = 0.7f;
    private SpriteRenderer spriteRenderer;
    public float runSpeed = 20.0f;
    public bool isRed;
    public GameObject PlayerPrefab;
   public  GameObject firstplat;
    public mapcontroller mapa;
    public string nam;
    public float rivokpower;
    public bool rivok = true;
    void Start()
    {
      /*  if(PhotonNetwork.IsMasterClient)
        {
            TMP_Text t = gameObject.GetComponentInChildren<TMP_Text>();
           // Debug.Log(PhotonNetwork.PlayerList[0].NickName);
            t.text ="Player 1";
            name = t.text;
        }
        if (PhotonNetwork.IsMasterClient != true )
        {
            TMP_Text t = gameObject.GetComponentInChildren<TMP_Text>();
            t.text = "Player2";
            name = t.text;
            //  Debug.Log(PhotonNetwork.PlayerList[1].NickName);
        }*/
        mapa = FindObjectOfType<mapcontroller>();
        photon = GetComponent<PhotonView>();
        body = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        // gameObject.name = nam;
        firstplat = mapcontroller.Instance.platforms[(int)gameObject.transform.position.x, (int)gameObject.transform.position.y];
    //    FindObjectOfType<mapcontroller>().addPlayer(gameObject.GetComponent<Movement>());
        gameObject.GetComponentInChildren<TMP_Text>().SetText(photon.Owner.NickName);
        gameObject.name = photon.Owner.NickName;
        mapcontroller.Instance.players.Add(this.gameObject);
        if (mapcontroller.Instance.players.Count == 2) { mapcontroller.Instance.ready = true; }
        //   TMP_Text t = gameObject.GetComponentInChildren<TMP_Text>();
        //   t.text = PhotonNetwork.PlayerList[0].NickName;
        //   name = t.text;
    }

    
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
           stream.SendNext(gameObject.GetComponent<BoxCollider2D>().enabled);
            
        //    stream.SendNext(name);
            //  stream.SendNext(nam);
        }
        else
        {
         gameObject.GetComponent<BoxCollider2D>().enabled = (bool)stream.ReceiveNext();
          //  name = (string)stream.ReceiveNext();
            // nam = (string)stream.ReceiveNext();
        }
        
    }
    void Update()
    {
        // Gives a value between -1 and 1
        horizontal = Input.GetAxisRaw("Horizontal"); // -1 is left
        vertical = Input.GetAxisRaw("Vertical"); // -1 is down
    }

    void FixedUpdate()
    {
       // horizontal = Input.GetAxisRaw("Horizontal"); // -1 is left
       // vertical = Input.GetAxisRaw("Vertical"); // -1 is down
        if (photon.IsMine)
        {
           
            if (horizontal != 0 && vertical != 0) // Check for diagonal movement
            {
                // limit movement speed diagonally, so you move at 70% speed
                horizontal *= moveLimiter;
                vertical *= moveLimiter;
            }
            body.velocity = new Vector2(horizontal * runSpeed, vertical * runSpeed);
            if (Input.GetKey(KeyCode.Space))
            {
               // spriteRenderer.color = Color.red;
                isRed = true;
            }
            else
            {
              //  spriteRenderer.color = Color.white;
                isRed = false;
            }
            if (Input.GetMouseButton(1) && rivok == true)
            {
        //        Debug.Log(1);
            //   body.velocity = new Vector2(0, 0);
                body.AddForce(new Vector2(horizontal, vertical) * rivokpower);
                rivok = false;
                gameObject.GetComponent<BoxCollider2D>().enabled = false;
                StartCoroutine(disableCollider());
                Invoke("Rivok", 0.7f);
               
            }
           
        }
      
    //   if(isRed)
      //  {
      //      spriteRenderer.color = Color.red;

     //   }
     //   if (!isRed)
      //  {
      //      spriteRenderer.color = Color.white;
      //  }
       
    }
    public IEnumerator disableCollider()
    {
        yield return new WaitForSeconds(0.5f);
        gameObject.GetComponent<BoxCollider2D>().enabled = true;
    }
   public void Rivok()
    {
       rivok = true;
        
    }

    public void Wait()
    {
        gameObject.GetComponent<BoxCollider2D>().enabled = true;
    }
}