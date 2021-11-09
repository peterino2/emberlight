using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update
    public CharacterBase Selected;

    [SerializeField] private GameObject targeting_obj;

    private bool is_targeting;
    private AbilityBase targeting_ability;

    public CharacterBase target_character;
    
    [SerializeField] public List<CharacterBase> party_members;

    NavMeshAgent na;
    [SerializeField] private LineRenderer navpath;

    private bool debounce = false;
    
    void Start()
    {
        clear_predictions();
    }

    public void Deselect()
    {
        if (Selected)
        {
            Selected.SetHighlight(false);
        }
    }

    public void SelectCharacter(CharacterBase obj)
    {
        if (obj.teamid != 0) return;
        Deselect();
        obj.SetHighlight(true);
        Selected = obj;
        debounce = false;
    }
    [SerializeField]Vector3 targetingPosition = new Vector3();

    public void SelectByPartyId(int id)
    {
        Debug.Log(id);
        if (id >= party_members.Count) return;
        SelectCharacter(party_members[id]);   
    }

    void HandleKeyboardSlots()
    {
        if (Selected)
        {
            if (!is_targeting)
            {
                if (Input.GetKeyDown(KeyCode.Alpha1) && Selected.Abilities.Count > 0) Selected.Abilities[0].begin_targeting();
                if (Input.GetKeyDown(KeyCode.Alpha2) && Selected.Abilities.Count > 1) Selected.Abilities[1].begin_targeting();
                if (Input.GetKeyDown(KeyCode.Alpha3) && Selected.Abilities.Count > 2) Selected.Abilities[2].begin_targeting();
                if (Input.GetKeyDown(KeyCode.Alpha4) && Selected.Abilities.Count > 3) Selected.Abilities[3].begin_targeting();
                if (Input.GetKeyDown(KeyCode.Alpha5) && Selected.Abilities.Count > 4) Selected.Abilities[4].begin_targeting();
            }
        }
        
    }
    
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (is_targeting)
            {
                endTargeting();
            }
            else if(Selected)
            {
                Deselect();
            }
        }

        if (Input.GetKeyDown(KeyCode.F1))
        {
            var mainui = FindObjectOfType<MainUi>();
            mainui.ToggleHelp();
        }

        HandleKeyboardSlots();

        
        Ray r = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit[] hits;
        hits = Physics.RaycastAll(r);
                
        bool solid = false;
        target_character = null;
        for(int i =0; i < hits.Length; i ++)
        {
            var hit = hits[i];
            if (hit.transform.gameObject != targeting_obj)
            {
                targeting_obj.SetActive( false );
                targeting_obj.transform.position = hit.point;
                var _tc = hit.transform.gameObject.GetComponent<CharacterBase>();
                if (_tc)
                {
                    target_character = _tc;
                }
                targetingPosition = hit.point;
                solid = true;
            }
        }
        if(solid == false)
        {
            target_character = null;
            targeting_obj.SetActive( false );
        }
        
        HandlePredictions();

        if (Input.GetKeyDown(KeyCode.Mouse0) && debounce && !IsPointerOverUIObject() && solid && is_targeting)
        {
            targeting_ability.try_activate(targetingPosition);
            endTargeting();
        }

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            debounce = true;
        }
        
        if (Input.GetKeyDown(KeyCode.F2))
        {
            if(Selected)
                Selected.TakeDamage(10);
        }
    }

    public void endTargeting()
    {
        is_targeting = false;
        clear_predictions();
    }

    public void startTargeting(AbilityBase target)
    {
        is_targeting = true;
        targeting_ability = target;
    }

    void HandlePredictions()
    {
        if (prediction_mode_movement)
        {
            var pathdata = new NavMeshPath();
            NavMesh.CalculatePath(Selected.transform.position, targetingPosition, NavMesh.AllAreas, pathdata);
            navpath.gameObject.SetActive(true);
            navpath.startWidth = (0.5f);
            navpath.endWidth = (0.5f);
            navpath.startColor = Color.yellow; 
            navpath.endColor = Color.yellow;
            int len = pathdata.corners.Length;
            navpath.positionCount = (len);
            for (int i = 0; i < len; i++)
            {
                navpath.SetPosition(i, pathdata.corners[i]);
            }
        }
    }
    
    public bool prediction_mode_movement = false;

    public void startShowingPrediction_Movement()
    {
        prediction_mode_movement = true;

    }

    public void clear_predictions()
    {
        prediction_mode_movement = false;
        navpath.positionCount = 0;
    }
    
    //When Touching UI
    private bool IsPointerOverUIObject()
    {
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
        eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
        return results.Count > 0;
    }
}
