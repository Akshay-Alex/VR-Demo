using System.Runtime.Serialization;
using VRBuilder.Core.Attributes;
using UnityEngine;
using Newtonsoft.Json;
using UnityEngine.Scripting;
using VRBuilder.Core.Conditions;
using VRBuilder.Core.SceneObjects;
using VRBuilder.Core;

[DataContract(IsReference = true)]
public class MCQConditions : Condition<MCQConditions.EntityData>
{
    [DisplayName("MCQ Conditions")]
    public class EntityData : IConditionData
    {
        public enum MCQEvents
        {
            OnCorrectAnswerSubmitted,
            OnWrongAnswerSubmitted,
            OnAnswerSubmitted,
            OnMCQComplete,
            OnMCQFailed,
            OnMCQPassed
        }
        /// <summary>
        /// Tray object reference
        /// </summary>
        [DataMember]
        [DisplayName("MCQ")]
        public SceneObjectReference MCQ;
        [DataMember]
        [DisplayName("EventToCheck")]
        public MCQEvents eventToCheck;

        /// <inheritdoc />
        public bool IsCompleted { get; set; }
        [IgnoreDataMember]
        [HideInProcessInspector]
        public string Name
        {
            get
            {
                return "MCQ Events";
            }
        }
        public Metadata Metadata { get; set; }
    }
    private class ActiveProcess : BaseActiveProcessOverCompletable<EntityData>
    {
        public ActiveProcess(EntityData data) : base(data)
        {
        }
        private MCQManager _mCQManager;
        /// <inheritdoc />
        protected override bool CheckIfCompleted()
        {
            return Data.IsCompleted;
        }
        public void OnAnswerSubmittedCallback()
        {
            Data.IsCompleted = true;
        }
        public void OnCorrectAnswerSubmittedCallback()
        {
            Data.IsCompleted = true;

        }
        public void OnWrongAnswerSubmittedCallback()
        {
            Data.IsCompleted = true;
        }
        public void OnMCQCompleteCallback()
        {
            Data.IsCompleted = true;
        }
        public void OnMCQFailedCallback()
        {
            Data.IsCompleted = true;
        } 
        public void OnMCQPassedCallback()
        {
            Data.IsCompleted = true;
        }
        public override void Start()
        {
            if (Data.MCQ.Value != null)
            {
                _mCQManager = Data.MCQ.Value.GameObject.GetComponent<MCQManager>();
                SubscribeToMCQEvents();
            }
            base.Start();
        }
        public void SubscribeToMCQEvents()
        {
            if (_mCQManager)
            {
                switch(Data.eventToCheck)
                {
                    case EntityData.MCQEvents.OnAnswerSubmitted:
                        _mCQManager.OnAnswerSubmitted.AddListener(OnAnswerSubmittedCallback);
                        break;
                    case EntityData.MCQEvents.OnCorrectAnswerSubmitted:
                        _mCQManager.OnCorrectAnswerSubmitted.AddListener(OnCorrectAnswerSubmittedCallback);
                        break;
                    case EntityData.MCQEvents.OnWrongAnswerSubmitted:
                        _mCQManager.OnWrongAnswerSubmitted.AddListener(OnWrongAnswerSubmittedCallback);
                        break;
                    case EntityData.MCQEvents.OnMCQComplete:
                        _mCQManager.OnMCQComplete.AddListener(OnMCQCompleteCallback);
                        break;
                    case EntityData.MCQEvents.OnMCQFailed:
                        _mCQManager.OnMCQFailed.AddListener(OnMCQFailedCallback);
                        break;
                    case EntityData.MCQEvents.OnMCQPassed:
                        _mCQManager.OnMCQPassed.AddListener(OnMCQPassedCallback);
                        break;
                }
            }
        }
    }
    

    [JsonConstructor, Preserve]
    public MCQConditions() : this(new SceneObjectReference())
    {
    }

    public MCQConditions(SceneObjectReference target)
    {
        Data.MCQ = target;
    }

    /// <inheritdoc />
    public override IStageProcess GetActiveProcess()
    {
        return new ActiveProcess(Data);
    }
}
