<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="index.aspx.cs" Inherits="MedschemeMemberUpdate.index" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/4.7.0/css/font-awesome.min.css">
    <style>
        html, body {
            font-family: Arial, Helvetica, sans-serif;
            height: 100%;
            margin: 0;
            display: flex;
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
            padding: 12px;
            border: none;
            border-radius: 4px;
            margin: 5px 0;
            opacity: 0.85;
            display: inline-block;
            font-size: 17px;
            line-height: 20px;
            text-decoration: none; /* remove underline from anchors */
        }

            input:hover,
            .btn:hover {
                opacity: 1;
            }

        /* add appropriate colors to fb, twitter and google buttons */
        .fb {
            background-color: #3B5998;
            color: white;
        }

        .twitter {
            background-color: #55ACEE;
            color: white;
        }

        .google {
            background-color: #dd4b39;
            color: white;
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

        /* Two-column layout */
        .col {
            float: left;
            width: 50%;
            margin: auto;
            padding: 0 50px;
            margin-top: 6px;
        }

        /* Clear floats after the columns */
        .row:after {
            content: "";
            display: table;
            clear: both;
        }

        /* vertical line */
        .vl {
            position: absolute;
            left: 50%;
            transform: translate(-50%);
            border: 2px solid #ddd;
            height: 175px;
        }

        /* text inside the vertical line */
        .vl-innertext {
            position: absolute;
            top: 50%;
            transform: translate(-50%, -50%);
            background-color: #f1f1f1;
            border: 1px solid #ccc;
            border-radius: 50%;
            padding: 8px 10px;
        }

        /* hide some text on medium and large screens */
        .hide-md-lg {
            display: none;
        }

        /* bottom container */
        .bottom-container {
            text-align: center;
            background-color: #666;
            border-radius: 0px 0px 4px 4px;
        }

        /* Responsive layout - when the screen is less than 650px wide, make the two columns stack on top of each other instead of next to each other */
        @media screen and (max-width: 650px) {
            .col {
                width: 100%;
                margin-top: 0;
            }
            /* hide the vertical line */
            .vl {
                display: none;
            }
            /* show the hidden text on small screens */
            .hide-md-lg {
                width: 50%;
                display: block;
                text-align: center;
            }
        }

        .img {
            width: 60%;
            margin: 0 auto 20px auto;
            display: block;
             }
    </style>
</head>
<body>
    <form id="form1" runat="server">

        <div style="width:90%; margin:0 auto 20px auto;">
            <br />

            <asp:Image ID="imgLogo" runat="server" ImageUrl="~/images/Medscheme-Logo.jpg" CssClass="img" />
            <br />

            <h2 style="margin: 0 auto 20px auto;">Welcome!</h2>
            <p style="margin: 0 auto 20px auto;">Please complete the below fileds to in order to access your member details.</p>

            <asp:Panel ID="pnlLogin" runat="server" CssClass="login-panel">

                <asp:TextBox ID="txtidnumber" runat="server" BorderStyle="Solid" BorderColor="LightBlue" BorderWidth="1px" PlaceHolder="ID Number"></asp:TextBox>
                <br />
                <br />
                <asp:Button ID="btnLogin" runat="server" Text="Login" OnClick="Button1_Click" />
                <asp:Label ID="lblMessage" runat="server" ForeColor="Red"></asp:Label>
            </asp:Panel>
        </div>
    </form>
</body>
</html>
