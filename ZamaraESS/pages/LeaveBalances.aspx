<%@ Page Language="C#" Title="Leave Balances" MasterPageFile="~/pages/Main.Master" AutoEventWireup="true" CodeBehind="LeaveBalances.aspx.cs" Inherits="ZamaraESS.pages.LeaveBalances" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Main1" runat="Server">
    <!-- Content Wrapper. Contains page content -->
    <div class="content-wrapper pagepostion">       

        <!-- Main content -->
        <section class="content">
            <div class="container-fluid">

                <div class="card bg-gradient-default cardposition">
                    <div class="card-header border-1">
                        <h3 class="card-title">
                            <strong>Leave Balances</strong>
                        </h3>
                        <!-- card tools -->
                        <div class="card-tools">                      
                -
                            <a href="" data-card-widget="collapse" title="Collapse">
                                <i class="fas fa-minus text-danger"></i>
                            </a>
                        </div>
                        <!-- /.card-tools -->
                    </div>
                    <div class="card-body">
                        <div class="row">
                            <div class="col-md-12">
                                <p id="SuperviseesBalances" class="text-center" runat="server" visible="false"><a class="btn btn-sm btn-success u-posRelative pull-right" href="SuperviseesBalances.aspx"><i class="fa fa-users"></i>My Supervisees Balances<span class="waves"></span> </a></p>
                            </div>
                            <div class="col-md-12">
                                <span style="font-size: 10pt">
                                    <iframe runat="server" id="myPDF" src="" width="100%" height="500" />
                                </span>
                            </div>
                        </div>
                </div>

            </div>
    </div>
    </section>
    </div>

</asp:Content>

