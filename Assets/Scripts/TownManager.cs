using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TownManager : MonoBehaviour
{
    [SerializeField] private List<TownVersion> townVersions = new List<TownVersion>();
    private int currentVersionIndex = 0;

    private TownUIManager UImanager;

    void Awake()
    {
        UImanager = GetComponent<TownUIManager>();
    }

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

        if (currentVersionIndex == townVersions.Count - 1)
        {
            // accendi pannello finale
            UImanager.ActivateEndGamePopup(true);
        }
        else
        {
            UImanager.ActivateTownUI(true);
        }
    }

}