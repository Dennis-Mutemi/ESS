<%@ Page Language="C#" Title="Imprest Surrender" MasterPageFile="~/pages/Main.Master" AutoEventWireup="true" CodeBehind="SurrenderLines.aspx.cs" Inherits="ZamaraESS.pages.SurrenderLines" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Main1" runat="Server">
    <!-- Content Wrapper. Contains page content -->
    <div class="content-wrapper pagepostion" runat="server" id="contents">

            <!-- Main content -->
        <section class="content">
            <div class="container-fluid">

                <div class="card bg-gradient-default cardposition">
                    <div class="card-header border-1">
                        <h3 class="card-title">
                            <strong>Imprest Surrender</strong>
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

                        <asp:MultiView ID="MultiView1" runat="server">
                            <asp:View ID="View1" runat="server">

                                <div class="row">
                                    <div class="col-md-6">
                                        <div class="form-group">
                                            <label>Imprest</label>
                                            <asp:DropDownList ID="DdlImprests" class="form-control select2" runat="server"></asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-md-6">
                                        <div class="form-group">
                                            <label>Description</label>
                                            <div class="icon-after-input">
                                                <asp:TextBox ID="TxtDescription" class="form-control" TextMode="MultiLine" runat="server"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>

                                </div>
                                <hr />
                                <div style="margin-right: 1%">
                                    <asp:Button ID="btnSubmit" class="btn btn-sm btn-success pull-right" runat="server" Text="Next" OnClick="btnSubmit_Click" />
                                </div>

                            </asp:View>
                            <asp:View ID="View2" runat="server">

                                <asp:GridView ID="gvLines" AutoGenerateColumns="false" DataKeyNames="No" class="table table-responsive no-padding table-bordered table-hover small" runat="server"
                                    AllowSorting="True" AllowPaging="true" ShowFooter="false" PageSize="20">
                                    <Columns>
                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="#" SortExpression="">
                                            <HeaderStyle Width="30px" />
                                            <ItemTemplate>
                                                <%# string.Format("{0}",Container.DataItemIndex + 1 +".") %>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="LineNo" HeaderText="Line No." />
                                        <asp:BoundField DataField="ExpenditureType" HeaderText="Expenditure Type" />
                                        <asp:BoundField DataField="AccountNo" HeaderText="Account No." />
                                        <asp:BoundField DataField="AccountName" HeaderText="Account Name" />
                                        <asp:BoundField DataField="DailyRate" HeaderText="Daily Rate" DataFormatString="{0:n}" />
                                        <asp:BoundField DataField="TotalAmount" HeaderText="Total Amount" DataFormatString="{0:n}" />
                                        <asp:BoundField DataField="ActualAmountSpent" HeaderText="Actual Amount Spent" DataFormatString="{0:n}" />
                                        <asp:TemplateField HeaderText="Actual Amount Spent">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtActualAmountSpent" class="form-control" Font-Size="Small" runat="server" Text="" BorderStyle="Ridge" BorderWidth="1px" Width="100%"></asp:TextBox>
                                            </ItemTemplate>
                                            <ItemStyle Font-Bold="True"></ItemStyle>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="RemainingAmount" HeaderText="Remaining Amount" DataFormatString="{0:n}" />
                                    </Columns>
                                    <FooterStyle HorizontalAlign="Center" />
                                    <EmptyDataTemplate>
                                        <span style="color: red">No Records Found</span>
                                    </EmptyDataTemplate>
                                </asp:GridView>

                                <hr />

                                <%--Attachments--%>
                                <div id="DocAttachment" runat="server" visible="true">

                                    <b>Document Attachment</b>
                                    <hr />
                                    <div id="AttachmentForm" runat="server" class="col-md-12">

                                        <div class="row">
                                            <div class="col-md-6">
                                                <div class="form-group">
                                                    <asp:FileUpload ID="filetoupload" runat="server" CssClass="form-control btn btn-outline-success" />

                                                </div>
                                            </div>
                                            <div class="col-md-4">
                                                <div class="form-group">
                                                    <asp:Button ID="btnUpload" CssClass="btn" Style="background-color: #26284f; color: white" runat="server" Text="Upload" OnClick="btnUpload_Click" />

                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <asp:GridView ID="gvAttachmentLines" AutoGenerateColumns="false" DataKeyNames="ID" class="table table-responsive no-padding table-bordered table-hover small" runat="server"
                                        AllowSorting="True" AllowPaging="true" ShowFooter="false" PageSize="20">
                                        <Columns>
                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="#" SortExpression="">
                                                <HeaderStyle Width="30px" />
                                                <ItemTemplate>
                                                    <%# string.Format("{0}",Container.DataItemIndex + 1 +".") %>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="No" HeaderText="Document No." />
                                            <asp:BoundField DataField="FileName" HeaderText="File Name" />
                                            <asp:TemplateField HeaderText="Action" SortExpression="" HeaderStyle-HorizontalAlign="Left">
                                                <ItemStyle Width="110px" HorizontalAlign="Left" />
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="LinkButton1" ForeColor="Red" CssClass="label label-danger" runat="server" ToolTip="Click to Remove line" OnClick="CancelAttachment" CommandArgument='<%# Eval("ID") %>'><i class="fa fa-trash-alt fa-sm"></i> Delete</asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                        <FooterStyle HorizontalAlign="Center" />
                                        <EmptyDataTemplate>
                                            <span style="color: red">No Files Uploaded</span>
                                        </EmptyDataTemplate>
                                    </asp:GridView>

                                </div>

                                <hr />

                                <div class="col-sm-10" id="lblApprovalComment" runat="server" visible="false">
                                    <div class="form-group">
                                        <label for="txtcomment">Approval Comment</label>
                                        <div class="icon-after-input">
                                            <i>
                                                <asp:Label ID="txtcomments" Style="color: red" runat="server" Enabled="false"></asp:Label></i>
                                        </div>
                                    </div>
                                    <hr />
                                </div>

                                <div id="Approvals" runat="server" visible="false" class="col-md-12">
                                    <b>Approval Levels</b>
                                    <div class="table-responsive">
                                        <table id="example1" class="table no-margin table-hover">
                                            <thead>
                                                <tr class="small">
                                                    <th>#</th>
                                                    <th>No.</th>
                                                    <th>Approver ID</th>
                                                    <th>Status</th>
                                                    <th>Date Created</th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                                <%=Jobs()%>
                                            </tbody>
                                        </table>
                                    </div>
                                </div>

                                <div id="Comments" runat="server" visible="false" class="col-md-12">
                                    <b>Approval Comments</b>
                                    <div class="table-responsive">
                                        <table id="example1" class="table no-margin">
                                            <thead>
                                                <tr class="small">
                                                    <th>#</th>
                                                    <th>No.</th>
                                                    <th>Approver ID</th>
                                                    <th>Comment</th>
                                                    <th>Date</th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                                <%=Jobz()%>
                                            </tbody>
                                        </table>
                                    </div>
                                </div>
                                <!-- /.box-body -->
                                <div class="box-footer clearfix">
                                    <asp:LinkButton ID="btnApproval" class="btn btn-sm btn-flat pull-right" Style="background-color: #26284f; color: white" runat="server" OnClick="btnApproval_Click" Visible="false"> <i class="fa fa-check"></i> Send Approval Request</asp:LinkButton>
                                    <asp:LinkButton ID="lbtnCancel" class="btn btn-sm btn-danger btn-flat pull-right" runat="server" OnClick="lbtnCancel_Click" Visible="false"> <i class="fa fa-times"></i> Cancel Approval Request</asp:LinkButton>
                                    <asp:LinkButton ID="lbtnBack" class="btn btn-sm btn-flat pull-left" Style="background-color: #48A23F; color: white" runat="server" OnClick="lbtnBack_Click"> <i class="fa fa-backward"></i> Back</asp:LinkButton>

                                </div>
                            </asp:View>
                        </asp:MultiView>

                    </div>
                    <!-- /.box -->
                </div>
            </div>
        </section>
        <!-- /.content -->
    </div>
    <!-- /.content-wrapper -->
</asp:Content>

