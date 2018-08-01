
using UnityEngine;
using UnityEngine.Networking;
public class levelManager : NetworkBehaviour
{
    [SerializeField]
    private GameObject[] balls;
    private ballScript[] ballList;

    [SerializeField]
    private Transform[] spawns;
   
    private int itemInt;
    public float roundTime = 20f;

    public ballScript BS;

    void Awake ()
    {
        ballList = FindObjectsOfType<ballScript>();
        for (int i = 0; i < balls.Length; i++)
        {
            CmdSpawnItems();
        }
    }
	
	// Update is called once per frame
	void Update ()
    {
        roundTime -= Time.deltaTime;
        if (roundTime <= 0)
        {
            CmdSpawnItems();
            roundTime = 20;
            Debug.Log("spawning items again");
        }
    }
   
    [Command]
    public void CmdSpawnItems()
    {

        int spawnNum = RandomInt2();
        ballList = FindObjectsOfType<ballScript>();
        bool canSpawn = true;
        for (int i = 0; i < ballList.Length; i++)
        {
            if (ballList[i].spawnPlace == spawnNum)
            {
                canSpawn = false;
                break;
            }
        }
        //place a check here for if ball is spawnwed or not
        if (canSpawn)
        {
            GameObject item1 = Instantiate(balls[RandomInt()], spawns[spawnNum].position, spawns[spawnNum].rotation);
            BS = item1.GetComponent<ballScript>();
            Debug.Log("spawning balls");
            BS.spawnPlace = spawnNum;
            NetworkServer.Spawn(item1);
        }





    }
    private int RandomInt()
    {
        itemInt = Random.Range(0, balls.Length);
        return itemInt;
    }
    private int RandomInt2()
    {
        itemInt = Random.Range(0, spawns.Length);
        return itemInt;
    }
}
