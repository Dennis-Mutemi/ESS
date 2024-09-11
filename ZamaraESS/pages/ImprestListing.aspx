<%@ Page Language="C#" Title="Imprest Requisition" MasterPageFile="~/pages/Main.Master" AutoEventWireup="true" CodeBehind="ImprestListing.aspx.cs" Inherits="ZamaraESS.pages.ImprestListing" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Main1" runat="Server">
    <!-- Content Wrapper. Contains page content -->
    <div class="content-wrapper pagepostion">

       

        <!-- Main content -->
        <section class="content">
            <div class="container-fluid">

                <div class="card bg-gradient-default cardposition">
                    <div class="card-header border-1">
                        <h3 class="card-title">
                            <strong>Imprest Requisition</strong>
                        </h3>
                        <!-- card tools -->
                        <div class="card-tools">
                 
                            <a href="" data-card-widget="collapse" title="Collapse">
                                <i class="fas fa-minus text-danger"></i>
                            </a>
                        </div>
                        <!-- /.card-tools -->
                    </div>
                    <div class="card-body">
                        <div class="col-md-12 mb-2">
                                <a class="btn btn-sm btn-success u-posRelative pull-left" runat="server" id="BtnImprestRequisition" href="ImprestRequisition.aspx?An=&status=Open&Tp=new">New Requisition</a>
                        </div>
                        <div class="col-md-12">

                            <div class="row mb-3">
                             <div class="col-md-6">
                               <div class="form-inline">
                                 <label for="recordsPerPage" class="mr-2">Records per page:</label>
                                 <select id="recordsPerPage" class="form-control mr-2">
                                   <option value="5">5</option>
                                   <option value="10">10</option>
                                   <option value="20">20</option>
                                   <option value="50">50</option>
                                   <option value="100">100</option>
                                 </select>
                               </div>
                             </div>      
                             <div class="col-md-6">
                               <div class="form-group float-right">
                                 <input type="text" id="searchInput" class="form-control" placeholder="Search....">
                               </div>
                             </div>
                           </div>



                            <div class="table-responsive">
                                <table id="example1" class="table no-margin">
                                    <thead>
                                        <tr class="small">
                                            <th>#</th>
                                            <th>No.</th>
                                            <th>Description</th>
                                            <th>Destination Type</th>
                                            <th>Destination</th>
                                            <th>Customer</th>
                                            <th>Total Amount</th>
                                            <th>No. of Days</th>
                                            <th>Start Date</th>
                                            <th>End Date</th>
                                            <th>Surrender Date</th>
                                            <th>Date Created</th>
                                            <th>Status</th>
                                            <th>Action</th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <%=Jobs()%>
                                    </tbody>
                                </table>
                                <div class="pagination-wrapper">
                                  <div id="pagination" class="pagination"></div>
                                </div>
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


