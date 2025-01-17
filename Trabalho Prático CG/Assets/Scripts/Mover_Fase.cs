﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover_Fase : MonoBehaviour
{
    public GameObject fase1, fase2, boundary, self, bossPrefab, boss;
    private GameController gameController;
    private int flagFase;
    public float distancia;
    

    private GameObject player, camera;
    private Vector3 posicaoBoss;

    private void Start()
    {
        flagFase = 1;
        gameController = GameObject.Find("GameController").GetComponent<GameController>();
        player = GameObject.Find("Player");
        camera = GameObject.Find("Main Camera");
       
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            if (flagFase == 1)
            {
                fase1.transform.position = new Vector3(fase1.GetComponent<Transform>().position.x, fase1.GetComponent<Transform>().position.y, fase1.GetComponent<Transform>().position.z + distancia);
                moverObjetos();
                flagFase = 0;
            }
            else
            {
                fase2.transform.position = new Vector3(fase2.GetComponent<Transform>().position.x, fase2.GetComponent<Transform>().position.y, fase2.GetComponent<Transform>().position.z + distancia);
                flagFase = 1;
                moverObjetos();
            }
        }

        // Aparição do boss
        if(gameController.GetScore() >= gameController.pontuacaoBoss)
        {
            StartCoroutine(AparicaoBoss());
            gameController.Boss();
        }
       
    }

    IEnumerator AparicaoBoss()
    {
        yield return new WaitForSeconds(2);

        // Para o player e a câmera
        camera.GetComponent<PlayerScroller>().scrollSpeed = 0;
        player.GetComponent<PlayerScroller>().scrollSpeed = 0;
        

        Quaternion bossRotation = Quaternion.Euler(0f, 180f, 0f);
        posicaoBoss = new Vector3
        (
            player.GetComponent<Transform>().position.x - player.GetComponent<Transform>().position.x,
            player.GetComponent<Transform>().position.y - player.GetComponent<Transform>().position.y,
            player.GetComponent<Transform>().position.z + 11
        );

        // Invoca o boss
        yield return new WaitForSeconds(2);

        // Invoca apenas uma instância do boss
        if (boss == null)
        {
            boss = Instantiate(bossPrefab, posicaoBoss, bossRotation) as GameObject;
        }

        StopCoroutine(AparicaoBoss());

    }
    private void moverObjetos()
    {
        boundary.transform.position = new Vector3(boundary.GetComponent<Transform>().position.x, boundary.GetComponent<Transform>().position.y, boundary.GetComponent<Transform>().position.z + (distancia/2));
        self.transform.position = new Vector3(self.GetComponent<Transform>().position.x, self.GetComponent<Transform>().position.y, self.GetComponent<Transform>().position.z + (distancia/2));

    }
}
