using System.Runtime.Serialization;
using VRBuilder.Core.Attributes;
using UnityEngine;
using Newtonsoft.Json;
using UnityEngine.Scripting;
using VRBuilder.Core.Conditions;
using VRBuilder.Core.SceneObjects;
using VRBuilder.Core;

[DataContract(IsReference = true)]
public class TimerConditions : Condition<TimerConditions.EntityData>
{
    [DisplayName("Timer conditions")]
    public class EntityData : IConditionData
    {
        public enum TimerEvents
        {
            OnTimerRanOut
        }
        /// <summary>
        /// Tray object reference
        /// </summary>
        [DataMember]
        [DisplayName("Timer")]
        public SceneObjectReference timer;
        [DataMember]
        [DisplayName("EventToCheck")]
        public TimerEvents eventToCheck;

        /// <inheritdoc />
        public bool IsCompleted { get; set; }
        [IgnoreDataMember]
        [HideInProcessInspector]
        public string Name
        {
            get
            {
                return "Timer Events";
            }
        }
        public Metadata Metadata { get; set; }
    }
    private class ActiveProcess : BaseActiveProcessOverCompletable<EntityData>
    {
        public ActiveProcess(EntityData data) : base(data)
        {
        }
        private Timer _timer;
        /// <inheritdoc />
        protected override bool CheckIfCompleted()
        {
            return Data.IsCompleted;
        }
        public void OnTimerRanOutCallback()
        {
            Data.IsCompleted = true;
        }
      
        public override void Start()
        {
            if (Data.timer.Value != null)
            {
                _timer = Data.timer.Value.GameObject.GetComponent<Timer>();
                SubscribeToTimerEvents();
            }
            base.Start();
        }
        public void SubscribeToTimerEvents()
        {
            if (_timer)
            {
                switch (Data.eventToCheck)
                {
                    case EntityData.TimerEvents.OnTimerRanOut:
                        _timer.OnTimerRanOut.AddListener(OnTimerRanOutCallback);
                        break;
                }
            }
        }
    }


    [JsonConstructor, Preserve]
    public TimerConditions() : this(new SceneObjectReference())
    {
    }

    public TimerConditions(SceneObjectReference target)
    {
        Data.timer = target;
    }

    /// <inheritdoc />
    public override IStageProcess GetActiveProcess()
    {
        return new ActiveProcess(Data);
    }
}
