using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class AmmoFacade
{
    private string name;
    private int damage;
    private float accuracy;
    private float range;
    private float penetration;
    private string type;
    private List<ITraitBehaviour> list;

    /// <summary>
    /// Creates AmmoMod Usables more conveniently (easier to use) than chaining constructors.
    /// </summary>
    /// <param name="name">item name shown in the inventory</param>
    /// <param name="accuracy">amount of accuracy changed</param>
    /// <param name="damage">amount of damage changed</param>
    /// <param name="range">amount of range changed</param>
    /// <param name="penetration">amount of penetration changed</param>
    /// <param name="type">new damage type</param>
    /// <param name="list">Parameter that allows to add additional custom ITraitBehaviours, default or empty list results in nothing being added to the Usable Behaviour</param>
    public AmmoFacade(string name, int damage = 0, float accuracy = 0, float range = 0, float penetration = 0, string type = "default", List<ITraitBehaviour> list = null)
    {
        this.name = name;
        this.damage = damage;
        this.accuracy = accuracy;
        this.range = range;
        this.penetration = penetration;
        this.type = type;

        if (list.Count > 0)
        {
            this.list = list;
        }
    }

    public Usable Create()
    {
        Usable ammo = new Usable();
        Trait temp = new Trait();
        List<ITraitBehaviour> tempB = new List<ITraitBehaviour>();

        if (damage != 0) tempB.Add(new DamageTrait(damage));
        if (accuracy != 0) tempB.Add(new AccuracyTrait(accuracy));
        if (range != 0) tempB.Add(new RangeTrait(range));
        if (penetration != 0) tempB.Add(new PenetrationTrait(penetration));
        if (type != "default") tempB.Add(new DamageTypeTrait(type));

        temp.duration = -1;
        temp.name = "ammo_mod";
        temp.behaviour = tempB;
        if (list.Count > 0) temp.behaviour.AddRange(list);


        ammo.name = name;
        ammo.type = "use";
        ammo.behaviour = new List<Trait> { temp };

        return ammo;
    }
}
