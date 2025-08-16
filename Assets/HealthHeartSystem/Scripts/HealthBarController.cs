/*
 *  Author: ariel oliveira [o.arielg@gmail.com]
 */

using UnityEngine;
using UnityEngine.UI;

public class HealthBarController : MonoBehaviour
{
    private GameObject[] heartContainers;
    private RawImage[] heartFills;

    public PlayerHealth playerHealth;
    public Transform heartsParent;
    public GameObject heartContainerPrefab;

    private void Start()
    {
        // Should I use lists? Maybe :)
        heartContainers = new GameObject[playerHealth.maxHealth];
        heartFills = new RawImage[playerHealth.maxHealth];

        playerHealth.TakeDamageEvent.AddListener(UpdateHeartsHUD);
        playerHealth.HealEvent.AddListener(UpdateHeartsHUD);
        
        InstantiateHeartContainers();
        UpdateHeartsHUD();
    }

    public void UpdateHeartsHUD()
    {
        SetHeartContainers();
        SetFilledHearts();
    }

    void SetHeartContainers()
    {
        for (int i = 0; i < heartContainers.Length; i++)
        {
            if (i < playerHealth.maxHealth)
            {
                heartContainers[i].SetActive(true);
            }
            else
            {
                heartContainers[i].SetActive(false);
            }
        }
    }

    void SetFilledHearts()
    {
        for (int i = 0; i < heartFills.Length; i++)
        {
            if (i < playerHealth.GetCurrentHealth())
            {
                heartFills[i].enabled = true;
            }
            else
            {
                heartFills[i].enabled = false;
            }
        }

        if (playerHealth.GetCurrentHealth() % 1 != 0)
        {
            int lastPos = Mathf.FloorToInt(playerHealth.GetCurrentHealth());
            //heartFills[lastPos].enabled = playerHealth.GetCurrentHealth() % 1;
        }
    }

    void InstantiateHeartContainers()
    {
        for (int i = 0; i < playerHealth.maxHealth; i++)
        {
            GameObject temp = Instantiate(heartContainerPrefab);
            temp.transform.SetParent(heartsParent, false);
            heartContainers[i] = temp;
            heartFills[i] = temp.transform.Find("HeartFill").GetComponent<RawImage>();
        }
    }
}
