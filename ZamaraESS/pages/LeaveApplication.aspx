<%@ Page Language="C#" Title="Leave application" MasterPageFile="~/pages/Main.Master" AutoEventWireup="true" CodeBehind="LeaveApplication.aspx.cs" Inherits="ZamaraESS.pages.LeaveApplication" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Main1" runat="Server" >
    <!-- Content Wrapper. Contains page content -->
    <div class="content-wrapper" runat="server" id="contents" style="margin-top:168px;">       

        <!-- Main content -->
        <div class="content">
            <div class="container-fluid">
              <div class="row">
                   <div class="card-header">
                       <h3 class="card-title">
                           <strong>Leave Application</strong>
                       </h3>                      
                   </div>
                 <div class="col-lg-12 connectedSortable">
                  <div class="card-body bg bg-white" style="margin-top:1em!important;">
                      
                        <div class="row">
                            <div class="alert alert-danger alert-dismissible fade show" role="alert" runat="server" visible="false" id="displaymsg">
                              <strong runat="server" id="msg"></strong>
                              <button type="button" class="close" data-dismiss="alert" aria-label="Close">
                                <span aria-hidden="true">&times;</span>
                              </button>
                            </div>
                            <div class="col-md-4">
                                <div class="form-group">
                                    <label>Leave Type</label>
                                    <asp:DropDownList ID="DdLeaveType" CssClass="form-control select2 rounded-0" runat="server" AutoPostBack="true" OnSelectedIndexChanged="DdLeaveType_SelectedIndexChanged" required="required"></asp:DropDownList>
                                    <asp:TextBox ID="txtLeaveType" CssClass="form-control rounded-0" runat="server" Visible="false" Enabled="false"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-4">
                                <div class="form-group">
                                    <label>Reliever</label>
                                    <asp:DropDownList ID="ddlReliever" CssClass="form-control select2 rounded-0" runat="server" required="required"></asp:DropDownList>
                                    <asp:TextBox ID="txtReleiverName" CssClass="form-control rounded-0" runat="server" Visible="false" Enabled="false"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-4">
                                <div class="form-group">
                                    <label>Days applied</label>
                                    <asp:TextBox ID="TxtAppliedDays" CssClass="form-control rounded-0" runat="server" required="required" min="1" type="number"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-4">
                                <div class="form-group">
                                    <label>Start Date</label>
                                    <asp:TextBox ID="txtStartDate" CssClass="form-control rounded-0" runat="server" TextMode="Date" required="required" AutoPostBack="true" OnTextChanged="txtStartDate_TextChanged"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-4">
                                <div class="form-group">
                                    <label>End Date</label>
                                    <asp:TextBox ID="txtEndDate" CssClass="form-control rounded-0" runat="server" Text="" ReadOnly="true"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-4">
                                <div class="form-group">
                                    <label>Balance</label>                                    
                                   <asp:TextBox ID="lblLeaveBal" CssClass="form-control rounded-0" runat="server" Text="" ReadOnly="true"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-6">
                                <div class="form-group">
                                    <label for="atext3">Description</label>
                                    <div class="icon-after-input">
                                        <asp:TextBox ID="TxtPurpose" CssClass="form-control rounded-0" TextMode="MultiLine" runat="server" required="required"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div id="DocAttachment" runat="server" class="col-md-6">                               
                                <div class="form-group">
                                    <label for="atext3">Attach Supporting Document</label>
                                    <asp:FileUpload ID="filetoupload" runat="server" accept=".pdf" CssClass="form-control btn btn-outline-success rounded-0" />
                                </div>                                
                          </div>  
                       </div>                      
                      <div class="box-footer clearfix">
                        <asp:LinkButton ID="lbtnBack" class="btn btn-sm btn-flat pull-left rounded-0"  style="background-color: #26284f; color:white" runat="server" OnClick="lbtnBack_Click"><i class="fas fa-chevron-left"></i>&nbsp Back</asp:LinkButton>
                        <asp:Button ID="lbtnApply" class="btn btn-sm btn-flat pull-right rounded-0" style="background-color:#48A23F;color:white" runat="server" OnClick="lbtnApply_Click" Visible="false" Text="Apply"/>
                        <asp:Button ID="lbtnCancel" class="btn btn-sm btn-danger btn-flat pull-right rounded-0" runat="server" OnClick="lbtnCancel_Click" Visible="false" Text="Cancel Approval Request"/>                        
                    </div>
                </div>
            </div>
        </div>        
    </div></div></div>
</asp:Content>

