using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    public enum WeaponType
    {
        SWORD,
        BOW,
        WAND
    }
    public GameObject target;

    public WeaponType currentWeaponType;

    public float speed;
    public float destroyTime;
    public int damage;
    public Vector3 pos;

    GameObject firePosition;
   

    public GameObject attackEffect;
    public GameObject arrowSkillEffect;

    public GameObject swordNormalEffect;
    public GameObject swordSkill_1_Effect;
    public GameObject swordSkill_2_Effect;

    public GameObject magicNormalEffect;
    public GameObject magicSkill_1_Effect;
    public GameObject magicSkill_2_Effect;


    //[Reference] if add Skills, set the skillTypes value. And then, add in Attack Type Settings. 
    //const int skillTypes = 3;
    //int[] randForType = new int[skillTypes];


    //-----------------------------------------------------------------------
    //****************************Weapon Setting*****************************
    //-----------------------------------------------------------------------

    //근접무기
    public GameObject sword;
    public GameObject swordSkill_1;
    public GameObject swordSkill_2;

    //활,원거리무기
    public GameObject arrow;
    public GameObject arrowSkill_1;
    public GameObject arrowSkill_2;

    //마법무기
    public GameObject magic;
    public GameObject magicSkill_1;
    public GameObject magicSkill_2;



    //-----------------------------------------------------------------------
    public Quaternion roro = Quaternion.Euler(new Vector3(90, 0, 0));




    //-----------------------------------------------------------------------
    //*************************ATTACK TYPE SETTINGS**************************
    //-----------------------------------------------------------------------
    public float currentDurationOfAttackType;
    public enum DurationOfAttackType
    {
        SwordNormalAttack,
        SwordSkill1,
        SwordSKill2,

        BowNormalAttack,
        BowSkill1,
        BowSKill2,

        magicNormalAttack,
        magicSkill1,
        magicSkill2
    }
    //[Reference] currentDurationOfAttackType = animation duration time
    public void SetDurationOfAttackType(DurationOfAttackType currentAttackType)
    {
        switch (currentAttackType)
        {
            case DurationOfAttackType.SwordNormalAttack:
                currentDurationOfAttackType = 1.0f;
                break;
            case DurationOfAttackType.SwordSkill1:
                currentDurationOfAttackType = 1.0f;
                break;
            case DurationOfAttackType.SwordSKill2:
                currentDurationOfAttackType = 1.0f;
                break;

            case DurationOfAttackType.BowNormalAttack:
                currentDurationOfAttackType = 1.0f;
                break;
            case DurationOfAttackType.BowSkill1:
                currentDurationOfAttackType = 1.0f;
                break;
            case DurationOfAttackType.BowSKill2:
                currentDurationOfAttackType = 1.0f;
                break;

            case DurationOfAttackType.magicNormalAttack:
                currentDurationOfAttackType = 1.0f;
                break;
            case DurationOfAttackType.magicSkill1:
                currentDurationOfAttackType = 1.0f;
                break;
            case DurationOfAttackType.magicSkill2:
                currentDurationOfAttackType = 1.0f;
                break;
        }
    }

    //SWORD
    void SwordNormalAttack()
    {
		Debug.Log ("hello monster");

        GameObject clone;
        SetDefaultValues(0.0f, 7.0f, 2);

        clone = Instantiate(sword, firePosition.transform.position, firePosition.transform.rotation);

        Instantiate(swordNormalEffect, transform.parent.position, firePosition.transform.rotation);

    }
    void SwordSkill1()
    {
        GameObject clone;
        SetDefaultValues(4.0f, 7.0f, 3);

        clone = Instantiate(swordSkill_1, firePosition.transform.position, firePosition.transform.rotation);

        Instantiate(swordSkill_1_Effect, transform.parent.position +
            new Vector3(2 * transform.forward.x ,3, 2 * transform.forward.z), firePosition.transform.rotation * Quaternion.Euler(90,0,-90));

    }
    void SwordSkill2()
    {
        GameObject clone;
        SetDefaultValues(4.0f, 7.0f, 3);

        clone = Instantiate(swordSkill_1, firePosition.transform.position, firePosition.transform.rotation);

    }

    void BowNormalAttack()
    {
        GameObject clone;
        SetDefaultValues(4.0f, 7.0f, 3);

        clone = Instantiate(arrow, firePosition.transform.position + new Vector3(0, 2, 0), firePosition.transform.rotation * Quaternion.Euler(0, 90, 0));
        float step = 100 * Time.deltaTime;
        clone.transform.position = Vector3.MoveTowards(transform.position, transform.position + transform.forward * 100, step);

        arrow.SetActive(true);
    }

    void BowSkill1()
    {
        GameObject clone;
        SetDefaultValues(4.0f, 7.0f, 3);

        clone = Instantiate(arrowSkill_1, firePosition.transform.position + new Vector3(0, 2, 0), firePosition.transform.rotation * Quaternion.Euler(0, 90, 0));
        float step = 100 * Time.deltaTime;
        clone.transform.position = Vector3.MoveTowards(transform.position, transform.position + transform.forward * 100, step);
        Instantiate(arrowSkillEffect, transform.parent.position, firePosition.transform.rotation);

        arrow.SetActive(true);
    }

    void BowSkill2()
    {
        GameObject clone;
        SetDefaultValues(4.0f, 7.0f, 3);

        clone = Instantiate(arrowSkill_1, firePosition.transform.position + new Vector3(0, 2, 0), firePosition.transform.rotation * Quaternion.Euler(0, 90, 0));
        float step = 100 * Time.deltaTime;
        clone.transform.position = Vector3.MoveTowards(transform.position, transform.position + transform.forward * 100, step);
        //Instantiate(arrowSkillEffect, transform.parent.position, firePosition.transform.rotation);

        arrow.SetActive(true);
    }

    //magic
    void magicNormalAttack()
    {
        GameObject clone;
        SetDefaultValues(4.0f, 7.0f, 3);

        clone = Instantiate(magic, firePosition.transform.position + new Vector3(0, 2, 0), firePosition.transform.rotation * Quaternion.Euler(0, 90, 0));
        float step = 100 * Time.deltaTime;
        clone.transform.position = Vector3.MoveTowards(transform.position, transform.position + transform.forward * 100, step);

        arrow.SetActive(true);
    }
    void magicSkill1()
    {

    }
    void magicSkill2()
    {

    }

    public void SetAttackType(int skillType)
    {
        if (currentWeaponType == WeaponType.SWORD)
        {
            switch (skillType)
            {
                case 0:
                    SwordSkill1();
                    SetDurationOfAttackType(DurationOfAttackType.SwordNormalAttack);
                    break;
                case 1:
                    SwordSkill1();
                    SetDurationOfAttackType(DurationOfAttackType.SwordSkill1);
                    break;
                case 2:
                    SwordSkill1();
                    SetDurationOfAttackType(DurationOfAttackType.SwordSKill2);
                    break;
            }
        }
        if (currentWeaponType == WeaponType.BOW)
        {
            switch (skillType)
            {
                case 0:
                    BowNormalAttack();
                    SetDurationOfAttackType(DurationOfAttackType.BowNormalAttack);
                    break;
                case 1:
				    BowSkill1();
                    SetDurationOfAttackType(DurationOfAttackType.BowSkill1);
                    break;
                case 2:
                    BowSkill2();
                    SetDurationOfAttackType(DurationOfAttackType.BowSKill2);
                    break;
            }
        }
        if (currentWeaponType == WeaponType.WAND)
        {
            switch (skillType)
            {
                case 0:
                    magicNormalAttack();
                    SetDurationOfAttackType(DurationOfAttackType.magicNormalAttack);
                    break;
                case 1:
                    magicNormalAttack();
//                    magicSkill1();
                    SetDurationOfAttackType(DurationOfAttackType.magicSkill1);
                    break;
                case 2:
                    magicNormalAttack();
//                    magicSkill2();
                    SetDurationOfAttackType(DurationOfAttackType.magicSkill2);
                    break;
            }
        }
    }

    //---------------------------------------------------------------------------


    private void Start()
    {
        firePosition = GameObject.Find("FirePosition");
        //WeaponController weapon = GameObject.Find("Sword").GetComponent<WeaponController>();
    }

    //void OnTriggerEnter(Collider other)
    //{
    //    if (other.tag == "Smashable")
    //    {
    //        Instantiate(attackEffect, transform.position+new Vector3(-1,0,-1), transform.rotation);
    //        Debug.Log("monster hit!");
    //    }

    //}

    void SetDefaultValues(float currentSpeed, float currentDestroyTime, int currentDamage)
    {
        speed = currentSpeed;
        destroyTime = currentDestroyTime;
        damage = currentDamage;
    }
    //void SetSpeed(float currentSpeed)
    //{
    //    speed = currentSpeed;
    //}
    //void SetDestroyTime(float currentDestroyTime)
    //{
    //    destroyTime = currentDestroyTime;
    //}
    //void SetDamage(int currentDamage)
    //{
    //    damage = currentDamage;
    //}

    //void fireball()
    //{
    //    Instantiate(tempgameobject = fireball;)
    //}
}
