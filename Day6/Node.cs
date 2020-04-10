using System;
using System.Collections.Generic;
class Node
{
    public string data;
    public string parent; 

    public Node(string data, string parent) //Node parent)//, IEnumerable<Node> children)
    {
        this.data = data;
        this.parent = parent;
        //this.children = children;
    }

}

class TreeNode
{
  public string Data { get; private set; }
  public TreeNode FirstChild { get; private set; }
  public TreeNode NextSibling { get; private set; }
  public TreeNode (string data, TreeNode firstChild, TreeNode nextSibling)
  {
    this.Data = data;
    this.FirstChild = firstChild;
    this.NextSibling = nextSibling;
  }
}