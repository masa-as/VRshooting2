﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;
using System.Threading;

public class StarfighterControl : MonoBehaviour
{

    float X_Speed = 1.0f;
    float Y_Speed = 1.0f;

    public GameObject EnemyBullet;
    public GameObject EnemyBullet2;
    public GameObject EnemyBullet3;
    public GameObject EnemyBullet4;
    public GameObject EnemyObject;
    public GameObject EnemyObject2;
    public GameObject EnemyObject3;
    public GameObject EnemyObject4;
    public GameObject Explosion;
    public Quaternion quat = Quaternion.Euler(0, 180, 0);

    //int mode_flag = 0;
    int life = 3;

    float time;
    float intervalTime;
    float enemyintervalTime;

    // Use this for initialization
    void Start()
    {
        time = 0;
        intervalTime = 0.5f;
        enemyintervalTime = 0;
    }

    // Update is called once per frame
    void Update()
    {
        float vertical = Input.GetAxis("Vertical");
        float horizontal = Input.GetAxis("Horizontal");

        transform.Translate(0, vertical * Y_Speed, 0);
        transform.Translate(horizontal * X_Speed, 0, 0);


        time += Time.deltaTime;

        if (time >= intervalTime)
        {
            time = 0.0f;


            if (Input.GetButton("Fire1") || Input.GetKey("left shift"))
            {
                //X_Speed = Y_Speed = 1;
                Instantiate(EnemyBullet4, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity);
            }
            //else if (Input.GetButtonUp("Fire1") || Input.GetKeyUp("left shift"))
            //{
            //    X_Speed = Y_Speed = 0.5f;
            //}
            if (Input.GetButton("Fire2") || Input.GetKey("z"))
            {
                //if (mode_flag == 0)
                //{
                //    Instantiate(Prefab, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity);
                //}
                //else if (mode_flag == 1)
                //{
                //    Instantiate(Prefab, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.Euler(0, 10, 0));
                //    Instantiate(Prefab, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.Euler(0, -10, 0));
                //}
                Instantiate(EnemyBullet, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity);
            }
            if (Input.GetButton("Fire3"))
            {
                Instantiate(EnemyBullet2, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity);
            }
            if (Input.GetButton("Jump") || Input.GetKeyDown("a"))
            {
                //if (mode_flag == 0)
                //{
                //    intervalTime = 1.0f;
                //    mode_flag = 1;
                //}
                //else if (mode_flag == 1)
                //{
                //    intervalTime = 0.5f;
                //    mode_flag = 0;
                //}
                Instantiate(EnemyBullet3, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity);
            }
        }

        enemyintervalTime += Time.deltaTime;
            if (enemyintervalTime >= 1.0f)
            {
                enemyintervalTime = 0;
                int rnd = Random.Range(1, 5);
                if (rnd == 1)
                    Instantiate(EnemyObject, new Vector3(Random.Range(-35.0f, 35.0f), transform.position.y, transform.position.z + 200), quat);
                if (rnd == 2)
                    Instantiate(EnemyObject2, new Vector3(Random.Range(-35.0f, 35.0f), transform.position.y, transform.position.z + 200), quat);
                if (rnd == 3)
                    Instantiate(EnemyObject3, new Vector3(Random.Range(-35.0f, 35.0f), transform.position.y, transform.position.z + 200), quat);
                if (rnd == 4)
                    Instantiate(EnemyObject4, new Vector3(Random.Range(-35.0f, 35.0f), transform.position.y, transform.position.z + 200), quat);
            }

            Vector3 pos = transform.position;

            pos.x = Mathf.Clamp(transform.position.x, -35, 35);
            pos.y = Mathf.Clamp(transform.position.y, -15, 15);

            transform.position = pos;

        }
        void OnTriggerEnter(Collider coll)
        {
            if (coll.gameObject.tag == "EnemyBullet")
            {
                life--;
                Instantiate(Explosion, new Vector3(transform.position.x, transform.position.y, transform.position.z + 5), Quaternion.identity);
                StartCoroutine(ChangeScene("gameover"));
            }
        }

        IEnumerator ChangeScene(string scene)
        {
            GetComponent<MeshCollider>().enabled = false;
            yield return new WaitForSeconds(1);
            GetComponent<MeshCollider>().enabled = true;
            if (life == 0)
                SceneManager.LoadScene(scene);
        }
    }