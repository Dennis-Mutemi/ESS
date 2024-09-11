<%@ Page Language="C#" Title="Purchase Requisition" MasterPageFile="~/pages/Main.Master" AutoEventWireup="true" CodeBehind="PurchaseReqListing.aspx.cs" Inherits="ZamaraESS.pages.PurchaseReqListing" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Main1" runat="Server">
    <!-- Content Wrapper. Contains page content -->
    <div id="contentz" class="content-wrapper pagepostion" runat="server">
   
        <!-- Main content -->
        <section class="content">
            <div class="container-fluid">
                <div class="card bg-gradient-default scardposition">
                    <div class="card-header border-1">
                        <h3 class="card-title">
                            <strong>My Purchase Requests</strong>
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
                        <p class="text-left"><a class="btn btn-pill btn-success btn-sm u-posRelative" href="PurchaseReqLines.aspx?An=&status=Open&Tp=new&Ac=new">New Requisition<span class="waves"></span> </a></p>
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
                                 <input type="text" id="searchInput" class="form-control" placeholder="Search ....">
                               </div>
                             </div>
                           </div>
                        <div class="table-responsive">
                            <table id="example1" class="table no-margin">
                                <thead>
                                    <tr>
                                        <th class="small">#</th>
                                        <th class="small">No.</th>
                                        <th class="small">Description</th>
                                        <th class="small">Document Date</th>
                                        <th class="small">Expected Delivery Date</th>
                                        <th class="small">Total Amount</th>
                                        <th class="small">Status</th>
                                        <th class="small">Action</th>
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

