using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class DamageTrait : ITraitBehaviour
{
    private float dmg;


    public DamageTrait(float dmg)
    {
        this.dmg = dmg;
    }
    public void Execute(Actor actor)
    {
        actor.Weapon.content.penetration += dmg;
    }

    public void OnRemove(Actor actor)
    {
        actor.Weapon.content.penetration -= dmg;
    }
}


