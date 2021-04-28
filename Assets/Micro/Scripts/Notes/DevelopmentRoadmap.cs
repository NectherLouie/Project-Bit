using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MB
{
    [Serializable]
    public class DevelopmentNote
    {
        public enum TaskType { BACKLOG, TODO, DOING, DONE };

        public TaskType type;
        public string text;
    }

    public class DevelopmentRoadmap : MonoBehaviour
    {
        public List<DevelopmentNote> devNotes = new List<DevelopmentNote>();
    }
}
