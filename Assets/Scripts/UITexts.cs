using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UITexts : MonoBehaviour
{
    public ObjText[] textsToChange;

    public FZButton[] langButtons;

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
        if (string.IsNullOrEmpty(settedLanguage))
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

    #region Translation text
    public static Dictionary<string, string> en = new Dictionary<string, string>()
    {
        { "whichCountryFlag", "Which country has this flag?" },
        { "canYouDraw", "Can you color this flag?" },
        { "missingPart", "Choose the missing symbol!" },

        { "gameOver", "Game Over" },
        { "continue", "Continue" },
        { "replay", "Replay" },
        { "pause", "Pause" },
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
        { "no", "No" },
        { "learn", "Learn" },
        { "difficulty", "Difficulty" },
        { "easy", "Easy" },
        { "medium", "Medium" },
        { "hard", "Hard" },
        { "unlock", "Unlock" },
        { "europe", "Europe" },
        { "africa", "Africa" },
        { "asia", "Asia" },
        { "oceania", "Oceania" },
        { "northAmerica", "North America" },
        { "southAmerica", "South America" },
        { "others", "Others" },
        { "tipPoints", "Earn points when you answer correctly"},
        { "leaderboards", "Leaderboards" }
    };

    public static Dictionary<string, string> ro = new Dictionary<string, string>()
    {
        { "whichCountryFlag", "Al carei tari este steagul acesta?" },
        { "canYouDraw", "Poti colora steagul?" },
        { "missingPart", "Ghiceste simbolul lipsa!" },

        { "gameOver", "Game Over" },
        { "continue", "Continua" },
        { "replay", "Joaca din nou" },
        { "pause", "Pauza" },
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
        { "no", "Nu" },
        { "learn", "Invata" },
        { "difficulty", "Dificultate" },
        { "easy", "Usor" },
        { "medium", "Mediu" },
        { "hard", "Greu" },
        { "unlock", "Deblocheaza" },
        { "europe", "Europa" },
        { "africa", "Africa" },
        { "asia", "Asia" },
        { "oceania", "Oceania" },
        { "northAmerica", "America de Nord" },
        { "southAmerica", "America de Sud" },
        { "others", "Altele" },
        { "tipPoints", "Castigi puncte cand raspunzi corect"},
        { "leaderboards", "Clasament" }
    };

    public static Dictionary<string, string> fr = new Dictionary<string, string>()
    {
          { "whichCountryFlag", "Quel pays a ce drapeau?" },
          { "canYouDraw", "Peux-tu colorier ce drapeau?" },
          { "missingPart", "Choisissez le symbole manquant!" },
          { "gameOver", "Jeu terminé" },
          { "continue", "Continuer" },
          { "replay", "Rejouer" },
          { "pause", "Pause" },
          { "menu", "Menu" },
          { "play", "Jouer" },
          { "options", "Choix" },
          { "stats", "Statistiques" },
          { "more", "Plus" },
          { "language", "Langue" },
          { "gameplay", "Jeu" },
          { "resetGame", "Réinitialiser les données du jeu" },
          { "highscore", "Score élevé" },
          { "symbols", "Symboles" },
          { "names", "Des noms" },
          { "colors", "Couleurs" },
          { "newHighscore", "Nouveau record!" },
          { "areYouSure", "Voulez-vous vraiment réinitialiser le jeu?" },
          { "yes", "Oui" },
          { "no", "Non" },
          { "learn", "Apprendre" },
          { "difficulty", "Difficulté" },
          { "easy", "Facile" },
          { "medium", "Moyen" },
          { "hard", "Dur" },
          { "unlock", "Ouvrir" },
          { "europe", "L'Europe " },
          { "africa", "Afrique" },
          { "asia", "Asie" },
          { "oceania", "Océanie" },
          { "northAmerica", "Amérique du Nord" },
          { "southAmerica", "Amérique du Sud" },
          { "others", "Autres" },
          { "tipPoints", "Gagnez des points lorsque vous répondez correctement"},
          { "leaderboards", "Classement" }
    };

    public static Dictionary<string, string> es = new Dictionary<string, string>()
    {
          { "whichCountryFlag", "¿Qué país tiene esta bandera?" },
          { "canYouDraw", "¿Puedes colorear esta bandera?" },
          { "missingPart", "¡Elige el símbolo que falta!" },
          { "gameOver", "Juego terminado" },
          { "continue", "Continuar" },
          { "replay", "Repetición" },
          { "pause", "Pausa" },
          { "menu", "Menú" },
          { "play", "Jugar" },
          { "options", "Opciones" },
          { "stats", "Estadísticas" },
          { "more", "Más" },
          { "language", "Idioma" },
          { "gameplay", "Juego" },
          { "resetGame", "Restablecer datos del juego" },
          { "highscore", "Puntuación más alta" },
          { "symbols", "Simbolos" },
          { "names", "Nombres" },
          { "colors", "Colores" },
          { "newHighscore", "¡Nuevo record!" },
          { "areYouSure", "¿Seguro que quieres reiniciar el juego?" },
          { "yes", "Sí" },
          { "no", "No" },
          { "learn", "Aprender" },
          { "difficulty", "Dificultad" },
          { "easy", "Fácil" },
          { "medium", "Medio" },
          { "hard", "Duro" },
          { "unlock", "Desbloquear" },
          { "europe", "Europa" },
          { "africa", "África" },
          { "asia", "Asia" },
          { "oceania", "Oceanía" },
          { "northAmerica", "América del norte" },
          { "southAmerica", "Sudamerica" },
          { "others", "Otros" },
          { "tipPoints", "Gana puntos cuando respondas correctamente"},
          { "leaderboards", "Tabla de clasificación" }
    };
    #endregion
}
