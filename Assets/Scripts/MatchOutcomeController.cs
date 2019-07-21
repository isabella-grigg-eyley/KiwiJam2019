using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Color = CarriageDefinition.Color;

public class MatchOutcomeController : MonoBehaviour
{
    public static System.Action<bool> OnMatchOutcomeDecided = null;

    private void OnEnable()
    {
        GameScript.OnReadyToFight += OnReadyToFight;
    }

    private void OnReadyToFight(Beetle p1, Beetle p2)
    {
        Color p1Dom = p1.GetDominantColor();
        Color p2Dom = p2.GetDominantColor();

        Color outcome = DecideOutcome(p1Dom, p2Dom);

        bool p1wins = outcome == p1Dom;

        OnMatchOutcomeDecided?.Invoke(p1wins);
        return;
    }

    /// <summary>
    /// Selects who wins over the other
    /// </summary>
    /// <param name="p1"></param>
    /// <param name="p2"></param>
    /// <returns>Constant of the match</returns>
    public Color DecideOutcome(Color p1, Color p2)
    {
        // R -> G -> B -> R

        if (p1 == p2)
            return Color.None;

        // Player 1 is RED
        if (p1 == Color.Red)
        {
            if (p2 == Color.Blue)
                return p2;
            else
                return p1;
        }
        // Player 1 is GREEN
        else if (p1 == Color.Green)
        {
            if (p2 == Color.Red)
                return p2;
            else
                return p1;
        }
        // Player 1 is BLUE
        else if (p1 == Color.Blue)
        {
            if (p2 == Color.Green)
                return p2;
            else
                return p1;
        }
        else
        {
            return Color.None;
        }
    }

    public void CheckCarriages(List<Color> p1, List<Color> p2)
    {
        p1 = new List<Color>() { Color.Red, Color.Green, Color.Green };
        p2 = new List<Color>() { Color.Red, Color.Red, Color.Blue };

        List<Color> outcomes = new List<Color>();
        for (int i = 0; i < GameConstants.MAX_CARRIAGE_CAPACITY; ++i)
        {
            outcomes.Add(DecideOutcome(p1[i], p2[i]));
        }

        string s = "Outcome: ";
        foreach (Color c in outcomes)
        {
            s += string.Format("{0}, ", c);
        }

        Debug.Log(s);
    }

#if UNITY_EDITOR
    [ContextMenu("Test")]
    public void Test()
    {
        CheckCarriages(new List<Color>(), new List<Color>());

        Color p1 = Color.Red;
        Color p2 = Color.Blue;
        Color winner1 = TestMatch(p1, p2);

        p1 = Color.Red;
        p2 = Color.Green;
        winner1 = TestMatch(p1, p2);

        p1 = Color.Red;
        p2 = Color.None;
        winner1 = TestMatch(p1, p2);

        p1 = Color.Red;
        p2 = Color.Red;
        winner1 = TestMatch(p1, p2);

        // GREEN

        p1 = Color.Green;
        p2 = Color.Red;
        winner1 = TestMatch(p1, p2);

        p1 = Color.Green;
        p2 = Color.Blue;
        winner1 = TestMatch(p1, p2);

        p1 = Color.Green;
        p2 = Color.None;
        winner1 = TestMatch(p1, p2);

        p1 = Color.Green;
        p2 = Color.Green;
        winner1 = TestMatch(p1, p2);

        // BLUE

        p1 = Color.Blue;
        p2 = Color.Red;
        winner1 = TestMatch(p1, p2);

        p1 = Color.Blue;
        p2 = Color.Blue;
        winner1 = TestMatch(p1, p2);

        p1 = Color.Blue;
        p2 = Color.None;
        winner1 = TestMatch(p1, p2);

        p1 = Color.Blue;
        p2 = Color.Green;
        winner1 = TestMatch(p1, p2);
    }

    public Color TestMatch(Color p1, Color p2)
    {
        Color winner = DecideOutcome(p1, p2);
        Debug.Log(string.Format("Winner ({0} vs {1}) = {2}", p1, p2, winner));
        return winner;
    }
#endif
}
