using UnityEngine;


[CreateAssetMenu(fileName = "Malediction", menuName = "Scriptable/Malediction", order = 0)]
public class Malediction : ScriptableObject
{
    public string maledictionName;

    [SerializeReference]
    public Prerequirement[] prerequirements;

    [SerializeReference]
    public MaledictionEffect[] maledictionEffects;

    public void Init()
    {
        foreach (Prerequirement item in prerequirements)
        {
            item.InitPrerequirement();
        }
    }

    public bool CanBeCast()
    {
        foreach (Prerequirement requirement in prerequirements)
        {
            if (!requirement.IsOkay())
                return false;
        }
        return true;
    }

    public void CastAllEffect()
    {
        foreach (MaledictionEffect male in maledictionEffects)
        {
            male.Cast();
        }
    }
}
