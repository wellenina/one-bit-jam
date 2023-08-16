using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TownManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> townVersions = new List<GameObject>();
    [SerializeField] private List<int> coinThresholds = new List<int>();
    private int currentVersionIndex = 0;

    void Start()
    {
        townVersions[currentVersionIndex].SetActive(true);
    }

    public void UpdateTownVersion(int coins)
    {
        // ugly but works
        for (int i = coinThresholds.Count-1; i >= 0; i--)
        {
            if (coins > coinThresholds[i])
            {
                if (currentVersionIndex != i)
                {
                    townVersions[currentVersionIndex].SetActive(false);
                    townVersions[i].SetActive(true);
                    currentVersionIndex = i;
                }
                break;
            }
        }
    }

}