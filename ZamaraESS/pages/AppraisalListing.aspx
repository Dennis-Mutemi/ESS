<%@ Page Language="C#" Title="Appraisal List" MasterPageFile="~/pages/Main.Master" AutoEventWireup="true" CodeBehind="AppraisalListing.aspx.cs" Inherits="ZamaraESS.pages.AppraisalListing" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Main1" runat="Server">
    <!-- Content Wrapper. Contains page content -->
    <div class="content-wrapper" style="margin-top:168px;">    
       <!-- Main content -->
        <div class="content">
            <div class="container-fluid">
                <div class="row">                    
    <div class="col-lg-12 connectedSortable">
                <div class="card bg-gradient-default" style="margin-top:1em!important;">                    
                    <div class="card-body">
                        <div class="col-md-12 mb-2" id="appraisalapplication" runat="server">
                            <div class="btn-group" role="group" aria-label="Basic example">
                                <div runat="server" id="newppraisal">
                                    <asp:LinkButton runat="server" id="newappraisal" class="btn btn-success u-posRelative pull-left" OnClick="AFAppraisal_Click"> <i class="fas fa-folder-plus"></i> New Appraisal</asp:LinkButton>  
                                </div>                                                             
                                <div class="dropdown">
                                  <button class="btn dropdown-toggle" style="background-color: #26284f;color:white;" type="button" id="dropdownMenuButton" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
                                    <i class="fas fa-book"></i> Reports
                                  </button>
                                  <div class="dropdown-menu bg-success" aria-labelledby="dropdownMenuButton">                                      
                                    <a class="dropdown-item text-dark" href="GoalSettinForm.aspx">Goal Setting Form</a>
                                    <a class="dropdown-item text-dark"  href="InterimReviewForm.aspx">Interim Review Form</a>
                                    <a class="dropdown-item text-dark"  href="GoalSettingReport.aspx">Goal Setting Status</a>  
                                    <a class="dropdown-item text-dark"  href="Midyearcheckinreport.aspx">Mid Year Check In</a>  
                                    <a class="dropdown-item text-dark"  href="AnnualReviewReport.aspx">Annual Review</a>  
                                     <a class="dropdown-item text-dark"  href="AppraisalSummary.aspx">Appraisal Summary</a> 
                                  </div>
                                </div>
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
                                            <th>Employee No</th>
                                            <th>Employee Name</th>
                                            <th>Period</th>                                           
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
                    </div>                    
                </div>
            </div>
            <!-- /.col -->
        </div>
        <!-- /.content -->
    </div>
  </div>
 </div>
    <!-- /.content-wrapper -->
</asp:Content>

