<%@ Page Language="C#" title="Report Grievance" AutoEventWireup="true" MasterPageFile="~/pages/Main.Master" CodeBehind="ReportGrievance.aspx.cs" Inherits="ZamaraESS.pages.ReportGrievance" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Main1" runat="Server">
   <!-- Content Wrapper. Contains page content -->
   <div class="content-wrapper" style="margin-top:168px;">
   <section class="content">
      <div class="container-fluid">
         <div class="card bg-gradient-default">
            <div class="card-header border-1">
               <h3 class="card-title">
                  <strong>Grievance Case</strong>
               </h3>
            </div>
            <!-- /.card-tools -->
            <div class="card-body">
               <asp:MultiView ID="MultiView1" runat="server">
                  <asp:View ID="View1" runat="server">
                     <div class="row">
                        <div class="col-md-4">
                           <div class="form-group">
                              <label>Aggressor Name</label>
                              <div class="icon-after-input">
                                 <asp:DropDownList ID="AggressorNo" CssClass="form-control select2"  runat="server"></asp:DropDownList>
                              </div>
                           </div>
                        </div>
                        <div class="col-md-4">
                           <div class="form-group">
                              <label>Incident Date</label>
                              <asp:TextBox ID="IncidentDate" class="form-control" BackColor="White" runat="server"></asp:TextBox>
                              <script>
                                  $j('#Main1_IncidentDate').Zebra_DatePicker({

                                  });
                              </script>
                           </div>
                        </div>
                        <div class="col-md-4">
                           <div class="form-group">
                              <label>Incident Time</label>
                              <input type="time" id="IncidentTime" runat="server" Class="form-control" />
                           </div>
                        </div>
                     </div>
                     <div class="row">
                        <div class="col-md-4">
                           <div class="form-group">
                              <label >Incident Location</label>
                              <asp:TextBox ID="Incident" runat="server" TextMode="MultiLine"  CssClass="form-control"></asp:TextBox>
                           </div>
                        </div>
                        <div class="col-md-4">
                           <div class="form-group">
                              <label>Grievance Type</label>
                              <div class="icon-after-input">
                                 <asp:DropDownList ID="GrievanceType" CssClass="form-control select2"  runat="server"></asp:DropDownList>
                              </div>
                           </div>
                        </div>
                        <div class="col-md-4">
                           <div class="form-group">
                              <label >General Description</label>
                              <asp:TextBox ID="Description" runat="server" TextMode="MultiLine"  CssClass="form-control"></asp:TextBox>
                           </div>
                        </div>
                     </div>
                     <hr />
                     <div style="margin-right: 1%">
                        <asp:Button ID="Back" class="btn btn-sm btn-success pull-right"  Style="background-color: #48A23F; color: white" runat="server" Text="Back" OnClick ="Back_Click" />
                        <asp:Button ID="update" class="btn btn-sm btn-success pull-right"  runat="server" Text="Next" onClick ="update_Click"/>
                     </div>
                  </asp:View>
                  <asp:View ID="View2" runat="server">
                     <div id="NewLines" runat="server"  visible="true">
                        <b>Aggrieved Parties</b>
                        <hr />
                        <div id="Parties" runat="server">
                           <div class="row">
                              <div class="col-md-4">
                                 <div class="form-group">
                                    <label>Employee No </label>
                                    <asp:DropDownList ID="AggrievedNo" CssClass="form-control select2"   runat="server" OnSelectedIndexChanged="Action_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                 </div>
                              </div>
                              <div  class="col-md-4">
                                 <div class="form-group">
                                    <label>Employee Name </label>
                                    <asp:TextBox ID="Employeediv" CssClass="form-control" runat="server" Enabled="false"></asp:TextBox>
                                 </div>
                              </div>
                              <div class="col-md-2">
                                 <hr />
                                 <div class="form-group">
                                    <asp:Button ID="btnLine" class="btn btn-success pull-left" runat="server" Text="Add" OnClick="btnLine_Click"/>
                                 </div>
                              </div>
                           </div>
                        </div>
                     </div>
                     <br />
                     <asp:LinkButton ID="lbnAddLine" ToolTip="Add New Lines" class="pull-right text-success" runat="server" Visible="false" Text="Add New Line"><i class="fa fa-plus-circle"></i> Add Line</asp:LinkButton>
                     <asp:GridView ID="AggrievedParty" style="margin-left: 20px;" AutoGenerateColumns="false" DataKeyNames="DocumentNo" class="table table-responsive no-padding table-bordered table-hover small" runat="server"
                        AllowSorting="True" AllowPaging="true" ShowFooter="false" PageSize="20">
                        <Columns>
                           <asp:TemplateField HeaderStyle-HorizontalAlign="Justify" HeaderText="#" SortExpression="">
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
                                 <asp:LinkButton ID="lbtnCancll" ForeColor="Red" runat="server" ToolTip="Click to Remove line" OnClick="lbtnCancll_Click" CommandArgument='<%# Eval("EmployeeNo") %>'><i class="fa fa-trash-alt fa-sm"></i> Delete</asp:LinkButton>
                              </ItemTemplate>
                           </asp:TemplateField>
                        </Columns>
                        <FooterStyle HorizontalAlign="Center" />
                        <EmptyDataTemplate>
                           <span style="color: red">No Records Found</span>
                        </EmptyDataTemplate>
                     </asp:GridView>
                     <!-- Resolution lines-->
                     <div id="ResolutionLines" runat="server" style="margin-left: 20px;" visible="false">
                        <b>Resolution Lines</b>
                        <hr />
                        <div id="Resolutiondiv" runat="server">
                           <div class="row">
                              <div class="col-md-4">
                                 <div class="form-group">
                                    <label>Verdict </label>
                                    <asp:DropDownList ID="Verdict" CssClass="form-control select2"   runat="server"></asp:DropDownList>
                                 </div>
                              </div>
                              <div  class="col-md-4">
                                 <div class="form-group">
                                    <label>Description </label>
                                    <asp:TextBox ID="Descriptiondiv" CssClass="form-control" runat="server" ></asp:TextBox>
                                 </div>
                              </div>
                              <div class="col-md-3">
                                 <div class="form-group">
                                    <label>Remark</label>
                                    <asp:TextBox ID="Remark" runat="server" TextMode="MultiLine"  CssClass="form-control"></asp:TextBox>
                                 </div>
                              </div>
                              <div class="col-md-2">
                                 <div class="form-group">
                                    <asp:Button ID="Button1" class="btn btn-success pull-left" runat="server" Text="Add" OnClick="Button1_Click"/>
                                 </div>
                              </div>
                           </div>
                        </div>
                        <br />
                     </div>
                     <asp:LinkButton ID="Resolutions" ToolTip="Add Resolutions" class="pull-right text-success" runat="server" Visible="false" Text="Add Resolutions"><i class="fa fa-plus-circle"></i> Add Resolution</asp:LinkButton>
                     <asp:GridView ID="ResolutionLine" AutoGenerateColumns="false" DataKeyNames="DocumentNo" class="table table-responsive no-padding table-bordered table-hover small" runat="server"
                        AllowSorting="True" AllowPaging="true" ShowFooter="false" PageSize="20" style="margin-left: 20px;">
                        <Columns>
                           <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="#" SortExpression="">
                              <HeaderStyle Width="30px" />
                              <ItemTemplate>
                                 <%# string.Format("{0}",Container.DataItemIndex + 1 +".") %>
                              </ItemTemplate>
                           </asp:TemplateField>
                           <asp:BoundField DataField="VerdictCode" HeaderText="Verdict Code" />
                           <asp:BoundField DataField="Description" HeaderText="Description" />
                           <asp:BoundField DataField="Remarks" HeaderText="Remarks" />
                           <asp:TemplateField HeaderText="Action" SortExpression="" HeaderStyle-HorizontalAlign="Left">
                              <ItemStyle Width="110px" HorizontalAlign="Left" />
                              <ItemTemplate>
                                 <asp:LinkButton ID="LinkButton1" ForeColor="Red" runat="server" ToolTip="Click to Remove line" OnClick="lbtnCancll_Click" CommandArgument='<%# Eval("VerdictCode") %>'><i class="fa fa-trash-alt fa-sm"></i> Delete</asp:LinkButton>
                              </ItemTemplate>
                           </asp:TemplateField>
                        </Columns>
                        <FooterStyle HorizontalAlign="Center" />
                        <EmptyDataTemplate>
                           <span style="color: red">No Records Found</span>
                        </EmptyDataTemplate>
                     </asp:GridView>
                     <%--Attachments--%>
                     <div id="DocAttachment" style="margin-left: 20px;" runat="server" visible="true">
                        <b>Document Attachment</b>
                        <hr />
                        <div id="AttachmentForm" runat="server">
                           <div class="row">
                              <div class="col-md-2">
                                 <div class="form-group">
                                    <asp:Label ID="GrievanceNo" runat="server" Enabled="false" CssClass="form-control"></asp:Label>
                                 </div>
                              </div>
                              <div class="col-md-6">
                                 <div class="form-group">
                                    <asp:FileUpload ID="filetoupload" runat="server" CssClass="form-control btn btn-outline-success" />
                                 </div>
                              </div>
                              <div class="col-md-4">
                                 <div class="form-group">
                                    <asp:Button ID="btnUpload" CssClass="btn" Style="background-color: #26284f; color: white" runat="server" Text="Upload" onClick="btnUpload_Click"/>
                                 </div>
                              </div>
                           </div>
                        </div>
                        <asp:GridView ID="gvAttachmentLines" AutoGenerateColumns="false" DataKeyNames="ID" class="table table-responsive no-padding table-bordered table-hover small" runat="server"
                           AllowSorting="True"  AllowPaging="true" ShowFooter="false" PageSize="20" >
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
                                    <asp:LinkButton id="Cancel" ForeColor="Red" CssClass="label label-danger" runat="server" ToolTip="Click to remove line" CommandArgument='<%# Eval("ID") %>' OnClick="Cancel_Click"><i class="fa fa-trash-alt fa-sm"></i>Delete</asp:LinkButton>
                                 </ItemTemplate>
                              </asp:TemplateField>
                           </Columns>
                           <FooterStyle HorizontalAlign="Center" />
                           <EmptyDataTemplate>
                              <span style="color: red">No Files Uploaded</span>
                           </EmptyDataTemplate>
                        </asp:GridView>
                     </div >
                     <!-- /.box-body -->
                     <div class="box-footer clearfix">
                        <asp:LinkButton ID="newback" class="btn btn-sm btn-flat pull-left" Style="background-color: #48A23F; color: white; margin-left: 20px;" runat="server" OnClick="newback_Click"> <i class="fa fa-backward"></i> Back</asp:LinkButton>
                        <asp:LinkButton ID="lbtnSubmit" class="btn btn-sm btn-info btn-flat pull-right" style="background-color: #26284f;  color: white; margin-left: 20px;" runat="server" OnClick="lbtnSubmit_Click"> <i class="fa fa-check"></i> submit</asp:LinkButton>
                     </div>
                  </asp:View>
               </asp:MultiView>
            </div>
            <!-- /.box -->
         </div>
   </section>
   </div>      
</asp:Content>