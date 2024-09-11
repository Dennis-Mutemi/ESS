<%@ Page Language="C#" Title="Imprest Surrender" MasterPageFile="~/pages/Main.Master" AutoEventWireup="true" CodeBehind="SurrenderListing.aspx.cs" Inherits="ZamaraESS.pages.SurrenderListing" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Main1" runat="Server">
    <!-- Content Wrapper. Contains page content -->
    <div class="content-wrapper pagepostion">       
       

        <!-- Main content -->
        <section class="content">
            <div class="container-fluid">

                <div class="card bg-gradient-default cardposition">
                    <div class="card-header border-1">
                        <h3 class="card-title">
                            <strong>Imprest Surrender</strong>
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
                        <div class="col-md-12 mb-2">
                                <a class="btn btn-sm btn-success u-posRelative pull-left" runat="server" id="BtnImprestSurrender" href="SurrenderLines.aspx?An=&status=Open&Tp=new">New Surrender</a>
                        </div>
                        <div class="col-md-12">
                            <div class="table-responsive">
                                <table id="example1" class="table no-margin">
                                    <thead>
                                        <tr class="small">
                                            <th>#</th>
                                            <th>No.</th>
                                            <th>Description</th>
                                            <th>Imprest No.</th>
                                            <th>Payment Mode</th>
                                            <th>Total Amount</th>
                                            <th>Actual Amount Spent</th>
                                            <th>Receipt Amount</th>
                                            <th>Date Created</th>
                                            <th>Status</th>
                                            <th>Action</th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <%=Jobs()%>
                                    </tbody>
                                </table>
                            </div>
                        </div>
                        <!-- /.table-responsive -->
                    </div>
                    <!-- /.box-body -->
                </div>
            </div>
            <!-- /.col -->
        </section>
        <!-- /.content -->
    </div>
    <!-- /.content-wrapper -->
</asp:Content>



