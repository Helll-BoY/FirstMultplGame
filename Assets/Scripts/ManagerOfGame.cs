using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;
using Photon.Realtime;
using TMPro;
using UnityEngine.EventSystems;
public class ManagerOfGame : MonoBehaviourPunCallbacks
{
    public Movement PlayerPrefab1;
    public Movement PlayerPrefab2;
    public GameObject platformPrefab;
    public GameObject firstplat;
    public GameObject secondplat;
    public int a;
    public int b;
    public int c;
    public int d;
    public static  ManagerOfGame Instance;
    public int ii = 0;

    public float rivokpower;
    public bool rivok = true;
    public List<SpriteRenderer> check;
   
    public List<Movement> players = new List<Movement>();
    private void Awake()
    {
        Instance = this;
    }
   
    void Start()
    {
       
        //  OnRoomPropertiesUpdate(hash);
        /*    platforms = new GameObject[10,10];
            check = new List<SpriteRenderer>();
            platform_sprites = new SpriteRenderer[10, 10];
            int k = 0;
            for(int i = 0; i < 10; i++)
            {
                for(int j = 0; j < 10; j++)
                {
                    GameObject pl = Instantiate(platformPrefab, new Vector2(i, j), Quaternion.identity);
                    pl.transform.SetParent(gameObject.transform);
                    pl.name = "block" + k;
                    k++;
                    platforms[i, j] = pl;
               //     platform_sprites[i,j] = pl.GetComponent<SpriteRenderer>();
               //     check.Add(pl.GetComponent<SpriteRenderer>());
                }
            }*/

        a = Random.Range(0, 10);
            b = Random.Range(0, 10);
            Vector2 pos = new Vector2(a, b);
            PhotonNetwork.Instantiate(PlayerPrefab1.name, pos, Quaternion.identity);
   
       
        StartCoroutine(Nam( PhotonNetwork.PlayerList[ii].NickName, ii));
    }

    
    void Update()
    {
        
    }

    public void Leave()
    {
        PhotonNetwork.LeaveRoom();
    }
    public override void OnLeftRoom ()
    {
        // когда случайно ливаем
        SceneManager.LoadScene(0);
    }



    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        // Debug.LogFormat("Player {0} entered room" ,  newPlayer.NickName);
        ii++;
      
       StartCoroutine(Nam(PhotonNetwork.PlayerList[ii].NickName,ii));
        //mapcontroller.Instance.players[ii].GetComponent<Movement>().Name(newPlayer.NickName);
        
    }
    public IEnumerator Nam(string na, int index)
    {
        yield return new WaitForSeconds(3);
        mapcontroller.Instance.players[index].GetComponentInChildren<TMP_Text>().text = na;
        mapcontroller.Instance.players[index].name = na;
    }
    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
     //   Debug.LogFormat("Player {0} left room", otherPlayer.NickName);
    }
}
