<%@ Page Language="C#" Title="Report Incident" MasterPageFile="~/pages/Main.Master" AutoEventWireup="true" CodeBehind="ReportIncident.aspx.cs" Inherits="ZamaraESS.pages.ReportIncident" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Main1" runat="Server">
    <!-- Content Wrapper. Contains page content -->
    <div class="content-wrapper" runat="server" id="contents" style="margin-top:168px;">
        <%
            String category = "Melko";
            int size = 0;
            foreach (string key in Session.Keys)
            {
                if (key == "HsCategory")
                {
                    category = Session["HsCategory"].ToString();
                    break;
                }
            }            
        %>         

        <!-- Main content -->
        <section class="content">
            <div class="container-fluid">
                                            <div class="row">                    
<div class="col-lg-12 connectedSortable">
                <div class="card bg-gradient-default" style="margin-top:1em!important;">
                    <div class="card-header border-1">
                        <h3 class="card-title">
                            <strong>Report <%=category %></strong>
                        </h3>                        
                    </div>
                    <div class="card-body">
                        <asp:MultiView ID="MultiView1" runat="server">
                            <asp:View ID="View1" runat="server">
                                <div class="row">
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label><%=Session["HsCategory"].ToString() %> Type</label>
                                            <asp:DropDownList ID="IncidentType" CssClass="form-control select2" runat="server"></asp:DropDownList>
                                        </div>
                                    </div>
                                     <%      
                                          int size = 0;
      
                                          if (Session["HsCategory"].ToString() == "Disease")
                                          {
                                              size = 2;
                                          }
                                          else
                                          {
                                              size = 4;
                                           }
                                      %>

                                        <%if (Session["HsCategory"].ToString() == "Disease")
                                            {
                                        %>
                                         <div class="col-md-<%=size%>">
                                            <label>Other <%=Session["HsCategory"].ToString() %> Type</label>
                                            <asp:TextBox ID="occdtype" CssClass="form-control" runat="server"></asp:TextBox>
                                         </div>  
                                        <%} %>
                              
                                    <div class="col-md-<%=size%>">
                                        <div class="form-group">
                                            <label><%=Session["HsCategory"].ToString() %> Date</label>
                                            <asp:TextBox ID="IncidentDate" CssClass="form-control" BackColor="White" runat="server"></asp:TextBox>
                                            <script>
                                                $j('#Main1_IncidentDate').Zebra_DatePicker({

                                                });
                                            </script>
                                        </div>
                                    </div>
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <label><%=Session["HsCategory"].ToString() %> Time</label>
                                            <input type="time" id="IncidentTime" runat="server" class="form-control" />
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <div class="form-group">
                                                <%if (Session["HsCategory"].ToString() == "Incident")
                                                    { %>
                                                <label><%=Session["HsCategory"].ToString() %> Location</label>
                                                <asp:TextBox ID="IncidentLocation" CssClass="form-control" runat="server"></asp:TextBox>
                                                <% }
                                                    else
                                                    { %>
                                                <div class="icon-after-input mt-4" style="display: flex;">
                                                    <label class="form-check-label" for="location">Within the office?</label>
                                                    <div class="form-check form-switch ml-4">
                                                        <input class="form-check-input" value='1' type="checkbox" runat="server" id="location">
                                                    </div>
                                                </div>
                                                <%} %>
                                            </div>
                                        </div>
                                    </div>
                                </div>


                                <div class="row">
                                    <div class="col-md-5">
                                        <div class="form-group">
                                            <label><%=Session["HsCategory"].ToString() %> Description</label>
                                            <div class="icon-after-input">
                                                <asp:TextBox ID="IncidentDesc" CssClass="form-control" TextMode="MultiLine" runat="server"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-5">
                                        <div class="form-group" runat="server" id="OSH" visible="false">
                                            <label>OSH Remark</label>
                                            <div class="icon-after-input">
                                                <asp:TextBox ID="OSHRemark" CssClass="form-control" TextMode="MultiLine" runat="server"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-2">
                                        <div class="form-group" runat="server">
                                            <div class="form-group" runat="server" id="Amount" visible="false">
                                                <label>Alocated Amount</label>
                                                <div class="icon-after-input">
                                                    <asp:TextBox ID="AlocatedAmount" CssClass="form-control" runat="server">Allocated Amount</asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>

                                <div class="row">
                                    <div class="col-md-4">
                                        <div class="form-group" runat="server" visible="false" id="HrRem">
                                            <label id="Hr">HR Remark</label>
                                            <div class="icon-after-input">
                                                <asp:TextBox ID="HrRemark" CssClass="form-control" TextMode="MultiLine" runat="server"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                </div>


                                <hr />
                                <div style="margin-right: 1%">
                                    <asp:LinkButton ID="listback" runat="server" OnClick="listback_Click" class="btn btn-sm btn-flat pull-left" Style="background-color: #48A23F; color: white;"><i class="fas fa-chevron-left"></i>&nbsp Back</asp:LinkButton>
                                    <asp:LinkButton ID="create_next" runat="server" OnClick="create_next_Click" class="btn btn-success btn-sm btn-flat pull-right" Style="color: white;">Next &nbsp <i class="fas fa-chevron-right"></i></asp:LinkButton>
                                    <asp:LinkButton ID="update" runat="server" Visible="false" OnClick="update_Click" class="btn btn-success btn-sm btn-flat pull-right" Style="color: white;">Next &nbsp <i class="fas fa-chevron-right"></i></asp:LinkButton>
                                </div>
                            </asp:View>






                            <asp:View ID="View2" runat="server">
                                <div class="shadow-lg p-3 mb-1 bg-white rounded">
                                    <div id="involvedparties" runat="server" visible="false">
                                        <div id="PartiesHeader" runat="server">
                                            <h5 class="font-weight-bold">Involved Parties</h5>
                                            <hr />
                                        </div>
                                        <asp:LinkButton ID="lbnCloseparties" ToolTip="Close Lines" class="pull-right text-red" runat="server" OnClick="lbnCloseparties_Click"><i class="fa fa-minus-circle"></i> Close lines</asp:LinkButton>

                                        <div class="col-md-12">
                                            <div class="row" runat="server" id="involvedpartycontrols">
                                                <div class="col-md-2">
                                                    <div class="form-group">
                                                        <label>Incident No.</label>
                                                        <asp:Label ID="txtReqNo" runat="server" Enabled="false" CssClass="form-control"></asp:Label>
                                                    </div>
                                                </div>
                                                <div class="col-md-4">
                                                    <div class="form-group">
                                                        <label>Employee Name</label>
                                                        <asp:DropDownList ID="EmployeeNo" runat="server" CssClass="form-control select2"></asp:DropDownList>
                                                    </div>
                                                </div>
                                                <div class="col-md-2">
                                                    <hr />
                                                    <div class="form-group">
                                                        <asp:Button ID="involvedpartiesLines" class="btn btn-success pull-left" runat="server" Text="Add" OnClick="involvedpartiesLines_Click" />
                                                    </div>
                                                </div>
                                            </div>
                                        </div>

                                    </div>
                                    <asp:LinkButton ID="lbnAddLine" ToolTip="Add New Involved party" class="pull-right text-success" runat="server" Visible="false" Text="Add New Involved party" Style="color: #48A23F!important;" OnClick="lbnAddLine_Click"><i class="fa fa-plus-circle"></i>Add New Involved party</asp:LinkButton>

                                    <asp:GridView ID="involvedpartdata" AutoGenerateColumns="false" DataKeyNames="Document_No" class="table table-responsive no-padding table-bordered table-hover small" runat="server"
                                        AllowSorting="True" AllowPaging="true" ShowFooter="false" PageSize="20">
                                        <Columns>
                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="#" SortExpression="">
                                                <HeaderStyle Width="30px" />
                                                <ItemTemplate>
                                                    <%# string.Format("{0}",Container.DataItemIndex + 1 +".") %>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="Employee_No" HeaderText="Employee No." />
                                            <asp:BoundField DataField="Employee_Name" HeaderText="Employee Name" />
                                            <asp:TemplateField HeaderText="Action" SortExpression="" HeaderStyle-HorizontalAlign="Left">
                                                <ItemStyle Width="110px" HorizontalAlign="Left" />
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="removeinvolvedparty" ForeColor="Red" runat="server" ToolTip="Click to remove this involved party" OnClientClick="return confirmDelete();" OnClick="removeinvolvedparty_Click" CommandArgument='<%# Eval("Employee_No") %>'><i class="fa fa-trash-alt fa-sm"></i> Delete</asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                        <FooterStyle HorizontalAlign="Center" />
                                        <EmptyDataTemplate>
                                            <span style="color: red">No Records Found</span>
                                        </EmptyDataTemplate>
                                    </asp:GridView>
                                </div>
                                <hr />


                                <div class="shadow-lg p-3 mb-1 bg-white rounded">
                                    <div id="witness" runat="server" visible="false">
                                        <div id="Div3" runat="server">
                                            <h5 class="font-weight-bold">Witneses</h5>
                                            <hr />
                                        </div>

                                        <asp:LinkButton ID="witclose" ToolTip="Close Lines" class="pull-right text-red" runat="server" OnClick="witclose_Click"><i class="fa fa-minus-circle"></i> Close lines</asp:LinkButton>

                                        <div class="col-md-12">
                                            <div class="row" runat="server" id="witnesscontrols">
                                                <div class="col-md-2">
                                                    <div class="form-group">
                                                        <label>Incident No.</label>
                                                        <asp:Label ID="uniqNo" runat="server" Enabled="false" CssClass="form-control"></asp:Label>
                                                    </div>
                                                </div>
                                                <div class="col-md-4">
                                                    <div class="form-group">
                                                        <label>Employee Name</label>
                                                        <asp:DropDownList ID="witnessNo" runat="server" CssClass="form-control select2"></asp:DropDownList>
                                                    </div>
                                                </div>
                                                <div class="col-md-2">
                                                    <hr />
                                                    <div class="form-group">
                                                        <asp:Button ID="witnessLines" class="btn btn-success pull-left" runat="server" Text="Add" OnClick="witnessLines_Click" />
                                                    </div>
                                                </div>
                                            </div>
                                        </div>

                                    </div>
                                    <asp:LinkButton ID="newwitness" ToolTip="Add New witness" class="pull-right text-success" runat="server" Visible="false" Text="Add New witness" Style="color: #48A23F!important;"  OnClick="newwitness_Click"><i class="fa fa-plus-circle"></i> Add New witness</asp:LinkButton>

                                    <asp:GridView ID="witnessdata" AutoGenerateColumns="false" DataKeyNames="DocumentNo" class="table table-responsive no-padding table-bordered table-hover small" runat="server"
                                        AllowSorting="True" AllowPaging="true" ShowFooter="false" PageSize="20">
                                        <Columns>
                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="#" SortExpression="">
                                                <HeaderStyle Width="30px" />
                                                <ItemTemplate>
                                                    <%# string.Format("{0}",Container.DataItemIndex + 1 +".") %>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="EmployeeNo" HeaderText="Employee No." />
                                            <asp:BoundField DataField="EmployeeName" HeaderText="Employee Name" />
                                            <asp:TemplateField HeaderText="Action" SortExpression="" HeaderStyle-HorizontalAlign="Left">
                                                <ItemStyle Width="110px" HorizontalAlign="Left" />
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="removewitness" ForeColor="Red" runat="server" ToolTip="Click to remove this witness" OnClientClick="return confirmDelete();" OnClick="removewitness_Click" CommandArgument='<%# Eval("EmployeeNo") %>'><i class="fa fa-trash-alt fa-sm"></i> Delete</asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                        <FooterStyle HorizontalAlign="Center" />
                                        <EmptyDataTemplate>
                                            <span style="color: red">No Records Found</span>
                                        </EmptyDataTemplate>
                                    </asp:GridView>
                                </div>
                                <hr />

                                <div class="shadow-lg p-3 mb-1 bg-white rounded">
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
                                                        <asp:LinkButton ID="lbtnCancll" ForeColor="Red" CssClass="label label-danger" runat="server" ToolTip="Click to Remove line" OnClientClick="return confirmDelete();" OnClick="RemoveAttachment" CommandArgument='<%# Eval("ID") %>'><i class="fa fa-trash-alt fa-sm"></i> Delete</asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Action" SortExpression="" HeaderStyle-HorizontalAlign="Left">
                                                    <ItemStyle Width="110px" HorizontalAlign="Left" />
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="viewdocument" ForeColor="#48a23f" CssClass="label label-success" runat="server" ToolTip="View this document" OnClick="viewdocument_Click" CommandArgument='<%# Eval("ID") %>'><i class="fas fa-eye"></i>&nbsp View</asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                            <FooterStyle HorizontalAlign="Center" />
                                            <EmptyDataTemplate>
                                                <span style="color: red">No Files Uploaded</span>
                                            </EmptyDataTemplate>
                                        </asp:GridView>

                                    </div>
                                    <div>
                                        <span style="font-size: 10pt" runat="server" visible="false" id="docpdf">
                                            <span class="float-sm-right bg-danger p-2" style="cursor: pointer;" onclick="removedoc()"><i class="fas fa-times"></i></span>
                                            <iframe runat="server" id="myPDF" src="" width="100%" height="500" />
                                        </span>
                                    </div>
                                </div>
                                <hr />

                                <!-- /.box-body -->
                                <div class="box-footer clearfix">
                                    <asp:LinkButton ID="back" runat="server" class="btn btn-sm btn-flat pull-left" Style="background-color: #48A23F; color: white;" OnClick="back_Click"><i class="fas fa-chevron-left"></i>&nbsp Back</asp:LinkButton>
                                    <asp:LinkButton ID="Nsubumit" Visible="false" CssClass="btn btn-sm btn-flat btn-success pull-left" runat="server" OnClick="Nsubumit_Click1"><i class="fas fa-paper-plane"></i>&nbsp Submit</asp:LinkButton>
                                </div>
                            </asp:View>
                        </asp:MultiView>

                    </div>
                    <!-- /.box -->
                </div>
            </div></div></div>
        </section>
        <!-- /.content -->
    </div>
    <script language="javascript" type="text/javascript">
        function confirmDelete() {
            return confirm("Are you sure you want to delete this item?");
        }
        function removedoc() {
            $('#<%=docpdf.ClientID %>').hide();
        }
    </script>
    <!-- /.content-wrapper -->
</asp:Content>











































































































