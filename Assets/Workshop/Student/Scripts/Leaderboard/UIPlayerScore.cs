using Searching;
using System.Diagnostics;
using TMPro;
using UnityEngine;

[DebuggerDisplay("{" + nameof(GetDebuggerDisplay) + "(),nq}")]
public class UIPlayerScore : MonoBehaviour
{
    public TMP_Text Text;

    public void SetUpTextScore(PlayerScore txt)
    {
        Text.text = txt.playerName + " " + txt.score;
    }

    private string GetDebuggerDisplay()
    {
        return ToString();
    }
}