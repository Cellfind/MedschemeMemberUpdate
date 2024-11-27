<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MemberUpdate.aspx.cs" Inherits="MedschemeMemberUpdate.memberpage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=0.8">
 <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/4.7.0/css/font-awesome.min.css">
 <style>
     html, body {
         font-family: Arial, Helvetica, sans-serif;
         height: 100%;
         margin: 0;
         justify-content: center;
     }

     * {
         box-sizing: border-box;
     }

     /* style inputs and link buttons */
     input,
     .btn {
            padding: 9px;
            border: none;
            border-radius: 4px;
            /*margin: 5px 10px;*/
            opacity: 0.85;
            display: inline-block;
            font-size: 17px;
            line-height: 18px;
            width:100%;
            float:left;
            text-decoration: none; /* remove underline from anchors */
     }

     /* style the submit button */
     input[type=submit] {
         background-color: #424242;
         color: white;
         cursor: pointer;
     }

    input[type=submit]:hover {
        background-color: #666;
    }

        .img {
            width: 60%;
            margin: 15px auto 20px auto;
            display: block;
        }

     .dynamic-image {
            display: block;
            margin: 0 auto 20px auto;
            width: 20%;
        }

        .myGridClass {
          width: 100%;
          /*this will be the color of the odd row*/
          background-color: #fff;
          margin: 5px 0 10px 0;
          border: solid 1px #525252;
          border-collapse:collapse;
        }

        /*data elements*/
        .myGridClass td {
          padding: 2px;
          border: solid 1px #c1c1c1;
          color: #717171;
        }

        /*header elements*/
        .myGridClass th {
          padding: 4px 2px;
          color: #fff;
          background: #424242;
          border-left: solid 1px #525252;
          font-size: 0.9em;
        }

        /*his will be the color of even row*/
        .myGridClass .myAltRowClass { background: #c1c1c1 repeat-x top; }

        /*and finally, we style the pager on the bottom*/
        .myGridClass .myPagerClass { background: #424242; }

        .myGridClass .myPagerClass table { margin: 5px 0; }

        .myGridClass .myPagerClass td {
          border-width: 0;
          padding: 0 6px;
          border-left: solid 1px #666;
          font-weight: bold;
          color: #fff;
          line-height: 12px;
        }

        .myGridClass .myPagerClass a { color: #666; text-decoration: none; }

        .myGridClass .myPagerClass a:hover { color: #000; text-decoration: none; } 
 </style>
</head>
<body>
    <div style="width:90%; margin:0 auto 20px auto;">
    <form id="form1" runat="server">
        <div style="text-align: center;width:90%; margin:0 auto 15px auto;">
                <asp:Image ID="Image1" runat="server" CssClass="img"/>
                <h3 style="margin:15px auto 20px auto;text-align: center;">Welcome to our Dependants update form!</h3>
                <p style="margin: 0 auto 20px auto;text-align: center;">Please select a member in the list below for editing.</p>
            </div>
            <asp:GridView ID="GridView1" CssClass="myGridClass  myPagerClass" AlternatingRowStyle-CssClass="myAltRowClass" runat="server" AutoGenerateColumns="False" 
                          DataKeyNames="MemberNumber,IDNumber" 
                          OnRowEditing="GridView1_RowEditing"
                          OnRowUpdating="GridView1_RowUpdating"
                          OnRowCancelingEdit="GridView1_RowCancelingEdit"
                          OnRowDataBound="GridView1_RowDataBound" >
                          
                <Columns>
                    <asp:BoundField DataField="MemberNumber" HeaderText="Member Number" ReadOnly="True" Visible="false"/>
                    <asp:BoundField DataField="Name" HeaderText="Name"/>
                    <asp:BoundField DataField="Surname" HeaderText="Surname" />
                    <asp:BoundField DataField="NickName" HeaderText="NickName" Visible="false" />
                    <asp:BoundField DataField="IDNumber" HeaderText="IDNumber" ReadOnly="false" Visible="false"/>
                    <asp:BoundField DataField="CellNumber" HeaderText="CellNumber" Visible="false" />
                    <asp:BoundField DataField="EmailAddress" HeaderText="EmailAddress" Visible="false" />
                    <asp:BoundField DataField="TaxRefNumber" HeaderText="TaxRefNumber" Visible="false" />
                    <asp:BoundField DataField="Updated" HeaderText="Completed" Visible="True"/>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:Button ID="EditButton" runat="server" CommandName="Edit" Text="Edit" Width="100%" />
                        </ItemTemplate>
                   </asp:TemplateField>
                </Columns>
            </asp:GridView>
      <asp:Label ID="lblMessage" runat="server" ForeColor="Red"></asp:Label>
        <br />
         <div style="text-align: center;margin-bottom:100px;">
                <div>
                    <asp:Button ID="btnlogoutn" runat="server" Text="Log Out" OnClick="btnLogin_Click" />
                </div>
         </div>
        <br />
        <br />
        <br />
        <br />
        <br />
    </form>
  </div>
</body>
</html>
