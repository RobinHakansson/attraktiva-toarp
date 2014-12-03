﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EventHandlingSystem.Database;
using Microsoft.Ajax.Utilities;

namespace EventHandlingSystem
{
    public partial class Navigation1 : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                AddNodesToTreeView(TreeViewNavigation, 1); 
            }

            foreach (TreeNode node in TreeViewNavigation.Nodes)
            {
                if(node.NavigateUrl == Request.Url.PathAndQuery)
                {
                    TreeViewNavigation.FindNode(node.ValuePath).Select();
                }
                SelectTreeNodeByNavUrl(node);
            }
        }

        private void SelectTreeNodeByNavUrl(TreeNode node)
        {
            foreach (TreeNode n in node.ChildNodes)
            {
                if (n.NavigateUrl == Request.Url.PathAndQuery)
                {
                    TreeViewNavigation.FindNode(n.ValuePath).Select();
                }
                SelectTreeNodeByNavUrl(n);
            }
        }

        private void AddNodesToTreeView(TreeView treeView, int taxId)
        {
            //Hämtar taxonomin
            Taxonomy tax = TaxonomyDB.GetTaxonomyById(taxId);

            if (tax != null)
            {
                TreeNode startNode = new TreeNode
                {
                    Text = "Start",
                    Value = tax.Id.ToString(),
                    Expanded = true,
                    SelectAction = TreeNodeSelectAction.Select
                };
                //Lägger till HuvudNoden (ex. Publiceringstaxonomi).
                treeView.Nodes.Add(startNode);

                //Hämtar all TermSets som ligger på den översta nivån i taxonomin
                List<TermSet> parentTermSets = TermSetDB.GetAllParentTermSetsByTaxonomy(tax).OrderBy(ts => ts.Name).ToList();

                //Lägger till alla ParentNodes (ex. Äspered).
                foreach (var parentTermSet in parentTermSets)
                {
                    TreeNode node = new TreeNode
                    {
                        Text = parentTermSet.Name,
                        Value = parentTermSet.Id.ToString(),
                        Expanded = false,
                        NavigateUrl = "/SitePage.aspx?id="+ CommunityDB.GetCommunityByPublishingTermSetId(parentTermSet.Id).WebPage.Id,
                        SelectAction = TreeNodeSelectAction.Select
                    };
                    startNode.ChildNodes.Add(node);

                    //För att hitta alla ChildNodes till den aktuella ParentNoden.
                    FindChildNodesAndAddToParentNode(parentTermSet, node);

                }
            }
        }

        private void FindChildNodesAndAddToParentNode(TermSet termSet, TreeNode parentNode)
        {
            //Lägger till alla ChildrenNodes (ex. Vikingen IF).
            foreach (var ts in TermSetDB.GetChildTermSetsByParentTermSetId(termSet.Id).OrderBy(ts => ts.Name).ToList())
            {
                TreeNode childNode = new TreeNode
                {
                    Text = ts.Name,
                    Value = ts.Id.ToString(),
                    Expanded = false,
                    NavigateUrl = "/SitePage.aspx?id=" + AssociationDB.GetAssociationByPublishingTermSetId(ts.Id).WebPage.Id,
                    SelectAction = TreeNodeSelectAction.Select
                };

                parentNode.ChildNodes.Add(childNode);

                //För att hitta alla ChildNodes till den aktuella ParentNoden. 
                //Redundant anropning av metoden görs för att bygga upp hela "grenen".
                FindChildNodesAndAddToParentNode(ts, childNode);
            }


        }

        protected void TreeViewNavigation_OnSelectedNodeChanged(object sender, EventArgs e)
        {

        }

        
    }
}