using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UITexts : MonoBehaviour
{
    public ObjText[] textsToChange;

    public Button[] langButtons;

    public static Dictionary<string, string> selectedLanguage = UITexts.en;


    [Serializable]
    public class ObjText
    {
        public string name;
        public Text text;
    }

    #region Basic Events
    void Awake()
    {
        string settedLanguage = PlayerPrefs.GetString("language", null);
        if (!string.IsNullOrEmpty(settedLanguage))
        {
            if (Application.systemLanguage == SystemLanguage.Romanian)
            {
                settedLanguage = "ro";
                selectedLanguage = UITexts.ro;
            }
            else if (Application.systemLanguage == SystemLanguage.French)
            {
                settedLanguage = "fr";
                selectedLanguage = UITexts.fr;
            }
            else if (Application.systemLanguage == SystemLanguage.Spanish)
            {
                settedLanguage = "es";
                selectedLanguage = UITexts.es;
            }
            else
            {
                settedLanguage = "en";
                selectedLanguage = UITexts.en;
            }
        }
        if (SceneManager.GetActiveScene().buildIndex == 0)
            ChangeLanguage(settedLanguage);
    }

    void Start()
    {
        foreach (ObjText obj in textsToChange)
        {
            obj.text.text = selectedLanguage[obj.name];
        }
    }
    #endregion

    #region Helpers
    public void ChangeLanguage(string language)
    {
        if (language.Equals("ro"))
        {
            foreach (var button in langButtons)
            {
                button.interactable = true;
            }
            langButtons[2].interactable = false;

            selectedLanguage = UITexts.ro;
        }
        else if (language.Equals("fr"))
        {
            foreach (var button in langButtons)
            {
                button.interactable = true;
            }
            langButtons[1].interactable = false;

            selectedLanguage = UITexts.fr;
        }
        else if (language.Equals("es"))
        {
            foreach (var button in langButtons)
            {
                button.interactable = true;
            }
            langButtons[3].interactable = false;

            selectedLanguage = UITexts.es;
        }
        else
        {
            foreach (var button in langButtons)
            {
                button.interactable = true;
            }
            langButtons[0].interactable = false;

            selectedLanguage = UITexts.en;
        }

        foreach (ObjText obj in textsToChange)
        {
            obj.text.text = selectedLanguage[obj.name];
        }

        PlayerPrefs.SetString("language", language);

        FlagsManager.Manager.ChangeFlagsPack();
    }
    #endregion

    public static Dictionary<string, string> en = new Dictionary<string, string>()
    {
        { "whichCountryFlag", "Which country has this flag?" },
        { "canYouDraw", "Can you color this flag?" },
        { "missingPart", "Choose the missing symbol!" },

        { "continue", "Continue" },
        { "replay", "Replay" },
        { "menu", "Menu" },
        { "play", "Play" },
        { "options", "Options" },
        { "stats", "Statistics" },
        { "more", "More" },
        { "language", "Language" },
        { "gameplay", "Game" },
        { "resetGame", "Reset game data" },
        { "highscore", "Highscore" },
        { "symbols", "Symbols" },
        { "names", "Names" },
        { "colors", "Colors" },
        { "newHighscore", "New highscore!" },
        { "areYouSure", "Are you sure you want to reset the game?" },
        { "yes", "Yes" },
        { "no", "No" }

    };

    public static Dictionary<string, string> ro = new Dictionary<string, string>()
    {
        { "whichCountryFlag", "Al carei tari este steagul acesta?" },
        { "canYouDraw", "Poti colora steagul?" },
        { "missingPart", "Poti colora steagul acestei tari?" },

        { "continue", "Continua" },
        { "replay", "Joaca din nou" },
        { "menu", "Meniu" },
        { "play", "Joaca" },
        { "options", "Optiuni" },
        { "stats", "Statistici" },
        { "more", "Mai multe" },
        { "language", "Limba" },
        { "gameplay", "Joc" },
        { "resetGame", "Reseteaza jocul" },
        { "highscore", "Cel mai bun scor" },
        { "symbols", "Simboluri" },
        { "names", "Nume" },
        { "colors", "Culori" },
        { "newHighscore", "Un nou scor mare!" },
        { "areYouSure", "Esti sigur ca vrei sa resetezi jocul?" },
        { "yes", "Da" },
        { "no", "Nu" }
    };

    public static Dictionary<string, string> fr = new Dictionary<string, string>()
    {
        { "whichCountryFlag", "Al carei tari este steagul acesta?" },
        { "canYouDraw", "Poti colora steagul?" },
        { "missingPart", "Poti colora steagul acestei tari?" },

        { "continue", "Continue" },
        { "replay", "Rejouer" },
        { "menu", "Menu" },
        { "play", "Jouer" },
        { "options", "Réglages" },
        { "stats", "Statistiques" },
        { "more", "Suite" },
        { "language", "Langue" },
        { "gameplay", "Jeu" },
        { "resetGame", "Réinitialiser le jeu" },
        { "highscore", "Score élevé" },
        { "symbols", "Symboles" },
        { "names", "Des noms" },
        { "colors", "Couleurs" },
        { "newHighscore", "Nouveau record!" },
        { "areYouSure", "Are you sure you want to reset the game?" },
        { "yes", "Yes" },
        { "no", "No" }

    };

    public static Dictionary<string, string> es = new Dictionary<string, string>()
    {
        { "whichCountryFlag", "Al carei tari este steagul acesta?" },
        { "canYouDraw", "Poti colora steagul?" },
        { "missingPart", "Poti colora steagul acestei tari?" },

        { "continue", "Seguir" },
        { "replay", "Repetición" },
        { "menu", "Menú" },
        { "play", "Desempeñar" },
        { "options", "Opciones" },
        { "stats", "Estadísticas" },
        { "more", "Más" },
        { "language", "Idioma" },
        { "gameplay", "Como se juega" },
        { "resetGame", "reiniciar el juego" },
        { "highscore", "Puntuación más alta" },
        { "symbols", "Simbolos" },
        { "names", "Nombres" },
        { "colors", "Colores" },
        { "newHighscore", "Nuevo record!" },
        { "areYouSure", "Are you sure you want to reset the game?" },
        { "yes", "Yes" },
        { "no", "No" }
    };
}
