using UnityEngine;

public interface IWeapon //Interface used to allow agnostic weapon function calls
{
    //The functions below are shared between all that include this interface
    //When inheriting this interface you must include these functions
    //Doing so allows for their usage without specific types needing to be known
    void Equip(GameObject player); //Equips the weapon
    void Use(); //Uses the attack of the weapon
}
