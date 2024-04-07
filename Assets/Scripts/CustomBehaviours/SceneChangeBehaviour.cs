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
using UnityEngine.SceneManagement;


/// <summary>
/// This behavior changes the parent of a game object in the scene hierarchy. It can accept a null parent, in which case the object will be unparented.
/// </summary>
[DataContract(IsReference = true)]
[HelpLink("https://www.mindport.co/vr-builder/manual/default-behaviors/set-parent")]
public class SceneChangeBehaviour : Behavior<SceneChangeBehaviour.EntityData>
{
    [DisplayName("Change Scene")]
    [DataContract(IsReference = true)]
    public class EntityData : IBehaviorData
    {
        [DataMember]
        [DisplayName("Scene Name")]
        public string sceneName { get; set; }

        public Metadata Metadata { get; set; }

        [IgnoreDataMember]
        public string Name
        {
            get
            {
                return "Scene Change behavior";
            }
        }
    }

    [JsonConstructor, Preserve]
    public SceneChangeBehaviour(string _sceneName)
    {
        Data.sceneName = _sceneName;
    }
    public SceneChangeBehaviour() : this("")
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
            if(Data.sceneName != null)
            {
                SceneManager.LoadScene(Data.sceneName);
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

