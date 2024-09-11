<%@ Page Language="C#" Title="Disciplinary Case" AutoEventWireup="true" MasterPageFile="~/pages/Main.Master" CodeBehind="DisciplinaryCase.aspx.cs" Inherits="ZamaraESS.pages.DisciplinaryCase" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Main1" runat="Server">
   <!-- Content Wrapper. Contains page content -->
   <div class="content-wrapper" style="margin-top:168px;">   
   <!-- Main content -->
   <section class="content">
      <div class="container-fluid">
         <div class="card bg-gradient-default cardposition">
            <div class="card-header border-1">
               <h3 class="card-title">
                  <strong>Disciplinary Case</strong>
               </h3>               
            </div>
            <!-- /.card-tools -->
            <div class="card-body">
               <asp:MultiView ID="MultiView1" runat="server">
                  <asp:View ID="View1" runat="server">
                     <div class="row">
                        <div class="col-md-3">
                           <div class="form-group">
                              <label>Offender Name</label>
                              <div class="icon-after-input">
                                 <asp:DropDownList ID="OffendorNo" CssClass="form-control select2"   runat="server"></asp:DropDownList>
                              </div>
                           </div>
                        </div>
                        <div class="col-md-3">
                           <div class="form-group">
                              <label>Offence Type</label>
                              <asp:DropDownList ID="OffenceType" runat="server" CssClass="form-control select2"></asp:DropDownList>
                           </div>
                        </div>
                        <div class="col-md-3">
                           <div class="form-group">
                              <label>Offence Category</label>
                              <asp:DropDownList ID="OffenceCategory" runat="server" CssClass="form-control select2"></asp:DropDownList>
                           </div>
                        </div>
                        <div class="col-md-3">
                           <div class="form-group">
                              <label>Case Description</label>
                              <asp:TextBox ID="Description" runat="server" TextMode="MultiLine" CssClass="form-control"></asp:TextBox>
                           </div>
                        </div>
                        <div class="col-md-3">
                           <label>Case Date</label>
                           <asp:TextBox ID="Casedate" CssClass="form-control" BackColor="White" runat="server"></asp:TextBox>
                           <script>
                               $j('#Main1_Casedate').Zebra_DatePicker({                                                                                      
                                   direction: [1, true]                                   
                               });                               
                           </script>
                        </div>
                        <div class="col-md-3">
                           <div id="HrRemarkDiv" class="form-group" runat="server" visible="false">
                              <label >HR Remark</label>
                              <asp:TextBox ID="HRRemark" runat="server" TextMode="MultiLine"  CssClass="form-control"></asp:TextBox>
                           </div>
                        </div>
                        <div class="col-md-3">
                           <div id="Suspectremark" class="form-group" runat="server" visible="false">
                              <label >Suspect Remark</label>
                              <asp:TextBox ID="Suspect" runat="server" TextMode="MultiLine"  CssClass="form-control"></asp:TextBox>
                           </div>
                        </div>
                        <div class="col-md-3">
                           <div id="DecisionSummary" class="form-group" runat="server" visible="false">
                              <label >Decision Summary</label>
                              <asp:TextBox ID="Decision" runat="server" TextMode="MultiLine" CssClass="form-control"></asp:TextBox>
                           </div>
                        </div>
                        <div class="col-md-4" id="Start" runat="server" visible="false">
                           <label>Start Date</label>
                           <asp:TextBox ID="startdate" CssClass="form-control" BackColor="White" runat="server"></asp:TextBox>
                           <script>
                               $j('#Main1_startdate').Zebra_DatePicker({                                                                                  
                                   direction: [1, true]                                    
                               });                              
                           </script>
                        </div>
                        <div class="col-md-4" id="EndDate" runat="server" visible="false">
                           <label>End Date</label>
                           <asp:TextBox ID="End" CssClass="form-control" BackColor="White" runat="server"></asp:TextBox>
                           <script>
                               $j('#Main1_enddate').Zebra_DatePicker({                                                                                      
                                   direction: [1, true]                                   
                               });                              
                           </script>
                        </div>
                        <hr />
                        <div style="margin-right: 1%">
                           <asp:Button ID="Back" class="btn btn-sm btn-success pull-right"  Style="background-color: #48A23F; color: white" runat="server" Text="Back" OnClick="Back_Click" />
                           <asp:Button ID="update" class="btn btn-sm btn-success pull-right" Visible="false" runat="server" Text="Next" onClick ="Update_Click"/>
                        </div>
                  </asp:View>
                  <asp:View ID="View2" runat="server">                              
                  <div id="NewLines" runat="server" visible="true"><b>Committee Members</b>
                  <div class="col-md-12">
                  <div class="row">
                  <div id="Committee" runat="server" visible="false">
                  <div  class="col-md-2">
                  <div class="form-group">
                  <label>Disciplinary No.</label>
                  <asp:Label ID="txtReqNo" runat="server" Enabled="false" CssClass="form-control"></asp:Label>
                  </div>
                  </div>
                  <div class="col-md-4">
                  <div ID="Employee" class="form-group">
                  <label>Employee Name</label>
                  <asp:DropDownList ID="EmployeeNos" CssClass="form-control select2"   runat="server"></asp:DropDownList>
                  </div>
                  </div>                                            
                  <div class="col-md-2">
                  <hr />
                  <div class="form-group">
                  <asp:Button ID="btnLine" class="btn btn-success pull-left" runat="server" Text="Add" OnClick="btnLine_Click"/>
                  </div>
                  </div>
                  </div>
                  </div></div>
                  <br />
                  </div>
                  <asp:LinkButton ID="lbnAddLine" ToolTip="Add New Lines" class="pull-right text-success" runat="server" Visible="false" Text="Add New Line"><i class="fa fa-plus-circle"></i> Add Line</asp:LinkButton>
                  <asp:GridView ID="DisciplinaryActionLine" AutoGenerateColumns="false" DataKeyNames="CaseNo" class="table table-responsive no-padding table-bordered table-hover small" runat="server"
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
                  <asp:LinkButton ID="lbtnCancll" ForeColor="Red" runat="server" ToolTip="Click to Remove line" OnClick="lbtnCancll_Click" CommandArgument='<%# Eval("EmployeeNo") %>'><i class="fa fa-trash-alt fa-sm"></i> Delete</asp:LinkButton>
                  </ItemTemplate>
                  </asp:TemplateField>
                  </Columns>
                  <FooterStyle HorizontalAlign="Center" />
                  <EmptyDataTemplate>
                  <span style="color: red">No Records Found</span>
                  </EmptyDataTemplate>
                  </asp:GridView>
                  <!-- Action lines-->
                  <div id="ActionLines" runat="server" visible="true">
                  <b>Action Lines</b>
                  <hr />
                  <div id="Actiondiv" runat="server">
                  <div class="row">
                  <div class="col-md-4">
                  <div class="form-group">
                  <label>Action No </label>
                  <asp:DropDownList ID="Action" CssClass="form-control select2"   runat="server" OnSelectedIndexChanged="Action_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                  </div>
                  </div>        
                  <div  class="col-md-4">
                  <div class="form-group">
                  <label>Description No </label>
                  <asp:TextBox ID="Descriptiondiv" CssClass="form-control" runat="server" ></asp:TextBox>
                  </div>
                  </div>                                            
                  <div class="col-md-2">
                  <hr />
                  <div class="form-group">
                  <asp:Button ID="Button1" class="btn btn-success pull-left" runat="server" Text="Add" OnClick="Button1_Click"/>
                  </div>
                  </div>
                  </div>
                  </div>
                  <br />
                  </div>
                  <asp:LinkButton ID="AddLine" ToolTip="Add New Lines" class="pull-right text-success" runat="server" Visible="false" Text="Add New Line"><i class="fa fa-plus-circle"></i> Add Line</asp:LinkButton>
                  <asp:GridView ID="ActionLine" AutoGenerateColumns="false" DataKeyNames="DocumentNo" class="table table-responsive no-padding table-bordered table-hover small" runat="server"
                     AllowSorting="True" AllowPaging="true" ShowFooter="false" PageSize="20">                                        
                  <Columns>
                  <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="#" SortExpression="">
                  <HeaderStyle Width="30px" />
                  <ItemTemplate>
                  <%# string.Format("{0}",Container.DataItemIndex + 1 +".") %>
                  </ItemTemplate>
                  </asp:TemplateField>
                  <asp:BoundField DataField="ActionNo" HeaderText="Action No." />
                  <asp:BoundField DataField="ActionDescription" HeaderText="Action Description" />                                         
                  <asp:TemplateField HeaderText="Action" SortExpression="" HeaderStyle-HorizontalAlign="Left">
                  <ItemStyle Width="110px" HorizontalAlign="Left" />
                  <ItemTemplate>
                  <asp:LinkButton ID="lbtnCancll" ForeColor="Red" runat="server" ToolTip="Click to Remove line" OnClick="lbtnCancll_Click" CommandArgument='<%# Eval("ActionNo") %>'><i class="fa fa-trash-alt fa-sm"></i> Delete</asp:LinkButton>
                  </ItemTemplate>
                  </asp:TemplateField>
                  </Columns>
                  <FooterStyle HorizontalAlign="Center" />
                  <EmptyDataTemplate>
                  <span style="color: red">No Records Found</span>
                  </EmptyDataTemplate>
                  </asp:GridView>
                  <%--Attachments--%>
                  <div id="DocAttachment" runat="server" visible="true">
                  <b>Document Attachment</b>
                  <hr />
                  <div id="AttachmentForm" runat="server">
                  <div class="row">
                  <div class="col-md-2">
                  <div class="form-group">            
                  <asp:Label ID="DisciplinaryNo" runat="server" Enabled="false" CssClass="form-control"></asp:Label>
                  </div>
                  </div>
                  <div class="col-md-6">
                  <div class="form-group">
                  <asp:FileUpload ID="filetoupload" runat="server" CssClass="form-control btn btn-outline-success" />
                  </div>
                  </div>
                  <div class="col-md-4">
                  <div class="form-group">
                  <asp:Button ID="btnUpload" CssClass="btn" Style="background-color: #26284f; color: white" runat="server" Text="Upload" onClick="btnUpload_Click1"/>
                  </div>
                  </div>
                  </div>
                  </div>
                  <asp:GridView ID="gvAttachmentLines" AutoGenerateColumns="false" DataKeyNames="ID" class="table table-responsive no-padding table-bordered table-hover small" runat="server"
                     AllowSorting="True" AllowPaging="true" ShowFooter="false" PageSize="20" OnRowDataBound="gvAttachmentLines_RowDataBound">
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
                  <asp:LinkButton id="Cancel" ForeColor="Red" CssClass="label label-danger" runat="server" ToolTip="Click to remove line" CommandArgument='<%# Eval("ID") %>' OnClick="Cancel_Click"><i class="fa fa-trash-alt fa-sm"></i></asp:LinkButton>
                  <asp:LinkButton ID="View" ForeColor="Red" CssClass="label label-danger" runat="server" ToolTip="Click to View document" CommandArgument='<%# Eval("ID") %>' OnClick="View_Click"><i class="fa fa-eye fa-sm"></i></asp:LinkButton>
                  </ItemTemplate>
                  </asp:TemplateField>
                  </Columns>
                  <FooterStyle HorizontalAlign="Center" />
                  <EmptyDataTemplate>
                  <span style="color: red">No Files Uploaded</span>
                  </EmptyDataTemplate>
                  </asp:GridView>                                    
                  </div >  
                  <div id="viewdoc" class="card-body" runat="server" visible="false">
                  <div class="row">                                            
                  <div class="col-md-12">
                  <span style="font-size: 10pt">
                  <iframe runat="server" id="myPDF" src="" width="100%" height="500" />
                  </span>
                  </div>
                  </div>
                  </div>                               
                  <!-- /.box-body -->
                  <div class="box-footer clearfix">                                    
                  <asp:LinkButton ID="newback" class="btn btn-sm btn-flat pull-left" Style="background-color: #48A23F; color: white" runat="server" OnClick="newback_Click"> <i class="fa fa-backward"></i> Back</asp:LinkButton>
                  <asp:LinkButton ID="lbtnSubmit" class="btn btn-sm btn-info btn-flat pull-right" style="background-color: #26284f;  color: white" runat="server" OnClick="lbtnSubmit_Click"> <i class="fa fa-check"></i> submit</asp:LinkButton>
                  </div>
                  </asp:View>
               </asp:MultiView>
               </div>
               <!-- /.box -->
            </div>
         </div>
   </section>
   </div>
   <!-- /.content -->
   <!-- /.content-wrapper -->
</asp:Content>