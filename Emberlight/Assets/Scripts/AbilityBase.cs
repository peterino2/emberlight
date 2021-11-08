using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class AbilityBase : MonoBehaviour
{
    [SerializeField] public string ability_name = "default ability";
    [SerializeField] public string tooltip = "make a tooltip";
    // Start is called before the first frame update

    protected CharacterBase character;
    void Start()
    {
        character = gameObject.GetComponent<CharacterBase>();
        Assert.IsNotNull(character);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public virtual string get_name()
    {
        return "default ability";
    }
    
    public virtual void begin_targeting()
    {
        
    }
    
    public virtual bool can_afford(Vector3 targetPosition, CharacterBase target=null)
    {
        return false;
    }

    public virtual string get_cost(Vector3 targetPosition, CharacterBase target=null)
    {
        return "no_cost";
    }

    public virtual bool try_activate(Vector3 targetPosition, CharacterBase target = null)
    {
        return false;
    }

    public virtual void ActivateAbility(Vector3 targetPosition, CharacterBase target=null)
    {
        
    }
    
    public virtual void ApplyCost(Vector3 targetPosition, CharacterBase target=null)
    {
        
    }
}
