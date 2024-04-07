using VRBuilder.Core.Behaviors;
using VRBuilder.Editor.UI.StepInspector.Menu;

/// <inheritdoc />
public class AnimateBehaviourMenuItem : MenuItem<IBehavior>
{
    /// <inheritdoc />
    public override string DisplayedName { get; } = "Animate/Set animation state";

    /// <inheritdoc />
    public override IBehavior GetNewItem()
    {
        return new AnimateBehaviour();
    }
}
