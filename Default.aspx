<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style>
        .index
        {
            cursor:pointer;
        }

        .footer {
            position: fixed;
            bottom: 0;
            left: 0;
            width: 20px;
            height: 20px;
            background: #000;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:Literal ID="errorlbl" runat="server"></asp:Literal>
    <div class="container">
        <div class="row">
             
        <div class="form-group col-md-5">
            <asp:Label ID="Label1" runat="server" Text="טקסט לחיפוש:" AssociatedControlID="TextBox1"></asp:Label>
          <!  <asp:TextBox ID="TextBox1" class="form-control1" runat="server"></asp:TextBox>
            <asp:FileUpload ID="FileUpload1" CssClass="form-control1" runat="server" AllowMultiple="True" BorderColor="Black" BorderStyle="Inset" Font-Size="Large" Font-Underline="False" BorderWidth="1" />
        </div>
        <div class="form-group col-md-4">
            <asp:Label ID="Label2" runat="server" Text="כמה מילים:" AssociatedControlID="TextBox2"></asp:Label>
            <asp:TextBox ID="TextBox2" class="form-control" runat="server" BorderColor="Black" BorderWidth="1"></asp:TextBox>
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
    <script>
        $(document).ready(function () {
            $(document).on('click', '.index', function (event) {
                event.preventDefault();

                var target = "#" + $(this).data('id');
              
                $('html, body').animate({
                    scrollTop: $(target).offset().top
                }, 2000);
            });


            $("a[href='#top']").click(function () {
                $("html, body").animate({ scrollTop: 0 }, "slow");
                return false;
            });
        });
    </script>
    <div class="footer"><a href="#top">top</a></div>
</asp:Content>

