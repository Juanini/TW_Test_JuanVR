using UnityEngine;
using FSA = UnityEngine.Serialization.FormerlySerializedAsAttribute;

namespace SpaceGraphicsToolkit
{
	/// <summary>This component allows you to procedurally generate the SpriteRenderer.color setting.</summary>
	[RequireComponent(typeof(SpriteRenderer))]
	[HelpURL(SgtHelper.HelpUrlPrefix + "SgtProceduralTint")]
	[AddComponentMenu(SgtHelper.ComponentMenuPrefix + "Procedural Tint")]
	public class SgtProceduralTint : SgtProcedural
	{
		/// <summary>A color will be randomly picked from this gradient.</summary>
		public Gradient Colors { get { if (colors == null) colors = new Gradient(); return colors; } } [FSA("Colors")] [SerializeField] private Gradient colors;

		protected override void DoGenerate()
		{
			var spriteRenderer = GetComponent<SpriteRenderer>();

			spriteRenderer.color = colors.Evaluate(Random.value);
		}
	}
}

#if UNITY_EDITOR
namespace SpaceGraphicsToolkit
{
	using TARGET = SgtProceduralTint;

	[UnityEditor.CanEditMultipleObjects]
	[UnityEditor.CustomEditor(typeof(TARGET))]
	public class SgtProceduralTint_Editor : SgtProcedural_Editor
	{
		protected override void OnInspector()
		{
			TARGET tgt; TARGET[] tgts; GetTargets(out tgt, out tgts);

			base.OnInspector();

			Draw("colors", "A color will be randomly picked from this gradient.");
		}
	}
}
#endif