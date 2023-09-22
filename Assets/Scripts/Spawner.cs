using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    //Helps make a dropdown option for waveType in inspector when making waves
    public enum WaveType { 
        Sprinkle,
        Line,
        DiagLine,
        RevDiagLine,
        Arrow,
        RevArrow
    }

    public enum Difficulty { 
        Easy,
        Normal,
        Hard,
        Legend
    }

    //Creates a wave that is called later in sequence that uses the corresponding spawn functions
    [System.Serializable]
    public class Wave
    {
        //For determing what spawn function needs to be called
        public WaveType type;
        //Value that corresponds to the index in the Zombies[] list 
        public int zombieIndex;
        public int numToSpawn;
        public float delay;

        //For creating a new wave
        public Wave(WaveType t, int zI, int amt, float d)
        {
            type = t;
            zombieIndex = zI;
            numToSpawn = amt;
            delay = d;
        }
    }

    //For Endless mode
    public bool endless = false;
    private Difficulty difficulty = Difficulty.Easy;


    //A set of pre determined waves that will be executed when the scene is loaded
    public Wave[] Waves;
    public float waveBufferTime = 5;

    //For spawn location and boundaries
    public Transform[] spawnPoints;
    public Transform _sp;

    //For picking which zombie to spawn;
    public Transform[] Zombies;

    //For Testing
    public float heightOffset = -0.5f;

    //For determining if the player defeated all the zombies
    private bool allSpawned = false;
    private int zSpawned = 0;
    private bool allDead = false;

    //For WaveDisplayer
    public WaveDisplayer waveDisplayer;
    public int currentWave = 0;
    private int totalWaves;

    GameObject zombieContainer;

    void Start()
    {
        zombieContainer = GameObject.Find("Enemy Container");
        waveDisplayer = FindObjectOfType<WaveDisplayer>();

        //If theres a set number of waves, make the ui display how many waves there will be
        if (!endless)
        {
            totalWaves = Waves.Length;
        }
        //If endless, populate Waves[] with a new random wave
        else {
            totalWaves = 0;
            Waves = new Wave[1];
            Waves[0] = NewRandomWave();
        }
        
        //Start Spawning waves
        StartCoroutine(SpawnWaves());
    }

    private void Update()
    {
        
        if (((allSpawned && (FindObjectOfType<GameManager>().counter == zSpawned)) && !allDead )&&!endless) {
            allDead = true;
            //End the game and display victory UI
            FindObjectOfType<GameManager>().Victory();
        }
    }

    private IEnumerator SpawnWaves() {
        WaveType t;
        int zI;
        int amt;
        float d;

        if (Waves[0] == null) {
            yield break;
        }

        for (int i = 0; i < Waves.Length; i++) {
            
            currentWave++;
            //Create local variables and store the corresponding values from the current wave
            t = Waves[i].type;
            zI = Waves[i].zombieIndex;
            amt = Waves[i].numToSpawn;
            d = Waves[i].delay;
            //Pause for a specified amount of seconds before starting the next wave automatically
            yield return new WaitForSeconds(waveBufferTime);
            waveDisplayer.UpdateText(currentWave, totalWaves);
            
            //Depending on the type of wave specifed in the inspector, use its corresponding function to spawn zombies
            if (t == WaveType.Sprinkle)
            {
                yield return StartCoroutine(Sprinkle(amt, zI, d));
            }
            else if (t == WaveType.Line)
            {
                yield return StartCoroutine(SpawnLine(amt, zI));
            }
            else if (t == WaveType.DiagLine)
            {
                yield return StartCoroutine(SpawnLine(amt, zI, d));
            }
            else if (t == WaveType.RevDiagLine) {
                yield return StartCoroutine(SpawnRevDiagLine(amt, zI, d));
            }
            else if (t == WaveType.Arrow)
            {
                yield return StartCoroutine(SpawnArrow(amt, zI, d));
            }
            else if (t == WaveType.RevArrow)
            {
                yield return StartCoroutine(SpawnRevArrow(amt, zI, d));
            }

            //If endless prep next wave by setting it as first and reset i so function doesnt go out of bounds
            if (endless){
                Waves[0] = NewRandomWave();
                UpdateDifficulty();
                i--;
            }
        }

        //For dislaying win screen UPDATE THIS AREA
        if (!endless) { 
            allSpawned = true;
        }
        
    }

    // Spawn a zombie at a random Y point between the two spawn pointss
    private void SpawnRandomZombie(Transform enemy) {

        _sp.transform.position = new Vector3(spawnPoints[0].position.x, Random.Range(spawnPoints[1].position.y, spawnPoints[0].position.y+heightOffset), _sp.position.z);
        GameObject newZombie = Instantiate(enemy, _sp.position, _sp.rotation).gameObject;
        newZombie.transform.parent = zombieContainer.transform;
        zSpawned++;
    }
    // Spawn a zombie at a provided Y point (yLocation) alligned on the x axis with the first spawn point
    private void SpawnZombie(Transform enemy, float yLocation)
    {
        _sp.transform.position = new Vector3(spawnPoints[0].position.x, yLocation+heightOffset, _sp.position.z);
        GameObject newZombie = Instantiate(enemy, _sp.position, _sp.rotation).gameObject;
        newZombie.transform.parent = zombieContainer.transform;
        zSpawned++;
    }


    //Wave Spawn functions


    //Spawn (amt) zombies in a straight vertical line at the same time
    private IEnumerator SpawnLine(int amt, int zombieType)
    {
        yield return new WaitForSeconds(0);
        //Calculate the distance needed to evenly space amt number of zombies between two points on the y-axis 
        float distance = CalculateEquiDistant(amt);
        //Spawn in the zombies all at the same time down vertically.
        for (int i = 1; i < amt+1; i++)
        {
            SpawnZombie(Zombies[zombieType], spawnPoints[0].position.y + (i*distance));
        }
    }
    //Spawn (amt) zombies in a diagonal line with spacing 
    private IEnumerator SpawnLine(int amt, int zombieType, float delay)
    {
        float distance = CalculateEquiDistant(amt);

        for (int i = 1; i < amt+1; i++)
        {
            yield return new WaitForSeconds(delay);
            SpawnZombie(Zombies[zombieType], spawnPoints[0].position.y + (i * distance));
        }
    }

    private IEnumerator SpawnRevDiagLine(int amt, int zombieType, float delay)
    {
        float distance = CalculateEquiDistant(amt);

        for (int i = amt; i > 0; i--)
        {
            yield return new WaitForSeconds(delay);
            SpawnZombie(Zombies[zombieType], spawnPoints[0].position.y + (i * distance));
        }
    }
    private IEnumerator SpawnArrow(int lines, int zombieType, float delay) {
        float distance = CalculateEquiDistant(1+(lines*2));
        float midpoint = (spawnPoints[1].position.y + spawnPoints[0].position.y)/2f;
        SpawnZombie(Zombies[zombieType], midpoint);
        for (int i = 1; i < lines + 1; i++) {
            yield return new WaitForSeconds(delay);
            SpawnZombie(Zombies[zombieType], midpoint + (i * distance));
            SpawnZombie(Zombies[zombieType], midpoint - (i * distance));
        }
    }

    private IEnumerator SpawnRevArrow(int lines, int zombieType, float delay)
    {
        float distance = CalculateEquiDistant(1 + (lines * 2));
        float midpoint = (spawnPoints[1].position.y + spawnPoints[0].position.y) / 2f; 
        for (int i = lines; i > 0; i--)
        {
            SpawnZombie(Zombies[zombieType], midpoint + (i * distance));
            SpawnZombie(Zombies[zombieType], midpoint - (i * distance));
            yield return new WaitForSeconds(delay);
        }
        SpawnZombie(Zombies[zombieType], midpoint);
    }

    private IEnumerator Sprinkle(int amt, int zombieType, float maxdelay) {
        float pause;
        for (int i = 0; i < amt; i++) { 
            pause = Random.Range(0, maxdelay);
            yield return new WaitForSeconds(pause);
            SpawnRandomZombie(Zombies[zombieType]);
        }
    }
    //Finds the distance (amt) number of points need to be from eachother and the supplied endpoints in order to be equidistant and returns that float
    float CalculateEquiDistant(int amt) {
       return (spawnPoints[1].position.y - spawnPoints[0].position.y) / (amt+1);
    }

    private void UpdateDifficulty() { 
        if (currentWave <= 5)
        {
            difficulty = Difficulty.Easy;
        } else if (currentWave <= 20)
        {
            waveBufferTime =3.5f;
            difficulty = Difficulty.Normal;
        } else if (currentWave <= 50)
        {
            waveBufferTime = 3f;
            difficulty = Difficulty.Hard;
        }
        else
        {
            waveBufferTime =3;
            difficulty = Difficulty.Legend;
        }
    }

    //For use in endless mode
    public Wave NewRandomWave() {
        Wave temp;
        int wTIndex = Random.Range(0, 7);
        WaveType tempType = WaveType.Line;
        int tempZI = 0;
        int tempAmt = 3;
        float tempDelay = 1;

        //For setting what kind of zombie can spawn and how far apart they can be;
        if (difficulty == Difficulty.Easy) {
            //Only spawn the crawler
            tempZI = 0;
            tempDelay = Random.Range(3, 4f);
            tempAmt = Random.Range(3, 5);
        } else if(difficulty == Difficulty.Normal) {
            //Spawn the first and second level of zombie 
            tempZI = Random.Range(0, 2);
            tempDelay = Random.Range(2.5f, 3.5f);
            tempAmt = Random.Range(4, 6);
        } else if (difficulty == Difficulty.Hard)
        {
            //Spawn all kinds of zombies except tough zombie, no more crawlers
            tempZI = Random.Range(1, 3);
            tempDelay = Random.Range(2, 3f);
            tempAmt = Random.Range(5, 7);
        }
        else if (difficulty == Difficulty.Legend)
        {
            //Spawn every type of zombie ecept crawler
            tempZI = Random.Range(1, 4);
            tempDelay = Random.Range(1.5f, 2.5f);
            tempAmt = Random.Range(6, 9);
        }

        //For setting random wave type
        if (wTIndex == 0 || wTIndex == 1)
        {
            tempType = WaveType.Sprinkle;
        }
        else if (wTIndex == 2) {
            tempType = WaveType.Line;
        }
        else if (wTIndex == 3)
        {
            tempType = WaveType.DiagLine;
        }
        else if (wTIndex == 4)
        {
            tempType = WaveType.RevDiagLine;
        }
        else if (wTIndex == 5)
        {
            tempType = WaveType.Arrow;
            tempAmt = tempAmt / 2;
        }
        else if (wTIndex == 6)
        {
            tempType = WaveType.RevArrow;
            tempAmt = tempAmt / 2;
        }

        //Assign all random values to the temp wave
        temp = new Wave(tempType,tempZI,tempAmt,tempDelay);
        return temp;
    }
}
