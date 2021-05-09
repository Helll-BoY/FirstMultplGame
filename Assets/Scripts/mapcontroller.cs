using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Chat;
using Photon.Realtime;
using ExitGames.Client.Photon;
using System.Linq;

public enum PhotonEventCodes
{
    DestroyPlatform = 10,
    DieNorm = 20,
        Dielol = 30
}
public class mapcontroller : MonoBehaviourPunCallbacks, IOnEventCallback
{
    public static mapcontroller Instance;
    public List<GameObject> players = new List<GameObject>();
    public PhotonView view;
    public GameObject first;
  //  public  hash = new ExitGames.Client.Photon.Hashtable();
    public GameObject second;
    public byte x = 0;
    public byte y = 0;
 //   public Dictionary<string ,SpriteRenderer> deletedplatforms = new Dictionary<string, SpriteRenderer>();
     public List<string> _Keys = new List<string>();
    public List<SpriteRenderer> _Values = new List<SpriteRenderer>();
    //public List<SpriteRenderer> check;
    public SpriteRenderer[,] platform_sprites;
    public bool ready = false;
    public bool rivok = true;
    public const byte DestroyPlatCode = 10;
    public const byte Die = 20;
    public GameObject platformPrefab;
    public GameObject[,] platforms;
    public RaiseEventOptions opt;
    private void Awake()
    {
        Instance = this;
      //  ExitGames.Client.Photon.Hashtable = hash;
    }
    void Start()
    {
        view = GetComponent<PhotonView>();
        opt = new RaiseEventOptions
        {
            Receivers = ReceiverGroup.All
        };
       
        platforms = new GameObject[10, 10];
       // check = new List<SpriteRenderer>();
        platform_sprites = new SpriteRenderer[10, 10];
        
        int k = 0;
        for (int i = 0; i < 10; i++)
        {
            for (int j = 0; j < 10; j++)
            {
                GameObject pl = Instantiate(platformPrefab, new Vector2(i, j), Quaternion.identity);
                pl.transform.SetParent(gameObject.transform);
                pl.name = "block" + k;
                k++;
                platforms[i, j] = pl;
            }
        }
    }
    /*public override void OnRoomPropertiesUpdate(ExitGames.Client.Photon.Hashtable propertiesThatChanged)
    {
        
       
        for(byte i = 0; i < 10; i++)
        {
            for(byte j = 0; j < 10; j++)
            {
                if(propertiesThatChanged[i+""+j] == platform_sprites[i,j])
                {
                //    platform_sprites[i, j].enabled = false;
                }
            }
        }
       
    }
    */



    
   
  // public override void 

   void Update()
    {
        if(players.Count == 2)
        {
            players.OrderBy(pl => pl.GetPhotonView());
        }
    }
    private void OnEnable()
    {
        PhotonNetwork.AddCallbackTarget(this);
    }

    private void OnDisable()
    {
       
        PhotonNetwork.RemoveCallbackTarget(this);
    }
    public void OnEvent(EventData photonEvent)
    {
       // Debug.Log("event");
        byte eventCODE = photonEvent.Code;
        if(eventCODE == (byte)PhotonEventCodes.DestroyPlatform)
        {
          //  Debug.Log("eventdone");
            object[] data = (object[])photonEvent.CustomData;
                int x = (byte)data[1];
                int y = (byte)data[2];
            // GameObject g = (b)data[0];
            //  Debug.Log(x+" "+ y);
            platforms[x, y].GetComponent<SpriteRenderer>().enabled = false;
        }
        if(eventCODE == (byte)PhotonEventCodes.DieNorm)
        {
        
           string[] data = (string[])photonEvent.CustomData;
         string xx = data[0];
            for(int i = 0; i < players.Count;i++)
            {
                if(players[i].name == xx) { players[i].SetActive(false); }
            }
        
        }
        if (eventCODE == (byte)PhotonEventCodes.Dielol)
        {

            int[] data = (int[])photonEvent.CustomData;
            int xx = data[0];

            players[xx].SetActive(false);
            

        }

    }

    public IEnumerator DestroyPlatform(GameObject g, GameObject gg)
    {
       // if (!PhotonNetwork.IsMasterClient) {yield return null; }
       if (PhotonNetwork.IsMasterClient)
        {
            yield return new WaitForSeconds(1);
          
           // gg.GetComponent<SpriteRenderer>().enabled = false;
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    if (gg == platforms[i, j] )

                    {
                        x = (byte)i;
                        y = (byte)j;
                        _Keys.Add(x + "" + y);
                        _Values.Add(gg.GetComponent<SpriteRenderer>());
                    }
                }
            }
             //    Debug.Log(opt.Receivers);
            
            object[] datas = new object[] { gg.GetComponent<SpriteRenderer>().enabled, x,y };
     //       Debug.Log(datas[0]+ "" + datas[1] + "" + datas[2]);
            PhotonNetwork.RaiseEvent((byte)PhotonEventCodes.DestroyPlatform, datas, opt, ExitGames.Client.Photon.SendOptions.SendUnreliable);
            
           
            yield return new WaitForSeconds(0.2f);

            if (gg.GetComponent<BoxCollider2D>().IsTouching(g.GetComponent<BoxCollider2D>()) == true)
            {
                g.SetActive(false);
            }
       }

    }

    public IEnumerator dieplayer(GameObject g)
    {
        if (PhotonNetwork.IsMasterClient)
        {
          //  Debug.Log(g);
            for (int i = 0; i < players.Count; i++)
            {
                if (players[i] == g)
                {
                    string[] datas = new string[] { players[i].name };
                   
                    PhotonNetwork.RaiseEvent((byte)PhotonEventCodes.DieNorm, datas, opt, ExitGames.Client.Photon.SendOptions.SendUnreliable);
                }
            }

            yield return null;
        }
        
    }

    public IEnumerator dieagain(int g)
    {
        if (PhotonNetwork.IsMasterClient)
        {
            //  Debug.Log(g);
           
                    int[] datas = new int[] { g };

                    PhotonNetwork.RaiseEvent((byte)PhotonEventCodes.Dielol, datas, opt, ExitGames.Client.Photon.SendOptions.SendUnreliable);
           

            yield return null;
        }

    }
    /* public void addPlayer(Movement m)
     {

          players.Add(m);

        CheckForPlat();
     }*/





}
