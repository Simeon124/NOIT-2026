using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStateSaveSystem : MonoBehaviour
{
    public GameObject player;

    private void Awake()
    {
        player = GameObject.FindWithTag("Player");

        float savedPosX = PlayerPrefs.GetFloat("PlayerPosX");
        float savedPosY = PlayerPrefs.GetFloat("PlayerPosY");
        float savedPosZ = PlayerPrefs.GetFloat("PlayerPosZ");

        Vector3 savedPos = new Vector3(savedPosX, savedPosY, savedPosZ);

        if(savedPos != Vector3.zero)
        {
            player.transform.position = savedPos;
        }
        else
        {
            PlayerPrefs.SetFloat("PlayerPosX", player.transform.position.x);
            PlayerPrefs.SetFloat("PlayerPosY", player.transform.position.y);
            PlayerPrefs.SetFloat("PlayerPosZ", player.transform.position.z);
        }
    }

    public void SaveGame()
    {
        PlayerPrefs.SetFloat("PlayerPosX", player.transform.position.x);
        PlayerPrefs.SetFloat("PlayerPosY", player.transform.position.y);
        PlayerPrefs.SetFloat("PlayerPosZ", player.transform.position.z);
    }

}
