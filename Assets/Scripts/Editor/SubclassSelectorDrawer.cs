#if UNITY_EDITOR
using System;
using System.Linq;
using System.Collections.Generic;
using System.Reflection;
using UnityEditor;
using UnityEngine;



[CustomPropertyDrawer(typeof(SubclassSelectorAttribute))]
public class SubclassSelectorDrawer : PropertyDrawer
{
    private static readonly Dictionary<Type, Type[]> _cache = new();

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        return EditorGUI.GetPropertyHeight(property, label, true);
    }

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        // Vẽ nhãn
        position = EditorGUI.PrefixLabel(position, label);

        // Nút thêm/chọn type
        var left = new Rect(position.x, position.y, 80, EditorGUIUtility.singleLineHeight);
        var right = new Rect(position.x + 85, position.y, position.width - 85, EditorGUIUtility.singleLineHeight);

        if (GUI.Button(left, property.managedReferenceValue == null ? "Add" : "Change"))
        {
            ShowTypeMenuFor(property);
        }

        // Vẽ phần object nếu đã có
        if (property.managedReferenceValue != null)
        {
            EditorGUI.PropertyField(right, property, GUIContent.none, true);
        }
        else
        {
            EditorGUI.HelpBox(right, "Chưa chọn Effect", MessageType.Info);
        }
    }

    private void ShowTypeMenuFor(SerializedProperty property)
    {
        var menu = new GenericMenu();

        // Tìm interface/abstract gốc từ field info
        var fieldType = GetFieldSystemType(fieldInfo);
        if (fieldType == null)
            return;

        var baseType = fieldType;
        if (fieldType.IsArray) baseType = fieldType.GetElementType();
        if (baseType.IsGenericType && baseType.GetGenericArguments().Length == 1)
            baseType = baseType.GetGenericArguments()[0];

        var allowedTypes = GetAllImplementations(baseType);
        foreach (var t in allowedTypes)
        {
            menu.AddItem(new GUIContent(t.Name), false, () =>
            {
                var instance = Activator.CreateInstance(t);
                property.serializedObject.Update();
                property.managedReferenceValue = instance;
                property.serializedObject.ApplyModifiedProperties();
            });
        }

        menu.ShowAsContext();
    }

    private static Type GetFieldSystemType(FieldInfo fi)
    {
        if (fi == null) return null;
        return fi.FieldType;
    }

    private static Type[] GetAllImplementations(Type baseType)
    {
        if (_cache.TryGetValue(baseType, out var cached))
            return cached;

        var types = AppDomain.CurrentDomain.GetAssemblies()
            .Where(a =>
            {
                // bỏ editor assemblies linh tinh
                var name = a.GetName().Name;
                return !name.StartsWith("UnityEditor") && !name.StartsWith("nunit") && !name.StartsWith("Unity.") && !name.StartsWith("JetBrains");
            })
            .SelectMany(a =>
            {
                try { return a.GetTypes(); }
                catch { return Array.Empty<Type>(); }
            })
            .Where(t =>
                t != null &&
                !t.IsAbstract &&
                baseType.IsAssignableFrom(t) &&
                t.GetConstructor(Type.EmptyTypes) != null // cần ctor mặc định cho Activator
            )
            .OrderBy(t => t.Name)
            .ToArray();

        _cache[baseType] = types;
        return types;
    }
}
#endif
