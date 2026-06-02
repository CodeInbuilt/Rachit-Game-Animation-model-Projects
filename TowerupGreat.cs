using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerUpgrades : MonoBehaviour
{
    [System.Serializable]
    public class Level
    {
        public float range = 8f;
        public int damage = 25;
        public float fireRate = 1f;
        public int cost = 100;
    }

    [SerializeField] public Level[] levels = new Level[3]
    {
        new Level { range = 8f, damage = 25, fireRate = 1f, cost = 100 },
        new Level { range = 10f, damage = 50, fireRate = 1.5f, cost = 200 },
        new Level { range = 12f, damage = 75, fireRate = 2f, cost = 300 }
    };

    public int currentLevel = 0;
    public string currentCost;

    private Tower tower;
    [SerializeField] public TowerRange towerRange;

    void Awake()
    {
        tower = GetComponent<Tower>();
        RefreshCost();
    }

    void Start()
    {
        // Double check in Start in case Awake order causes issues
        RefreshCost();
    }

    private void RefreshCost()
    {
        if (levels != null && levels.Length > 0 && levels[0] != null)
            currentCost = levels[0].cost.ToString();
        else
            currentCost = "N/A";
    }

    public void Upgrade()
    {
        if (currentLevel < levels.Length && levels[currentLevel] != null)
        {
            if (levels[currentLevel].cost <= Player.main.money)
            {
                tower.range = levels[currentLevel].range;
                tower.damage = levels[currentLevel].damage;
                tower.fireRate = levels[currentLevel].fireRate;
                towerRange.UpdateRange();

                Player.main.money -= levels[currentLevel].cost;
                currentLevel++;

                if (currentLevel >= levels.Length)
                {
                    currentCost = "MAX";
                }
                else
                {
                    currentCost = levels[currentLevel].cost.ToString();
                }

                Debug.Log("Upgraded to level: " + GetDisplayLevel());
            }
            else
            {
                Debug.Log("Not enough money. Need: " + levels[currentLevel].cost + " Have: " + Player.main.money);
            }
        }
        else
        {
            Debug.Log("Already MAX level");
        }
    }

    public string GetDisplayLevel()
    {
        return currentLevel >= levels.Length ? "MAX" : (currentLevel + 1).ToString();
    }
}