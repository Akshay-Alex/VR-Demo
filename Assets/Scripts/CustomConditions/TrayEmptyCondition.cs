using System.Runtime.Serialization;
using VRBuilder.Core.Attributes;
using UnityEngine;
using Newtonsoft.Json;
using UnityEngine.Scripting;
using  VRBuilder.Core.Conditions;
using VRBuilder.Core.SceneObjects;
using VRBuilder.Core;

[DataContract(IsReference = true)]
public class TrayEmptyCondition : Condition<TrayEmptyCondition.EntityData>
{
    [DisplayName("Tray Empty")]
    public class EntityData : IConditionData
    {
        public enum TrayState
        {
            Empty,
            NotEmpty,
            Full
        }
        /// <summary>
        /// Tray object reference
        /// </summary>
        [DataMember]
        [DisplayName("Tray")]
        public SceneObjectReference tray;
        [DataMember]
        [DisplayName("StateToCheck")]
        public TrayState stateToCheck;

        /// <inheritdoc />
        public bool IsCompleted { get; set; }
        [IgnoreDataMember]
        [HideInProcessInspector]
        public string Name
        {
            get
            {
                return "Check if tray is empty";
            }
        }
        public Metadata Metadata { get; set; }
    }
    private class ActiveProcess : BaseActiveProcessOverCompletable<EntityData>
    {
        public ActiveProcess(EntityData data) : base(data)
        {
        }
        private Tray _tray;
        /// <inheritdoc />
        protected override bool CheckIfCompleted()
        {
            switch(Data.stateToCheck)
            {
                case EntityData.TrayState.Empty:
                    return _tray.numberOfObjectsOnTray == 0;
                case EntityData.TrayState.NotEmpty:
                    return ((_tray.numberOfObjectsOnTray > 0) && (_tray.numberOfObjectsOnTray != _tray.trayMaximumCapacity));
                case EntityData.TrayState.Full:
                    return _tray.numberOfObjectsOnTray == _tray.trayMaximumCapacity;
                default:
                    return false;
            }
        }
        public override void Start()
        {
            if(Data.tray.Value != null)
            {
                _tray = Data.tray.Value.GameObject.GetComponent<Tray>();
            }
            base.Start();
        }
    }
    
    [JsonConstructor, Preserve]
    public TrayEmptyCondition() : this(new SceneObjectReference())
    {
    }

    public TrayEmptyCondition(SceneObjectReference target)
    {
        Data.tray = target;
    }

    /// <inheritdoc />
    public override IStageProcess GetActiveProcess()
    {
        return new ActiveProcess(Data);
    }
}
