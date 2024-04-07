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
public class AnimateBehaviour : Behavior<AnimateBehaviour.EntityData>
{
    [DisplayName("Animate")]
    [DataContract(IsReference = true)]
    public class EntityData : IBehaviorData
    {
        // Process object to reparent.
        [DataMember]
        public SceneObjectReference animator { get; set; }


        [DataMember]
        [DisplayName("State Name")]
        public string stateName { get; set; }

        public Metadata Metadata { get; set; }

        [IgnoreDataMember]
        public string Name
        {
            get
            {
                return "Animate behavior";
            }
        }
    }

    [JsonConstructor, Preserve]
    public AnimateBehaviour(SceneObjectReference _timerObject, string _stateName)
    {
        Data.animator = _timerObject;
        Data.stateName = _stateName;
    }
    public AnimateBehaviour() : this(new SceneObjectReference(), "")
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
            Animator _currentAnimator = Data.animator.Value.GameObject.GetComponent<Animator>();
            if (_currentAnimator)
            {
                _currentAnimator.Play(Data.stateName);
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

