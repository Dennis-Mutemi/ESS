<%@ Page Language="C#" Title="Change Password" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Changepassword.aspx.cs" Inherits="ZamaraESS.pages.Changepassword" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <br />
    <div class="box box-success box-solid">
        <div class="box-header with-border">
            <h3 class="box-title"> Change Password</h3>
            <div class="box-tools pull-right">
                <button type="button" class="btn btn-box-tool" data-widget="remove"><i class="fa fa-times"></i></button>
            </div>
            <!-- /.box-tools -->
        </div>
        <!-- /.box-header -->
        <div class="box-body">
            <div class="form-horizontal">
                <div class="box-body">
                    <asp:Label ID="LblError" runat="server" CssClass="label"></asp:Label>
                    <div class="form-group">
                        <label for="inputPassword3" class="col-sm-4 control-label">Current Password</label>

                        <div class="col-sm-8">
                            <asp:TextBox ID="txtOldPass" type="password" class="form-control" placeholder="Current Password" runat="server"></asp:TextBox>
                        </div>
                    </div>
                    <div class="form-group">
                        <label for="inputPassword3" class="col-sm-4 control-label">New Password</label>

                        <div class="col-sm-8">
                            <asp:TextBox ID="TxtNewPass" type="password" class="form-control" placeholder="New Password" runat="server"></asp:TextBox>
   
                        </div>
                    </div>
                    <div class="form-group">
                        <label for="inputPassword3" class="col-sm-4 control-label">Confirm Password</label>

                        <div class="col-sm-8">
                            <asp:TextBox ID="TxtConfirmNewPass" type="password" class="form-control" placeholder="Confirm Password" runat="server"></asp:TextBox>
                        </div>
                    </div>
                </div>
                <!-- /.box-body -->
                <div class="box-footer">
                    <a ID="lbtnForgot" href="Dashboard.aspx" class="btn" style="background-color:#26284f; color:white"><i class="fa fa-backward"></i> Back</a>
                    <asp:LinkButton ID="LbtnLogin" type="submit" class="btn btn-success pull-right" runat="server" OnClick="BtnResetPass_Click"><i class="fa fa-check"></i> Change Password</asp:LinkButton>
                </div>
                <!-- /.box-footer -->
            </div>
        </div>
        <!-- /.box-body -->
    </div>
</asp:Content>

