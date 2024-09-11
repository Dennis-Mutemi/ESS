<%@ Page Language="C#" Title="Leave Scheduling" MasterPageFile="~/pages/Main.Master" AutoEventWireup="true" CodeBehind="LeaveScheduling.aspx.cs" Inherits="ZamaraESS.pages.LeaveScheduling" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Main1" runat="Server">
    <!-- Content Wrapper. Contains page content -->
    <div class="content-wrapper pagepostion">

        <!-- Main content -->
        <section class="content">
            <div class="container-fluid">

                <div class="card bg-gradient-default cardposition">
                    <div class="card-header border-1">
                        <h3 class="card-title">
                            <strong>Leave Scheduling</strong>
                        </h3>                       
                    </div>
                    <div class="card-body">
                        <div class="row">
                            <div class="col-md-4">
                                <div class="form-group">
                                    <label>Leave Type</label>
                                    <asp:DropDownList ID="DdLeaveType" class="form-control select2" runat="server" AutoPostBack="true" OnSelectedIndexChanged="DdLeaveType_SelectedIndexChanged"></asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-md-4">
                                <div class="form-group">
                                    <label>Days applied</label>
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
                                            //direction: [1, false],
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
                            <div class="col-sm-8">
                                <div class="form-group">
                                    <label for="atext3">Description</label>
                                    <div class="icon-after-input">
                                        <asp:TextBox ID="TxtPurpose" class="form-control" TextMode="MultiLine" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="col-sm-4">
                            <div class="form-group">
                                <label>Balance:</label>
                                <asp:Label ID="lblLeaveBal" ForeColor="Red" runat="server" Text=""></asp:Label>
                            </div>
                            </div>
                        </div>
                        <div class="row">
                        </div>
                        <!-- /.box-body -->
                        <div class="box-footer clearfix">
                            <asp:LinkButton ID="lbtnApply" class="btn btn-sm btn-flat pull-right" style="background-color:#26284f;color:white" runat="server" OnClick="lbtnApply_Click"> <i class="fa fa-check"></i> Schedule</asp:LinkButton>
                            <asp:LinkButton ID="lbtnBack" class="btn btn-sm btn-flat pull-left" style="background-color:#48A23F;color:white" runat="server" OnClick="lbtnBack_Click"> <i class="fa fa-backward"></i> Back</asp:LinkButton>
                            
                        </div>
                    </div>
                    <!-- /.box -->
                </div>
            </div>
        </section>
        <!-- /.content -->
    </div>
    <!-- /.content-wrapper -->
    <!-- BEGIN: modal -->
    <div class="modal fade" id="eventModal" tabindex="-1" role="dialog" aria-labelledby="eventModalLabel">
        <div class="modal-dialog" role="document">
            <div class="modal-content bg-emerland" runat="server" id="dvMdlContentPass" visible="false">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                    <h4 class="modal-title" id="eventModalLabel">Suucess!!</h4>
                </div>
                <div class="modal-body">
                    <div>
                        <p>Your leave Request has been successfully sent for Approval.</p>
                    </div>
                </div>
            </div>
            <div class="modal-content bg-alizarin text-white" runat="server" id="dvMdlContentFail" visible="false">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                    <h4 class="modal-title" id="eventModalLabelf">Error!</h4>
                </div>
                <div class="modal-body">
                    <div>
                        <p id="pMsg" runat="server">Oparation failed.</p>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <!-- END: modal -->
</asp:Content>

