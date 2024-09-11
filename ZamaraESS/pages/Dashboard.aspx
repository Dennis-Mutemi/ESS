<%@ Page Language="C#" Title="Dashboard" MasterPageFile="~/pages/Main.Master" AutoEventWireup="true"
    CodeBehind="Dashboard.aspx.cs" Inherits="ZamaraESS.pages.Dashboard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Main1" runat="Server">
    <!-- Content Wrapper. Contains page content -->
    <div class="content-wrapper" style="margin-top:168px;">      
        <!-- Main content -->
        <div class="content">
            <div class="container-fluid">               
                <!-- Main row -->
                <div class="row">                    
                    <div class="col-lg-12 connectedSortable">
                        <!-- Map card -->
                        <div class="card bg bg-white" style="margin-top:1em!important;">                         
                            <div class="card-body">
                                <h5><i style="color:green;" class="bi bi-person-circle"></i>&nbsp Basic Information</h5><hr style="font-weight: bold;"/>
                                <div class="row">
                                    <!-- /.col -->
                                    <div class="col-md-12">
                                        <div class="box-body">
                                            <div class="row">
                                                <div class="col-md-4">
                                                    <div class="form-group">
                                                        <asp:Label ID="EmployeeNoTextBox" runat="server" Text="Staff No:" CssClass="font-weight-bold"></asp:Label>
                                                        <asp:TextBox ID="LblEmployeeNo" runat="server" CssClass="form-control form-control-sharp" TextMode="SingleLine" ClientIDMode="Static" required="required" ReadOnly="true"></asp:TextBox>
                                                    </div>
                                                </div>                                                
                                                <div class="col-md-4">
                                                    <div class="form-group">
                                                        <asp:Label ID="IDNoTextBox" runat="server" Text="ID Number:" CssClass="font-weight-bold"></asp:Label>
                                                        <asp:TextBox ID="LblIDNo" runat="server" CssClass="form-control form-control-sharp" TextMode="SingleLine" ClientIDMode="Static" required="required"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="col-md-4">
                                                    <div class="form-group">
                                                        <asp:Label ID="lblEmailTextBox" runat="server" Text="Email Address:" CssClass="font-weight-bold"></asp:Label>
                                                        <asp:TextBox ID="lblEmail" runat="server" CssClass="form-control form-control-sharp" TextMode="Email" ClientIDMode="Static" required="required"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="col-md-4">
                                                    <div class="form-group">
                                                        <asp:Label ID="lblPhoneNoTextBox" runat="server" Text="Phone Number:" CssClass="font-weight-bold"></asp:Label>
                                                        <asp:TextBox ID="lblPhoneNo" runat="server" CssClass="form-control form-control-sharp" TextMode="SingleLine" ClientIDMode="Static" required="required"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="col-md-4">
                                                    <div class="form-group">
                                                        <asp:Label ID="LblCitizenshipTextBox" runat="server" Text="Citizenship:" CssClass="font-weight-bold"></asp:Label>
                                                        <asp:TextBox ID="LblCitizenship" runat="server" CssClass="form-control form-control-sharp" TextMode="SingleLine" ClientIDMode="Static" required="required" ReadOnly="true"></asp:TextBox>
                                                    </div>
                                                </div>
                                                 <div class="col-md-4">
                                                    <div class="form-group">
                                                        <asp:Label ID="lbdob" runat="server" Text="Birth Date:" CssClass="font-weight-bold"></asp:Label>
                                                        <asp:TextBox ID="txtdob" runat="server" CssClass="form-control form-control-sharp" TextMode="SingleLine" ClientIDMode="Static" required="required" ReadOnly="true"></asp:TextBox>
                                                    </div>
                                                </div>
                                                 <div class="col-md-4">
                                                        <div class="form-group">
                                                            <asp:Label ID="ImageLabel" runat="server" Text="Signature" CssClass="font-weight-bold"></asp:Label>
                                                            <div>                                                               
                                                                <asp:FileUpload ID="ProfileImageUpload" runat="server" CssClass="form-control form-control-sharp mt-2" ClientIDMode="Static" />
                                                                 <asp:Image ID="ProfileImage" runat="server" CssClass="img-thumbnail-sharp" ImageUrl="~/path/to/default/image.jpg" AlternateText="Staff signature" style="width:20%;height:20%;"/>
                                                            </div>
                                                        </div>
                                                   </div>
                                                    
                                                <div class="col-md-12 text-right">
                                                    <asp:Button ID="btnUpdate" runat="server" style="background:#28a745;" Text="Update" CssClass="btn btn-primary form-control-sharp" onclick="btnUpdate_Click"/>
                                                </div>
                                            </div>
                                            <!-- /.row -->
                                        </div>
                                    </div>
                                </div>                        
                            </div>
                        </div>
                  </div>
                </div>
           </div>
        </div>
    </div>
    <!-- /.content-wrapper -->
</asp:Content>
