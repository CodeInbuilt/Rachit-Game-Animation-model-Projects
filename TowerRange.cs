using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerRange : MonoBehaviour
{
    [SerializeField] private Tower Tower;
    private List<GameObject> targets = new List<GameObject>();

    void Start()
    {
        UpdateRange();
    }

    void Update()
    {
        if (targets.Count > 0)
        {
            if (Tower.first)
            {
              float minDistance = Mathf.Infinity;
                 int maxIndex = 0;
               GameObject firstTarget = null;

                foreach (GameObject target in targets)
                {
                int index = target.GetComponent<Enemy>().index;
                float distance = target.GetComponent<Enemy>().distance;

                if (index > maxIndex || (index == maxIndex && distance < minDistance))
                {
                    maxIndex = index;
                    minDistance = distance;
                    firstTarget = target;
                }
               }

                Tower.target = firstTarget;
            }
            else if (Tower.last)
            {

              float maxDistance = Mathf.Infinity;
                 int minIndex = int.MaxValue;
               GameObject lastTarget = null;

                foreach (GameObject target in targets)
                {
                int index = target.GetComponent<Enemy>().index;
                float distance = target.GetComponent<Enemy>().distance;

                if (index < minIndex || (index == minIndex && distance < maxDistance))
                {
                    minIndex = index;
                    maxDistance = distance;
                    lastTarget = target;
                }
               }

                Tower.target = lastTarget;

            }else if(Tower.strong)
            {
                
                 GameObject strongestTarget = null;
                float maxHealth = 0;

             foreach (GameObject target in targets)
             {
                     float health = target.GetComponent<Enemy>().health;

                if (health > maxHealth)
                {
               maxHealth = health;
              strongestTarget = target;
              }
             }

              Tower.target = strongestTarget;    
            }
            else
            {
                 Tower.target = targets[0];
            }
           
        }
        else
        {
            Tower.target = null;
        }
    }

    public void UpdateRange()
    {
        transform.localScale = new Vector3(Tower.range, Tower.range, Tower.range);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            targets.Add(collision.gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            targets.Remove(collision.gameObject);
        }
    }
}