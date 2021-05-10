using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIGameplay : MonoBehaviour
{
    public Text AmountDart;
    public Text ScoreText;
    public Text SumScoreText;
    public Text BonusText;
    public static string Score;
    public static string SumScore;
    public static string Bonus;
    [SerializeField]
    private GameObject _uiWinGame = null;
    
    public static bool IsThrown =false;

    private void Start( )
    {
        AmountDart.text = "Number of Darts: " + "5";
    }
    private void Update( )
    {
        AmountDart.text = "Number of Darts: " + DartGameManager.AmountDart.ToString( );
        if(IsThrown)
        {
            SumScoreText.text = SumScore;

            ScoreText.text = Score;
            ScoreText.gameObject.SetActive(true);

            BonusText.text = Bonus;
            BonusText.gameObject.SetActive(true);
            IsThrown = false;
            StartCoroutine(CoolDownShowScoreInGame( ));
        }
        StartCoroutine(TimerShow( ));
    }
    private IEnumerator CoolDownShowScoreInGame( )
    {
        yield return new WaitForSeconds(2);
        ScoreText.gameObject.SetActive(false);
        BonusText.gameObject.SetActive(false);
    }

    private IEnumerator TimerShow( )
    {
        if(DartGameManager.AmountDart == 0)
        {
            yield return new WaitForSeconds(2.5f);
            _uiWinGame.SetActive(true);
        }
    }
}
