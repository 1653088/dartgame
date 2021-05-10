using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIWinGame : MonoBehaviour
{
    public Text ScoreWinGame;
    private void Update( )
    {
        if(DartGameManager.AmountDart == 0)
        {
            //cooldown 3s
            ScoreWinGame.text = UIGameplay.SumScore.ToString( );
        }
    }

    public void ClickPlayGame( )
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene( ).name);
    }
}
