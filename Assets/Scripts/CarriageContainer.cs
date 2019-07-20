using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CarriageContainer : MonoBehaviour
{
    [SerializeField]
    private Image m_image = null;

    [SerializeField]
    private CarriageDefinition m_carriageDefinition = null;

    void Awake()
    {
        if (!m_image)
            m_image = GetComponent<Image>();
    }

    public void Init(CarriageDefinition c)
    {
        m_carriageDefinition = c;
    }

    private void OnValidate()
    {

    }
}
