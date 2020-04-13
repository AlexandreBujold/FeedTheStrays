using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cat : MonoBehaviour
{
    public Animator m_animator;
    public float hunger = 50;
    public float maxHunger = 100;

    public bool dead;

    public void Feed(float amount)
    {
        hunger = Mathf.Clamp(hunger - amount, 0, maxHunger);

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
        hunger = Mathf.Clamp(hunger + 1, 0, maxHunger);

        if (hunger >= maxHunger)
        {
            CatDead();
        }
    }

    public void CatDead()
    {
        dead = true;

        if (m_animator != null)
        {
            m_animator.SetBool("Dead", true);
        }
    }
}
