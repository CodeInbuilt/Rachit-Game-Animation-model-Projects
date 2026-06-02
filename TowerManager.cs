using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TowerManager : MonoBehaviour
{
    [Header("Towers")]
    [SerializeField] private GameObject pistolTower;
    [SerializeField] private GameObject sniperTower;
    [SerializeField] private GameObject shotgunTower;
    [SerializeField] private GameObject machineGunTower;

    [SerializeField] private LayerMask TowerLayer;
    [SerializeField] private GameObject panel;
    [SerializeField] private TextMeshProUGUI towerName;
    [SerializeField] private TextMeshProUGUI towerLevel;
    [SerializeField] private TextMeshProUGUI UpgradeCost;
    [SerializeField] private TextMeshProUGUI towerTargeting;
    private GameObject placingTower;
    private GameObject SelectedTower;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ClearSelected();
        }

        if (placingTower)
        {
            TowerPlacement tp = placingTower.GetComponent<TowerPlacement>();
            if (tp != null && !tp.isPlacing)
            {
                placingTower = null;
            }
        }

        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit2D hit = Physics2D.Raycast(
                Camera.main.ScreenToWorldPoint(Input.mousePosition),
                Vector2.zero, 100f, TowerLayer);

            if (hit.collider != null)
            {
                // Hide previous tower range
                if (SelectedTower != null)
                {
                    GameObject range1 = SelectedTower.transform.GetChild(1).gameObject;
                    range1.GetComponent<SpriteRenderer>().enabled = false;
                }

                // Select new tower
                SelectedTower = hit.collider.gameObject;

                // Show new tower range
                GameObject range2 = SelectedTower.transform.GetChild(1).gameObject;
                range2.GetComponent<SpriteRenderer>().enabled = true;

                // Show panel
                panel.SetActive(true);

                // Update panel info
                UpdateSelected();

                // Update targeting text
                Tower tower = SelectedTower.GetComponent<Tower>();
                if (tower != null)
                {
                    if (tower.first)
                        towerTargeting.text = "First";
                    else if (tower.last)
                        towerTargeting.text = "Last";
                    else if (tower.strong)
                        towerTargeting.text = "Strong";
                    else
                        towerTargeting.text = "Weak";
                }
            }
            else
            {
                // Clicked empty space, hide panel
                if (SelectedTower != null)
                {
                    GameObject range1 = SelectedTower.transform.GetChild(1).gameObject;
                    range1.GetComponent<SpriteRenderer>().enabled = false;
                    SelectedTower = null;
                }
                panel.SetActive(false);
            }
        }
    }

    private void ClearSelected()
    {
        if (placingTower)
        {
            Destroy(placingTower);
            placingTower = null;
        }
    }

    public void setTower(GameObject tower)
    {
        ClearSelected();
        placingTower = Instantiate(tower);
    }

    public void UpdateSelected()
    {
        if (SelectedTower)
        {
            towerName.text = SelectedTower.name.Replace("(Clone)", "").Trim();

            TowerUpgrades tu = SelectedTower.GetComponent<TowerUpgrades>();
            if (tu != null)
            {
                // FIXED: use GetDisplayLevel() instead of currentLevel.ToString()
                towerLevel.text = "Tower LVL: " + tu.GetDisplayLevel();
                UpgradeCost.text = tu.currentCost;
            }
            else
            {
                Debug.LogWarning("TowerUpgrades missing on: " + SelectedTower.name);
                towerLevel.text = "Tower LVL: N/A";
                UpgradeCost.text = "N/A";
            }
        }
    }

    public void ChangeTargetting()
    {
        if (SelectedTower)
        {
            Tower tower = SelectedTower.GetComponent<Tower>();

            if (tower.first)
            {
                tower.first = false;
                tower.last = true;
                tower.strong = false;
                towerTargeting.text = "Last";
            }
            else if (tower.last)
            {
                tower.first = false;
                tower.last = false;
                tower.strong = true;
                towerTargeting.text = "Strong";
            }
            else if (tower.strong)
            {
                tower.first = true;
                tower.last = false;
                tower.strong = false;
                towerTargeting.text = "First";
            }
        }
    }
}