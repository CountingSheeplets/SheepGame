using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudGenerator : MonoBehaviour {
    public int maxClouds = 5;
    public GameObject cloudPrefab;
    //public bool right = true;
    public float maxY = 5;

    [SerializeField]
    [Range(0.01F, 2.0F)]
    float cloudSpeedBase;
    [SerializeField]
    [Range(0.01F, 1.0F)]
    float cloudSpeedRandomDelta;

    [SerializeField]
    [Range(0.01F, 1.0F)]
    float spawnChance = 0.3f;

    [SerializeField]
    [Range(0.01F, 1.0F)]
    float sizeDelta = 0.3f;

    [SerializeField]
    Sprite[] sprites;

    void Start() {
        EventCoordinator.StartListening(EventName.System.Sheep.Roam(), OnSpawnCloud);
        EventCoordinator.StartListening(EventName.Input.StartGame(), SpawnStartingClouds);
    }
    private void OnDestroy() {
        EventCoordinator.StopListening(EventName.System.Sheep.Roam(), OnSpawnCloud);
    }
    void OnSpawnCloud(GameMessage msg) {
        if (gameObject.activeInHierarchy)
            if (Random.Range(0, 1f) < spawnChance)
                if (transform.childCount < maxClouds) {
                    GameObject newCloud = Instantiate(cloudPrefab, transform);
                    float sizeMultiplier = 1 + Random.Range(-sizeDelta, sizeDelta);
                    newCloud.GetComponentInChildren<SpriteRenderer>().transform.localScale = sizeMultiplier * Vector3.one;

                    Cloud cloud = newCloud.GetComponent<Cloud>();
                    //cloud.SetDir(right);
                    float randomY = Random.Range(-maxY, maxY);
                    newCloud.transform.localPosition = new Vector2(0, randomY);
                    float randomSpeed = Random.Range(cloudSpeedBase - cloudSpeedRandomDelta, cloudSpeedBase + cloudSpeedRandomDelta);
                    cloud.StartFloating(randomSpeed, new Vector2(-transform.localPosition.x, randomY));
                    newCloud.GetComponentInChildren<SpriteRenderer>().sprite = sprites[Random.Range(0, sprites.Length)];
                }
    }

    void SpawnStartingClouds(GameMessage msg) {
        for (int i = 0; i < 2; i++) {
            GameObject newCloud = Instantiate(cloudPrefab, transform);
            float sizeMultiplier = 1 + Random.Range(-sizeDelta, sizeDelta);
            newCloud.GetComponentInChildren<SpriteRenderer>().transform.localScale = sizeMultiplier * Vector3.one;

            Cloud cloud = newCloud.GetComponent<Cloud>();
            float randomY = Random.Range(-maxY, maxY);
            float xOffset = Random.Range(-3, 3);
            newCloud.transform.localPosition = new Vector2(-transform.localPosition.x + xOffset, randomY);
            float randomSpeed = Random.Range(cloudSpeedBase - cloudSpeedRandomDelta, cloudSpeedBase + cloudSpeedRandomDelta);
            cloud.StartFloating(randomSpeed, new Vector2(-transform.localPosition.x * 2, randomY));
            newCloud.GetComponentInChildren<SpriteRenderer>().sprite = sprites[Random.Range(0, sprites.Length)];
        }
    }
}