using System.Reflection;
using UnityEngine;

public abstract class RootBehaviour : MonoBehaviour
{
#if UNITY_EDITOR
    protected virtual void OnValidate()
    {
        // Predefine flags
        const BindingFlags flags =
              BindingFlags.Public
            | BindingFlags.NonPublic
            | BindingFlags.Instance;

        // Get fields
        var type = GetType();
        var fields = type.GetFields(flags);
        foreach (var field in fields)
        {
            // Get field value
            var attributes = field.GetCustomAttributes(typeof(ValidateAttribute), true);

            // Check if has any attributes to safe some calculations
            if (attributes.Length < 1)
                continue;

            // Get the field its value
            var value = field.GetValue(this);

            // Validate every field
            foreach (var attribute in attributes)
            {
                // Convert to validate
                var validateAttribute = attribute as ValidateAttribute;
                if (validateAttribute == null)
                    continue;

                // Perform action
                validateAttribute.Validate(ref value, field, this);
            }

            // Apply
            field.SetValue(this, value);
        }
    }
#else
        protected virtual void OnValidate() { }
#endif
}
