using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStateSaveSystem : MonoBehaviour
{
    //This is an important variable that determines what will be assigned on the level save PlayerPrefs of the player (0 = null)
    [SerializeField] private int currentLevel;
    
    //Assign the fragments of the scene so they can be saved. ORDER IS IMPORTANT - THE FIRST IS 'FRAGMENT 1' THE SECOND 'FRAGMENT 2'.
    [SerializeField] List<GameObject> fragments;

    //Assign this to true when in cutscene. It allows us to make the fragments finished when the puzzle is done.
    [SerializeField] private bool isCutscene;

    public GameObject player;

    private void Awake()
    {
        if (isCutscene == false)
        {
            //Save the level that the player is currently in
            PlayerPrefs.SetInt("CurrentLevel", currentLevel);
            
            player = GameObject.FindWithTag("Player");

            float savedPosX = PlayerPrefs.GetFloat("PlayerPosX");
            float savedPosY = PlayerPrefs.GetFloat("PlayerPosY");
            float savedPosZ = PlayerPrefs.GetFloat("PlayerPosZ");

            Vector3 savedPos = new Vector3(savedPosX, savedPosY, savedPosZ);

            if (savedPos != Vector3.zero)
            {
                player.transform.position = savedPos;
            }
            else
            {
                PlayerPrefs.SetFloat("PlayerPosX", player.transform.position.x);
                PlayerPrefs.SetFloat("PlayerPosY", player.transform.position.y);
                PlayerPrefs.SetFloat("PlayerPosZ", player.transform.position.z);
            }

            //The fragments of the level are always two (for now) so we assign two values. This is not really a good practice, but in our case it is manageable
            //2 - enabled, 1- disabled, 0 - null.
            if (PlayerPrefs.GetInt("Fragment 1") != 1)
            {
                PlayerPrefs.SetInt("Fragment 1", 2);
            }
            else if (PlayerPrefs.GetInt("Fragment 2") != 1)
            {
                PlayerPrefs.SetInt("Fragment 2", 2);
            }
            
            if (PlayerPrefs.GetInt("Fragment 1") == 1)
            {
                fragments[0].SetActive(false);
            }

            if (PlayerPrefs.GetInt("Fragment 2") == 1)
            {
                fragments[1].SetActive(false);
            }
        }
    }

    public void SaveGame()
    {
        PlayerPrefs.SetFloat("PlayerPosX", player.transform.position.x);
        PlayerPrefs.SetFloat("PlayerPosY", player.transform.position.y);
        PlayerPrefs.SetFloat("PlayerPosZ", player.transform.position.z);
    }

    public void Fragment1Disable()
    {
        Debug.Log("Disable Fragment 1");
        PlayerPrefs.SetInt("Fragment 1", 1);
    }

    public void Fragment2Disable()
    {
        Debug.Log("Disable Fragment 2");
        PlayerPrefs.SetInt("Fragment 2", 1);
        
    }

    public void ClearFragments()
    {
        PlayerPrefs.SetInt("Fragment 1", 0);
        PlayerPrefs.SetInt("Fragment 2", 0);
    }
    
    public void ClearPositions()
    {
        PlayerPrefs.SetFloat("PlayerPosX", 0);
        PlayerPrefs.SetFloat("PlayerPosY", 0);
        PlayerPrefs.SetFloat("PlayerPosZ", 0);
    }
}