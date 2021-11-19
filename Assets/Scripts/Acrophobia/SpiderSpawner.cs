using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using TMPro;

public class SpiderSpawner : MonoBehaviour
{

    struct SpiderLevel
    {
        public string introText;
        public int numberOfSpiders;
        public float speed;
        public float scale;
        public int interval;

        public SpiderLevel(string introText, int numberOfSpiders, float speed, float scale, int interval)
        {
            this.introText = introText;
            this.numberOfSpiders = numberOfSpiders;
            this.speed = speed;
            this.scale = scale;
            this.interval = interval;
        }
    }

    [SerializeField] private GameObject spiderPrefab;
    [SerializeField] private GameObject player;
    [SerializeField] private float spawnRadius = 5.0f;
    [SerializeField] private TextMeshProUGUI levelText;

    private const float levelStartDelay = 3f;
    private const float levelEndDelay = 20f;

    private List<SpiderLevel> spiderLevels = new List<SpiderLevel>()
    {
        new SpiderLevel("LEVEL 1 - SMALL SPIDERS", 7, 2, 0.3f, 2),
        new SpiderLevel("LEVEL 2 - MEDIUM SPIDERS", 5, 3, 0.6f, 3),
        new SpiderLevel("LEVEL 3 - BIG SPIDERS", 3, 1, 3f, 5),
    };
    private List<GameObject> spiders = new List<GameObject>();
    private int currentLevel = 0;
    // Start is called before the first frame update
    void Start()
    {
        increaseLevel();
    }

    private void increaseLevel()
    {
        currentLevel += 1;
        removeAllSpiders();
        if (currentLevel > spiderLevels.Count)
        {
            levelText.text = "GAME OVER";
            Color color = levelText.color;
            color.a = 1f;
            levelText.color = color;
            return;
        }
        displayLevelTexts(spiderLevels[currentLevel - 1].introText);
        StartCoroutine(spiderWave());
    }

    private void displayLevelTexts(string text)
    {
        levelText.text = text;
        Color color = levelText.color;
        color.a = 1f;
        updateTextColor(color);
        Color fadeoutcolor = color;
        fadeoutcolor.a = 0f;
        LeanTween.value(gameObject, updateTextColor, color, fadeoutcolor, 2f).setEase(LeanTweenType.easeOutQuad).setDelay(levelStartDelay);
    }

    private void updateTextColor(Color color)
    {
        levelText.color = color;
    }

    private void spawnSpider(SpiderLevel spiderLevel)
    {
        GameObject spider = Instantiate(spiderPrefab) as GameObject;
        spider.transform.localScale = new Vector3(spiderLevel.scale, spiderLevel.scale, spiderLevel.scale);
        
        // set position of the new spider in a radius of the player
        Vector3 randomDirection = Random.insideUnitSphere * spawnRadius;
        randomDirection += player.transform.position;
        NavMeshHit hit;
        NavMesh.SamplePosition(randomDirection, out hit, spawnRadius, 1);
        Vector3 finalPosition = hit.position;
        spider.transform.position = finalPosition;

        spiders.Add(spider);
    }

    private void removeAllSpiders()
    {
        foreach(var spider in spiders)
        {
            Destroy(spider);
        }
        spiders.Clear();
    }

    IEnumerator spiderWave()
    {
        yield return new WaitForSeconds(levelStartDelay);
        SpiderLevel spiderLevel = spiderLevels[currentLevel - 1];
        for (int i = 0; i < spiderLevel.numberOfSpiders; i++)
        {
            spawnSpider(spiderLevel);
            yield return new WaitForSeconds(spiderLevel.interval);
        }
        yield return new WaitForSeconds(levelEndDelay);
        increaseLevel();
    }

}
