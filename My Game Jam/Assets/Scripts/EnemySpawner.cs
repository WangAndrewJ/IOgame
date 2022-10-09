using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using TMPro;

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
    public List<Ctr> listOfCtr = new List<Ctr>();
    [SerializeField] TextMeshProUGUI leaderboard;
    private List<Ctr> topPlayers = new List<Ctr>();
    private string leaderBoardText;

    private void Start()
    {
        listOfUsernames = usernames.Split(' ').ToList();
        StartCoroutine(LateStart());
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
                listOfCtr.Add(ctr);
                ctr.healthScript = health;
                ctr.scoreScript = score;
                ctr.username = listOfUsernames[Random.Range(0, listOfUsernames.Count)];
                ctr.usernameText.text = ctr.username;
                ctr.spawner = this;
            }
        }
    }

    private IEnumerator LateStart()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.5f);
            UpdateScore();
        }
    }

    public void UpdateScore()
    {
        leaderBoardText = "";
        topPlayers = listOfCtr.OrderBy(item => item.score).Reverse().ToList();

        for (int i = 0; i < 8; i++)
        {
            leaderBoardText += topPlayers[i].isPlayer ? $"<b>{topPlayers[i].username} - {topPlayers[i].score}</b>\n" : $"{topPlayers[i].username} - {topPlayers[i].score}\n";
        }

        leaderboard.text = leaderBoardText;
    }
}
