using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelDescription : MonoBehaviour {
    [SerializeField] Text levelDescriptionText;

    public void UpdateLevelDescripton(int selectedLevelNumber) {
        if (selectedLevelNumber == 1) {
            levelDescriptionText.text = "The Battle begins! Eliminate enemies and protect Planet Dawn from the onslaught!";
            levelDescriptionText.fontSize = 35;
        }else if (selectedLevelNumber == 2) {
            levelDescriptionText.text = "Enemy reinforcements arrive! Continue fighting toward the enemy mothership. ";
            levelDescriptionText.text += "Beware of strikers! These enemies have high-tech armour that can't be damaged! Best you can do is dodge!";
            levelDescriptionText.fontSize = 35;
        }else if (selectedLevelNumber == 3) {
            levelDescriptionText.text = "The final battle is here! Take out the enemies and destroy the enemy mothership to bring victory for Planet Dawn!";
            levelDescriptionText.fontSize = 35;
        }
    }
}
