using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GaiaPulse.SC.FrameAnimation
{
    public class FrameAnimation : AnimBase
    {
        public List<AnimNode> Nodes { get; private set; } //List of nodes

        private Dictionary<String, DrawData> _drawdata;

        public bool IsLooping { get; private set; } //Whether the animation loops or not.

        public int CurrFrame { get; private set; } //The current frame number.

        public float TimingIndex { get; private set; } //Tracks the time since last sprite change.

        public bool AnimFinished { get; private set; } //Whether or not the anim finished.

        private Vector2 _position;
        private Vector2 _scale;
        private float _rotation;
        
        public FrameAnimation(String name, bool looping) //Constructor
        {
            Nodes = new List<AnimNode>();
            _drawdata = new Dictionary<string, DrawData>();
            IsLooping = looping;
            TimingIndex = 0;
            CurrFrame = 0;
            this.Name = name;
        }

        public void Clear() //Clears the animation.
        {
            Nodes.Clear();
        }

        public void SetID(String id)
        {
            Name = id;
        }
        
        public void AddNode(AnimNode node) //Adds a node.
        {
            Nodes.Add(node);
        }

        public override void Update(float animSpeed, Vector2 position, Vector2 globalScale, float globalRotation)
        {
            this._position = position;
            this._scale = globalScale;

            AnimFinished = false;
            TimingIndex += animSpeed;

            if (Nodes.Count > 0)
            {
                if (TimingIndex > Nodes[CurrFrame].TimeTillNext)
                {
                    TimingIndex = 0;
                    CurrFrame++;

                    if (CurrFrame == Nodes.Count)
                    {
                        if (IsLooping)
                        {
                            CurrFrame = 0;
                            AnimFinished = true;
                        }
                        else
                        {
                            CurrFrame--;
                            AnimFinished = true;
                        }
                    }
                }
            }
        }

        public override void Draw(SpriteBatch spriteBatch, bool facingRight)
        {
            if (Nodes.Count > 0)
            {
            Nodes[CurrFrame].Draw(spriteBatch, _position, facingRight, _scale, _rotation);
                }
        }

        public override AnimBase Clone()
        {
            FrameAnimation retanim = new FrameAnimation(Name, IsLooping);

            foreach (var node in Nodes)
            {
                retanim.AddNode(node);
            }
            
            return retanim;
        }

        public override bool IsAnimationFinished()
        {
            return AnimFinished;
        }

        public override int GetFrameNumber()
        {
            return CurrFrame;
        }

        public override List<BoundBox> GetBoundingBoxes()
        {
            return Nodes[CurrFrame].GetBoxes();
        }

        public void SetFrame(int frame)
        {
            CurrFrame = frame;
        }

        public static Tuple<String, FrameAnimation> LoadFromXML(string filename)
        {
            XDocument doc = XDocument.Load(filename); //Load XML doc.

            //Animation

            string name = "0";
            bool looping = false;

            var nameattrib = doc.Root.Attribute("name");

            if (nameattrib != null)
            {
                name = nameattrib.Value;
            }


            var loopattrib = doc.Root.Attribute("looping");

            if (loopattrib != null)
            {
                    looping = bool.Parse(loopattrib.Value);
            }

            FrameAnimation anim = new FrameAnimation(name, looping);
            
            //Frames

            var frames = doc.Root.Element("frames");

            if (frames != null)
            {
                foreach (var frame in frames.Elements("frame"))
                {
                    string id = "";
                    string texture = "";
                    Rectangle drawarea = Rectangle.Empty;
                    Vector3 offsets = Vector3.Zero;
                    Vector2 origin = Vector2.Zero;
                    
                    var idattrib = frame.Attribute("id");
                    
                    if (idattrib != null)
                    {
                        id = idattrib.Value;
                    }

                    var textureattrib = frame.Attribute("texture");

                    if (textureattrib != null)
                    {
                        texture = textureattrib.Value;
                    }

                    var drawareaattrib = frame.Attribute("drawarea");

                    if (drawareaattrib != null)
                    {
                        String drawareastring = drawareaattrib.Value;

                        String[] split = drawareastring.Split(',');

                        if (split.Length == 4)
                        {
                            bool success = true;

                            int x;
                            int y;
                            int w;
                            int h;

                            if (!int.TryParse(split[0], out x) && success)
                            {
                                success = false;
                            }

                            if (!int.TryParse(split[1], out y) && success)
                            {
                                success = false;
                            }

                            if (!int.TryParse(split[2], out w) && success)
                            {
                                success = false;
                            }

                            if (!int.TryParse(split[3], out h) && success)
                            {
                                success = false;
                            }

                            if (success)
                            {
                                drawarea = new Rectangle(x, y, w, h);
                            }
                        }
                    }

                    var xoffsetrightattrib = frame.Attribute("xoffsetright");

                    if (xoffsetrightattrib != null)
                    {
                        offsets.X = int.Parse(xoffsetrightattrib.Value);
                    }

                    var xoffsetleftattrib = frame.Attribute("xoffsetleft");

                    if (xoffsetleftattrib != null)
                    {
                       offsets.Z = int.Parse(xoffsetleftattrib.Value);
                    }

                    var yoffsetattrib = frame.Attribute("yoffset");

                    if (yoffsetattrib != null)
                    {
                        offsets.Y = int.Parse(yoffsetattrib.Value);
                    }

                    var originattrib = frame.Attribute("origin");

                    if (originattrib != null)
                    {
                        String originstring = originattrib.Value;

                        String[] split = originstring.Split(',');

                        if (split.Length == 2)
                        {
                            bool success = true;

                            float x;
                            float y;

                            if (!float.TryParse(split[0], out x) && success)
                            {
                                success = false;
                            }

                            if (!float.TryParse(split[1], out y) && success)
                            {
                                success = false;
                            }

                            if (success)
                            {
                                origin = new Vector2(x, y);
                            }
                        }
                    }

                    DrawData drawdata = new DrawData(id, texture, drawarea, offsets, origin);

                    //Hitboxes

                    var hitboxes = frame.Element("hitboxes");

                    if (hitboxes != null)
                    {
                        foreach (var hitbox in hitboxes.Elements("hitbox"))
                        {
                            String hbid = "";
                            Rectangle hbrect = Rectangle.Empty;
                            int hbleftoffset = 0;
                            BoundingType hbtype = BoundingType.Body;
                            
                            var hbidattrib = hitbox.Attribute("group");

                            if (hbidattrib != null)
                            {
                                hbid = hbidattrib.Value;
                            }

                            var hbrectattrib = hitbox.Attribute("rectangle");

                            if (hbrectattrib != null)
                            {
                                String hbrectstring = hbrectattrib.Value;

                                String[] split = hbrectstring.Split(',');

                                if (split.Length == 4)
                                {
                                    bool success = true;

                                    int x;
                                    int y;
                                    int w;
                                    int h;

                                    if (!int.TryParse(split[0], out x) && success)
                                    {
                                        success = false;
                                    }

                                    if (!int.TryParse(split[1], out y) && success)
                                    {
                                        success = false;
                                    }

                                    if (!int.TryParse(split[2], out w) && success)
                                    {
                                        success = false;
                                    }

                                    if (!int.TryParse(split[3], out h) && success)
                                    {
                                        success = false;
                                    }

                                    if (success)
                                    {
                                        hbrect = new Rectangle(x, y, w, h);
                                    }
                                }
                            }

                            var hboffsetleftattrib = hitbox.Attribute("leftoffset");

                            if (hboffsetleftattrib != null)
                            {
                                hbleftoffset = int.Parse(hboffsetleftattrib.Value);
                            }

                            var hbtypeattrib = hitbox.Attribute("boundingtype");

                            if (hbtypeattrib != null)
                            {
                                BoundingType.TryParse(hbtypeattrib.Value, out hbtype);
                            }

                            BoundBox box = new BoundBox(hbrect, hbleftoffset, hbtype);
                            box.SetGroup(hbid);
                            drawdata.AddBoundingBox(box);
                        }
                    }

                    anim.AddDrawData(drawdata.ID, drawdata);
                }
            }

            //Cycle

             var cycle = doc.Root.Element("cycle");

             if (cycle != null)
             {
                 foreach (var node in cycle.Elements("node"))
                 {
                     string id = "";
                     int timetillnext = 0;

                     var idattrib = node.Attribute("frame");

                     if (idattrib != null)
                     {
                         id = idattrib.Value;
                     }

                     var ttnattrib = node.Attribute("timetillnext");

                     if (ttnattrib != null)
                     {
                         timetillnext = int.Parse(ttnattrib.Value);
                     }

                     AnimNode animnode = new AnimNode(anim, timetillnext);
                     animnode.SetDrawData(anim.GetDrawData(id));
                     
                     //Velocity

                     Vector2 velocity = Vector2.Zero;
                     bool xsmooth = false;
                     bool ysmooth = false;

                     var velocityelement = node.Element("velocity");

                     if (velocityelement != null)
                     {
                         float x = 0;
                         float y = 0;

                         var xattrib = velocityelement.Attribute("x");

                         if (xattrib != null)
                         {
                             x = float.Parse(xattrib.Value);
                         }

                         var yattrib = velocityelement.Attribute("y");

                         if (yattrib != null)
                         {
                             y = float.Parse(yattrib.Value);
                         }

                         velocity = new Vector2(x,y);

                         var xsmoothattrib = velocityelement.Attribute("xsmooth");

                         if (xsmoothattrib != null)
                         {
                             xsmooth = bool.Parse(xsmoothattrib.Value);
                         }

                         var ysmoothattrib = velocityelement.Attribute("ysmooth");

                         if (ysmoothattrib != null)
                         {
                             ysmooth = bool.Parse(ysmoothattrib.Value);
                         }

                         animnode.SetVelocity(velocity, xsmooth, ysmooth);
                     }

                     //Rotation

                     var rotationelement = node.Element("rotation");

                     if (rotationelement != null)
                     {
                         float radians = 0f;
                         bool smoothrot = false;
                         
                         var radianattrib = velocityelement.Attribute("radians");

                         if (radianattrib != null)
                         {
                             radians = float.Parse(radianattrib.Value);
                         }

                         var smoothrotattrib = velocityelement.Attribute("radians");

                         if (smoothrotattrib != null)
                         {
                             smoothrot = bool.Parse(smoothrotattrib.Value);
                         }

                         animnode.SetRotation(radians, smoothrot);
                     }

                     //Load custom XML.

                     animnode.CustomXML = node.Element("CustomXML");
                     
                     anim.AddNode(animnode);
                 }
             }

            return new Tuple<string, FrameAnimation>(name, anim);
        }

        public AnimNode GetCurrNode()
        {
            return Nodes[CurrFrame];
        }

        public void SetLooping(bool islooping)
        {
            IsLooping = islooping;
        }

        public int GetNumberOfDrawDatas()
        {
            return _drawdata.Count;
        }

        public void AddDrawData(String name, DrawData dd)
        {
            _drawdata.Add(name, dd);
        }

        public void SetDrawDatas(Dictionary<String, DrawData> data)
        {
            _drawdata = data;
        }

        public DrawData GetDrawData(String id)
        {
            return _drawdata[id];
        }

        public List<DrawData> GetDrawDataList()
        {
            List<DrawData> dd = new List<DrawData>();

            foreach (var fbf in _drawdata)
            {
                dd.Add(fbf.Value);
            }

            return dd;
        }

        public List<AnimNode> GetNodes()
        {
            return Nodes;
        }

    }
}