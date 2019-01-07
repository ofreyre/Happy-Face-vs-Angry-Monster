using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FillStar : MonoBehaviour
{
    public Text m_stars;

    public virtual void SetStars(int amount, int of)
    {
        m_stars.text = amount.ToString() + "/" + of.ToString();
    }
}
