<%@ Page Language="C#" Title="P9 Form" MasterPageFile="~/pages/Main.Master" AutoEventWireup="true" CodeBehind="pnineform.aspx.cs" Inherits="ZamaraESS.pages.pnineform" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Main1" runat="Server">
    <!-- Content Wrapper. Contains page content -->
    <div class="content-wrapper">
           <!-- Content Header (Page header) -->
    <div class="content-header">
        <div class="container-fluid">
            <div class="row mb-2">
                <div class="col-sm-6">
                    <h4 class="m-0">Staff P9 Form</h4>
                </div><!-- /.col -->
                <div class="col-sm-6">
                    <ol class="breadcrumb float-sm-right">
                        <li class="breadcrumb-item"><a href="Dashboard.aspx">Home</a></li>
                        <li class="breadcrumb-item active">Staff P9 Form</li>
                    </ol>
                </div><!-- /.col -->
            </div><!-- /.row -->
        </div><!-- /.container-fluid -->
    </div>
    <!-- /.content-header -->

        <!-- Main content -->
        <section class="content">
        <div class="container-fluid">

             <div class="card bg-gradient-default">
                <div class="card-header border-1">
                    <h3 class="card-title">
                        <strong>Staff P9 Form</strong>
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
                     <%--<span style="font-size: 12pt; color: red; font-family: Palatino Linotype">Please, select the period to view the p9 for that period</span><br />--%>
                            <div class="form-horizontal">
                                <table id="table1" style="width: 100%;">
                                    <tr>
                                        <td class="auto-style1">
                                            <asp:Label ID="Label11" runat="server"><strong>Period Year:</strong></asp:Label>
                                        </td>
                                        <td colspan="3" class="auto-style1">
                                            <asp:DropDownList ID="ddlYear" runat="server" Width="233px" CssClass="form-control select2" AutoPostBack="True" OnSelectedIndexChanged="ddlYear_SelectedIndexChanged">
                                            </asp:DropDownList>&nbsp;
                                        </td>
                                    </tr>
                                </table>
                            </div>

                            <span style="font-size: 10pt">
                                <iframe runat="server" id="myPDF" src="" width="100%" height="500" />
                            </span>

                </div>

            </div>
            </div>
        </section>
    </div>
</asp:Content>

