<%@ Page Language="C#" Title="Leave Schedules" MasterPageFile="~/pages/Main.Master" AutoEventWireup="true" CodeBehind="LeaveSchedules.aspx.cs" Inherits="ZamaraESS.pages.LeaveSchedules" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Main1" runat="Server">
    <!-- Content Wrapper. Contains page content -->
    <div class="content-wrapper pagepostion"> 

        <!-- Main content -->
        <section class="content">
        <div class="container-fluid">
             <div class="card bg-gradient-default cardposition">
                <div class="card-header border-1">
                    <h3 class="card-title">
                        <strong>Leave Shedules</strong>
                    </h3>                  
                    <!-- /.card-tools -->
                </div>
                <div class="card-body">
                            <div class="col-md-12">
                                <p><a class="btn btn-sm u-posRelative pull-left" style="background-color:#48A23F; color:white" href="LeaveScheduling.aspx">Schedule Leave</a></p>
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
                                                <th>Period</th>
                                                <th>Leave Type</th>
                                                <th>No. of Days</th>
                                                <th>Date Created</th>
                                                <th>Start</th>
                                                <th>End</th>
                                                <th>Return</th>
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
                        <%--  <div class="box-footer clearfix">
                            <a href="javascript:void(0)" class="btn btn-sm style="background-color:#26284f; color:white" btn-flat pull-left">Place New Order</a>
                            <a href="javascript:void(0)" class="btn btn-sm btn-default btn-flat pull-right">View All Orders</a>
                        </div>--%>
                        <!-- /.box-footer -->
                    </div>
                </div>
        </section>
        <!-- /.content -->
    </div>
    <!-- /.content-wrapper -->
</asp:Content>

