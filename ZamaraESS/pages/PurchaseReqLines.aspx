<%@ Page Language="C#" Title="Purchase Requisition" MasterPageFile="~/pages/Main.Master" AutoEventWireup="true" CodeBehind="PurchaseReqLines.aspx.cs" Inherits="ZamaraESS.pages.PurchaseReqLines" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Main1" runat="Server">
    <!-- Content Wrapper. Contains page content -->
    <div id="contentz" class="content-wrapper pagepostion" runat="server">     
        

        <!-- Main content -->
        <section class="content">
            <div class="container-fluid">
                <div class="row">
                    <div class="col-md-12">
                        <asp:MultiView ID="MultiView1" runat="server">
                            <asp:View ID="View1" runat="server">
                                <div class="card bg-gradient-default cardposition">
                                    <div class="card-header border-1">
                                        <h3 class="card-title">
                                            <strong>Purchase Requisition</strong>
                                        </h3>
                                        <!-- card tools -->
                                        <div class="card-tools">
                                            <!--
                                  <button type="button" class="btn btn-success btn-sm daterange" title="Date range">
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
                                        <div class="row">
                                            <div class="col-md-4">
                                                <div class="form-group">
                                                    <label for="ddlbudgetcode">Budget Code</label>
                                                    <asp:DropDownList ID="ddlbudgetcode" runat="server" CssClass="form-control select2">
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                            <div class="col-md-4">
                                                <div class="form-group">
                                                    <label for="deliverydate">Expected Delivery Date</label>
                                                    <asp:TextBox ID="deliverydate" CssClass="form-control" runat="server"></asp:TextBox>
                                                    <script>
                                                        $j('#Main1_deliverydate').Zebra_DatePicker({
                                                            // remember that the way you write down dates
                                                            // depends on the value of the "format" property!
                                                            direction: [1, false],
                                                            //disabled_dates: ['* * * 0,6'] 
                                                        });</script>
                                                </div>
                                            </div>
                                            <div class="col-md-4">
                                                <div class="form-group">
                                                    <label for="description">Description</label>
                                                    <asp:TextBox ID="description" TextMode="MultiLine" CssClass="form-control" runat="server"></asp:TextBox>
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
                                        </div>
                                        <hr />
                                        <div style="margin-right: 1%">
                                            <asp:Button ID="btnSubmit" class="btn btn-success pull-right" runat="server" Text="Next" OnClick="btnSubmit_Click" />
                                        </div>

                                    </div>
                                </div>
                            </asp:View>
                            <asp:View ID="View2" runat="server">
                                <div class="card bg-gradient-default">
                                    <div class="card-header border-1">
                                        <h3 class="card-title">
                                            <strong>Purchase Requisition Lines</strong>
                                        </h3>
                                        <!-- card tools -->
                                        <div class="card-tools">
                                            <!--
                                  <button type="button" class="btn btn-success btn-sm daterange" title="Date range">
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
                                        <div id="btnImportLines" runat="server" visible="false">
                                            <div class="row">
                                                <div class="col-md-2">
                                                    <div class="form-group">
                                                        <label>Req No.</label>
                                                        <asp:Label ID="txtReqNo" runat="server" Enabled="false" CssClass="form-control"></asp:Label>
                                                    </div>
                                                </div>
                                                <div class="col-md-2">
                                                    <div class="form-group">
                                                        <label>Line Type</label>
                                                        <asp:DropDownList ID="ddlLineType" runat="server" CssClass="form-control select2" AutoPostBack="true" OnSelectedIndexChanged="ddlLineType_SelectedIndexChanged">
                                                            <asp:ListItem Value="">--Please Select--</asp:ListItem>
                                                            <asp:ListItem Value="0">Direct Expense</asp:ListItem>
                                                            <asp:ListItem Value="1">Inventory Item</asp:ListItem>
                                                            <asp:ListItem Value="2">Fixed Asset</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                                <div class="col-md-2">
                                                    <div class="form-group">
                                                        <label>Item</label>
                                                        <asp:DropDownList ID="ddlItems" runat="server" CssClass="form-control select2" AutoPostBack="true" OnSelectedIndexChanged="ddlItems_SelectedIndexChanged"></asp:DropDownList>
                                                    </div>
                                                </div>
                                                <div class="col-md-1">
                                                    <div class="form-group">
                                                        <label>UoM</label>
                                                        <asp:Label ID="txtUoM" runat="server" Enabled="false" CssClass="form-control"></asp:Label>
                                                    </div>
                                                </div>
                                                <div class="col-md-1">
                                                    <div class="form-group">
                                                        <label>Qty</label>
                                                        <asp:TextBox ID="txtQuantity" runat="server" CssClass="form-control"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="col-md-1">
                                                    <div class="form-group">
                                                        <label>Unit Cost</label>
                                                        <asp:TextBox ID="txtcost" runat="server" CssClass="form-control"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="col-md-1">
                                                    <div class="form-group">
                                                        <label>Recoverable</label>
                                                        <asp:CheckBox ID="chkRecoverable" runat="server" Checked="false"></asp:CheckBox>
                                                    </div>
                                                </div>
                                                <div class="col-md-2">
                                                    <div class="form-group">
                                                        <label>Department</label>
                                                        <asp:DropDownList ID="ddlDepartment" runat="server" CssClass="form-control select2"></asp:DropDownList>
                                                    </div>
                                                </div>
                                                <div class="col-md-2">
                                                    <div class="form-group">
                                                        <hr />
                                                        <asp:Button ID="btnAddLine" class="btn btn-success pull-left" runat="server" Text="Add" OnClick="btnAddLine_Click" />
                                                    </div>
                                                </div>
                                                <%--                                                <div class="col-md-10">
                                                    <div class="form-group">
                                                        <hr />
                                                        <asp:Button ID="btnLine" class="btn btn-success pull-left" runat="server" Text="Select Items" OnClick="ProcPlan_Click" />
                                                    </div>
                                                </div>--%>
                                            </div>
                                        </div>
                                        <small>
                                            <asp:GridView ID="gvLines" AutoGenerateColumns="false" DataKeyNames="DocumentNo" class="table table-responsive no-padding table-bordered table-hover" runat="server"
                                                AllowSorting="True" AllowPaging="true" ShowFooter="false" PageSize="50">
                                                <Columns>
                                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="#" SortExpression="">
                                                        <HeaderStyle Width="30px" />
                                                        <ItemTemplate>
                                                            <%# string.Format("{0}",Container.DataItemIndex + 1 +".") %>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="LineNo" HeaderText="Line No_" />
                                                    <asp:BoundField DataField="LineType" HeaderText="Type" />
                                                    <asp:BoundField DataField="No" HeaderText="No." />
                                                    <asp:BoundField DataField="Description" HeaderText="Description" />
                                                    <asp:BoundField DataField="UnitofMeasure" HeaderText="Unit of Measure" />
                                                    <asp:BoundField DataField="Quantity" HeaderText="Quantity" DataFormatString="{0:n}" />
                                                    <asp:BoundField DataField="ApproxUnitCost" HeaderText="Unit Cost" DataFormatString="{0:n}" />
                                                    <asp:BoundField DataField="Recoverable" HeaderText="Recoverable" />
                                                    <asp:BoundField DataField="Amount" HeaderText="Total Cost" DataFormatString="{0:n}" />
                                                    <asp:BoundField DataField="GlobalDimension1Code" HeaderText="Department" />
                                                    <asp:TemplateField HeaderText="Action" SortExpression="" HeaderStyle-HorizontalAlign="Left">
                                                        <ItemStyle Width="110px" HorizontalAlign="Left" />
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lbtnCancll" ForeColor="red" runat="server" ToolTip="Click to Remove line" OnClick="Cancelz" CommandArgument='<%# Eval("LineNo") %>'><i class="fa fa-trash-alt fa-sm"></i> Delete</asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                                <FooterStyle HorizontalAlign="Center" />
                                                <EmptyDataTemplate>
                                                    <span style="color: red">No Records Found</span>
                                                </EmptyDataTemplate>
                                            </asp:GridView>
                                        </small>

                                        <div class="col-sm-10" id="lblApprovalComment" runat="server" visible="false">
                                            <div class="form-group">
                                                <label for="txtcomment" style="color: red">Approval Comment</label>
                                                <div class="icon-after-input">
                                                    <asp:Label ID="txtcomments" class="text-danger" runat="server" Enabled="false"></asp:Label>
                                                </div>
                                            </div>
                                        </div>

                                        <%--Attachments--%>
                                        <div id="DocAttachment" runat="server" visible="true">

                                            <b>Document Attachment</b>
                                            <hr />
                                            <div id="AttachmentForm" runat="server" class="col-md-12">

                                                <div class="row">
                                                    <div class="col-md-4">
                                                        <div class="form-group">
                                                            <asp:FileUpload ID="filetoupload" runat="server" CssClass="form-control btn btn-outline-success" />

                                                        </div>
                                                    </div>
                                                    <div class="col-md-4">
                                                        <div class="form-group">
                                                            <asp:Button ID="Button1" CssClass="btn btn-default" runat="server" Text="Upload" OnClick="btnLine_Click" />

                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <small>
                                                <asp:GridView ID="gvAttachmentLines" AutoGenerateColumns="false" DataKeyNames="ID" class="table table-responsive no-padding table-bordered table-hover" runat="server"
                                                    AllowSorting="True" AllowPaging="true" ShowFooter="false" PageSize="20">
                                                    <Columns>
                                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="#No" SortExpression="">
                                                            <HeaderStyle Width="30px" />
                                                            <ItemTemplate>
                                                                <%# string.Format("{0}",Container.DataItemIndex + 1 +".") %>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:BoundField DataField="No" HeaderText="Document No." />
                                                        <asp:BoundField DataField="FileName" HeaderText="File Name" />
                                                        <asp:BoundField DataField="FileExtension" HeaderText="File Type" />
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
                                            </small>
                                        </div>

                                        <div id="Approvals" runat="server" visible="false" class="col-md-12">
                                            <b>Approval Levels</b>
                                            <div class="table-responsive">
                                                <table id="example1" class="table no-margin">
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

                                        <asp:Button ID="btnBack" CssClass="btn btn-danger pull-right btn-sm" runat="server" Text="Go Back" OnClick="btnBack_Click" />
                                        <asp:Button ID="btnApproval" CssClass="btn btn-success pull-left btn-sm" runat="server" Visible="false" Text="Send Approval Request" OnClick="btnApproval_Click" />
                                        <asp:Button ID="btnCancel" CssClass="btn btn-danger pull-right btn-sm" runat="server" Visible="false" Text="Cancel Approval Request" OnClick="btnCancel_Click" />

                                    </div>
                                </div>
                            </asp:View>
                        </asp:MultiView>
                    </div>
                </div>

            </div>

        </section>
    </div>
</asp:Content>




