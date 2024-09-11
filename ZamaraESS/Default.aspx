<%@ Page Title="Login" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="ZamaraESS._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">  

    <style>
        html, body {
            height: 100%;
        }

        .global-container {
            height: 100%;
            display: flex;
            align-items: center;
            justify-content: center;
            background-color: #f5f5f5;
        }

        form {
            padding-top: 30px;
            font-size: 14px;
            margin-top: -20px;          
        }

        .card-title {
            font-weight: 300;
        }

        .btn {
            font-size: 14px;
            margin-top: 20px;
        }

        .login-form {
            width: 330px;
            margin: 20px;
        }

        .sign-up {
            text-align: center;
            padding: 20px 0 0;
        }

        .alert {
            margin-bottom: -30px;
            font-size: 13px;
            margin-top: 20px;
        }

        .form-control, .custom-select {
            background-color: white !important; 
            border: 1px solid #28a745 !important; 
            color: black !important; 
        }

        .form-control:focus, .custom-select:focus {
            background-color: white !important;
            border: 1px solid #28a745 !important; 
            color: black !important; 
            box-shadow: none; 
        }

        .custom-select {
            height: 2.7em !important; 
        }
    </style>
    <div class="global-container">
        <div class="card login-form shadow-sm p-3 mb-5 bg-white rounded">
            <div class="card-body">
                <div class="text-center mb-4">
                    <img src="images/logo.png" alt="Logo" class="img-fluid" style="max-width: 150px;">
                </div>
                <h4 class="text-center font-weight-bold">Employee Self Service Portal</h4>
                <div class="card-text">            
                    <form runat="server">
                        <asp:Label ID="LblError" runat="server" CssClass="text-white text-center p-b-0 myglow"></asp:Label>
                        <div class="form-group">
                            <label for="txtusername">Username</label>
                            <input type="text" class="form-control form-control-sm" required id="txtusername" runat="server">
                        </div>
                        <div class="form-group">
                            <label for="txtpassword">Password</label>
                            <a href="#" style="float:right; font-size:12px; color:#26284f;" class="font-weight-bold">Forgot password?</a>
                            <input type="password" class="form-control form-control-sm" id="txtpassword" runat="server" required>
                        </div>
                        <div class="form-group">
                            <label for="companySelect">Company</label>
                            <select id="ddlCompany" runat="server" class="form-control form-control-sm custom-select" required>
                                <option selected value="">Choose Company</option>
                                <option value="ZAAC">ZAAC</option>
                                <option value="ZARIB">ZARIB</option>                             
                            </select>
                        </div>
                        <asp:Button ID="LbtnLogin" type="submit" style="background:#26284f !important; padding-top:1em !important; height:3em !important; color:white !important;" class="form-control text-center btn mt-2" runat="server" Text="LOG IN" OnClick="LbtnLogin_Click"/>                
                        <div class="w-100 text-center mt-2 text">
                            <p class="mb-0"><strong>&copy; <script type="text/javascript">document.write(new Date().getFullYear());</script> <a href="https://zamaragroup.com/" target="_blank" style="color:#48A23F !important;">Zamara</a> | All Rights Reserved.</strong></p>              
                        </div>
                    </form>
                </div>
            </div>
        </div>
    </div>   
</asp:Content>
