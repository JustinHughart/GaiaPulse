using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace SC
{
    public static class TextureManager //Holds a list of textures and a list of references to said textures. Also handles the disposing and loading of said textures.
    {
        private static Dictionary<String, Texture2D> TextureDictionary; //A list of textures.
        private static Dictionary<String, List<Object>> ReferenceList; //A list of objects that are referencing the textures.
        private static ContentManager Content; //Used for loading content.

        public static bool Initialized { get; private set; } //Whether or not the texture manager is initialized. Captain Obvious, up and away!

        private static GraphicsDevice GraphicsDevice;

        public static void InitializeWithoutContent(GraphicsDevice GFX)
        {
            if (Initialized)
            {
                foreach (var Texture in TextureDictionary)
                {
                    Texture.Value.Dispose();
                }
            }

            TextureDictionary = new Dictionary<String, Texture2D>();
            ReferenceList = new Dictionary<String, List<Object>>();
            GraphicsDevice = GFX;
            Initialized = true;
        }

        public static void Initialize(ContentManager NewContent) //Initialization method. Assigns a ContentManager and initializes the class. Also disposes of textures if the class is already initialized.
        {
            if (Initialized)
            {
                foreach (var Texture in TextureDictionary)
                {
                    Texture.Value.Dispose();
                }
            }

            TextureDictionary = new Dictionary<String, Texture2D>();
            ReferenceList = new Dictionary<String, List<Object>>();
            Content = NewContent;
            Initialized = true;
        }

        public static Texture2D GetTexture(String Name, Object Object) //Returns a texture. If the texture exists, it returns the texture, otherwise it loads it into the texture dictionary and then returns it. Also adds the reference to the reference list.
        {
            if (Initialized)
            {
                if (TextureDictionary.ContainsKey(Name))
                {
                    AddReference(Name, Object);
                    return TextureDictionary[Name];
                }
                else
                {
                    TextureDictionary.Add(Name, Content.Load<Texture2D>(Name));
                    AddReference(Name, Object);
                    return TextureDictionary[Name];
                }
            }

            throw new Exception("Texture manager is not initialized.");
        }

        public static Texture2D GetTextureFromFile(String Name, Object Object)
        {
            if (Initialized && GraphicsDevice != null)
            {
                if (TextureDictionary.ContainsKey(Name))
                {
                    AddReference(Name, Object);
                    return TextureDictionary[Name];
                }
                else
                {
                    Stream Stream = File.OpenRead(Name);

                    Texture2D OutputTexture = Texture2D.FromStream(GraphicsDevice, Stream);

                    Stream.Close();

                    TextureDictionary.Add(Name, OutputTexture);
                    AddReference(Name, Object);
                    return TextureDictionary[Name];
                }
            }

            throw new Exception("Texture manager is not initialized for files.");
        }

        private static void AddReference(String Name, Object Object) //Adds a reference to the reference list, creating the list if necessary.
        {
            if (ReferenceList.ContainsKey(Name))
            {
                if (!ReferenceList[Name].Contains(Object))
                {
                    ReferenceList[Name].Add(Object);
                }
            }
            else
            {
                List<Object> NewList = new List<Object>();
                NewList.Add(Object);
                ReferenceList.Add(Name, NewList);
            }
        }

        public static void RemoveReference(String Name, Object Object) //Removes a reference, disposing of the texture if necessary.
        {
            if (Initialized)
            {
                if (ReferenceList.ContainsKey(Name))
                {
                    if (ReferenceList[Name].Contains(Object))
                    {
                        ReferenceList[Name].Remove(Object);

                        if (ReferenceList[Name].Count == 0)
                        {
                            ReferenceList.Remove(Name);

                            TextureDictionary[Name].Dispose();
                            TextureDictionary.Remove(Name);
                        }
                    }
                    else
                    {
                        throw new Exception("Object was not referencing " + Name);
                    }
                }
                else
                {
                    throw new Exception("Reference does not exist.");
                }
            }
            else
            {
                throw new Exception("Texture Manager not initialized.");
            }
        }

        public static void ClearTextures() //Disposes of all the textures and clears the texture dictionary and reference list.
        {
            if (Initialized)
            {
                foreach (var Texture in TextureDictionary)
                {
                    Texture.Value.Dispose();
                }

                TextureDictionary.Clear();
                ReferenceList.Clear();
            }
            else
            {
                throw new Exception("Texture manager is not initialized.");
            }
        }

        public static List<Object> GetReferenceList(String Name) //Returns the reference list itself. Why? I'm not sure but hey, it's functionality.
        {
            if (ReferenceList.ContainsKey(Name))
            {
                return ReferenceList[Name];
            }

            return null;
        }
    }
}