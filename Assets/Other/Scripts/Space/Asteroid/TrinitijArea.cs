using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class TrinitijArea : MonoBehaviour {

    public float areaSize = 0.0f;
    public GameObject[] asteroids = null;
    public int[] asteroidsAmount = null;
    public Vector2[] asteroidsSizeRange = null;

    public enum RandomChoice { INSIDE_SPHERE, ON_SPEHERE };
    public RandomChoice randomChoice;

    public GameObject trinitj = null;
    public Vector3 sizeOfProtectedArea = Vector3.zero;
    Bounds protectedArea;

    public GameObject player = null;
    public GameObject enemy = null;
    Bounds enemySpawnArea;

    public Text instructionText = null;
    public float timeTillRescue = 30;

    void Start()
    {
        if (areaSize > 0 && asteroids != null && asteroidsAmount != null && trinitj != null && instructionText != null && player != null && enemy != null)
        {
            protectedArea = new Bounds(transform.position, sizeOfProtectedArea);
            enemySpawnArea = new Bounds(transform.position, Vector3.one * (areaSize/1.5f));
            Vector3 randomPosition = Vector3.zero;
            float minSize = 0;
            float maxSize = 0;

            for (int i = 0; i < asteroids.Length; i++)
            {
                minSize = asteroidsSizeRange[i].x;
                maxSize = asteroidsSizeRange[i].y;

                for (int j = 0; j < asteroidsAmount[i]; j++)
                {
                    switch (randomChoice)
                    {
                        case RandomChoice.INSIDE_SPHERE:
                            do{
                                randomPosition = Random.insideUnitSphere * areaSize;
                            } while (protectedArea.Contains(randomPosition));
                            break;
                        case RandomChoice.ON_SPEHERE:
                            randomPosition = Random.onUnitSphere * areaSize;
                            break;
                    }

                    GameObject asteroid = Instantiate<GameObject>(asteroids[i]);
                    asteroid.transform.parent = transform;
                    asteroid.transform.position = transform.position + randomPosition;
                    asteroid.transform.rotation = Random.rotation;
                    asteroid.transform.localScale = Vector3.one * Random.Range(minSize, maxSize);
                }
            }

            GameObject tmpTrinitij = Instantiate<GameObject>(trinitj);
            tmpTrinitij.transform.position = transform.position;
        }
    }

    //state of mission
    private int stateMission = 0;

    void FixedUpdate()
    {
        if (stateMission != -1)
        {
            switch (stateMission)
            {
                case 0:
                    setInstructionText("Find Trinity");
                    stateMission = -1;
                    break;

                case 1:
                    setInstructionText("Protect Trinity");
                    stateMission = -1;
                    break;

                case 2:
                    setInstructionText("Help has arrived!\nYou won :)");
                    stateMission = -2;
                    break;

                //THE END
                case -2:
                    if(!showText && player != null)
                    {
                        int curIdx = Globals.instance.sceneManager.GetCurSceneIndex();
                        Globals.instance.sceneManager.SwitchToScene(0);
                    }
                    break;
            }
        }

        isPlayerInEnemySpawnArea();
        displayInstructionText(30.0f);
    }

    //Za lazjo vizualizacijo in izbire

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, areaSize);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(transform.position, sizeOfProtectedArea);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(transform.position, Vector3.one * (areaSize/1.5f));
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, areaSize/2);
    }

    //Instruction Text Control
    private bool showText = false;
    private int stateText = 0;
    private Color alphaText = new Color(0, 0, 0, 0.01f);

    void setInstructionText(string text)
    {
        instructionText.text = text;
        instructionText.color = new Color(1, 1, 1, 0);
        showText = true;
    }

    void displayInstructionText(float speed)
    {
        if (showText)
        {
            instructionText.color = instructionText.color + (alphaText * speed * Time.deltaTime);

            if (instructionText.color.a >= 1.0f)
                alphaText = alphaText * -1;

            if (instructionText.color.a <= 0.0f)
            {
                alphaText = alphaText * -1;
                showText = false;
            }
        }
    }

    //enemy control
    private bool playerFounTrinity = false;
    private bool spawnEnemy = false;
    private bool timeUp = false;
    private float ENEMY_SPAWN_TIME = 8;
    private float enemySpawnTime = 8;

    void isPlayerInEnemySpawnArea()
    {
        if (!spawnEnemy)
        {
            if (enemySpawnArea.Contains(player.transform.position))
            {
                if (!playerFounTrinity)
                {
                    playerFounTrinity = true;
                    stateMission = 1;
                }

                spawnEnemy = true;
            }
        }
        else
        {
            spawningEnemies();
        }
    }

    void spawningEnemies()
    {
        //if enemy spawning, time is not up and text is not shown
        if (spawnEnemy && !timeUp && !showText)
        {
            instructionText.text = timeTillRescue + "s";
            instructionText.color = Color.white;

            if (enemySpawnTime < 0)
            {
                Vector3 randomPosition = Vector3.zero;

                for (int i = 0; i < 5; i++)
                {
                    randomPosition = Random.onUnitSphere * (areaSize/2);

                    GameObject tmpEnemy = Instantiate<GameObject>(enemy);
                    tmpEnemy.transform.parent = transform;
                    tmpEnemy.transform.position = transform.position + randomPosition;
                    tmpEnemy.transform.rotation = Random.rotation;
                    if (player != null)
                        tmpEnemy.GetComponent<EnemyAI>().target = player.transform;
                }

                enemySpawnTime = ENEMY_SPAWN_TIME;
            }

            enemySpawnTime -= 1 * Time.deltaTime;
            timeTillRescue -= 1 * Time.deltaTime;

            if(timeTillRescue < 0)
            {
                timeUp = true;
                stateMission = 2;
            }
        }
    }

}
