using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GaiaPulse.AnimationManager.EditorData
{
    public class MouseInputBase
    {
        protected EditorControl Editor;

        public bool IsGrabbing { get; private set; }
        public string GrabTarget { get; private set; }

        public MouseInputBase(EditorControl editor)
        {
            Editor = editor;
        }

        public virtual bool Update() //Returns whether or not it's in control. This is for camera control.
        {
            return false;
        }

        public void Grab(String target)
        {
            IsGrabbing = true;
            GrabTarget = target;
        }

        public void Release()
        {
            IsGrabbing = false;
            GrabTarget = "";
        }
    }
}
