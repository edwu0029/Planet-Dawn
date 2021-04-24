using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour {
    [Header("Scene String References")] //Scene string references
    [SerializeField] string startMenuStrRef = "Start Menu";
    [SerializeField] string mainMenuStrRef  = "Main Menu";
    [SerializeField] string instructionsStrRef = "Instructions";
    [SerializeField] string gameOverStrRef = "Game Over";
    [SerializeField] string winSceneStrRef = "Win Scene";
    [SerializeField] string bossWinSceneStrRef = "Boss Win Scene";
    
    [Header("Scene String References")] //Scene string references
    [SerializeField] string menuMusicRef = "MenuMusic";
    [SerializeField] string gameMusicRef = "GameMusic";
    [SerializeField] string gameOverMusicRef = "GameOverMusic";
    [SerializeField] string winMusicRef = "WinMusic";

    Level level;
    AudioManager audioManager;
    [SerializeField] float gameDelayTransition = 2f;

    private void Start() {
        level = FindObjectOfType<Level>();
        audioManager = FindObjectOfType<AudioManager>();
    }
    private void Update() {
        if(SceneManager.GetActiveScene().name == "Win Scene" || SceneManager.GetActiveScene().name == "Boss Win Scene" || SceneManager.GetActiveScene().name == "Game Over") {
            StopAllCoroutines();
        }
    }
    public void LoadStartMenu() {
        audioManager.PlaySound(menuMusicRef);
        SceneManager.LoadScene(startMenuStrRef);
    }
    public void Quit() {
        Application.Quit();
    }
    public void LoadMainMenu() {
        audioManager.PlaySound(menuMusicRef);
        SceneManager.LoadScene(mainMenuStrRef);
        if (SceneManager.GetActiveScene().name == "Game Over" || SceneManager.GetActiveScene().name == "Win Scene"
            || SceneManager.GetActiveScene().name == "Boss Win Scene") {
            Destroy(level.gameObject);
        }
    }
    public void LoadInstructions() {
        audioManager.PlaySound(menuMusicRef);
        SceneManager.LoadScene(instructionsStrRef);
    }
    public void LoadLevel(int levelNumber) {
        if (levelNumber <= 0) return;

        string levelStrRef = "Level " + levelNumber.ToString();
        audioManager.PlaySound(gameMusicRef);
        SceneManager.LoadScene(levelStrRef);
    }
    public void ReloadLevel() {
        LoadLevel(level.GetLevelNumber());
        if (SceneManager.GetActiveScene().name == "Game Over" || SceneManager.GetActiveScene().name == "Win Scene"
            || SceneManager.GetActiveScene().name == "Boss Win Scene") {
            Destroy(level.gameObject);
        }
    }
    public void LoadGameOver() {
        StartCoroutine(WaitAndLoad(gameOverStrRef, gameOverMusicRef));
    }
    public void LoadWinScene() {
        StartCoroutine(WaitAndLoad(winSceneStrRef, winMusicRef));
    }
    public void LoadBossWinScene() {
        StartCoroutine(WaitAndLoad(bossWinSceneStrRef, winMusicRef));
        //*PEHAPS SWITCH TO ANOTHER MUSIC CLIP FOR BOSS WIN*
    }
    private IEnumerator WaitAndLoad(string strSceneRef, string strMusicref) {
        yield return new WaitForSeconds(gameDelayTransition);
        audioManager.PlaySound(strMusicref);
        SceneManager.LoadScene(strSceneRef);
    }
}
