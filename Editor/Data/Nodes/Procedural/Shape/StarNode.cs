using System.Reflection;
using UnityEditor.ShaderGraph;
using UnityEngine;

[Title("Procedural", "Shape", "Star")]
public class StarNode : CodeFunctionNode
{
    public override bool hasPreview { get { return true; } }

    public StarNode()
    {
        name = "Star";
    }

    public override string documentationURL
    {
        get { return "https://github.com/andydbc/unity-shadergraph-nodes"; }
    }

    protected override MethodInfo GetFunctionToConvert()
    {
        return GetType().GetMethod("StarFunction", BindingFlags.Static | BindingFlags.NonPublic);
    }

    static string StarFunction(
        [Slot(0, Binding.MeshUV0)] Vector2 UV,
        [Slot(1, Binding.None, 6, 0, 0, 0)] Vector1 Sides,
        [Slot(2, Binding.None, 0.5f, 0, 0, 0)] Vector1 Scale,
        [Slot(3, Binding.None)] out DynamicDimensionVector Out)
    {
        return @"
{ 
    // based on : https://www.shadertoy.com/view/ldyczd

    float pi = 3.14159265359;

    float N = Sides;
    float r = .5; 
    float2 U = ((UV - 0.5) * 2.) * 1/Scale;

    float a = atan2(U.x, U.y);
    float l = length(U);
    float b = pi/N;
    float tb = tan(b);
      
    float s = (r * tb)/(sqrt(1.+tb*tb)-r); 

    float b2 = 2.*b;
    a = (a-b2*floor(a/b2)) -b;
    U = l * float2(cos(a),sin(a)) / cos(b);
    U.y = abs(U.y);
    U.x -= 1.;

    Out = step(0.001, -(s*U.x+U.y)/sqrt(1.+s*s));
}";
    }
}