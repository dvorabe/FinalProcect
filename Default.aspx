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
    
    <div class="container">
        <div class="row">
            <div class="col-md-10">
                <div class="col-md-12">
                    <asp:Label ID="Label1" runat="server" Text="בחר קובץ לטעינה:" AssociatedControlID="FileUpload1"></asp:Label>
                    <asp:FileUpload ID="FileUpload1" CssClass="form-control1" Width="100%" runat="server" AllowMultiple="True" BorderColor="Black" BorderStyle="Inset" Font-Size="Large" Font-Underline="False" BorderWidth="1" />
                </div>
        <div class="form-group col-md-4">
            <asp:Label ID="Label2" runat="server" Text="כמה מילים:" AssociatedControlID="TextBox2"></asp:Label>
            <asp:TextBox ID="TextBox2" class="form-control required NumbersOnly" 
                TextMode="Number" runat="server"></asp:TextBox>
            
        </div>
        <asp:Button ID="Button1" runat="server" Text="חיפוש" class="btn btn-default" OnClick="Button1_Click"/>
            </div>
            <div class="col-md-2">
                <div id="refText"></div>
            </div>
        </div>
        <% if (!string.IsNullOrEmpty(errorlbl.Text)){ %>
        <div class="row">

            <div class="btn label-danger col-md-12" style="width:100%;cursor:text;">
            <asp:Literal ID="errorlbl" runat="server"></asp:Literal>
                </div>
        </div>
        <% } %> 
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
    <script>
        $('input').on('input', function () {
            Validate();
        });

        $('#<%= Button1.ClientID %>').click(function (e) {
            var IsValid = Validate();
            if (!IsValid)
                e.preventDefault();
        });

        function Validate() {
            var val = true;
            $.each($('.required'), function () {
                if ($(this).val() == '') {
                    $(this).parent('.form-group').addClass('has-error');
                    val = false;
                }
                else
                    $(this).parent('.form-group').removeClass('has-error');
            });
            return val;
        }
            </script>

     <script>
         $('a.match').click(function () {
             var ref = $(this).data('reference');
             $('#refText').empty().append(ref);
         });

         function GetPasuk(psk, prk, sfr) {
             /*
             var prk = $(this).data('prk');
             var psk = $(this).data('psk');
             var sfr = $(this).data('sfr');
             */
             $.get("Handler.ashx?prk="+prk+"&sfr="+sfr+"&psk="+psk, function (data) {
                 alert( data );
             });   
         }
     </script>
    <div class="footer"><a href="#top">top</a></div>
</asp:Content>

