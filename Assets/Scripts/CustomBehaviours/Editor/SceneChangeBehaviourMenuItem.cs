using VRBuilder.Core.Behaviors;
using VRBuilder.Editor.UI.StepInspector.Menu;

/// <inheritdoc />
public class SceneChangeBehaviourMenuItem : MenuItem<IBehavior>
{
    /// <inheritdoc />
    public override string DisplayedName { get; } = "Scenes/Change Scene";

    /// <inheritdoc />
    public override IBehavior GetNewItem()
    {
        return new SceneChangeBehaviour();
    }
}
