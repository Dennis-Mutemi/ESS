<%@ Page Language="C#" Title="Approval Requests" MasterPageFile="~/pages/Main.Master" AutoEventWireup="true" CodeBehind="ApprovalRequests.aspx.cs" Inherits="ZamaraESS.pages.Approvals_LeaveRecalls" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Main1" runat="Server">
    <!-- Content Wrapper. Contains page content -->
    <div class="content-wrapper">
        <!-- Content Header (Page header) -->
        <div class="content-header">
            <div class="container-fluid">
                <div class="row mb-2">
                    <div class="col-sm-6">
                        <h4 class="m-0">Approval Requests</h4>
                    </div>
                    <!-- /.col -->
                    <div class="col-sm-6">
                        <ol class="breadcrumb float-sm-right">
                            <li class="breadcrumb-item"><a href="Dashboard.aspx">Home</a></li>
                            <li class="breadcrumb-item active">Approval Requests</li>
                        </ol>
                    </div>
                    <!-- /.col -->
                </div>
                <!-- /.row -->
            </div>
            <!-- /.container-fluid -->
        </div>
        <!-- /.content-header -->

        <!-- Main content -->
        <section class="content">
            <div class="container-fluid">
                <div class="row">
                    <div class="col-md-12">
                        <asp:MultiView ID="MultiView1" runat="server">
                            <asp:View ID="View1" runat="server">
                                <div class="card bg-gradient-default">
                                    <div class="card-header border-1">
                                        <h3 class="card-title">
                                            <strong>Pending Approvals</strong>
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

                                        <asp:GridView ID="gvLeaveApprovals" runat="server" AllowPaging="true" AutoGenerateColumns="false"
                                            EmptyDataText="You have no Approvals Requests" ShowHeaderWhenEmpty="true"
                                            OnRowCommand="gvLeaveApprovals_RowCommand" CssClass="table table-striped table-bordered"
                                            BorderWidth="0" ShowFooter="true" PageSize="20">
                                            <Columns>
                                                <asp:TemplateField HeaderText="#">
                                                    <ItemTemplate>
                                                        <%# Container.DataItemIndex + 1 %>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:BoundField DataField="DocumentNo" HeaderText="Document No." ItemStyle-CssClass="text-uppercase"
                                                    ReadOnly="true" />
                                                <%--<asp:BoundField DataField="Sender ID" HeaderText="Sender ID" ItemStyle-CssClass="text-uppercase"
                                                    ReadOnly="true" />--%>
                                                <asp:BoundField DataField="ApproverID" HeaderText="Approver ID" ItemStyle-CssClass="text-uppercase"
                                                    ReadOnly="true" />
                                                <asp:BoundField DataField="DateTimeSentforApproval" HeaderText="Date Sent"
                                                    DataFormatString="{0:dd/MM/yyyy}" ReadOnly="true" />
                                                <asp:TemplateField HeaderText="Action" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                                                    <ItemTemplate>
                                                        <i class='fa fa-list text-primary' title='view details'></i>
                                                        <asp:LinkButton ID="btnDetails" runat="server" CssClass="text-info" CommandName="ViewDetails"
                                                            Style="padding-right: 5px;">View Details</asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                            <EmptyDataRowStyle CssClass="small text-center" ForeColor="#ff3300" />
                                            <HeaderStyle CssClass="small" BorderWidth="0" ForeColor="#525252" />
                                            <RowStyle CssClass="small" BorderWidth="0" ForeColor="#337ab7" />
                                            <FooterStyle CssClass="small" Font-Bold="true" />
                                        </asp:GridView>
                                    </div>
                                </div>
                            </asp:View>
                            <asp:View ID="View2" runat="server">
                                <div class="modal-dialog" role="document">
                                    <div class="modal-content">
                                        <div class="modal-body padding-0">
                                            <asp:Literal ID="gvDocumentDetails" runat="server"></asp:Literal>
                                            <br />
                                            <div class="row">
                                                <div class="col-lg-12 col-md-8">
                                                    <div class="form-group">
                                                        <asp:Label ID="Label1" runat="server"><strong>Approver Comments</strong></asp:Label>
                                                        <asp:TextBox ID="txtApproverComments" runat="server" CssClass="form-control" TextMode="MultiLine"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <br />
                                                <div class="col-lg-12 col-md-6">
                                                    <div runat="server" id="dvSaveBooking" class="col-md-12">
                                                        <a runat="server" id="btnApprove" onserverclick="btnApprovalLeave"><span class="btn btn-outline-success">
                                                            <i class="fa fa-check"></i> Approve </span></a>

                                                        <%--<br />--%>

                                                        <a runat="server" id="btnReject" onserverclick="btnRejectlBooking"><span class="btn btn-outline-danger">
                                                            <i class="fa fa-times"></i> Reject </span></a>

                                                        <%--<br />--%>

                                                        <a runat="server" id="btnCancel" onserverclick="btnCancelBooking"><span
                                                            class="btn btn-outline-info"><i class="fa fa-arrow-left"></i> Back </span></a>
                                                    </div>
                                                </div>
                                            </div>


                                        </div>

                                    </div>
                                </div>
                            </asp:View>
                        </asp:MultiView>
                    </div>
                </div>
            </div>
        </section>
    </div>

    <div class="modal fade msg-modal" tabindex="-1" role="dialog" aria-labelledby="MsgModalLabel">
        <div class="modal-dialog" role="document">
            <div runat="server" id="dvMdlContentAprConfirm" class="modal-content" visible="false">
                <div class="modal-header bg-success padding-10">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span></button>
                    <h5 class="modal-title">Success!</h5>
                </div>
                <div class="modal-body padding-10">
                    <p>
                        The Leave Request has been successfully Approved.
                    </p>
                </div>
            </div>
            <div runat="server" id="dvMdlContentRejConfirm" class="modal-content" visible="false">
                <div class="modal-header bg-danger padding-10">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span></button>
                    <h5 class="modal-title">Success!</h5>
                </div>
                <div class="modal-body padding-10">
                    <p>
                        The Leave Request has been successfully Rejected.
                    </p>
                </div>
            </div>
        </div>
    </div>
</asp:Content>

