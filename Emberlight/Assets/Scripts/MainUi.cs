using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

public class MainUi : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private PlayerController player;

    [SerializeField] private Text name_ref;
    [SerializeField] private Text Health_ref;
    [SerializeField] private Text AP_ref;
    [SerializeField] private Text TargetName_ref;
    [SerializeField] private Text TargetHealth_ref;
    [SerializeField] private Text AbilitiesHeader_ref;
    [SerializeField] private Image help_dialog;
    
    [SerializeField] private GameObject button_prefab;

    [SerializeField] private List<UiAbilityActivationButton> buttons;

    private bool show_help = false;
    private float dt = 0.0f;
    void Start()
    {
        player = FindObjectOfType<PlayerController>();
        Assert.IsNotNull(player);
        help_dialog.gameObject.SetActive(false);
    }

    public void ToggleHelp()
    {
        show_help = !show_help;
        Debug.Log(show_help);
        help_dialog.gameObject.SetActive(show_help);
    }

    // Update is called once per frame
    void Update()
    {
        dt += Time.deltaTime;
        if (player.Selected)
        {
            var character = player.Selected;
            name_ref.gameObject.SetActive(true);
            Health_ref.gameObject.SetActive(true);
            AP_ref.gameObject.SetActive(true);
            AbilitiesHeader_ref.gameObject.SetActive(true);

            name_ref.text = string.Format("{0}:", character.char_name);
            Health_ref.text = string.Format("HP: {0}/{1}", character.health, character.max_health);
            AP_ref.text = string.Format("AP: {0}/{1}", character.ap, character.max_ap);
            
            for (int i = 0; i < character.Abilities.Count; i++)
            {
                buttons[i].gameObject.SetActive(true);
                buttons[i].setAbility(i, character.Abilities[i]);
            }
        }
        else
        {
            name_ref.gameObject.SetActive(false);
            Health_ref.gameObject.SetActive(false);
            AP_ref.gameObject.SetActive(false);
            AbilitiesHeader_ref.gameObject.SetActive(false);
            for (int i = 0; i < buttons.Count; i++)
            {
                buttons[i].gameObject.SetActive(false);
            }
        }

        if (player.target_character)
        {
            TargetHealth_ref.gameObject.SetActive(true);
            TargetName_ref.gameObject.SetActive(true);

            TargetHealth_ref.text = string.Format("{0}/{1}", player.target_character.health, player.target_character.max_health);
            TargetName_ref.text = string.Format("{0}", player.target_character.char_name);
        }
        else
        {
            TargetHealth_ref.gameObject.SetActive(false);
            TargetName_ref.gameObject.SetActive(false);
        }
    }
}
