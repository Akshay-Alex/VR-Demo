using UnityEngine;
using Newtonsoft.Json;
using System.Collections;
using System.Runtime.Serialization;
using VRBuilder.Core.Attributes;
using VRBuilder.Core.SceneObjects;
using VRBuilder.Core.Utils;
using UnityEngine.Scripting;
using VRBuilder.Core.Behaviors;
using VRBuilder.Core;


/// <summary>
/// This behavior changes the parent of a game object in the scene hierarchy. It can accept a null parent, in which case the object will be unparented.
/// </summary>
[DataContract(IsReference = true)]
[HelpLink("https://www.mindport.co/vr-builder/manual/default-behaviors/set-parent")]
public class TimerBehaviour : Behavior<TimerBehaviour.EntityData>
{
    [DisplayName("Set Timer")]
    [DataContract(IsReference = true)]
    public class EntityData : IBehaviorData
    {
        // Process object to reparent.
        [DataMember]
        public SceneObjectReference timer { get; set; }


        [DataMember]
        [DisplayName("Time in seconds")]
        public float time { get; set; }

        public Metadata Metadata { get; set; }

        [IgnoreDataMember]
        public string Name
        {
            get
            {
                return "Timer behavior";
            }
        }
    }

    [JsonConstructor, Preserve]
    public TimerBehaviour(SceneObjectReference _timerObject,float _time)
    {
        Data.timer = _timerObject;
        Data.time = _time;
    }
    public TimerBehaviour() : this(new SceneObjectReference(),0f)
    {
    }


    private class ActivatingProcess : StageProcess<EntityData>
    {
        public ActivatingProcess(EntityData data) : base(data)
        {
        }

        /// <inheritdoc />
        public override void Start()
        {
            Timer timer = Data.timer.Value.GameObject.GetComponent<Timer>();
            if(timer)
            {
                timer.StartTimer(Data.time);
            }
        }

        /// <inheritdoc />
        public override IEnumerator Update()
        {
            yield return null;
        }

        /// <inheritdoc />
        public override void End()
        {
           
        }

        /// <inheritdoc />
        public override void FastForward()
        {
        }
    }

    /// <inheritdoc />
    public override IStageProcess GetActivatingProcess()
    {
        return new ActivatingProcess(Data);
    }
}

