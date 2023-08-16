using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TownManager : MonoBehaviour
{
    [SerializeField] private List<TownVersion> townVersions = new List<TownVersion>();
    private int currentVersionIndex = 0;

    void Start()
    {
        townVersions[currentVersionIndex].image.SetActive(true);
    }

    public void UpdateTownVersion(int coins)
    {
        // ugly but works
        for (int i = townVersions.Count-1; i >= 0; i--)
        {
            if (coins > townVersions[i].coinThreshold)
            {
                if (currentVersionIndex != i)
                {
                    townVersions[currentVersionIndex].image.SetActive(false);
                    townVersions[i].image.SetActive(true);
                    currentVersionIndex = i;
                }
                break;
            }
        }
    }

}