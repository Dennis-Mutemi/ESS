<%@ Page Language="C#" Title="Procurement Plan" MasterPageFile="~/pages/Main.Master" AutoEventWireup="true" CodeBehind="ProcurementPlan.aspx.cs" Inherits="ZamaraESS.pages.ProcurementPlan" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Main1" runat="Server">
    <!-- Content Wrapper. Contains page content -->
    <div class="content-wrapper">
        <!-- Content Header (Page header) -->
        <div class="content-header">
            <div class="container-fluid">
                <div class="row mb-2">
                    <div class="col-sm-6">
                        <h4 class="m-0">Procurement Plan</h4>
                    </div>
                    <!-- /.col -->
                    <div class="col-sm-6">
                        <ol class="breadcrumb float-sm-right">
                            <li class="breadcrumb-item active">Procurement Plan</li>
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
                                <div class="card bg-gradient-default">
                                    <div class="card-header border-1">
                                        <h3 class="card-title">
                                            <strong>Procurement Plan Lines</strong>
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
                                        <small>
                                            <asp:GridView ID="gvPlanLines" AutoGenerateColumns="false" DataKeyNames="DocumentNo" class="table table-responsive no-padding table-bordered table-hover" runat="server"
                                                AllowSorting="True" AllowPaging="true" ShowFooter="false" PageSize="50">
                                                <Columns>
                                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="#" SortExpression="">
                                                        <HeaderStyle Width="30px" />
                                                        <ItemTemplate>
                                                            <%# string.Format("{0}",Container.DataItemIndex + 1 +".") %>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="DocumentNo" HeaderText="Plan No." />
                                                    <asp:BoundField DataField="PlannedProcurementPeriod" HeaderText="Quarter" />
                                                    <asp:BoundField DataField="LineNo" HeaderText="Line No." />
                                                    <asp:BoundField DataField="LineType" HeaderText="Type"  Visible="false"/>
                                                    <asp:BoundField DataField="ItemNo" HeaderText="Item No." />
                                                    <asp:BoundField DataField="Description" HeaderText="Description" />
                                                    <asp:BoundField DataField="UnitofMeasure" HeaderText="Unit of Measure" />
                                                    <asp:BoundField DataField="ApproxUnitCost" HeaderText="Approx. Unit Cost" DataFormatString="{0:n}" />
                                                    <asp:BoundField DataField="AvailableQuantity" HeaderText="Available Quantity" DataFormatString="{0:n}" />
                                                    <asp:TemplateField HeaderText="Quantity to Request">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtQty" runat="server" Enabled="false" Text="" BorderStyle="Ridge" BorderWidth="1px" Width="80%"></asp:TextBox>
                                                        </ItemTemplate>
                                                        <ItemStyle Font-Bold="True"></ItemStyle>
                                                    </asp:TemplateField>
                                                    <%--<asp:BoundField DataField="Approx_ Unit Cost" HeaderText="Unit Cost" DataFormatString="{0:n}" />
                                                <asp:BoundField DataField="Amount" HeaderText="Amount" DataFormatString="{0:n}" />--%>
                                                    <asp:TemplateField>
                                                        <HeaderTemplate>
                                                            <asp:CheckBox ID="chkHeader" Text="Select All" runat="server" />
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:CheckBox ID="chkRow" runat="server" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                                <FooterStyle HorizontalAlign="Center" />
                                                <EmptyDataTemplate>
                                                    <span style="color: red">No Records Found</span>
                                                </EmptyDataTemplate>
                                            </asp:GridView>
                                        </small>
                                        <hr />
                                        <asp:Button ID="btnLine" class="btn btn-info pull-left btn-sm" runat="server" Visible="false" Text="Apply" OnClick="btnLine_Click" />
                                        <asp:Button ID="btnClose" class="btn btn-danger pull-right btn-sm" runat="server" Text="Close" OnClick="btnClose_Click" />

                                    </div>
                                </div>
                    </div>
                </div>

            </div>

        </section>
    </div>
</asp:Content>





