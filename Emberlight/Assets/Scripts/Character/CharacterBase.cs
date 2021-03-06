using System.Collections.Generic;
using System.Net.Mime;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Assertions;
using UnityEngine.UI;

public class CharacterBase : MonoBehaviour
{
    [SerializeField] private GameObject highlighter;
    private List<string> Tags = new List<string>();
    private NavMeshAgent NavAgent;
    private Transform movement_goal;
    private PlayerController _playerController;

    [SerializeField] public Sprite portrait;

    [SerializeField] public int teamid;
    [SerializeField] public List<AbilityBase> Abilities = new List<AbilityBase>();
    
    [SerializeField] public float health = 100.0f;
    [SerializeField] public float max_health = 100.0f;
    [SerializeField] public float ap = 6.0f;
    [SerializeField] public float max_ap = 6.0f;
    [SerializeField] public string char_name = "joe";
    [SerializeField] string[]  names =  {"joe", "biden", "kazuha", "don", "cheadle"}; 
    
    [SerializeField] private GameObject damageFloaterPrefab;

    // Start is called before the first frame update
    void Start()
    {
        NavAgent = GetComponent<NavMeshAgent>();
        _playerController = FindObjectOfType<PlayerController>();

        char_name = names[Random.Range(0, names.Length)];

        Assert.IsNotNull(_playerController);
        Assert.IsNotNull(highlighter);
        Assert.IsNotNull(NavAgent);
        SetHighlight(false);
    }

    public void CmdMoveTo(Vector3 position)
    {
        NavAgent.destination = position;
    }

    public void OnMouseDown()
    {
        _playerController.SelectCharacter(this);
    }

    public void SetHighlight(bool highlight)
    {
        if (highlight)
        {
            highlighter.SetActive(true);
        }
        else
        {
            highlighter.SetActive(false);
        }
    }
    
    public void ShowDamageFloater(int i)
    {
        var x = Instantiate(damageFloaterPrefab);
        x.GetComponent<DamageNumber>().setText(string.Format("{0}", i));
        x.transform.position = transform.position + new Vector3(0,0,0);
    }

    public void TakeDamage(int i)
    {
        ShowDamageFloater(i);
        health -= i;
        if (health <= 0) die();
    }

    public void die()
    {
        transform.Rotate(90, 0 ,0);
    }
    // Update is called once per frame
    void Update()
    {
    }
}
