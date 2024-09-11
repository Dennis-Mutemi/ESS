<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CaseListing.aspx.cs" MasterPageFile="~/pages/Main.Master" Inherits="ZamaraESS.pages.CaseListing" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Main1" runat="Server">
    <!-- Content Wrapper. Contains page content -->
    <div class="content-wrapper" style="margin-top:168px;">
        <!-- Main content -->
        <section class="content">
            <div class="container-fluid">

                <div class="card bg-gradient-default cardposition">
                    <div class="card-header border-1">
                        <h3 class="card-title">
                            <strong>Disciplinary Case</strong>
                        </h3>                     
                    </div>
                    <div class="card-body">
                        <div class="col-md-12">
                            <div class="table-responsive">
                                <table id="example1" class="table no-margin">
                                    <thead>
                                        <tr class="small">
                                            <th>#</th>
                                            <th>No.</th>
                                            <th>Offender Name</th>
                                            <th>Offence Type</th>
                                            <th>Offence Category</th>
                                            <th>Case Description</th>
                                            <th>Case Date</th>
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
                </div>
            </div>
            <!-- /.col -->
        </section>
        <!-- /.content -->
    </div>
    <!-- /.content-wrapper -->
</asp:Content>
