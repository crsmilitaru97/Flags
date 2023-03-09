using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Languages : MonoBehaviour
{
    public ObjText[] textsToChange;
    public Button[] langButtons;

    public static Dictionary<string, string> selectedLanguage = en;

    private void Start()
    {
        ChangeLanguage(PlayerPrefs.GetString("language", "en"));
    }

    public void ChangeLanguage(string language)
    {
        if (language.Equals("ro"))
        {
            if (langButtons.Length > 0)
            {
                langButtons[0].interactable = false;
                langButtons[1].interactable = true;
            }

            selectedLanguage = ro;
        }
        else
        {
            if (langButtons.Length > 0)
            {
                langButtons[0].interactable = true;
                langButtons[1].interactable = false;
            }

            selectedLanguage = en;
        }

        foreach (ObjText obj in textsToChange)
        {
            foreach (Text txt in obj.text)
                txt.text = selectedLanguage[obj.name];
        }

        PlayerPrefs.SetString("language", language);
    }

    [Serializable]
    public class ObjText
    {
        public string name;
        public Text[] text;
    }


    public static Dictionary<string, string> en = new Dictionary<string, string>()
        {
            { "newGame", "New Game" },
            { "teams", "Teams" },
            { "rules", "Rules" },
            { "SETTINGS", "SETTINGS" },
            { "RULES", "RULES" },
            { "settings", "Settings"  },
            { "back", "Back"  },
            { "time", "Time"  },
            { "language", "Language"  },
            { "mime", "Mime"  },
            { "draw", "Draw"  },
            { "explain", "Explain"  },
            { "rulesText", "Each team has one meeple, moving over the board. Their goal is to reach the finish line first. One player has to act (pantomime) or describe or paint a phrase. The phrase is given on a card and the form of presenting it is shown on the field where the team’s meeple is currently standing. The other members of his team need to find out what that phrase should be. If they manage to do so within the given amount of time, their meeple moves forward. If not, the other team can also start guessing and get the point, too. The team whose meeple crosses the finish line first wins the game."},
            { "drawCard", "Draw a card"  },
            { "cards", "Cards"  },
            { "map", "Board"  },
            { "skip", "Skip"  },
            { "done", "Done"  },
            { "_everyone", "All teams can answer"},
            { "_justMime", "Just mime"},
            { "_justDraw", "Just draw"},
            { "_justExplain", "Just explain"},
            { "_ready", "Ready?"},
            { "_go", "Go!"},
            { "_1step", "1 step"},
            { "_2steps", "2 steps"},
            { "_littleTime", "Little time left!"},
            { "_wins", "wins"},
            { "_purple", "Purple"},
            { "_red", "Red"},
            { "_blue", "Blue"},
            { "_yellow", "Yellow"},
            { "pause", "Pause"},
            { "_timeOut","Time is up!" }


    };

    public static Dictionary<string, string> ro = new Dictionary<string, string>()
        {
            { "newGame", "Joc Nou" },
            { "teams", "Echipe" },
            { "rules", "Reguli" },
            { "SETTINGS", "SETĂRI" },
            { "RULES", "REGULI" },
            { "settings", "Setări" },
            { "back", "Înapoi"  },
            { "time", "Timp"  },
            { "language", "Limbă"  },
            { "mime", "Mimează"  },
            { "draw", "Desenează"  },
            { "explain", "Descrie"  },
            { "rulesText", " Jucătorii trebuie să descrie, să mimeze, sau să deseneze cuvinte și fraze. Astfel, la joc pot participa 2-4 echipe, iar fiecare echipă trebuie să aibă minim 2 jucători.  Uneori o să prezinți conceptul de pe cartea ta doar membrilor echipei tale, alteori toată lumea va încerca să ghicească conceptul prezentat. Pentru asta, ai la dispoziție 60 de secunde! Dacă membrii echipei tale ghicesc conceptul în primele 30 de secunde, atunci o să avansați 2 câmpuri pe tabla de joc. Dacă ghicesc în ultimele 30 de secunde, atunci o să avansați doar 1 câmp. Echipa care ajunge prima pe câmpul de sosire, sau îl depășește, este declarată câștigătoare."},
            { "drawCard", "Trage o carte"  },
            { "cards", "Cărți"  },
            { "map", "Tablă"  },
            { "skip", "Sari"  },
            { "done", "Gata"  },
            { "_everyone", "Toți pot răspunde"},
            { "_justMime", "Doar mimă"},
            { "_justDraw", "Doar desen"},
            { "_justExplain", "Doar descriere"},
            { "_ready", "Pe locuri?"},
            { "_go", "Start!"},
            { "_1step", "1 pas"},
            { "_2steps", "2 pași"},
            { "_littleTime", "Puțin timp rămas!"},
            { "_wins", "câștigă"},
            { "_purple", "Mov"},
            { "_red", "Roșu"},
            { "_blue", "Albastru"},
            { "_yellow", "Galben"},
            { "pause", "Pauză"},
            { "_timeOut","S-a terminat timpul!" }
    };
}
