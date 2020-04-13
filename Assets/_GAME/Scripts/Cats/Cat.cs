using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cat : MonoBehaviour
{
    public Animator m_animator;
    public float hunger = 50;
    public float maxHunger = 100;

    public GameObject canvasPrefab;
    private CatUI m_catUI;

    private void Start()
    {
        if (canvasPrefab != null)
        {
            m_catUI = Instantiate(canvasPrefab, transform.position + new Vector3(0, 1, 0), Quaternion.identity, transform).GetComponentInChildren<CatUI>();
            if (m_catUI != null)
            {
                m_catUI.m_cat = this;
                m_catUI.gameObject.transform.localScale = Vector3.one * 0.01f;
                Canvas catCanvas = m_catUI.GetComponent<Canvas>();
                if (catCanvas != null)
                {
                    catCanvas.worldCamera = Camera.main;
                }
            }
        }
    }

    public bool dead;

    public void Feed(float amount)
    {
        hunger = Mathf.Clamp(hunger - amount, 0, maxHunger);
        //Debug.Log(gameObject.name + " has been fed. Current hunger: " + hunger.ToString());
        if (m_animator != null)
        {
            m_animator.SetBool("Eating", true);
            Invoke("SetEatingFalse", 1f);
        }
    }

    private void SetEatingFalse()
    {
        if (m_animator != null)
        {
            m_animator.SetBool("Eating", false);
        }
    }

    public void IncreaseHunger()
    {
        IncreaseHunger(1f);
    }

    public void IncreaseHunger(float amount)
    {
        hunger = Mathf.Clamp(hunger + amount, 0, maxHunger);

        if (hunger >= maxHunger && dead == false)
        {
            CatDead();
        }
    }

    public void CatDead()
    {
        dead = true;
        gameObject.transform.eulerAngles = new Vector3(gameObject.transform.eulerAngles.x, gameObject.transform.eulerAngles.y, 90);
        if (m_animator != null)
        {
            m_animator.SetBool("Dead", true);
        }
    }
}
