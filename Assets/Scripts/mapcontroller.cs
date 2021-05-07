using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
public class mapcontroller : MonoBehaviourPunCallbacks
{
    public static mapcontroller Instance;
    public List<Movement> players = new List<Movement>();
   // public List<GameObject> firstplatforms = new List<GameObject>();
    public GameObject first;
    public ExitGames.Client.Photon.Hashtable hash = new ExitGames.Client.Photon.Hashtable();
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

    public GameObject platformPrefab;
    public GameObject[,] platforms;
    private void Awake()
    {
        Instance = this;
      //  ExitGames.Client.Photon.Hashtable = hash;
    }
    void Start()
    {
        
       
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
    public override void OnRoomPropertiesUpdate(ExitGames.Client.Photon.Hashtable propertiesThatChanged)
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
    



    
    public void CheckForPlat()
    {
        if (players[0] != null)
        {
            first = players[0].firstplat;
        }
      //  Debug.Log(players.Count);
        if ( players.Count == 2 && players[1] != null )
        {
            second = players[1].firstplat;
            ready = true;
        }
       
           
        
    }

    void Update() {
      //  Debug.Log(PhotonNetwork.CurrentRoom.CustomProperties);
    }
    public IEnumerator DestroyPlatform(GameObject g, GameObject gg)
    {
       
        yield return new WaitForSeconds(1);

        gg.GetComponent<SpriteRenderer>().enabled = false;
      
       for(int i = 0; i < 10; i++)
        {
            for (int j = 0; j < 10; j++)
            {
                if(gg == platforms[i,j] && gg.GetComponent<SpriteRenderer>().enabled == false)

                {
                    x = (byte)i;
                    y = (byte)j;
                  
                 
                    hash.Add(x+""+y, gg.GetComponent<SpriteRenderer>());
              //      PhotonNetwork.CurrentRoom.SetCustomProperties(hash);
                    OnRoomPropertiesUpdate(hash);
                    _Keys.Add(x+""+y);
                    _Values.Add(gg.GetComponent<SpriteRenderer>());
                }
            }
        }
        yield return new WaitForSeconds(0.2f);

        if (gg.GetComponent<BoxCollider2D>().IsTouching(g.GetComponent<BoxCollider2D>()) == true)
        {
            g.SetActive(false);
        }

    }
    public void addPlayer(Movement m)
    {
         players.Add(m);
        
       CheckForPlat();
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        
    }
}
