using System;

namespace GaiaPulse.PartManager.PartData
{
    [Serializable]
    public class PartCommonData
    {
        public int AnchorPoints { get; private set; }

        public void SetAnchorNumber(int Number)
        {
            AnchorPoints = Number;
        }
    }
}