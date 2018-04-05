using System.Reflection;
using UnityEditor.Graphing;
using UnityEditor.ShaderGraph;
using UnityEditor.ShaderGraph.Drawing.Controls;
using UnityEngine;

[Title("Math", "Interpolation", "Easing")]
public class EasingNode : CodeFunctionNode
{
    public override bool hasPreview { get { return true; } }

    public EasingNode()
    {
        name = "Easing";
    }

    public override string documentationURL
    {
        get { return "https://github.com/andydbc/unity-shadergraph-nodes"; }
    }

    [SerializeField]
    EasingType m_EasingType = EasingType.Linear;

    [EnumControl("Type")]
    public EasingType Type
    {
        get { return m_EasingType; }
        set
        {
            if (m_EasingType == value)
                return;

            m_EasingType = value;
            Dirty(ModificationScope.Graph);
        }
    }

    string GetTypeName()
    {
        return System.Enum.GetName(typeof(EasingType), m_EasingType);
    }

    protected override MethodInfo GetFunctionToConvert()
    {
        return GetType().GetMethod(string.Format("Easing_{0}", GetTypeName()), BindingFlags.Static | BindingFlags.NonPublic);
    }

    static string Easing_Linear(
        [Slot(0, Binding.None)] Vector1 In,
        [Slot(2, Binding.None)] out Vector1 Out)
    {
        return "{ Out = In; }";
    }

    static string Easing_QuadraticIn(
        [Slot(0, Binding.None)] Vector1 In,
        [Slot(2, Binding.None)] out Vector1 Out)
    {
        return @"{ 
            Out = In * In;
        }";
    }

    static string Easing_QuadraticOut(
        [Slot(0, Binding.None)] Vector1 In,
        [Slot(2, Binding.None)] out Vector1 Out)
    {
        return @"{
            Out = -In * (In - 2.0); 
        }";
    }

    static string Easing_QuadraticInOut(
        [Slot(0, Binding.None)] Vector1 In,
        [Slot(2, Binding.None)] out Vector1 Out)
    {
        return @"{ 
            float p = 2.0 * In * In;
            Out = In < 0.5 ? p : -p + (4.0 * In) - 1.0;
        }";
    }

    static string Easing_CubicIn(
       [Slot(0, Binding.None)] Vector1 In,
       [Slot(2, Binding.None)] out Vector1 Out)
    {
        return @"{ 
            Out = In * In * In;
        }";
    }

    static string Easing_CubicOut(
       [Slot(0, Binding.None)] Vector1 In,
       [Slot(2, Binding.None)] out Vector1 Out)
    {
        return @"{ 
            float f = In - 1.0;
            Out = f * f * f  + 1.0;
        }";
    }

    static string Easing_CubicInOut(
       [Slot(0, Binding.None)] Vector1 In,
       [Slot(2, Binding.None)] out Vector1 Out)
    {
        return @"{ 
            Out = In < 0.5
            ? 4.0 * In * In * In
            : 0.5 * pow(2.0 * In - 2.0, 3.0) + 1.0;
        }";
    }

    static string Easing_QuarticIn(
      [Slot(0, Binding.None)] Vector1 In,
      [Slot(2, Binding.None)] out Vector1 Out)
    {
        return @"{ 
            Out = pow(In, 4.0);
        }";
    }

    static string Easing_QuarticOut(
      [Slot(0, Binding.None)] Vector1 In,
      [Slot(2, Binding.None)] out Vector1 Out)
    {
        return @"{ 
            Out = pow(In - 1.0, 3.0) * (1.0 - In) + 1.0;
        }";
    }

    static string Easing_QuarticInOut(
      [Slot(0, Binding.None)] Vector1 In,
      [Slot(2, Binding.None)] out Vector1 Out)
    {
        return @"{ 
            Out = In < 0.5
            ? +8.0 * pow(In, 4.0)
            : -8.0 * pow(In - 1.0, 4.0) + 1.0;
        }";
    }

    static string Easing_QuinticIn(
      [Slot(0, Binding.None)] Vector1 In,
      [Slot(2, Binding.None)] out Vector1 Out)
    {
        return @"{ 
            Out = pow(In, 5.0);
        }";
    }

    static string Easing_QuinticOut(
      [Slot(0, Binding.None)] Vector1 In,
      [Slot(2, Binding.None)] out Vector1 Out)
    {
        return @"{ 
             float f = In - 1;
             Out = f * f * f * f * f + 1;
        }";
    }

    static string Easing_QuinticInOut(
      [Slot(0, Binding.None)] Vector1 In,
      [Slot(2, Binding.None)] out Vector1 Out)
    {
        return @"{ 
            float f = (2.0 * In) - 2.0;
            Out = In < 0.5
                ? 16 * pow(In, 5) 
                : 0.5 * pow(f, 5) + 1.0;
        }";
    }

    static string Easing_SinIn(
      [Slot(0, Binding.None)] Vector1 In,
      [Slot(2, Binding.None)] out Vector1 Out)
    {
        return @"{ 
            float half_pi = 1.5707963267948966;
            Out = sin((In - 1.0) * half_pi) + 1.0;
        }";
    }

    static string Easing_SinOut(
      [Slot(0, Binding.None)] Vector1 In,
      [Slot(2, Binding.None)] out Vector1 Out)
    {
        return @"{ 
            float half_pi = 1.5707963267948966;
            Out = sin(In * half_pi);
        }";
    }

    static string Easing_SinInOut(
      [Slot(0, Binding.None)] Vector1 In,
      [Slot(2, Binding.None)] out Vector1 Out)
    {
        return @"{ 
            float pi = 3.141592653589793;
            Out = -0.5 * (cos(pi * In) - 1.0);
        }";
    }

    static string Easing_ExponentialIn(
      [Slot(0, Binding.None)] Vector1 In,
      [Slot(2, Binding.None)] out Vector1 Out)
    {
        return @"{ 
            Out = (In == 0.0) 
            ? In 
            : pow(2.0, 10.0 * (In - 1.0));
        }";
    }

    static string Easing_ExponentialOut(
      [Slot(0, Binding.None)] Vector1 In,
      [Slot(2, Binding.None)] out Vector1 Out)
    {
        return @"{ 
            Out = (In == 1.0) 
            ? In 
            : 1.0 - pow(2.0, -10.0 * In);
        }";
    }

    static string Easing_ExponentialInOut(
      [Slot(0, Binding.None)] Vector1 In,
      [Slot(2, Binding.None)] out Vector1 Out)
    {
        return @"{ 
            Out = In == 0.0 || In == 1.0
            ? In
            : In < 0.5
            ? +0.5 * pow(2.0, (20.0 * In) - 10.0)
            : -0.5 * pow(2.0, 10.0 - (In * 20.0)) + 1.0;
        }";
    }

    static string Easing_CircularIn(
      [Slot(0, Binding.None)] Vector1 In,
      [Slot(2, Binding.None)] out Vector1 Out)
    {
        return @"{ 
            Out = 1.0 - sqrt(1.0 - In * In);
        }";
    }

    static string Easing_CircularOut(
      [Slot(0, Binding.None)] Vector1 In,
      [Slot(2, Binding.None)] out Vector1 Out)
    {
        return @"{ 
            Out = sqrt((2.0 - In) * In);
        }";
    }

    static string Easing_CircularInOut(
      [Slot(0, Binding.None)] Vector1 In,
      [Slot(2, Binding.None)] out Vector1 Out)
    {
        return @"{ 
            Out =  In < 0.5
            ? 0.5 * (1.0 - sqrt(1.0 - 4.0 * In * In))
            : 0.5 * (sqrt((3.0 - 2.0 * In) * (2.0 * In - 1.0)) + 1.0);
        }";
    }

    static string Easing_ElasticIn(
      [Slot(0, Binding.None)] Vector1 In,
      [Slot(2, Binding.None)] out Vector1 Out)
    {
        return @"{ 
            float half_pi = 1.5707963267948966;
            Out = sin(13.0 * In * half_pi) * pow(2.0, 10.0 * (In - 1.0));
        }";
    }

    static string Easing_ElasticOut(
      [Slot(0, Binding.None)] Vector1 In,
      [Slot(2, Binding.None)] out Vector1 Out)
    {
        return @"{ 
            float half_pi = 1.5707963267948966;
            Out = sin(-13.0 * (In + 1.0) * half_pi) * pow(2.0, -10.0 * In) + 1.0;
        }";
    }

    static string Easing_ElasticInOut(
      [Slot(0, Binding.None)] Vector1 In,
      [Slot(2, Binding.None)] out Vector1 Out)
    {
        return @"{ 
            float half_pi = 1.5707963267948966;
            Out = In < 0.5
            ? 0.5 * sin(+13.0 * half_pi * 2.0 * In) * pow(2.0, 10.0 * (2.0 * In - 1.0))
            : 0.5 * sin(-13.0 * half_pi * ((2.0 * In - 1.0) + 1.0)) * pow(2.0, -10.0 * (2.0 * In - 1.0)) + 1.0;
        }";
    }

    static string Easing_BackIn(
      [Slot(0, Binding.None)] Vector1 In,
      [Slot(2, Binding.None)] out Vector1 Out)
    {
        return @"{ 
            float pi = 3.141592653589793;
            Out = pow(In, 3.0) - In * sin(In * pi);
        }";
    }

    static string Easing_BackOut(
      [Slot(0, Binding.None)] Vector1 In,
      [Slot(2, Binding.None)] out Vector1 Out)
    {
        return @"{ 
            float pi = 3.141592653589793;
            float f = 1.0 - In;
            Out = 1.0 - (pow(f, 3.0) - f * sin(f * pi));
        }";
    }

    static string Easing_BackInOut(
      [Slot(0, Binding.None)] Vector1 In,
      [Slot(2, Binding.None)] out Vector1 Out)
    {
        return @"{ 
            float pi = 3.141592653589793;
            float f = In < 0.5
            ? 2.0 * In
            : 1.0 - (2.0 * In - 1.0);

            float g = pow(f, 3.0) - f * sin(f * pi);

            Out = In < 0.5
            ? 0.5 * g
            : 0.5 * (1.0 - g) + 0.5;
        }";
    }

    static string Easing_BounceIn(
      [Slot(0, Binding.None)] Vector1 In,
      [Slot(2, Binding.None)] out Vector1 Out)
    {
        return @"{ 
            Out = 1.0 - easing_custom_bounce_out(1.0 - In);
        }";
    }

    static string Easing_BounceOut(
      [Slot(0, Binding.None)] Vector1 In,
      [Slot(2, Binding.None)] out Vector1 Out)
    {
        return @"{ 
            Out = easing_custom_bounce_out(In);
        }";
    }

    static string Easing_BounceInOut(
      [Slot(0, Binding.None)] Vector1 In,
      [Slot(2, Binding.None)] out Vector1 Out)
    {
        return @"{ 
            Out = In < 0.5
            ? 0.5 * (1.0 - easing_custom_bounce_out(1.0 - In * 2.0))
            : 0.5 * easing_custom_bounce_out(In * 2.0 - 1.0) + 0.5;
        }";
    }

    public override void GenerateNodeFunction(FunctionRegistry registry, GenerationMode generationMode)
    {
        registry.ProvideFunction("easing_custom_bounce_out", s => s.Append(@"
            float easing_custom_bounce_out(float t)
            {
                const float a = 4.0 / 11.0;
                const float b = 8.0 / 11.0;
                const float c = 9.0 / 10.0;

                const float ca = 4356.0 / 361.0;
                const float cb = 35442.0 / 1805.0;
                const float cc = 16061.0 / 1805.0;
            
                float t2 = t * t;

                return t < a
                ? 7.5625 * t2
                : t < b
                    ? 9.075 * t2 - 9.9 * t + 3.4
                    : t < c
                    ? ca * t2 - cb * t + cc
                    : 10.8 * t * t - 20.52 * t + 10.72;
            }
        "));

        base.GenerateNodeFunction(registry, generationMode);
    }
}
