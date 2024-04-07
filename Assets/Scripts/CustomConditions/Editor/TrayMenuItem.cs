using VRBuilder.BasicInteraction.Conditions;
using VRBuilder.Core.Conditions;
using VRBuilder.Editor.UI.StepInspector.Menu;

public class TrayMenuItem : MenuItem<ICondition>
{
    public override string DisplayedName { get; } = "Tray/Tray Empty";

    public override ICondition GetNewItem()
    {
        return new TrayEmptyCondition();
    }
}