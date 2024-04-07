using VRBuilder.BasicInteraction.Conditions;
using VRBuilder.Core.Conditions;
using VRBuilder.Editor.UI.StepInspector.Menu;

/// <inheritdoc />
public class TimerConditionsMenuItem : MenuItem<ICondition>
{
    /// <inheritdoc />
    public override string DisplayedName { get; } = "Timer/Timeout";

    /// <inheritdoc />
    public override ICondition GetNewItem()
    {
        return new TimerConditions();
    }
}
