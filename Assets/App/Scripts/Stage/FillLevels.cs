using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using AudioManagement;

public class FillLevels : MonoBehaviour
{
    public GameObject m_itemPrefab;
    public Globals m_globals;
    public GlobalFlow m_flow;
    public Color[] m_colors;
    public ButtonPreLoadLevel m_btnLoadLevel;
    public string m_selectSFX;

    // Use this for initialization
    public int Fill () {
        GameObject gobj;
        DBlevels levels = DBmanager.GetLevels();
        int[] stars = levels.stars;
        bool[] unlocked = levels.unlocked;
        int firstLevel = m_flow.stage * m_globals.levelsPerStage;
        LevelUI levelItem;
        int starsC = 0, starsTotal = 0;
        Color color = m_colors[m_flow.stage];
        Button btn;
        for (int i = 0, n = m_globals.levelsPerStage; i < n; i++) {
            gobj = Instantiate(m_itemPrefab);
            gobj.transform.SetParent(transform);
            gobj.transform.localScale = Vector3.one;
            levelItem = gobj.GetComponent<LevelUI>();
            levelItem.number = (i + 1).ToString();
            levelItem.frameColor = color;
            starsC = stars[firstLevel + i];
            starsTotal += starsC;
            levelItem.SetStars(starsC, 3);
            btn = gobj.GetComponent<Button>();
            SetListener(btn, firstLevel + i);
            btn.interactable = unlocked[firstLevel + i];
        }
        return starsTotal;
    }

    void SetListener(Button btn, int level) {
        btn.onClick.AddListener(() => m_btnLoadLevel.Load(level));
        btn.onClick.AddListener(() => PlayConsume());
    }

    void PlayConsume() {
        AudioManager.instance.Play(m_selectSFX);
    }
}
