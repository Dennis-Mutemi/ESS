<%@ Page Language="C#" Title="Grievances" AutoEventWireup="true" MasterPageFile="~/pages/Main.Master" CodeBehind="GrievanceCase.aspx.cs" Inherits="ZamaraESS.pages.GrievanceCase" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Main1" runat="Server">
   <!-- Content Wrapper. Contains page content -->
   <div class="content-wrapper" style="margin-top:168px;">
      <section class="content">
         <div class="container-fluid">
            <div class="card bg-gradient-default cardposition">
               <div class="card-header border-1">
                  <h3 class="card-title">
                     <strong>Grievances</strong>
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
                                 <th>Aggriever No</th>
                                 <th>Incident Date</th>
                                 <th>Incident Time</th>
                                 <th>Incident Location</th>
                                 <th>Grievance Code</th>
                                 <th>General Description</th>
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