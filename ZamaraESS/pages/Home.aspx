<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Home.aspx.cs" MasterPageFile="~/pages/Main.Master" Inherits="ZamaraESS.pages.Home" %>


<asp:Content ID="Content1" ContentPlaceHolderID="Main1" runat="Server">
    <!-- Content Wrapper. Contains page content -->
      <style>
       .card-body {
            position: relative;
            padding-bottom: 30px;
        }
        .employee-card {
            width: 100%;
            margin-bottom: 1rem;
            display: flex;
            align-items: center;
            padding: 1rem;
            background-color: #f8f9fa;
            border-radius: 0.25rem;
            transition: box-shadow 0.3s;
        }
        .employee-card:hover {
            box-shadow: 0 4px 8px rgba(0,0,0,0.1);
        }
        .employee-card img {
            width: 60px;
            height: 60px;
            border-radius: 50%;
            margin-right: 1rem;
            border: 2px solid #26284f;
            object-fit: cover;
        }
        .employee-card div {
            flex: 1;
        }
        .card-title {
            color: #48A23F;
            font-size: 24px;
        }
        .birthday-message {
            margin: 0;
            font-style: italic;
            color: #555;            
        }
       .pagination-wrapper {
            position: absolute;
            right: 1rem;
            bottom: 1rem;
        }
        .pagination {
            margin: 0;
        }
        .pagination .page-link {
            color: #007bff;
        }       
        .pagination .page-item .page-link {
            border-radius: 50%;
            margin: 0 5px;
        }
        .pagination .page-item:hover .page-link {
            background-color: #48A23F;
        }        
  </style>
       <div class="content-wrapper pagepostion" style="margin-top:178px!important;">
        <!-- Main content -->
        <div class="container">
            <div>
                <div class="row">
                    <div class="col-sm-4">
                        <div class="card">
                            <div class="card-body">
                                <div class="card-header" style="background:#26284f!important;color:white!important;">Leave Balances</div>

                                <div id="carouselExampleControls" class="carousel slide" data-ride="carousel">
                                    <div class="carousel-inner">

                                        <% if (getLeaveBalances() != null)
                                            {
                                                int k = 0; string act = "", bg = "";%>
                                        <% foreach (var site in getLeaveBalances())
                                            {
                                                act = "";
                                                k++;
                                                if (k == 1)
                                                {
                                                    act = "active";
                                                }
                                                if (k % 2 == 0)
                                                {
                                                    bg = "bg-blue";
                                                }
                                                else if (k % 4 == 0)
                                                {
                                                    bg = "bg-green";
                                                }
                                                else if (k % 3 == 0)
                                                {
                                                    bg = "bg-green";
                                                }
                                                else
                                                {
                                                    bg = "bg-blue";
                                                }
                                        %>
                                        <div class="carousel-item <% =bg %> text-center <% =act %>" style="min-height: 120px;">
                                            <div><%= site.Key %></div>
                                            <h5><%= site.Value %> days</h5>
                                            <h6>Remaining</h6>
                                        </div>
                                        <% } %>
                                        <% } %>
                                    </div>
                                    <button class="carousel-control-prev" type="button" data-target="#carouselExampleControls" data-slide="prev">
                                        <span class="carousel-control-prev-icon" aria-hidden="true"></span>
                                        <span class="sr-only">Previous</span>
                                    </button>
                                    <button class="carousel-control-next" type="button" data-target="#carouselExampleControls" data-slide="next">
                                        <span class="carousel-control-next-icon" aria-hidden="true"></span>
                                        <span class="sr-only">Next</span>
                                    </button>
                                </div>

                                <a href="LeaveApplication.aspx?An=&status=Open&Tp=new" class="btn btn-primary mt-2" style="border-radius: 0px!important;background:#28a745!important;" data-toggle="modal" data-target="#staticBackdrop">Take a day off</a>
                          </div>
                        </div>
                    </div>
                    <div class="col-sm-8">
                        <div class="card" style="min-height:16em!important;">
                            <div class="card-body">
                                <div class="card-header" style="background:#26284f!important;color:white!important;">
                                    Approval Request
                                </div>
                                <asp:GridView ID="gvAttachmentLines" AutoGenerateColumns="false" DataKeyNames="EntryNo" class="table table-responsive no-padding table-bordered table-hover small mt-2" runat="server"
                                    AllowSorting="True" AllowPaging="true" ShowFooter="false" PageSize="20">
                                    <Columns>
                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="#" SortExpression="">
                                            <HeaderStyle Width="30px" />
                                            <ItemTemplate>
                                                <%# string.Format("{0}",Container.DataItemIndex + 1 +".") %>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="RecordIDtoApprove" HeaderText="Record to Approve" />
                                        <asp:BoundField DataField="SenderID" HeaderText="Sender" />
                                       <asp:TemplateField HeaderText="Action" SortExpression="" HeaderStyle-HorizontalAlign="Left">
                                            <ItemStyle Width="110px" HorizontalAlign="Left" />
                                            <ItemTemplate>
                                                <asp:LinkButton ID="reject" ForeColor="Red" CssClass="label label-danger" runat="server" ToolTip="Click to Reject this request" CommandArgument='<%# Eval("EntryNo") %>' OnClick="reject_Click"><i class="fa fa-times"></i> Reject</asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Action" SortExpression="" HeaderStyle-HorizontalAlign="Left">
                                            <ItemStyle Width="110px" HorizontalAlign="Left" />
                                            <ItemTemplate>
                                                <asp:LinkButton ID="approve" ForeColor="#48a23f" CssClass="label label-success" runat="server" ToolTip="Click to approve this request" CommandArgument='<%# Eval("EntryNo") %>' OnClick="approve_Click"><i class="fa fa-check"></i>&nbsp Approve</asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <FooterStyle HorizontalAlign="Center" />
                                    <EmptyDataTemplate>
                                        <span style="color: red">No available requests to approve</span>
                                    </EmptyDataTemplate>
                                </asp:GridView>
                            </div>
                        </div>
                    </div>
                </div>                
                <div class="row">
                    <div class="col-sm-6">
                        <div class="card">
                            <div class="card-body">
                                <div id="employee-cards-container" class="d-flex flex-wrap">
                                    <h1 class="card-title font-weight-bold">Celebrations <i class="fas fa-birthday-cake glitter"></i></h1><hr />
                                        <%=Birthdaywishs()%>
                                  </div>                                
                                  <div class="pagination-wrapper">
                                    <nav aria-label="Page navigation">
                                        <ul class="pagination">
                                            <!-- Pagination items will be dynamically inserted here -->
                                        </ul>
                                    </nav>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="col-sm-6">
                        <div class="card">
                            <div class="card-body">
                                <h5 class="card-title">Who is out</h5>
                                <p class="card-text">Need to know who is out,view the leave calendar.</p>
                                <a href="LeaveCalendar.aspx" class="btn btn-primary" style="border-radius: 0px!important;background:#28a745!important;color:white!important;">View Leave Calendar</a>
                            </div>
                        </div>
                    </div>
                </div>             
            </div>
        </div>    
</div>
    
</asp:Content>


