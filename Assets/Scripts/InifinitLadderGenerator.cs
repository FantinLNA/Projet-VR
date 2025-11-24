using System.Collections.Generic;
using UnityEngine;

public class InfinitLadderGenerator : MonoBehaviour
{
    [Header("Setup")]
    public GameObject ladderPrefab;
    public Transform player;
    public float ladderHeight = 3f;
    public int laddersAbove = 10;
    public int laddersBelow = 10;

    private Dictionary<int, GameObject> ladderSections = new Dictionary<int, GameObject>();

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        UpdateLadder();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateLadder();
    }

    void UpdateLadder()
    {
        int playerIndex = Mathf.RoundToInt(player.position.y / ladderHeight);
        int minIndex = playerIndex - laddersBelow;
        int maxIndex = playerIndex + laddersAbove;
        for (int i = minIndex; i <= maxIndex; i++)
        {
            if (!ladderSections.ContainsKey(i))
            {
                Vector3 newPosition = new Vector3(0, i * ladderHeight, 0);
                GameObject newLadder = Instantiate(
                    ladderPrefab,
                    newPosition,
                    Quaternion.identity,
                    transform
                );
                ladderSections.Add(i, newLadder);
            }
        }

        List<int> toRemove = new List<int>();
        foreach (var keyValuePair in ladderSections)
        {
            int index = keyValuePair.Key;
            if (index < minIndex || index > maxIndex)
            {
                Destroy(keyValuePair.Value);
                toRemove.Add(index);
            }
        }
        foreach (int index in toRemove)
        {
            ladderSections.Remove(index);
        }
    }
}
