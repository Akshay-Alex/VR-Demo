using VRBuilder.Core.Behaviors;
using VRBuilder.Editor.UI.StepInspector.Menu;

/// <inheritdoc />
public class TimerBehaviourMenuItem : MenuItem<IBehavior>
{
    /// <inheritdoc />
    public override string DisplayedName { get; } = "Timer/Set Timer";

    /// <inheritdoc />
    public override IBehavior GetNewItem()
    {
        return new TimerBehaviour();
    }
}
