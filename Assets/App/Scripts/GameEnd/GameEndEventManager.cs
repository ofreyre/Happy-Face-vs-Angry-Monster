using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEndEventManager : MonoBehaviour
{
    public delegate void DelegateScoreEnd();
    public DelegateScoreEnd ScoreEnd;
    public delegate void DelegateStarsEnd(int stars);
    public DelegateStarsEnd StarsEnd;
    public delegate void DelegateAddCoin(int score);
    public DelegateAddCoin AddCoin;

    public void DispatchScoreEnd()
    {
        if (ScoreEnd != null)
        {
            ScoreEnd();
        }
    }

    public void DispatchStarsEnd(int stars)
    {
        if (StarsEnd != null)
        {
            StarsEnd(stars);
        }
    }

    public void DispatchAddCoin(int score)
    {
        if (AddCoin != null)
        {
            AddCoin(score);
        }
    }
}
