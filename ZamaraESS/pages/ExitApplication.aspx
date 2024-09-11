<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/pages/Main.Master" CodeBehind="ExitApplication.aspx.cs" Inherits="ZamaraESS.pages.ExitApplication" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Main1" runat="Server">
   <!-- Content Wrapper. Contains page content -->
   <div class="content-wrapper" style="margin-top:168px;" runat="server" id="contents">
      <!-- Main content -->
      <section class="content">
         <div class="container-fluid">
            <div class="card bg-gradient-default cardposition">
               <div class="card-header border-1">
                  <h3 class="card-title">
                     <strong>Exit Application</strong>
                  </h3>
               </div>
               <div class="card-body">
                  <asp:MultiView ID="MultiView1" runat="server">
                     <asp:View ID="View1" runat="server">
                        <div class="row">
                           <div class="col-md-3">
                              <div class="form-group">
                                 <label>Exit Reason</label>
                                 <asp:DropDownList ID="exitreason" runat="server" CssClass="form-control select2"></asp:DropDownList>
                              </div>
                           </div>
                           <div class="col-md-3">
                              <label>Notice Start Date</label>
                              <asp:TextBox ID="startdate" CssClass="form-control" BackColor="White" runat="server"></asp:TextBox>
                              <script>
                                  $j('#Main1_startdate').Zebra_DatePicker({                                                                                         
                                      direction: [1, true]                                    
                                  });                                
                              </script>
                           </div>
                           <div class="col-md-4">
                              <div class="form-group">
                                 <label>Leave Balance</label>
                                 <div class="icon-after-input">
                                    <asp:TextBox ID="leavebalance" CssClass="form-control" style="color:blue;" Enabled="false" runat="server"></asp:TextBox>
                                 </div>
                              </div>
                           </div>
                        </div>
                        <div class="row">
                           <div class="col-md-3">
                              <label>Last Date of Service</label>
                              <asp:TextBox ID="lastdate" CssClass="form-control" BackColor="White" runat="server"></asp:TextBox>
                              <script>
                                  $j('#Main1_lastdate').Zebra_DatePicker({
                                      // remember that the way you write down dates
                                      // depends on the value of the "format" property!                                                    
                                      direction: [1, true]
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
                           <div class="col-md-3">
                              <div id="TerminationDiv" class="form-group" runat="server">
                                 <label>Termination Date</label>
                                 <asp:TextBox ID="Termination" CssClass="form-control" BackColor="White" runat="server"></asp:TextBox>
                                 <script>
                                     $j('#Main1_Termination').Zebra_DatePicker({                                                                                            
                                         direction: [1, true]                                        
                                     });                                    
                                 </script>
                              </div>
                           </div>
                           <div class="col-md-6">
                              <div id="Acceptanceremark" class="form-group" runat="server" visible="false">
                                 <label >Acceptance Remark</label>
                                 <asp:TextBox ID="Acceptance" runat="server" TextMode="MultiLine"  CssClass="form-control"></asp:TextBox>
                              </div>
                           </div>
                           <div class="col-md-3">
                              <div id="ApprovalDiv1" class="form-group" runat="server" visible="false">
                                 <label >Approval Remark</label>
                                 <asp:TextBox ID="Approval" runat="server" TextMode="MultiLine"  CssClass="form-control"></asp:TextBox>
                              </div>
                           </div>
                        </div>
                        <hr />
                        <%--Attachments--%>
                        <div id="DocAttachment" runat="server" visible="true">
                           <b>Document Attachment</b>
                           <hr />
                           <div id="AttachmentForm" runat="server">
                              <div class="row">
                                 <div class="col-md-8">
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
                                       <asp:LinkButton ID="Cancel" ForeColor="Red" CssClass="label label-danger" runat="server" ToolTip="Click to remove line" CommandArgument='<%# Eval("ID") %>' OnClick="Cancel_Click"><i class="fa fa-trash-alt fa-sm"></i>Delete</asp:LinkButton>
                                    </ItemTemplate>
                                 </asp:TemplateField>
                              </Columns>
                              <FooterStyle HorizontalAlign="Center" />
                              <EmptyDataTemplate>
                                 <span style="color: red">No Files Uploaded</span>
                              </EmptyDataTemplate>
                           </asp:GridView>
                        </div>
                        <!-- /.box-body -->
                        <div class="box-footer clearfix">
                           <asp:linkbutton id="btnapproval" class="btn btn-sm btn-flat pull-right"  style="background-color: #26284f; color: white" runat="server" visible="false"><i class="fa fa-check"></i> approve</asp:linkbutton>
                           <asp:LinkButton ID="newback" class="btn btn-sm btn-flat pull-left" Style="background-color: #48A23F; color: white" runat="server" OnClick="newback_Click"> <i class="fa fa-backward"></i> Back</asp:LinkButton>
                           <asp:LinkButton ID="Recall" class="btn btn-sm btn-flat btn-success pull-left" runat="server" OnClick="Recall_Click"><i class="fa fa-check"></i>&nbsp Recall</asp:LinkButton>
                           <asp:LinkButton ID="lbtnSubmit" class="btn btn-sm btn-info btn-flat pull-right" style="background-color: #26284f;  color: white" runat="server" OnClick="lbtnSubmit_Click"> <i class="fa fa-check"></i> submit</asp:LinkButton>
                           <asp:LinkButton ID="Acceptancebtn" class="btn btn-sm btn-info btn-flat pull-right" style="background-color: #26284f;  color: white" runat="server" OnClick="Acceptance_Click"> <i class="fa fa-check"></i> Accept</asp:LinkButton>
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