using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UiAbilityActivationButton : MonoBehaviour
{
    [SerializeField]
    public GameObject go;
    public AbilityBase ability;
    public Text t; // Start is called before the first frame update
    void Start()
    {
        go = gameObject;
        Button btn = GetComponent<Button>();
        
        btn.onClick.AddListener(OnClick);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnClick()
    {
        Debug.Log(ability.get_name());
        var p = FindObjectOfType<PlayerController>();
        if (p.Selected)
        {
            ability.begin_targeting();
        }
    }

    public void setAbility(int slot, AbilityBase ab)
    {
        ability = ab;
        t.text = string.Format( "{0}. {1}", slot + 1, ab.get_name());
    }
    
}
