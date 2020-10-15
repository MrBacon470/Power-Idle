using UnityEngine;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{
    public IdleGame game;
    public Text notationTypeText;
    /*
     * Notation Key:
     * 0 = Sci
     * 1 = Eng
     * 2 = Letter
     * 3 = Word
     */

    private void Start()
    {
        UpdateNotationText();
    }

    private void UpdateNotationText()
    {
        var note = game.data.notationType;
        switch (note)
        {
            case 0:
                notationTypeText.text = "Notation:Scientific";
                break;
            case 1:
                notationTypeText.text = "Notation:Engineering";
                break;
            case 2:
                notationTypeText.text = "Notation:Word";
                break;
        }
    }

    public void ChangeNotation()
    {
        var note = game.data.notationType;

        switch (note)
        {
            case 0:
                note = 1;
                break;
            case 1:
                note = 2;
                break;
            case 2:
                note = 0;
                break;
        }
        game.data.notationType = note;
        Methods.NotationSettings = note;
        UpdateNotationText();
    }
}