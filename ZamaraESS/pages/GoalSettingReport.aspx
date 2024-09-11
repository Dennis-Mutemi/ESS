<%@ Page Language="C#" Title="Goal Setting Report" AutoEventWireup="true"  MasterPageFile="~/pages/Main.Master"  CodeBehind="GoalSettingReport.aspx.cs" Inherits="ZamaraESS.pages.GoalSettingReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Main1" runat="Server">
    <!-- Content Wrapper. Contains page content -->
    <div class="content-wrapper pagepostion">

        <!-- Main content -->
        <section class="content">
            <div class="container-fluid">
                <div class="card bg-gradient-default cardposition">
                    <div class="card-header border-1">
                        <h3 class="card-title">
                            <strong>Goal Setting Report</strong>
                        </h3>                       
                    </div>
                    <div class="card-body">
                        <div class="row">
                            <div class="col-md-4">
                                <label>Appraisal Period:</label>
                                <div class="form-group">
                                    <asp:DropDownList ID="period" class="form-control" runat="server" OnSelectedIndexChanged="period_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                </div>
                            </div>                            
                        </div>
                        <span style="font-size: 10pt">
                            <iframe runat="server" id="myPDF" src="" width="100%" height="500" />
                        </span>

                    </div>

                </div>
            </div>
        </section>
        <!-- /.content -->
    </div>
    <!-- /.content-wrapper -->
</asp:Content>

