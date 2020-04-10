using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using UnityEngine;

public class NotNullAttribute : ValidateAttribute
{
    public override void Validate(ref object value, FieldInfo field, RootBehaviour rootObject)
    {
        if(field.FieldType == typeof(GameObject))
        {
            GameObject x = value as GameObject;
            if(x == null)
            {
                Debug.LogError("<color=blue>[NotNull]</color> " + field.Name + " in " + rootObject.name + " is null");
            }
        }
        else
        {
            Debug.LogWarning("<color=blue>[NotNull]</color> Is only implemented for GameObjects, not for " + field.FieldType.ToString());
        }
            
    }
}
