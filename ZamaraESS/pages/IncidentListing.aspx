<%@ Page Language="C#" Title="Incidents List" MasterPageFile="~/pages/Main.Master" AutoEventWireup="true" CodeBehind="IncidentListing.aspx.cs" Inherits="ZamaraESS.pages.IncidentListing" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Main1" runat="Server">
    <!-- Content Wrapper. Contains page content -->
    <div class="content-wrapper" style="margin-top:168px;">
        <!-- Content Header (Page header) -->
        <div class="content-header">
            <%
                String category = "";

                foreach (string key in Session.Keys)
                {
                    if (key == "HsCategory")
                    {
                        category = Session["HsCategory"].ToString();
                        break;
                    }
                }
            %>           
        </div>
        <!-- /.content-header -->

        <!-- Main content -->
        <div class="content">
            <div class="container-fluid">
                            <div class="row">                    
<div class="col-lg-12 connectedSortable">

                <div class="card bg-gradient-default">
                    <div class="card-header border-1">
                        <h3 class="card-title">
                            <strong><%=category %> Listing</strong>
                        </h3>                       
                    </div>
                    <div class="card-body">
                        <div class="col-md-12 mb-2">
                            <div class="btn-group" role="group" aria-label="Basic example">
                                <%--<a class="btn btn-sm-success u-posRelative pull-left" runat="server" id="BtnLeaveApplication"  href="ReportIncident.aspx?An=&status=Open&Tp=new">Report Incident</a>--%>
                                <a class="btn btn-sm btn-success u-posRelative pull-left" href="ReportIncident.aspx?An=&status=Open&Tp=new">Report <%=category %></a>
                            </div>
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
                                      <input type="text" id="searchInput" class="form-control" placeholder="Search for names..">
                                    </div>
                                  </div>
                                </div>
                            <div class="table-responsive">
                                <table id="example1" class="table no-margin">
                                    <thead>
                                        <tr class="small">
                                            <th>#</th>
                                            <th>No.</th>
                                            <th>Reported By</th>
                                            <th><%=category %> Type</th>                                            
                                            <th><%=category %> Date</th>
                                            <th><%=category %> Time</th>
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
                </div>
            </div></div></div>
            <!-- /.col -->
        </div>
        <!-- /.content -->
    </div>
    <!-- /.content-wrapper -->
</asp:Content>


