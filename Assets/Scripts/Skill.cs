using System;
using UnityEngine;

[Serializable]
public class Skill
{
    public string SkillName;         // Tên kỹ năng
    public string Description;       // Mô tả kỹ năng
    public SkillType Type;           // Loại kỹ năng (Quái, Phép, Bẫy)
    public SkillTrigger Trigger;     // Khi nào kích hoạt kỹ năng
    public SkillEffectType Effect;   // Loại hiệu ứng của kỹ năng
    public int EffectValue;          // Giá trị (Ví dụ: %ATK tăng, lượng HP hồi)
    public bool RequiresSacrifice;   // Cần hiến tế để kích hoạt không?
    public bool AffectsOpponent;     // Ảnh hưởng lên đối thủ không?
    public bool AffectsField;        // Ảnh hưởng toàn bộ sân không?

    public Skill(string name, string description, SkillType type, SkillTrigger trigger, SkillEffectType effect, int value, bool requiresSacrifice, bool affectsOpponent, bool affectsField)
    {
        SkillName = name;
        Description = description;
        Type = type;
        Trigger = trigger;
        Effect = effect;
        EffectValue = value;
        RequiresSacrifice = requiresSacrifice;
        AffectsOpponent = affectsOpponent;
        AffectsField = affectsField;
    }

    public void ActivateSkill(Card owner, Card target = null)
    {
        switch (Effect)
        {
            case SkillEffectType.ReflectDamage:
                if (target != null)
                {
                    int reflectedDamage = Mathf.CeilToInt(target.Attack * (EffectValue / 100f));
                    target.HP -= reflectedDamage;
                    Debug.Log($"{owner.CardName} phản sát thương {reflectedDamage} lên {target.CardName}!");
                }
                break;

            case SkillEffectType.DestroyCard:
                if (target != null)
                {
                    Debug.Log($"{owner.CardName} phá hủy {target.CardName}!");
                }
                break;

            case SkillEffectType.IncreaseATK:
                owner.Attack += Mathf.CeilToInt(owner.Attack * (EffectValue / 100f));
                Debug.Log($"{owner.CardName} tăng {EffectValue}% ATK!");
                break;

            case SkillEffectType.DrawCard:
                Debug.Log($"{owner.CardName} kích hoạt, rút {EffectValue} lá bài!");
                break;

            case SkillEffectType.SpecialSummon:
                Debug.Log($"{owner.CardName} triệu hồi đặc biệt một quái vật!");
                break;

            case SkillEffectType.Revival:
                Debug.Log($"{owner.CardName} hồi sinh một quái vật từ mộ bài!");
                break;

            case SkillEffectType.AbsorbCard:
                if (target != null)
                {
                    int absorbedAtk = Mathf.CeilToInt(target.Attack * (EffectValue / 100f));
                    owner.Attack += absorbedAtk;
                    Debug.Log($"{owner.CardName} hấp thụ {EffectValue}% ATK của {target.CardName}!");
                }
                break;

            case SkillEffectType.FieldWipe:
                Debug.Log($"{owner.CardName} kích hoạt, hủy diệt toàn bộ bài trên sân!");
                break;

            default:
                Debug.Log($"{owner.CardName} kích hoạt hiệu ứng {SkillName}!");
                break;
        }
    }
}
