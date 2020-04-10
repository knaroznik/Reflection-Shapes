using System;
using System.Diagnostics;
using System.Reflection;

public abstract class ValidateAttribute : Attribute
{
    [Conditional("UNITY_EDITOR")]
    public abstract void Validate(ref object value, FieldInfo field, RootBehaviour rootObject);
}
