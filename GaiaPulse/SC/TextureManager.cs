using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using GaiaPulse.AnimationManager;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GaiaPulse.SC
{
    /// <summary>
    /// Holds textures for the program.
    /// </summary>
    public static class TextureManager 
    {
        /// <summary>
        /// The texture dictionary for storing textures.
        /// </summary>
        static private Dictionary<String, Texture2D> _texturedictionary; //A list of textures.

        /// <summary>
        /// Gets a value indicating whether this <see cref="TextureManager"/> is initialized.
        /// </summary>
        /// <value>
        ///   <c>true</c> if initialized; otherwise, <c>false</c>.
        /// </value>
        static public bool Initialized { get; private set; } 

        /// <summary>
        /// The graphics device, for loading textures.
        /// </summary>
        static private GraphicsDevice _gfx;

        /// <summary>
        /// Initializes the texture manager. If texture manager is already initializes, restores the manager to its initial state by clearing and disposing of textures.
        /// </summary>
        /// <param name="gfx">The graphics device, for loading images.</param>
        static public void Initialize(GraphicsDevice gfx) 
        {
            _gfx = gfx;

            //Disposes of any textures before reinitializing.

            if (Initialized)
            {
                foreach (var texture in _texturedictionary)
                {
                    texture.Value.Dispose();
                }
            }

            _texturedictionary = new Dictionary<String, Texture2D>();
            Initialized = true;
        }

        /// <summary>
        /// Returns a texture. If it doesn't exist, load it. Appends the texture folder to the name to make the ID.
        /// </summary>
        /// <param name="name">The name of the texture.</param>
        /// <returns></returns>
        /// <exception cref="System.Exception">Texture manager not initialized.</exception>
        static public Texture2D GetTexture(String name) 
        {
            //Make the ID.

            name = name.Substring(name.LastIndexOf('/') + 1);
            String id = "TempSprites/" + name;

            //Check for proper initialization.

            if (Initialized)
            {
                //Check if the texture manager has the texture already.

                if (_texturedictionary.ContainsKey(name))
                {
                    //If it does, return it.

                    return _texturedictionary[name];
                }
                else
                {
                    //If it doesn't, load it.

                    Texture2D newTexture = null;
                    Stream stream = File.OpenRead(name);

                    newTexture = Texture2D.FromStream(_gfx, stream);
                    
                    //Get color data, for color keying.

                    Color[] colordata = new Color[newTexture.Width * newTexture.Height];
                    newTexture.GetData(colordata);

                    for (int i = 0; i < colordata.Length; i++)
                    {
                        //Check every color for death magenta and set it to transparent.

                        Color color = colordata[i];

                        if (color.R == 255 && color.G == 0 && color.B == 255)
                        {
                            color.R = 0;
                            color.G = 0;
                            color.B = 0;
                            color.A = 0;
                            colordata[i] = color;
                        }
                    }

                    //Set the data and the name.

                    newTexture.SetData(colordata);
                    newTexture.Name = id;
                    
                    //Adds the texture to the dictionary.

                    _texturedictionary.Add(name, newTexture);

                    return _texturedictionary[name];
                }
            }
            else
            {
                throw new Exception("Texture manager not initialized");
            }
        }
    }
}