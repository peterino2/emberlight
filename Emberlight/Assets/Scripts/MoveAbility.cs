using UnityEngine;
using UnityEngine.UIElements;

namespace DefaultNamespace
{
    public class MoveAbility : AbilityBase
    {
        public override void ActivateAbility(Vector3 targetPosition, CharacterBase target=null)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit, 100))
            {
                character.CmdMoveTo(hit.point);
            }
        }


        public override void begin_targeting()
        {
            if (character.ap > 0)
            {
                var p = FindObjectOfType<PlayerController>();
                p.startTargeting(this);
                p.startShowingPrediction_Movement();
            }
        }

        public override string get_cost(Vector3 targetPosition, CharacterBase target=null)
        {
            return "no_cost";
        }

        public override bool can_afford(Vector3 targetPosition, CharacterBase target=null)
        {
            return character.ap > 0;
        }

        public override string get_name()
        {
            return "Move";
        }

        public override void ApplyCost(Vector3 targetPosition, CharacterBase target = null)
        {
            character.ap -= 1;
        }

        public override bool try_activate(Vector3 targetPosition, CharacterBase target = null)
        {
            if (can_afford(targetPosition, target))
            {
                ActivateAbility(targetPosition, target);
                ApplyCost(targetPosition, target);
                return true;
            }

            return false;
        }
    }

}