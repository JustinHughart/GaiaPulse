using System;
using System.Collections.Generic;

namespace GaiaPulse.AnimationManager
{
    [Serializable]
    public class PartTree
    {
        public String ID { get; private set; } //The ID of the part that exists as is in the tree.

        public String Parent { get; private set; } //The ID of the parent to which the part belongs.

        public List<String> Children { get; private set; } //The IDs of all the children that the Part has.

        public void SetID(String NewID)
        {
            ID = NewID;
        }

        public void SetParent(String NewParent)
        {
            Parent = NewParent;
        }

        public void AddChild(String Child)
        {
            Children.Add(Child);
        }

        public void RemoveChild(int Position)
        {
            Children.RemoveAt(Position);
        }
    }
}