<%@ Page Language="C#" MasterPageFile="~/pages/Main.Master" Title="Leave Statement" AutoEventWireup="true" CodeBehind="LeaveStatements.aspx.cs" Inherits="ZamaraESS.pages.LeaveStatements" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Main1" runat="Server">
    <!-- Content Wrapper. Contains page content -->
    <div class="content-wrapper pagepostion"> 
    

        <!-- Main content -->
        <section class="content">
        <div class="container-fluid">

             <div class="card bg-gradient-default cardposition">
                <div class="card-header border-1">
                    <h3 class="card-title">
                        <strong>Leave Statement</strong>
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
                    <!-- /.box -->
                </div>
                <!-- /.col -->
        </section>
        <!-- /.content -->
    </div>
    <!-- /.content-wrapper -->
</asp:Content>

