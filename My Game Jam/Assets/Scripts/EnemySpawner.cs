using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class EnemySpawner : MonoBehaviour
{
    public int maxEnemiesAllowed;
    public float spawnRate = 1f;
    private float nextTimeToSpawn;
    public List<Transform> players = new List<Transform>();
    public float MinX;
    public float MaxX;
    public float MinY;
    public float MaxY;
    public GameObject AI;
    public Health health;
    public Score score;
    private string usernames = "sploshreckless provideact spiffydemocrat rowboatbovril gaiterssoviet kellogsample rustleserious utsiregam spellcrafting lavishlet doletrial managescumbag jackyardconfident theirqualified triangularorangutan briskbatter frockweeping southwesterlybrails shootrunning doughnutpan lodestonecoverage motiongnu mirroryearning defensiveseveral internetloss buzzsubtract interfereprideful jugularalveoli smockburgee unwieldygambler woundcreep prudentevent exclaimclimate whichloudmouth typeauk";
    private List<string> listOfUsernames = new List<string>();

    private void Start()
    {
        listOfUsernames = usernames.Split(' ').ToList();
    }

    private void Update()
    {
        if (players.Count < maxEnemiesAllowed)
        {
            if (Time.time >= nextTimeToSpawn)
            {
                nextTimeToSpawn = Time.time + 1f / spawnRate;

                GameObject spawnedPrefab = GameObject.Instantiate(AI, new Vector2(Random.Range(MinX, MaxX), Random.Range(MinY, MaxY)), Quaternion.identity);
                Ctr ctr = spawnedPrefab.GetComponent<Ctr>();

                players.Add(spawnedPrefab.transform);
                ctr.healthScript = health;
                ctr.scoreScript = score;
                ctr.username = listOfUsernames[Random.Range(0, listOfUsernames.Count)];
                ctr.usernameText.text = ctr.username;
                ctr.spawner = this;
            }
        }
    }
}
