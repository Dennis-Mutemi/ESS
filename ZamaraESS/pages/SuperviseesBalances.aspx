<%@ Page Language="C#" Title="Supervisees Balances" MasterPageFile="~/pages/Main.Master" AutoEventWireup="true" CodeBehind="SuperviseesBalances.aspx.cs" Inherits="ZamaraESS.pages.SuperviseesBalances" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Main1" runat="Server">
    <!-- Content Wrapper. Contains page content -->
    <div class="content-wrapper" id="Contents" runat="server">

        <!-- Content Header (Page header) -->
        <div class="content-header">
            <div class="container-fluid">
                <div class="row mb-2">
                    <div class="col-sm-6">
                        <h4 class="m-0">My Supervisees Balances</h4>
                    </div>
                    <!-- /.col -->
                    <div class="col-sm-6">
                        <ol class="breadcrumb float-sm-right">
                            <li class="breadcrumb-item"><a href="Dashboard.aspx">Home</a></li>
                            <li class="breadcrumb-item active">Supervisees Balances</li>
                        </ol>
                    </div>
                    <!-- /.col -->
                </div>
                <!-- /.row -->
            </div>
            <!-- /.container-fluid -->
        </div>
        <!-- /.content-header -->

        <!-- Main content -->
        <section class="content">
            <div class="container-fluid">

                <div class="card bg-gradient-default">
                    <div class="card-header border-1">
                        <h3 class="card-title">
                            <strong>Supervisees Leave Balances</strong>
                        </h3>
                        <!-- card tools -->
                        <div class="card-tools">
                            <!--
                                  <button type="button" class="btn btn-info btn-sm daterange" title="Date range">
                                    <i class="far fa-calendar-alt"></i>
                                  </button>
                -->
                            <a href="" data-card-widget="collapse" title="Collapse">
                                <i class="fas fa-minus text-danger"></i>
                            </a>
                        </div>
                        <!-- /.card-tools -->
                    </div>
                    <div class="card-body">
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


