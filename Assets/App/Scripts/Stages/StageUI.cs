using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageUI : FillStar
{
    public Transform m_star;
    public RectTransform m_mask;

    public override void SetStars(int amount, int of) {
        base.SetStars(amount, of);
        float y = m_mask.rect.height / of * amount;
        m_star.transform.localPosition = new Vector3(m_star.transform.localPosition.x, y, m_star.transform.localPosition.z);
    }
}
