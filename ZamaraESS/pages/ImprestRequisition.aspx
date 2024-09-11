<%@ Page Language="C#" Title="Imprest Requisition" MasterPageFile="~/pages/Main.Master" AutoEventWireup="true" CodeBehind="ImprestRequisition.aspx.cs" Inherits="ZamaraESS.pages.ImprestRequisition" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Main1" runat="Server">
    <!-- Content Wrapper. Contains page content -->
    <div class="content-wrapper pagepostion" runat="server" id="contents">

            <!-- /.content-header -->

        <!-- Main content -->
        <section class="content">
            <div class="container-fluid">

                <div class="card bg-gradient-defaultb cardposition">
                    <div class="card-header border-1">
                        <h3 class="card-title">
                            <strong>Imprest Requisition</strong>
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
                                            <label>Description</label>
                                            <div class="icon-after-input">
                                                <asp:TextBox ID="TxtDescription" class="form-control" TextMode="MultiLine" runat="server"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-6">
                                        <div class="form-group">
                                            <label>Purpose</label>
                                            <div class="icon-after-input">
                                                <asp:TextBox ID="TxtPurpose" class="form-control" TextMode="MultiLine" runat="server"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>

                                </div>
                                <div class="row">
                                    <div class="col-md-4">
                                        <div class="form-group">
                                            <label for="ddlbudgetcode">Category</label>
                                            <asp:DropDownList ID="ddlCategory" runat="server" CssClass="form-control select2" AutoPostBack="true" OnSelectedIndexChanged="Category_SelectedIndexChanged">
                                                <asp:ListItem Value="0">--Please Select--</asp:ListItem>
                                                <asp:ListItem Value="1">Customer</asp:ListItem>
                                                <asp:ListItem Value="2">Prospect</asp:ListItem>
                                                <asp:ListItem Value="3">Internal</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div id="CustomerName" runat="server" visible="false" class="col-md-4">
                                        <div class="form-group">
                                            <label for="ddlbudgetcode">Customer Name</label>
                                            <asp:DropDownList ID="ddlCustomers" runat="server" CssClass="form-control select2" AutoPostBack="true" OnSelectedIndexChanged="ddlCustomers_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div id="InvoicedAmount" runat="server" visible="false" class="col-md-4">
                                        <div class="form-group">
                                            <label for="deliverydate">Invoiced Amount</label>
                                            <asp:TextBox ID="txtInvoicedAmount" CssClass="form-control" runat="server" Enabled="false"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div id="Balance" runat="server" visible="false" class="col-md-4">
                                        <div class="form-group">
                                            <label for="deliverydate">Balance</label>
                                            <asp:TextBox ID="txtBalance" CssClass="form-control" runat="server" Enabled="false"></asp:TextBox>
                                        </div>
                                    </div>

                                    <div id="ProspectName" runat="server" visible="false" class="col-md-4">
                                        <div class="form-group">
                                            <label for="deliverydate">Prospect Name</label>
                                            <asp:TextBox ID="TxtProspectName" CssClass="form-control" runat="server"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div id="EstimatedRevenue" runat="server" visible="false" class="col-md-4">
                                        <div class="form-group">
                                            <label for="deliverydate">Estimated Revenue</label>
                                            <asp:TextBox ID="TxtEstimatedRevenue" CssClass="form-control" runat="server"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-4">
                                        <div class="form-group">
                                            <label>Destination Type</label>
                                            <asp:DropDownList ID="DdldestinationType" runat="server" CssClass="form-control select2" AutoPostBack="true" OnSelectedIndexChanged="DdldestinationType_SelectedIndexChanged">
                                                <asp:ListItem Value="">--Please Select--</asp:ListItem>
                                                <asp:ListItem Value="0">Local</asp:ListItem>
                                                <asp:ListItem Value="1">International</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-md-4">
                                        <div class="form-group">
                                            <label>Destination</label>
                                            <asp:DropDownList ID="DdlDestination" class="form-control select2" runat="server"></asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-md-4">
                                        <div class="form-group">
                                            <label>Preferred Hotel</label>
                                            <asp:DropDownList ID="ddlPreferredHotel" class="form-control select2" runat="server"></asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-md-4">
                                        <div class="form-group">
                                            <label>Alternative Hotel</label>
                                            <asp:DropDownList ID="ddlAlternativeHotel" class="form-control select2" runat="server"></asp:DropDownList>
                                        </div>
                                    </div>

                                    <div class="col-md-4">
                                        <div class="form-group">
                                            <label>No. of Days</label>
                                            <asp:TextBox ID="TxtAppliedDays" class="form-control" runat="server"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-4">
                                        <div class="form-group">
                                            <label>Start Date</label>
                                            <asp:TextBox ID="txtStartDate" class="form-control" BackColor="White" runat="server"></asp:TextBox>
                                            <script>
                                                $j('#Main1_txtStartDate').Zebra_DatePicker({
                                                    // remember that the way you write down dates
                                                    // depends on the value of the "format" property!                                                    
                                                    direction: [1, false]
                                                    //disabled_dates: ['* * * 0,6'] 
                                                });
                                                //$(function () {  //On document.ready
                                                //    var textbox = $("input[id$='txtStartDate']"); //ends with selector (or you can use ASP to get the id)
                                                //    $(textbox).change(function (e) {
                                                //        __doPostBack(this.attr("id"), e); //Call postback manually
                                                //    });
                                                //});
                                            </script>
                                        </div>
                                    </div>
                                </div>
                                <hr />
                                <div style="margin-right: 1%">
                                    <asp:Button ID="btnSubmit" class="btn btn-sm btn-success pull-right" runat="server" Text="Next" OnClick="btnSubmit_Click" />
                                </div>

                            </asp:View>
                            <asp:View ID="View2" runat="server">

                                <div id="newLines" runat="server" visible="false">
                                    <asp:LinkButton ID="lbnClose" ToolTip="Close Lines" class="pull-right text-red" runat="server" OnClick="lbnClose_Click"><i class="fa fa-minus-circle"></i> Close lines</asp:LinkButton>

                                    <div id="btnImportLines" runat="server" visible="false">
                                        <div class="col-md-12">
                                            <div class="row">
                                                <div class="col-md-2">
                                                    <div class="form-group">
                                                        <label>Imprest No.</label>
                                                        <asp:Label ID="txtReqNo" runat="server" Enabled="false" CssClass="form-control"></asp:Label>
                                                    </div>
                                                </div>
                                                <div class="col-md-4">
                                                    <div class="form-group">
                                                        <label>Expenditure Type</label>
                                                        <asp:DropDownList ID="ddExpentitureType" runat="server" CssClass="form-control select2" AutoPostBack="true" OnSelectedIndexChanged="ddExpentitureType_SelectedIndexChanged"></asp:DropDownList>
                                                    </div>
                                                </div>
                                                <div class="col-md-2">
                                                    <div class="form-group">
                                                        <label>Daily Rate</label>
                                                        <asp:TextBox ID="txtdailyRate" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="col-md-1">
                                                    <div class="form-group">
                                                        <label>Recoverable</label>
                                                        <asp:CheckBox ID="chkRecoverable" runat="server" Checked="false"></asp:CheckBox>
                                                    </div>
                                                </div>
                                                <div class="col-md-3">
                                                    <div class="form-group">
                                                        <label>Department</label>
                                                        <asp:DropDownList ID="ddlDepartment" runat="server" CssClass="form-control select2"></asp:DropDownList>
                                                    </div>
                                                </div>
                                                <div class="col-md-2">
                                                    <hr />
                                                    <div class="form-group">
                                                        <asp:Button ID="btnLine" class="btn btn-success pull-left" runat="server" Text="Add" OnClick="btnLine_Click" />
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <br />

                                </div>
                                <asp:LinkButton ID="lbnAddLine" ToolTip="Add New Lines" class="pull-right text-success" runat="server" Visible="false" Text="Add New Line" OnClick="lbnAddLine_Click"><i class="fa fa-plus-circle"></i> Add Line</asp:LinkButton>

                                <asp:GridView ID="gvLines" AutoGenerateColumns="false" DataKeyNames="No" class="table table-responsive no-padding table-bordered table-hover small" runat="server"
                                    AllowSorting="True" AllowPaging="true" ShowFooter="false" PageSize="20">
                                    <Columns>
                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="#" SortExpression="">
                                            <HeaderStyle Width="30px" />
                                            <ItemTemplate>
                                                <%# string.Format("{0}",Container.DataItemIndex + 1 +".") %>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="ExpenditureType" HeaderText="Expenditure Type" />
                                        <asp:BoundField DataField="AccountNo" HeaderText="Account No." />
                                        <asp:BoundField DataField="AccountName" HeaderText="Account Name" />
                                        <asp:BoundField DataField="DailyRate" HeaderText="Daily Rate" DataFormatString="{0:n}" />
                                        <asp:BoundField DataField="TotalAmount" HeaderText="Total Amount" DataFormatString="{0:n}" />
                                        <asp:BoundField DataField="Recoverable" HeaderText="Recoverable" />
                                        <asp:BoundField DataField="GlobalDimension1Code" HeaderText="Department" />
                                        <asp:TemplateField HeaderText="Action" SortExpression="" HeaderStyle-HorizontalAlign="Left">
                                            <ItemStyle Width="110px" HorizontalAlign="Left" />
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lbtnCancll" ForeColor="Red" runat="server" ToolTip="Click to Remove line" OnClick="Cancelz" CommandArgument='<%# Eval("LineNo") %>'><i class="fa fa-trash-alt fa-sm"></i> Delete</asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
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
                                                    <asp:LinkButton ID="lbtnCancll" ForeColor="Red" CssClass="label label-danger" runat="server" ToolTip="Click to Remove line" OnClick="CancelAttachment" CommandArgument='<%# Eval("ID") %>'><i class="fa fa-trash-alt fa-sm"></i> Delete</asp:LinkButton>
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
                                    <asp:LinkButton ID="lbtnBack" class="btn btn-sm btn-flat pull-left" Style="background-color: #48A23F; color: white" runat="server" OnClick="lbtnBack_Click"> <i class="fa fa-backward"></i> Go Back</asp:LinkButton>
                                    <asp:LinkButton ID="btnApproval" class="btn btn-sm btn-flat pull-right" Style="background-color: #26284f; color: white" runat="server" OnClick="btnApproval_Click" Visible="false"> <i class="fa fa-check"></i> Send Approval Request</asp:LinkButton>
                                    <asp:LinkButton ID="lbtnCancel" class="btn btn-sm btn-danger btn-flat pull-right" runat="server" OnClick="lbtnCancel_Click" Visible="false"> <i class="fa fa-times"></i> Cancel Approval Request</asp:LinkButton>

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

