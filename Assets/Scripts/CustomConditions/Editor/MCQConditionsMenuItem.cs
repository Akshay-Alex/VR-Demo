using VRBuilder.BasicInteraction.Conditions;
using VRBuilder.Core.Conditions;
using VRBuilder.Editor.UI.StepInspector.Menu;

public class MCQConditionsMenuItem : MenuItem<ICondition>
{
    public override string DisplayedName { get; } = "MCQ/MCQ Conditions";

    public override ICondition GetNewItem()
    {
        return new MCQConditions();
    }
}