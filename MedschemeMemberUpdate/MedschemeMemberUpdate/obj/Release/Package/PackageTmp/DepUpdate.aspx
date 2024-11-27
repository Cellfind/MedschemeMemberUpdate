<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DepUpdate.aspx.cs" Inherits="MedschemeMemberUpdate.DepUpdate" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/4.7.0/css/font-awesome.min.css">
    <style>
        html, body {
            font-family: Arial, Helvetica, sans-serif;
            height: 100%;
            margin: 0;
            display: block;
            justify-content: center;
        }

        * {
            box-sizing: border-box;
        }

        /* style the container */
        .container {
            position: relative;
            border-radius: 5px;
            background-color: #f2f2f2;
            padding: 20px 0 30px 0;
        }


        /* style inputs and link buttons */
        input,
        .btn {
            width: 100%;
            padding: 9px;
            border: none;
            border-radius: 4px;
            margin: 5px 10px;
            opacity: 0.85;
            display: inline-block;
            font-size: 15px;
            float: right;
            line-height: 18px;
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
            border-collapse: collapse;
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
            .myGridClass .myAltRowClass {
                background: #c1c1c1 repeat-x top;
            }

            /*and finally, we style the pager on the bottom*/
            .myGridClass .myPagerClass {
                background: #424242;
            }

                .myGridClass .myPagerClass table {
                    margin: 5px 0;
                }

                .myGridClass .myPagerClass td {
                    border-width: 0;
                    padding: 0 6px;
                    border-left: solid 1px #666;
                    font-weight: bold;
                    color: #fff;
                    line-height: 12px;
                }

                .myGridClass .myPagerClass a {
                    color: #666;
                    text-decoration: none;
                }

                    .myGridClass .myPagerClass a:hover {
                        color: #000;
                        text-decoration: none;
                    }


        .centered-container {
            flex-direction: column; /* Stack elements vertically; use 'row' for horizontal alignment */
            align-items: center; /* Center elements horizontally */
            justify-content: center; /* Center elements vertically */
            height: 100vh; /* Full height of the viewport */
            text-align: center;
            margin: 0px auto 0px 20px;
        }
    </style>
</head>
<body>
    <div style="width: 90%; margin: 0 auto 20px auto;">
        <asp:Image ID="Image1" runat="server" CssClass="img" />
        <h3 style="text-align: center;">Welcome to our Dependants update form!</h3>
        <p style="text-align: center;">Please complete as many of the below fields marked in red before saving.</p>
        <form id="form1" runat="server">
            <div class="centered-container">

                <div style="text-align: center; width: 100%; margin: 0 auto 20px auto;">


                    <asp:TextBox ID="MemberNumbertxt" runat="server" PlaceHolder="Member Number"></asp:TextBox>
                    <br />
                    <asp:TextBox ID="Nametxt" runat="server" PlaceHolder="Name"></asp:TextBox>
                    <br />
                    <asp:TextBox ID="Surnametxt" runat="server" PlaceHolder="Surname"></asp:TextBox>
                    <br />
                    <asp:TextBox ID="NickNametxt" runat="server" PlaceHolder="NickName"></asp:TextBox>
                    <br />
                    <asp:TextBox ID="IDNumbertxt" runat="server" PlaceHolder="ID Number"></asp:TextBox>
                    <br />
                    <asp:TextBox ID="CellNumbertxt" runat="server" PlaceHolder="Cell Number"></asp:TextBox>
                    <br />
                    <asp:TextBox ID="EmailAddresstxt" runat="server" PlaceHolder="Email Address" TextMode="Email"></asp:TextBox>
                    <br />
                    <asp:TextBox ID="TaxRefNumbertxt" runat="server" PlaceHolder="Tax Ref Number"></asp:TextBox>
                    <br />

                    <asp:Label ID="lblMessage" runat="server" ForeColor="Red"></asp:Label>
                    <div style="text-align: center;">
                        <div>
                            <asp:Button ID="btnUpdate" runat="server" Text="Update" OnClick="btnUpdate_Click" />
                            <asp:Button ID="Back" runat="server" Text="Back" OnClick="Back_Click" CssClass="button" />
                        </div>
                    </div>
                </div>
            </div>
        </form>
    </div>
</body>
</html>
