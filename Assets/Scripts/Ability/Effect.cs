using UnityEngine;

public abstract class Effect : ScriptableObject
{
    public abstract void Execute(Card origin, Card target, Player player, GameManager gm);
}

// Gây sát thương
[CreateAssetMenu(menuName = "Effects/DamageEffect")]
public class DamageEffect : Effect
{
    public int damageAmount;
    public bool isDamagePerCant;
    public override void Execute(Card origin, Card target, Player player, GameManager gm)
    {
       
    }
}

// Triệu hồi
[CreateAssetMenu(menuName = "Effects/SummonEffect")]
public class SummonEffect : Effect
{
    public int idCard;
    public int amount;
    public int level;
    public int ishighest;
    public KeyWords keyWords;
    public ElementType Element;
    public RaceType Race;
    public bool SummonFormDeck;
    public bool SummonFromHand;
    public bool SummonFromGraveyard;
    public override void Execute(Card origin, Card target, Player player, GameManager gm)
    {
       
    }
}

// Hồi HP
[CreateAssetMenu(menuName = "Effects/HealEffect")]
public class HealEffect : Effect
{
    public int healAmount;
    public bool isHealPercent;
    public bool HealforMonter;
    public override void Execute(Card origin, Card target, Player player, GameManager gm)
    {
        
    }
}
//Giảm damage
[CreateAssetMenu(menuName = "Effects/ReduceDamageEffect")]
public class ReduceDamage : Effect
{
    public int damageAmount;
    public bool isDamagePercent;
    public override void Execute(Card origin, Card target, Player player, GameManager gm)
    {

    }
}
//tấn công 2 lần
[CreateAssetMenu(menuName = "Effects/AttackTwiceEffect")]
public class AttackTwice : Effect
{
    public int count;   
    public override void Execute(Card origin, Card target, Player player, GameManager gm)
    {

    }
}
//returnCard
[CreateAssetMenu(menuName = "Effects/ReturnCardEffect")]
public class ReturnCard : Effect
{
    public int amount;
    [Header("From")]
    public bool fromfield;
    public bool fromGraveyard;
    public bool fromHand;
    [Header("To")]
    public bool toHand;
    public bool toDeck;
    public override void Execute(Card origin, Card target, Player player, GameManager gm)
    {

    }
}
//tăng tấn công
[CreateAssetMenu(menuName = "Effects/IncreaseAttackEffect")]
public class IncreaseAttack: Effect
{
    public int amount;
    public int atk;
    public bool percent;
    public override void Execute(Card origin, Card target, Player player, GameManager gm)
    {

    }
}
//Miễn hiệu ứng
[CreateAssetMenu(menuName = "Effects/ImmuneEffect")]
public class ImmuneEffect: Effect
{
    public bool SpellEff;
    public bool MonterEff;
    public override void Execute(Card origin, Card target, Player player, GameManager gm)
    {

    }
}
// Hấp thụ
[CreateAssetMenu(menuName = "Effects/AbsorbEffect")]
public class Absorb : Effect
{
    public bool isSpell;
    public int level;
    public int atk;
    public KeyWords keyWords;
    public ElementType Element;
    public RaceType Race;
    public override void Execute(Card origin, Card target, Player player, GameManager gm)
    {

    }
}
// Bỏ bài vào mộ
[CreateAssetMenu(menuName = "Effects/DiscardCardsEffect")]
public class DiscardCards: Effect
{
    public int number;
    [Header("from")]
    public bool Hand;
    public bool field;
    public bool Deck;
    [Header("to")]
    public bool Graveyard;
    public override void Execute(Card origin, Card target, Player player, GameManager gm)
    {

    }
}
// phas huyr bai
[CreateAssetMenu(menuName = "Effects/DestroyCardEffect")]
public class DestroyCard : Effect
{
    public int number;
    public bool isSpell;
    public int level;
    public int atk;
    public KeyWords keyWords;
    public ElementType Element;
    public RaceType Race;
    public override void Execute(Card origin, Card target, Player player, GameManager gm)
    {

    }
}
// vo hieu hoa eff
[CreateAssetMenu(menuName = "Effects/NegetiveEffEffect")]
public class NegetiveEff : Effect
{
    public int number;
    public bool isSpell;
    public int level;
    public int atk;
    public KeyWords keyWords;
    public ElementType Element;
    public RaceType Race;
    public override void Execute(Card origin, Card target, Player player, GameManager gm)
    {

    }
}
// doi quyen dieu khien quai
[CreateAssetMenu(menuName = "Effects/ChangeControlEffEffect")]
public class ChangeControl : Effect
{
  
    public override void Execute(Card origin, Card target, Player player, GameManager gm)
    {

    }
}
// drawCard
[CreateAssetMenu(menuName = "Effects/DrawCardlEffEffect")]
public class DrawCard : Effect
{
    public int number;
    public bool isSpell;
    public int level;
    public int atk;
    public KeyWords keyWords;
    public ElementType Element;
    public RaceType Race;
    public override void Execute(Card origin, Card target, Player player, GameManager gm)
    {

    }
}