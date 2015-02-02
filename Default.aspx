<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="container">
        <div class="row">
             
        <div class="form-group col-md-5">
            <asp:Label ID="Label1" runat="server" Text="טקסט לחיפוש:" AssociatedControlID="TextBox1"></asp:Label>
            <asp:TextBox ID="TextBox1" class="form-control" runat="server"></asp:TextBox>
            
        </div>
        <div class="form-group col-md-4">
            <asp:Label ID="Label2" runat="server" Text="כמה מילים:" AssociatedControlID="TextBox2"></asp:Label>
            <asp:TextBox ID="TextBox2" class="form-control" runat="server"></asp:TextBox>
        </div>
        <div class="checkbox col-md-3">
            <label><input type="checkbox"> סינון תוים מיוחדים</label>
        </div>
        <asp:Button ID="Button1" runat="server" Text="חיפוש" class="btn btn-default" OnClick="Button1_Click"/>
        </div>
    </div>
    <div class="container">
        <div class="row">
            
            <asp:PlaceHolder ID="PlaceHolderResultsPositions" runat="server"></asp:PlaceHolder>
        </div>
        <asp:PlaceHolder ID="PlaceHolderTextResults" runat="server"></asp:PlaceHolder>
    </div>
</asp:Content>

