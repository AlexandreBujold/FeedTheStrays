using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class CatUI : MonoBehaviour
{
    public Cat m_cat;

    [Space]
    public Slider slider;
    public TextMeshProUGUI hungerValueText;

    // Update is called once per frame
    void Update()
    {
        if (m_cat != null && hungerValueText != null && slider != null)
        {
            hungerValueText.SetText(m_cat.hunger.ToString());
            slider.value = (m_cat.hunger / m_cat.maxHunger);
        }
    }
}
