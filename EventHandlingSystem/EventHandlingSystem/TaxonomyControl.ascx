﻿<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="TaxonomyControl.ascx.cs" Inherits="EventHandlingSystem.TaxonomyControl" %>
<h1>Create your terms here ^^v</h1>
<br />
<asp:Label ID="LabelDisplay" runat="server" Text=""></asp:Label>
<br />
<br />
<asp:Button ID="BtnDelete" runat="server" Text="Delete" OnClick="BtnDelete_OnClick" />
<br />
<asp:TreeView ID="TreeViewTaxonomy" runat="server" OnSelectedNodeChanged="TreeViewTaxonomy_OnSelectedNodeChanged"></asp:TreeView>