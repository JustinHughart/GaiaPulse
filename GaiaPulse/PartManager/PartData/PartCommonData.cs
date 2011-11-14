using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
