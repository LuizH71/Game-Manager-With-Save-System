using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;
using UnityStandardAssets.Characters.FirstPerson;


public class GameManager : MonoBehaviour
{
    [SerializeField]  GameObject Flash;
    public float flashTime;

    public bool chaveSeshan = false;

    public int ammoStart;
    private bool IsFiring;
    public Text ammoDisplay;
    public SimpleGrabSystem ScriptPickup;

    public GameObject AudioMan;
    public GameObject PlayerLife;



    [SerializeField] public Transform[] transforms;



    public int gameLoaded;
    
    public bool deleted = false;
    public int[] desativarSmoke;
    public int[] desativarMonstro;
    public int[] desativarChave;
    public int[] desativarFilme;

    private bool waitshoot;

    public GameObject[] chaves;

    public GameObject Silence;

    [HideInInspector]
    public int ChaveQuarto = 0;

    [HideInInspector]
    public int Radio;


    public int Count;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Time.timeScale = 1f;
        GameObject.Find("Player").GetComponent<SimpleGrabSystem>().enabled = true;


    }

    private void Awake()
    {
        
        Load();

        if (ChaveQuarto == 1)
        {
            chaves[3].SetActive(true);
        }


    }




    // Update is called once per frame
    private void Update()
    {

        ammoDisplay.text = ammoStart.ToString();

        if (Input.GetKeyDown(KeyCode.E) && ammoStart > 0 && !IsFiring && ScriptPickup.hasItem == false && ScriptPickup.hasCamera == true)
        {
            waitshoot = false;
            Save();

            AudioMan.GetComponent<AudioMan>().Play("Flash");

            Flash.SetActive(true);
            Invoke("WaitShoot", flashTime);

            IsFiring = true;
            ammoStart--;
            IsFiring = false;

            


        }

        if (Input.GetKeyDown(KeyCode.E) && ammoStart == 0 && waitshoot == true && ScriptPickup.hasItem == false && ScriptPickup.hasCamera == true)
        {
            GameObject.Find("Audio Manager").GetComponent<AudioMan>().Play("NoFlash");
        }



        if (Input.GetKeyDown(KeyCode.P))
        {
            ammoStart = 100;

            PlayerLife.GetComponent<LifeSystem>().health = 1000000000;
            PlayerLife.GetComponent<LifeSystem>().enabled = false;

            for (int i = -1; i <= chaves.Length+1;i++)
            {
                chaves[i].SetActive(true);
            }

            
        }


        Console.Clear();
    }
    private void WaitShoot()
    {

        Flash.SetActive(false);
        waitshoot = true;
    }

    public void AvancarTexto()
    {
        GameObject.Find("RadioText").GetComponent<RadioTextScript>().count += 1;

        
    }

 
    public void Save()
    {
        gameLoaded = 1;


        for (int i = 0; i < transforms.Length; i++)
        {
            PlayerPrefs.SetFloat("transformX" + i, transforms[i].position.x);
            PlayerPrefs.SetFloat("transformY" + i, transforms[i].position.y);
            PlayerPrefs.SetFloat("transformZ" + i, transforms[i].position.z);
   
        }

        for(int i = 0; i < desativarSmoke.Length; i++)
        {
            PlayerPrefs.SetInt("Deleted" + i, desativarSmoke[i]);
        }

        for (int i = 0; i < desativarMonstro.Length; i++)
        {
            PlayerPrefs.SetInt("DeletedMonstro" + i, desativarMonstro[i]);
        }

        for (int i = 0; i < desativarFilme.Length; i++)
        {
            PlayerPrefs.SetInt("DeletedFilme" + i, desativarFilme[i]);
        }

        for (int i = 0; i < desativarChave.Length; i++)
        {
            PlayerPrefs.SetInt("DeletedChave" + i, desativarChave[i]);
        }

        PlayerPrefs.SetInt("ammo", ammoStart);

        
        PlayerPrefs.SetInt("Game", gameLoaded);

        PlayerPrefs.SetInt("chaveQuarto", ChaveQuarto);

        PlayerPrefs.SetInt("Radio", Radio);

        PlayerPrefs.SetInt("Count", Count);


        Debug.Log("Saved");
    }

    public void Load()
    {
        gameLoaded = PlayerPrefs.GetInt("Game");

        this.enabled = true;

        if(gameLoaded==1)
        {


            for (int i = 0; i < transforms.Length; i++)
            {

                transforms[i].position = new Vector3(PlayerPrefs.GetFloat("transformX" + i), PlayerPrefs.GetFloat("transformY" + i), PlayerPrefs.GetFloat("transformZ" + i));


            }

            for (int i = 0; i < desativarSmoke.Length; i++)
            {
                desativarSmoke[i] = PlayerPrefs.GetInt("Deleted"+i);
            }

            for (int i = 0; i < desativarMonstro.Length; i++)
            {
                desativarMonstro[i] = PlayerPrefs.GetInt("DeletedMonstro" + i);

 
            }

            for (int i = 0; i < desativarFilme.Length; i++)
            {
                desativarFilme[i] = PlayerPrefs.GetInt("DeletedFilme" + i);
            }

            for (int i = 0; i < desativarChave.Length; i++)
            {
                desativarChave[i] = PlayerPrefs.GetInt("DeletedChave" + i);
            }




            ammoStart = PlayerPrefs.GetInt("ammo");

            ChaveQuarto = PlayerPrefs.GetInt("chaveQuarto");

            Radio = PlayerPrefs.GetInt("Radio");

            GameObject.Find("RadioText").GetComponent<RadioTextScript>().count = PlayerPrefs.GetInt("Count");





        }



        Debug.Log("Loaded");

    }

}
