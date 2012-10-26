using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameBaseXNA
{

    /// <summary>
    /// This class represent a Quadtree structure that designed to partition
    /// space into sub-space (smaller), to find out what's collided or not with
    /// a specified Collidable game component.
    /// This class inherited from CollisionQuadTreeNode class and simply describle
    /// root node only.
    /// </summary>
    using CollisionQuadTree = CollisionQuadTreeNode;

    /// <summary>
    /// The Quadtree node to check collision of Collidable component
    /// </summary>
    public class CollisionQuadTreeNode
    {
        #region Fields
        /// <summary>
        /// The area of this node
        /// </summary>
        public Rectangle Bound { get; set; }

        /// <summary>
        /// The contents of this node
        /// This is the Collidable component are contained in node's area and intersect with one of sub-node bounds
        /// </summary>
        public List<ICollidable> NodeContents { get; set; }

        public List<CollisionQuadTreeNode> SubNodes { get; set; }

        private int layer;
        #endregion

        #region Constructors
        /// <summary>
        /// Create new Collision Quad tree with Empty bound
        /// </summary>
        public CollisionQuadTreeNode()
        {
            this.Bound = Rectangle.Empty;
            this.NodeContents = new List<ICollidable>();
            this.SubNodes = new List<CollisionQuadTreeNode>();
            this.layer = 0;
        }

        /// <summary>
        /// Create new collision quad tree with bound equal specified Rectangle
        /// </summary>
        /// <param name="bound">Bound</param>
        public CollisionQuadTreeNode(Rectangle bound)
        {
            this.Bound = bound;
            this.NodeContents = new List<ICollidable>();
            this.SubNodes = new List<CollisionQuadTreeNode>();
            this.layer = 0;
        }

        /// <summary>
        /// Create new collision quad tree with initial upper-top conner and size of bound
        /// </summary>
        /// <param name="left">Upper conner</param>
        /// <param name="top">Top conner</param>
        /// <param name="width">Width</param>
        /// <param name="height">Height</param>
        public CollisionQuadTreeNode(int left, int top, int width, int height)
        {
            this.Bound = new Rectangle(top, left, width, height);
            this.NodeContents = new List<ICollidable>();
            this.SubNodes = new List<CollisionQuadTreeNode>();
            this.layer = 0;
        }

        /// <summary>
        /// Create new collision quad tree node with initial size (position = (0, 0))
        /// </summary>
        /// <param name="width">Width</param>
        /// <param name="height">Height</param>
        public CollisionQuadTreeNode(int width, int height)
        {
            this.Bound = new Rectangle(0, 0, width, height);
            this.NodeContents = new List<ICollidable>();
            this.SubNodes = new List<CollisionQuadTreeNode>();
            this.layer = 0;
        }

        /// <summary>
        /// Copy constructors
        /// Create new copied quadtree node includes all sub-nodes
        /// </summary>
        /// <param name="node"></param>
        public CollisionQuadTreeNode(CollisionQuadTreeNode node)
        {
            this.Bound = node.Bound;
            this.NodeContents = new List<ICollidable>(node.NodeContents);
            this.SubNodes = new List<CollisionQuadTreeNode>();

            if (node.SubNodes.Count > 0)
            {
                foreach (CollisionQuadTreeNode subnode in node.SubNodes)
                {
                    this.SubNodes.Add(new CollisionQuadTreeNode(node));
                }
            }

            this.layer = node.layer;
        }
        #endregion

        #region Methods and Properties
        /// <summary>
        /// Is the node empty
        /// </summary>
        public bool IsEmpty
        {
            get
            {
                return ((this.Bound.IsEmpty) || (this.SubNodes.Count == 0));
            }
        }

        /// <summary>
        /// Total number of contents in this node and sub-nodes
        /// </summary>
        public int Count
        {
            get
            {
                int count = 0;

                count += this.NodeContents.Count;

                foreach (CollisionQuadTreeNode node in this.SubNodes)
                {
                    count += node.Count;
                }

                return count;
            }
        }

        /// <summary>
        /// Return all contents in this node
        /// include this contents and all of this sub-nodes contents
        /// </summary>
        public List<ICollidable> AllContents
        {
            get
            {
                List<ICollidable> contents = new List<ICollidable>();

                foreach (CollisionQuadTreeNode node in this.SubNodes)
                {
                    contents.AddRange(node.AllContents);
                }

                contents.AddRange(this.NodeContents);

                return contents;
            }
        }

        /// Return all components are collided with specified components
        /// </summary>
        /// <param name="cp">Component to check collide</param>
        /// <returns>
        /// This collision detect with sub-nodes only
        /// </returns>
        public List<ICollidable> SubCollided(ICollidable cp, List<ICollidable> collided)
        {
            //list of component found
            foreach (CollisionQuadTreeNode node in this.SubNodes)
            {
                if (node.IsEmpty)
                    continue;

                //Case 1: if cp completely be contained in sub-node
                //Go down checking
                if (cp.IsContained(node.Bound))
                {
                    collided.AddRange(node.SubCollided(cp, collided));
                    break;
                }
                
                //Case 2: if cp intersect with sub-node
                //Check in this node and continue search with other nodes
                if (cp.Intersects(node.Bound))
                {
                    foreach (ICollidable ccp in node.NodeContents)
                    {
                        if (cp.Collide(ccp) || (ccp.Collide(cp)))
                            collided.Add(ccp);
                    }

                    collided.AddRange(node.SubCollided(cp, collided));
                }
            }

            return collided;
        }

        /// <summary>
        /// Checking Collision
        /// </summary>
        public virtual void CollisionDetect()
        {
            for (int i = 0; i < this.NodeContents.Count; ++i)
            {
                for (int j = i + 1; j < this.NodeContents.Count; ++j)
                {
                    if ((this.NodeContents[i].Collide(this.NodeContents[j])) ||
                        (this.NodeContents[j].Collide(this.NodeContents[i])))
                    {
                        this.NodeContents[i].Collided(this.NodeContents[j]);
                        this.NodeContents[j].Collided(this.NodeContents[i]);
                    }
                }

                List<ICollidable> collided = new List<ICollidable>();
                this.SubCollided(this.NodeContents[i], collided);
                foreach (ICollidable collide in collided)
                {
                    this.NodeContents[i].Collided(collide);
                    collide.Collided(this.NodeContents[i]);
                }
            }

            foreach (CollisionQuadTreeNode node in this.SubNodes)
            {
                node.CollisionDetect();
            }
        }

        /// <summary>
        /// Insert a Collidable component to quad tree node
        /// including sub-nodes
        /// </summary>
        /// <param name="cp">Component to insert</param>
        public bool Insert(ICollidable cp)
        {
            if (!cp.IsContained(this.Bound))
                return false;

            //if sub-node are null: create them
            if ((this.NodeContents.Count > 0) && (this.SubNodes.Count == 0))
            {
                this.CreateSubNodes();
                ICollidable collideable = this.NodeContents[0];
                this.NodeContents.Clear();
                this.Insert(collideable);
            }

            //if component is contained by one of sub-nodes, add
            //component to that sub-node
            foreach (CollisionQuadTreeNode node in this.SubNodes)
            {
                if (cp.IsContained(node.Bound))
                {
                    return node.Insert(cp);
                }
            }

            //otherwise, if no sub-node are completely contain component
            //or there's the smallest node
            //add component to this node
            this.NodeContents.Add(cp);

            return true;
        }

        /// <summary>
        /// Insert a Collidable component to quad tree node (not include sub-nodes)
        /// </summary>
        /// <param name="cp">Component to insert</param>
        public void ImediatelyInsert(ICollidable cp)
        {
            this.NodeContents.Add(cp);
        }

        /// <summary>
        /// Create sub node
        /// </summary>
        private void CreateSubNodes()
        {
            if ((this.SubNodes.Count > 0) && (this.Bound.Width * this.Bound.Height <= 10))
                return;

            int halfWidth = this.Bound.Width / 2;
            int halfHeight = this.Bound.Height / 2;

            this.SubNodes.Add(new CollisionQuadTreeNode(this.Bound.X, this.Bound.Y, halfWidth, halfHeight));
            this.SubNodes.Add(new CollisionQuadTreeNode(this.Bound.X, this.Bound.Y + halfHeight, halfWidth, halfHeight));
            this.SubNodes.Add(new CollisionQuadTreeNode(this.Bound.X + halfWidth, this.Bound.Height, halfWidth, halfHeight));
            this.SubNodes.Add(new CollisionQuadTreeNode(this.Bound.X + halfWidth, this.Bound.Height + halfHeight, halfWidth, halfHeight));
        }

        /// <summary>
        /// Clear Node Contents;
        /// </summary>
        public void Clear()
        {
            this.NodeContents.Clear();
            
            foreach (CollisionQuadTreeNode node in this.SubNodes)
            {
                node.Clear();
            }
        }

        /// <summary>
        /// Optimize Quadtree
        /// </summary>
        public int Optimize()
        {
            int nContents = this.NodeContents.Count;

	        if (this.SubNodes.Count > 0)
	        {
                foreach (CollisionQuadTreeNode node in this.SubNodes)
                {
                    nContents += node.Optimize();
                }

		        if (nContents < 2)
		        {
                    foreach (CollisionQuadTreeNode node in this.SubNodes)
                    {
                        if (node.NodeContents.Count > 0)
                        {
                            this.NodeContents.Add(node.NodeContents[0]);
                            break;
                        }
                    }

                    this.SubNodes.Clear();
		        }
	        }

	        return nContents;
        }
        #endregion
    }
}
