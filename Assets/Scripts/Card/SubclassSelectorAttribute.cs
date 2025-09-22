using System;
using UnityEngine;

// Attribute phải tồn tại ở runtime để compiler nhìn thấy khi bạn dùng [SubclassSelector]
[AttributeUsage(AttributeTargets.Field, Inherited = true)]
public class SubclassSelectorAttribute : PropertyAttribute { }
