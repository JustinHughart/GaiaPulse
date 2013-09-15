using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml.Linq;
using GaiaPulse.SC.FrameAnimation;
using Microsoft.Xna.Framework;

namespace GaiaPulse.AnimationManager
{
    public partial class CycleNodeProperties : Form
    {
        private AnimNode _node;
        
        public CycleNodeProperties(AnimNode node, String id)
        {
            InitializeComponent();

            Text = "Cycle Node Properties: " + id;
            
            _node = node;

            LoadData();
        }

        private void LoadData()
        {
            txtVelocityX.Text = _node.Velocity.X.ToString();
            txtVelocityY.Text = _node.Velocity.Y.ToString();
            chkSmoothX.Checked = _node.SmoothX;
            chkSmoothY.Checked = _node.SmoothY;

            txtRotation.Text = _node.Rotation.ToString();
            chkSmoothRotation.Checked = _node.SmoothRotation;

            txtTimeTillNext.Text = _node.TimeTillNext.ToString();

            LoadXML();
        }

        private void BtnSaveClick(object sender, EventArgs e)
        {
            _node.SetVelocity(new Vector2(float.Parse(txtVelocityX.Text), float.Parse(txtVelocityY.Text)), chkSmoothX.Checked, chkSmoothY.Checked);
            _node.SetRotation(float.Parse(txtRotation.Text), chkSmoothRotation.Checked);
            _node.SetTTN(int.Parse(txtTimeTillNext.Text));

            SaveXML();

            Close();
        }

        private void LoadXML()
        {
            var root = _node.CustomXML;

            treeXML.Nodes.Clear();
            
            RecurseLoad(root, null);
        }

        private void RecurseLoad(XElement element, TreeNode parent)
        {
            TreeNode node = new TreeNode(element.Name.ToString());
            TreeNode attributenode = new TreeNode("Attributes");
            node.Nodes.Add(attributenode);

            var attributes = element.Attributes();

            foreach (var attribute in attributes)
            {
                TreeNode newnode = new TreeNode(attribute.Name + " = " + attribute.Value);
                attributenode.Nodes.Add(newnode);
            }

            if (parent == null)
            {
                treeXML.Nodes.Add(node);
            }
            else
            {
                parent.Nodes.Add(node);
            }

            var elementsnode = new TreeNode("Elements");
            node.Nodes.Add(elementsnode);

            var children = element.Elements();

            if (children.Count() > 0)
            {
                foreach (var child in children)
                {
                    RecurseLoad(child, elementsnode);
                }
            }
        }

        private void SaveXML()
        {
            
        }
    }
}
