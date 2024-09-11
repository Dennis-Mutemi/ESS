<%@ Page Language="C#" Title="Login" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="PasswordReset.aspx.cs" Inherits="ZamaraESS.PasswordReset" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="box box-success box-solid">
        <div class="box-header with-border">
            <h3 class="box-title">Reset Your Password</h3>
            <div class="box-tools pull-right">
                <button type="button" class="btn btn-box-tool" data-widget="remove"><i class="fa fa-times"></i></button>
            </div>
            <!-- /.box-tools -->
        </div>
        <!-- /.box-header -->
        <div class="box-body">
            <div class="form-horizontal">
                <div class="box-body">
                    <asp:Label ID="LblError" runat="server" CssClass="label label-success"></asp:Label>
                    <div class="form-group">
                        <label for="inputPassword3" class="col-sm-4 control-label">New Password</label>
                        <div class="col-sm-8">
                            <asp:TextBox ID="txtNewPass" class="form-control" placeholder="New Password" type="password" runat="server"></asp:TextBox>
                        </div>
                    </div>
                    <div class="form-group">
                        <label for="inputPassword3" class="col-sm-4 control-label">Confirm Password</label>
                        <div class="col-sm-8">
                            <asp:TextBox ID="txtConfirmpassword" class="form-control" placeholder="Confirm Password" type="password" runat="server"></asp:TextBox>
                        </div>
                    </div>
                </div>
                <!-- /.box-body -->
                <div class="box-footer">
                    <asp:LinkButton ID="LbtnLogin" type="submit" class="btn btn-success w-100" runat="server" OnClick="LbtnLogin_Click"><i class="fa fa-check"></i> Submit</asp:LinkButton>
                </div>
                <!-- /.box-footer -->
            </div>
        </div>
        <!-- /.box-body -->
    </div>
</asp:Content>
