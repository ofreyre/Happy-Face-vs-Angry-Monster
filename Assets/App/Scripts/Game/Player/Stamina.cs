using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stamina : MonoBehaviour {

    public RectTransform m_rectMax;
    public Transform m_staminaImage;
    [SerializeField]
    public float m_valueMax;
    float m_scaleK;
    Vector3 m_scale = Vector3.zero;
    public float m_value;
    

    public static Stamina instance;

    void Awake()
    {
        instance = this;
        ValueMax = m_valueMax;
        value = m_value;
    }
    


    public float ValueMax {
        set {
            m_valueMax = value;
            if (m_scale == Vector3.zero)
            {
                m_scale = m_staminaImage.localScale;
            }

            float canvasK = transform.parent.GetComponent<CanvasScaler>().referenceResolution.y / transform.parent.GetComponent<RectTransform>().rect.height;
            float canvasW = transform.parent.GetComponent<RectTransform>().rect.width * canvasK;
            float rectMaxW = canvasW + m_rectMax.offsetMax.x - m_rectMax.offsetMin.x;
            float staminaW = m_staminaImage.GetComponent<RectTransform>().rect.width;
            //Debug.Log(m_staminaImage.GetComponent<RectTransform>().rect);
            m_scaleK = rectMaxW / staminaW;
            //Debug.Log("m_valueMax "+ m_valueMax);
        }

        get {
            return m_valueMax;
        }
    }
    
    public float value {
        set {
            //Debug.Log("fffffffffffffff");
            m_value = value;
            m_staminaImage.localScale = new Vector3(m_scaleK * value / m_valueMax, m_scale.y, m_scale.z);
        }
    }

    public void Refresh()
    {
        value = m_value;
    }
}
