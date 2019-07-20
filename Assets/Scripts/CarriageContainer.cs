using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CarriageContainer : MonoBehaviour
{
    [System.Serializable]
    public enum State
    {
        Idle,
        Selectable
    }

    private Button m_button = null;

    private CarriageDefinition m_carriageDefinition = null;

    private Image m_image = null;

    public Button Button
    {
        get { return m_button; }
    }

    void Awake()
    {
        m_image = GetComponent<Image>();
        m_button = GetComponent<Button>();
    }

    public void Init(CarriageDefinition c)
    {
        m_carriageDefinition = c;
    }
}
