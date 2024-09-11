<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/pages/Main.Master" CodeBehind="Exit.aspx.cs" Inherits="ZamaraESS.pages.Exit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Main1" runat="Server">
   <!-- Content Wrapper. Contains page content -->
   <div class="content-wrapper" style="margin-top:168px;">
      <!-- Main content -->
      <section class="content">
         <div class="container-fluid">
            <div class="card bg-gradient-default cardposition">
               <div class="card-header border-1">
                  <h3 class="card-title">
                     <strong>Exit Applications</strong>
                  </h3>
               </div>
               <div class="card-body">
                  <div class="col-md-12 mb-2">
                     <div class="btn-group" role="group" aria-label="Basic example">
                        <asp:Button ID="BtnExit" class="btn btn-sm btn-success pull-right"  href="ExitApplication.aspx?An=&status=New&Tp=New" runat="server"  Text="Apply For Exit" onClick="BtnExit_Click"/>
                     </div>
                  </div>
                  <div class="col-md-12">
                     <div class="table-responsive">
                        <table id="example1" class="table no-margin">
                           <thead>
                              <tr class="small">
                                 <th>#</th>
                                 <th>No.</th>
                                 <th>Exit Reason</th>
                                 <th>Description</th>
                                 <th>Leave Balance</th>
                                 <th>Notice Start Date</th>
                                 <th>Last Date of Service</th>
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