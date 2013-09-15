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

            treeXML.ExpandAll();
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
                TreeNode namenode = new TreeNode(attribute.Name.ToString());
                attributenode.Nodes.Add(namenode);
                TreeNode valuenode = new TreeNode(attribute.Value);
                namenode.Nodes.Add(valuenode);
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
            XElement save = RecurseSave(null, treeXML.Nodes[0]);
            
            _node.CustomXML = save;
        }

        private XElement RecurseSave(XElement parent, TreeNode node)
        {
            XElement newnode = new XElement(node.Text);

            if (parent != null)
            {
                parent.Add(newnode);
            }

            var attribnode = node.Nodes[0];

            foreach (TreeNode attrib in attribnode.Nodes)
            {
                newnode.Add(new XAttribute(attrib.Text,  attrib.Nodes[0].Text));
            }

            var elenode = node.Nodes[1];

            foreach (TreeNode child in elenode.Nodes)
            {
                RecurseSave(newnode, child);
            }

            return newnode;
        }

        private void BtnAddNewClick(object sender, EventArgs e)
        {
            if (treeXML.SelectedNode != null)
            {
                if (treeXML.SelectedNode.Text == "Attributes" || treeXML.SelectedNode.Text == "Elements")
                {
                    String nodename = Microsoft.VisualBasic.Interaction.InputBox("Please enter the name of the new node.", "New Node Creation");

                    if (nodename != "")
                    {
                        foreach (TreeNode node in treeXML.SelectedNode.Nodes)
                        {
                            if (node.Text == nodename)
                            {
                                MessageBox.Show("Node already exists.");

                                return; 
                            }
                        }

                        if (nodename.ToLower() == "attributes" || nodename.ToLower() == "elements")
                        {
                            MessageBox.Show("Name is invalid.");
                        }
                        else
                        {
                            var newnode = new TreeNode(nodename);
                            treeXML.SelectedNode.Nodes.Add(newnode);
                            treeXML.SelectedNode.Expand();

                            if (treeXML.SelectedNode.Text == "Attributes")
                            {
                                var valuenode = new TreeNode("value");
                                newnode.Nodes.Add(valuenode);
                                newnode.Expand();
                            }

                            if (treeXML.SelectedNode.Text == "Elements")
                            {
                                var attribnode = new TreeNode("Attributes");
                                var elenode = new TreeNode("Elements");

                                newnode.Nodes.Add(attribnode);
                                newnode.Nodes.Add(elenode);

                                newnode.Expand();
                            }
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Please select either an 'attributes' or 'elements' node before attempting to add an item.");
                }
            }
            else
            {
                MessageBox.Show("Please select a node first.");
            }
        }

        private void BtnEditClick(object sender, EventArgs e)
        {
            if (treeXML.SelectedNode != null)
            {
                if (treeXML.SelectedNode.Parent == null)
                {
                    MessageBox.Show("Cannot edit this node.");
                    return; 
                }

                if (treeXML.SelectedNode.Text == "Attributes" || treeXML.SelectedNode.Text == "Elements" || treeXML.SelectedNode.Text == "CustomXML")
                {
                    MessageBox.Show("Cannot edit this node.");
                }
                else
                {
                    String nodename = Microsoft.VisualBasic.Interaction.InputBox("Please enter the new name of the new node.", "Node Editting");

                    if (nodename != "")
                    {
                        foreach (TreeNode node in treeXML.SelectedNode.Parent.Nodes)
                        {
                            if (node.Text == nodename)
                            {
                                MessageBox.Show("Cannot make this change.");
                                return;
                            }
                        }

                        treeXML.SelectedNode.Text = nodename;
                    }
                }
            }
            else
            {
                MessageBox.Show("Please select a node first.");
            }
        }

        private void BtnDeleteClick(object sender, EventArgs e)
        {
            if (treeXML.SelectedNode != null)
            {
                if (treeXML.SelectedNode.Parent != null)
                {
                    if (treeXML.SelectedNode.Parent.Text == "Attributes" || treeXML.SelectedNode.Parent.Text == "Elements")
                    {
                        var result = MessageBox.Show("Are you sure you want to delete this node and all subnodes?", "Really delete?", MessageBoxButtons.OKCancel);

                        if (result == DialogResult.OK)
                        {
                            treeXML.SelectedNode.Parent.Nodes.Remove(treeXML.SelectedNode);
                            MessageBox.Show("Node deleted.");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Cannot delete this node.");
                    }
                }
                else
                {
                    MessageBox.Show("Cannot delete this node.");
                }
            }
            else
            {
                MessageBox.Show("Please select a node first.");
            }
        }

        private void BtnCancelClick(object sender, EventArgs e)
        {
            Close();
        }
    }
}
