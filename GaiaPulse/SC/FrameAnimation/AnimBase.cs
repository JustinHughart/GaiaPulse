using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GaiaPulse.SC.FrameAnimation
{
    /// <summary>
    /// The basis for the animation classes.
    /// </summary>
    public class AnimBase
    {
        /// <summary>
        /// The name of the animation.
        /// </summary>
        public String Name;

        /// <summary>
        /// The default texture used for adding new frames.
        /// </summary>
        public String DefaultTexture;

        public AnimBase()
        {
            Name = "";
            DefaultTexture = "";
        }

        /// <summary>
        /// Updates the animation
        /// </summary>
        /// <param name="animSpeed">The rate at which to update.</param>
        /// <param name="position">The position at work the animation is..</param>
        /// <param name="globalScale">The global scale of the animation..</param>
        /// <param name="globalRotation">The global rotation of the animation.</param>
        public virtual void Update(float animSpeed, Vector2 position, Vector2 globalScale, float globalRotation)
        {
        }

        /// <summary>
        /// Draws the animation.
        /// </summary>
        /// <param name="spriteBatch">The sprite batch to draw with..</param>
        /// <param name="facingRight">if set to <c>true</c> [facing right].</param>
        public virtual void Draw(SpriteBatch spriteBatch, bool facingRight)
        {
        }

        /// <summary>
        /// Clones this instance.
        /// </summary>
        /// <returns></returns>
        public virtual AnimBase Clone()
        {
            return null;
        }

        /// <summary>
        /// Determines whether [is animation finished].
        /// </summary>
        /// <returns>
        ///   <c>true</c> if [is animation finished]; otherwise, <c>false</c>.
        /// </returns>
        public virtual bool IsAnimationFinished()
        {
            return false;
        }

        /// <summary>
        /// Gets the frame number.
        /// </summary>
        /// <returns></returns>
        public virtual int GetFrameNumber()
        {
            return -1;
        }

        /// <summary>
        /// Gets the bounding boxes.
        /// </summary>
        /// <returns></returns>
        public virtual List<BoundBox> GetBoundingBoxes()
        {
            return null;
        }
    }
}