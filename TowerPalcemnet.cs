using System;
using UnityEngine;

public class TowerPlacement : MonoBehaviour
{
    [SerializeField] private SpriteRenderer rangeSprite;
    [SerializeField] private CircleCollider2D rangeCollider;
    [SerializeField] private Color gray;
    [SerializeField] private Color red;

    // ADD THIS - separate click collider
    [SerializeField] private CircleCollider2D clickCollider;

    [NonSerialized] public bool isPlacing = true;

    private bool isRestricted = false;
    private Tower tower;

    void Awake()
    {
        if (rangeCollider != null)
            rangeCollider.enabled = false;

        // Disable click collider while placing
        if (clickCollider != null)
            clickCollider.enabled = false;

        tower = GetComponent<Tower>();

        if (tower == null)
            Debug.LogError("Tower component not found on " + gameObject.name);
    }

    void Update()
    {
        if (isPlacing)
        {
            Vector2 mousePosition =
                Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position = mousePosition;
        }

        if (Input.GetMouseButtonDown(1) && !isRestricted)
        {
            // Enable trigger collider for enemy detection
            if (rangeCollider != null)
                rangeCollider.enabled = true;

            // Enable non-trigger collider for click detection
            if (clickCollider != null)
                clickCollider.enabled = true;

            isPlacing = false;

            if (rangeSprite != null)
                rangeSprite.enabled = false;

            if (Player.main != null && tower != null)
                Player.main.money -= tower.cost;

            enabled = false;
        }

        if (rangeSprite != null)
        {
            rangeSprite.color = isRestricted ? red : gray;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ((collision.CompareTag("Restricted") ||
             collision.CompareTag("Tower")) &&
             isPlacing)
        {
            isRestricted = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if ((collision.CompareTag("Restricted") ||
             collision.CompareTag("Tower")) &&
             isPlacing)
        {
            isRestricted = false;
        }
    }
}