using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GaiaPulse.TextureManager
{
    [Serializable]
    public class TextureProfile //The profile for calling textures.
    {
        public String ID { get; private set; } //Identifier for the ingame texture manager when calling for the needed texture.
        public List<String> CostumeList { get; private set; }//What costumes the texture belongs to. Used for loading things properly.

        public TextureProfile(String ID, List<String> CostumeList)
        {
            this.ID = ID;
            this.CostumeList = CostumeList;
        }
    }
}
