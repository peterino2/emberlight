using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Mime;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DamageNumber : MonoBehaviour
{
    // Start is called before the first frame update
    public float damage_number = 0;
    [SerializeField] public float alive_time = 1.0f;
    [SerializeField] public float upspeed = 1.0f;
    [SerializeField] public TextMeshPro text_inner;

    private void Awake()
    {
        text_inner = GetComponentInChildren<TextMeshPro>();
    }

    void Start()
    {
    }

    private bool first;
    private float dt;

    public void setText(string text)
    {
        text_inner.text = text;
    }

    // Update is called once per frame
    void Update()
    {
        dt += Time.deltaTime;
        transform.position += new Vector3(0, upspeed * Time.deltaTime, 0);
        if (dt > alive_time)
        {
            gameObject.SetActive(false);
            Destroy(this.gameObject);
        }
    }
}
