<%@ Page Language="C#" Title="Appraisal Application" MasterPageFile="~/pages/Main.Master" AutoEventWireup="true" CodeBehind="AppraisalApplication.aspx.cs" Inherits="ZamaraESS.pages.AppraisalApplication" %>


<asp:Content ID="Content1" ContentPlaceHolderID="Main1" runat="Server">
    <!-- Content Wrapper. Contains page content -->
    <div class="content-wrapper" style="margin-top:168px;">
        <!-- Main content -->
        <div class="content">
            <div class="row">                    
    <div class="col-lg-12 connectedSortable">
            <div class="container-fluid">
                <div class="card bg-gradient-default" style="margin-top:1em;">
                    <div class="card-header border-1">
                        <h3 class="card-title">
                            <strong>Appraisal Application</strong>
                        </h3>                     
                    </div>
                    <div class="card-body">                     
                        <!-- Button trigger modal -->
                        
                           <%
                               String AppNumber = " ";
                               String ExpectedScore = "";
                               Dictionary<string, string> ratings = new Dictionary<string, string>();
                               foreach (string key in Session.Keys)
                               {
                                   if (key == "AppraisalNumber")
                                   {
                                       AppNumber = Session["AppraisalNumber"].ToString().ToString();
                                   }
                                   else if (key == "ExpectedScore")
                                   {
                                       ExpectedScore = Session["ExpectedScore"].ToString();
                                   }
                               }
                           %>  
                        
                           <%-- add appraisal category button--%>
                            <div class="row">
                                <div class="col-lg-12">                                                                    
                                        <button type="button" class="btn btn-sm btn-flat btn-success float-end shadow mb-1" style="background-color: #48A23F; color: white;display:none;" data-toggle="modal" data-target="#staticBackdro" id="EmployeeNo" empno='<%=Session["username"].ToString()%>' appno="<%=AppNumber%>">
                                        Add Appraisal Section
                                    </button>
                                </div>
                            </div>



                        <%--supervisor comment area--%>
                        
                        <div class="row mb-2" style="display: none;" id="supervisorcommentarea" >
                            <div class="border p-3">                                
                                <div class="row">
                                    <div class="col-lg-4">
                                        <label for="SupervisorSuccArea""> Supervisor Success Area</label>
                                        <textarea id='SupervisorSuccArea'  runat="server"  class="form-control supcomments" rows="3" placeholder="Enter text"></textarea>
                                    </div>
                                    <div class="col-lg-4 mt-3 mt-lg-0">
                                        <label for="SupervisorImprArea">Supervisor Improvement Areas</label>
                                        <textarea id='SupervisorImprArea' runat="server"  class="form-control supcomments" rows="3" placeholder="Enter text"></textarea>
                                    </div>                                    
                                    <div class="col-lg-4 mt-3 mt-lg-0">
                                        <label for="supervisoreremarks">Remarks</label>
                                        <textarea id='supervisoreremarks'  runat="server" class="form-control supcomments" rows="3" placeholder="Enter text"></textarea>
                                    </div>
                                </div>
                                <div class="row mt-3" style="display: none;" id="suprecom">
                                     <div class="col-lg-4 mt-3 mt-lg-0">
                                         <label for="recommend">Recommendation</label>
                                         <asp:DropDownList ID="recommend" runat="server" CssClass="form-control reccom"></asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                        </div>                      

                        <!-- Appriasl category modal -->
                        <div class="modal fade" id="staticBackdrop" data-backdrop="static" data-keyboard="false" tabindex="-1" aria-labelledby="staticBackdropLabel" aria-hidden="true">
                            <div class="modal-dialog">
                                <div class="modal-content" id="ExpectedValue" expectedscore="<%=ExpectedScore %>">
                                    <div class="modal-header">
                                        <h5 class="modal-title" id="staticBackdropLabel">Appraisal Categories</h5>
                                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                            <span aria-hidden="true">&times;</span>
                                        </button>
                                    </div>                                    
                                    <div class="modal-body">
                                        <div class="accordion">                                           
                                            <div class="card" id="sectionappcat">  
                                                
                                            </div>                                          
                                        </div>
                                    </div>
                                    <div class="modal-footer" style="color: white">
                                        <button type="button" class="btn btn-danger btn-sm btn-flat" data-dismiss="modal">Cancel</button>
                                        <button type="button" class="btn btn-sm btn-flat" style="background-color: #48A23F" onclick="AddSecAppraisalCat()">Add</button>
                                    </div>
                                </div>
                            </div>
                        </div>


                         <!-- Appriasl category section modal -->
                           <div class="modal fade" id="staticBackdro" data-backdrop="static" data-keyboard="false" tabindex="-1" aria-labelledby="staticBackdropLabe" aria-hidden="true">
                                   <div class="modal-dialog">
                                       <div class="modal-content">
                                           <div class="modal-header">
                                               <h5 class="modal-title" id="staticBackdropLabe">Appraisal Sections</h5>
                                               <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                                   <span aria-hidden="true">&times;</span>
                                               </button>
                                           </div>                                    
                                           <div class="modal-body">
                                               <div class="accordion">
                                                     <% if (LoadAppraisalSections() != null)
                                                         {
                                                             buttons.Visible = true; %> 
                                                      <% foreach (var site in LoadAppraisalSections())
                                                          {
                                                      %>
                                                      <div class="card">
                                                          <div class="card-header" id="<%= site.Key %>us" appraisaldescipt="<%= site.Value %>">
                                                              <h5 class="mb-0">
                                                                  <input type="checkbox" value="<%= site.Key %>" class="selectedApraisalSec"/>
                                                                  <button class="btn btn-link" type="button" data-toggle="collapse" data-target="#collapseOne" aria-expanded="true" aria-controls="collapseOne">
                                                                      <%= site.Value %>
                                                                  </button>
                                                              </h5>
                                                          </div>
                                                      </div>
                                                      <% } %>
                                                      <% } %>
                                               </div>
                                           </div>
                                           <div class="modal-footer" style="color: white">
                                               <button type="button" class="btn btn-danger btn-sm btn-flat" data-dismiss="modal">Cancel</button>
                                               <button type="button" class="btn btn-sm btn-flat" style="background-color: #48A23F" onclick="AddAppraisalCategorySections()">Add</button>
                                           </div>
                                       </div>
                                   </div>
                               </div>
                         </div>


                        <div id="alertMessage" class="alert alert-success" role="alert" style="display:none;position: absolute; top: 20px; right: 20px; z-index: 9999;width:50%;">
                                Item deleted successfully!
                        </div>
                        
                        <%--outer accordion--%>
                      <div class="accordion" id="accordionExampl" style="width:98%!important;margin:0 auto!important;">
                        <% int k = 100; int v = 0; Dictionary<string, Dictionary<string, string>> appraisalCat = new Dictionary<string, Dictionary<string, string>>();

                            foreach (var kvp in categoryjobs())
                            {
                                appraisalCat[kvp.Key] = kvp.Value;
                            }

                            %>
                        <% if (appraisalCat.Count != 0)
                            {  %>
                        <% foreach (var sit in appraisalCat)
                            { %>
                        <% k = k + 1;%>

                            <div class="card">
                                <div style="background:#26284f;" class="card-header" id="headingOne<%= k %>">
                                    <h2 class='mb-0 d-flex justify-content-between align-items-center'>
                                        <button class="btn btn-link btn-block text-left" style="color:white!important;" type="button" data-toggle="collapse" data-target="#collapseOne<%= k %>" aria-expanded="true" aria-controls="collapseOne">
                                             <%= sit.Key%>
                                        </button>
                                       <span class="minus-icon" data-toggle="tooltip" style="display:none;" data-placement="right" title="Remove this section" id="<%=sit.Key%>se" removecatsection="<%=sit.Key %>" onclick="RemoveAppSection(this)">
                                            <span class="remove-icon text-danger"><i class="fas fa-times"></i></span>
                                        </span>
                                     </h2>
                                </div>
                                <div id="collapseOne<%= k %>" class="collapse" aria-labelledby="headingOne<%= k %>" data-parent="#accordionExampl">                                  
                                    <div class="row">
                                        <div class="col-lg-12">                                                              
                                            <button type="button" class="btn btn-sm btn-flat btn-success float-end shadow p-2 mt-2 mr-4  appraiscategory" style="background:#48A23F;color:white;display:none;" data-toggle="modal" data-target="#staticBackdrop"  id="<%=sit.Key %>" section="<%=sit.Key %>" onclick="getAppraisalCategories(this)">
                                                Add Appraisal Category
                                            </button>
                                        </div>
                                       </div>       

                                   <%-- inner accordion--%>
                                     <div class='accordion' id="accordionExample<%= k %>" style="width:95%!important;margin:0 auto!important;margin-top:0.5em!important;">                                                            
                                        
                                         <% if (sit.Value.Count != 0)
                                             {  %>
                                         <% foreach (var site in sit.Value)
                                             { %>
                                             <% v = v + 1;%>
                                                 <div class='card'>
                                                     <div class='card-header' id='headingOne<%= v %>' style="background:#26284f;">
                                                         <h2 class='mb-0 d-flex justify-content-between align-items-center'>
                                                             <button class='btn btn-link btn-block text-left' style="color:white!important;"  type='button' data-toggle='collapse' data-target='#collapseOne<%= v %>' aria-expanded='true' aria-controls='collapseOne' appraisalcode="<%=site.Key %>" id="one<%= v %>" onclick="dennis(this)">
                                                                 <%= site.Value%>
                                                             </button>
                                                             <span class="minus-icon" data-toggle="tooltip" style="display:none;" data-placement="right" title="Remove this category" id="<%=site.Key%>RC" removecategorycode="<%=site.Key %>" onclick="DeleteAppraisalCategory(this)">
                                                                 <i class="fas fa-trash red-trash text-danger"></i>
                                                             </span>
                                                         </h2>
                                                     </div>

                                                     <div id='collapseOne<%= v %>' class='collapse' aria-labelledby='headingOne<%= v %>' data-parent="#accordionExample<%= k %>">
                                                         <div class='card-body'>
                                                             <div id='apprline<%= v %>' class='container-fluid'>
                                                                 <div class="shadow float-left p-1 mb-1 bg-white submitMsg"></div>
                                                                 <div style="display:none;" class="editbtn">
                                                                     <div style='display: flex;' class='float-end mb-1' id="custom<%= v %>">                                                
                                                                         <button type="button" id="delete<%= v %>" class="btn btn-sm btn-flat btn-danger pull-left" appraisalcode="<%=site.Key %>"  onclick="DeleteAppraisalLine(this)">Delete</button>
                                                                         <button type="button" id="newrow<%= v %>" class='btn btn-sm btn-flat btn-success pull-left' addappraisalcode="<%=site.Key %>" onclick="addRow(this); return false;">Add</button>
                                                                     </div>
                                                                 </div>
                                                                 <table class='table table-bordered table-responsive' id='<%= site.Key%>c' catdesc="<%=site.Value%>">
                                                                     <thead>
                                                                         <tr>
                                                                             <th style='min-width: 2rem'></th>
                                                                             <th style='min-width: 2rem'>No.</th>
                                                                             <th style='min-width: 22rem'>Performance Objective</th>
                                                                             <th style='min-width: 13rem;display:none;' class="newclosed">KPI Code</th>
                                                                             <th style='min-width: 22rem'>KPI/Measure Description</th>
                                                                             <th style='min-width: 22rem;'>Source of Evidence</th>
                                                                             <th style='min-width: 10rem;display:none;' class="newclosed">Expected</th>
                                                                             <th style='min-width: 10rem;display:none;' class="newclosed">Weight</th>
                                                                             <th style='min-width: 22rem;display:none;' class="employeecomment">Employee Success Area</th>
                                                                             <th style='min-width: 22rem;display:none;' class="employeecomment">Employee Improvement Area</th>

                                                                             <th style='min-width: 22rem;display:none;' class="employeerating">Employee Score Code</th>
                                                                             <th style='min-width: 10rem;display:none;' class="employeerating">Employee Rating</th>
                                                                             <th style='min-width: 22rem;display:none;' class="employeerating">Employee Rating Description</th>
                                                                             <th style='min-width: 22rem;display:none;' class="employeerating">Appraisee Comment</th>
                                  
                                 
                                                                             <th style='min-width: 22rem;display:none;' class="supervisorrating">Supervisor Score Code</th>
                                                                             <th style='min-width: 10rem;display:none;' class="supervisorrating">Supervisor Rating</th>
                                                                             <th style='min-width: 22rem;display:none;' class="supervisorrating">Supervisor Rating Description</th>                                                         
                                                                             <th style='min-width: 22rem;display:none;' class="supervisorrating">Agreed Score Code</th>
                                                                             <th style='min-width: 10rem;display:none;' class="supervisorrating">Agreed Rating</th>
                                                                             <th style='min-width: 22rem;display:none;' class="supervisorrating">Agreed Rating Description</th>
                                                                             <th style='min-width: 22rem;display:none;' class="supervisorrating">Appraiser Comment</th>


                                                                            <th style='min-width: 22rem;display:none;' class="moderationcomment">Moderation Comment</th>

                                                                      </tr>
                                                                     </thead>
                                                                     <tbody id="<%=site.Key %>">
                                                                         <tr class="form-status-holder"></tr>
                                                                     </tbody>
                                                                     <tfoot>
                                                                         <tr>
                                                                             <td ></td>
                                                                             <td ></td>
                                                                             <td style='display:none;' class="newclosed"></td>
                                                                             <td style='display:none;' class="newclosed"></td>
                                                                             <td style='display:none;' class="newclosed"></td>                                                                            
                                                                             <th scope="row" colspan="2" style="background: #26284f; color: white;">Total Weight for  <%= site.Value%>  </th>
                                                                             <td id="TotalWeight<%=site.Key %>"  style="background: #26284f; color: white;">0 %</td>
                                                                         </tr>
                                                                     </tfoot>
                                                                 </table>
                                                             </div>
                                                         </div>
                                                     </div>                                
                                                 </div>
                                            <% } %>
                                            <% }%>
                                         <%else
                                             {
                                                 if (v == 0)
                                                 {
                                                     buttons.Visible = false;
                                                 }
                                                 %>
                                         <div class="alert alert-info" style="background-color: #26284f; color: white" role="alert">
                                             Click add to add your appraisal categories!
                                         </div>
                                         <%  } %>                                         
                                     </div> 
                                </div>

                            </div>
                            <% } %>
                            <% }%>
                            <%else
                                {
                                    buttons.Visible = false; %>
                                <div class="alert alert-info" style="background-color: #26284f; color: white" role="alert">
                                    Click add to add your appraisal sections!
                                </div>
                            <%  } %>                          
                         </div>
                    <p id="noappraisal" nomberofappraisalcategory="<%= v %>"></p>  
                    <div id="ratings" getratings="<%=Session["Ratings"] %>"></div>
                        <div class="box-footer clearfix" runat="server" id="buttons" visible="false">
                            <asp:LinkButton ID="Moveback" runat="server" class="btn btn-sm btn-flat pull-left" Style="background-color: #48A23F; color: white" OnClick="Moveback_Click1"><i class="fas fa-chevron-left"></i>&nbsp Back</asp:LinkButton>
                            <asp:LinkButton ID="submitAppraisal" style="display:none;"  CssClass="btn btn-sm btn-flat btn-success pull-left" runat="server" OnClick="submitAppraisal_Click"><i class="fas fa-paper-plane"></i>&nbsp Submit</asp:LinkButton>
                            <asp:LinkButton ID="Recall" style="display:none; background:#26284f;color:white;" class="btn btn-sm btn-flat pull-left" runat="server" OnClick="Recall_Click"><i class="fas fa-history"></i>&nbsp Recall</asp:LinkButton> 
                            <asp:LinkButton ID="Decline" style="display:none;"  class="btn btn-sm btn-flat btn-danger pull-left" runat="server" OnClick="Decline_Click"><i class="fas fa-times-circle"></i>&nbsp Decline</asp:LinkButton>
                            <asp:LinkButton ID="Complete" style="display:none;background:#26284f;color:white;"  class="btn btn-sm btn-flat btn-success pull-left" runat="server" OnClick="Complete_Click"><i class="fas fa-check-circle"></i>&nbsp Complete</asp:LinkButton> 
                        </div>
                    </div>
                    <!-- /.box -->
                </div>           

        </div>
        <!-- /.content -->
    </div></div></div>
    <!-- /.content-wrapper -->
    <script language="javascript" type="text/javascript">
        window.onload = function () {
            let TotalAppraisalCategories = document.getElementById("noappraisal").getAttribute("nomberofappraisalcategory");
            let searchParams = new URLSearchParams(window.location.search);
            let Status = searchParams.get('status');
            let AppraisalempNo = searchParams.get('emp');
            let EmpNo = $('#EmployeeNo').attr('empno');
            let AppraisalType = searchParams.get("typ");
            if (Status == null) {
                Status = "New";
            }
            if (Status == "New") {
                $('.editbtn').show();
                $('#EmployeeNo').show();
                $('.minus-icon').show();
                $('.appraiscategory').show();
            }

            //control the visibility of the controls
            if (Status === "Mid Year submitted" || Status === "End Year Submitted") {
                if (EmpNo !== AppraisalempNo) {
                    $('#<%=submitAppraisal.ClientID %>').text("Accept");
                    $('#<%=submitAppraisal.ClientID %>').show();
                    $('#<%=Decline.ClientID %>').show();
                }

            } else if (Status === "End Year Closed") {
                if (EmpNo !== AppraisalempNo) {
                    $('#<%=submitAppraisal.ClientID %>').text("Moderation");
                    $('#<%=submitAppraisal.ClientID %>').show();
                    $('#<%=Complete.ClientID %>').show();
                }
            } else if (Status === "Moderation") {
                if (EmpNo !== AppraisalempNo) {
                    $('#<%=Complete.ClientID %>').show();
                }
            }
            else if (Status === "Submitted") {
                $('#<%= Recall.ClientID %>').show();
                $('#<%= submitAppraisal.ClientID %>').show();
            }
            else if (Status === "New" || Status === "Mid Year Review" || Status === "End Year Review") {
                $('#<%= submitAppraisal.ClientID %>').show();
            } else if (Status === "Mid Year Closed") {
                $('#<%= submitAppraisal.ClientID %>').show();
                $('#<%=submitAppraisal.ClientID %>').text("Start End Year Review");
            }

            //control the visibility of form controls(Table header)   
            if (Status === "New") {                
                $('.newclosed').show();
            }
            if (Status !== "New" && Status !== "Submitted") {
                $('.employeecomment').show();
                if (Status !== "Mid Year Review") {
                    $('#supervisorcommentarea').show();
                    if (Status !== "Mid Year Closed" && Status !== "Mid Year submitted") {
                        $('.employeerating').show();
                        if (Status !== "End Year Review") {
                            if (Status === "End Year Submitted") {
                                if (AppraisalempNo !== EmpNo) {
                                    $('.supervisorrating').show();
                                }
                            } else {
                                $('.supervisorrating').show();
                            }                            
                            if (Status === "Moderation") {
                                if (AppraisalempNo !== EmpNo) {
                                    $('.moderationcomment').show();
                                }
                            } else if (Status === "Completed" && AppraisalType ==="Annual") {
                                $('.moderationcomment').show();
                            }
                        }
                    }
                }
            }
            if (Status === "Completed") {
                if (AppraisalType === "Probationary") {
                    $('.supervisorrating').hide();
                    $('.employeerating').hide();
                }
            }

            if (Status == "Mid Year submitted") {
                if (AppraisalempNo === EmpNo) {
                    $('.supcomments').prop('disabled', true);
                }
            } else {
                $('.supcomments').prop('disabled', true);
            }


            //control visibility of recommendation field         

            if (Status === "End Year Submitted") {
                if (AppraisalempNo !== EmpNo) {
                    $('#suprecom').show();
                } 
               
            } else if (Status === "End Year Closed" || Status === "Moderation" || Status === "Completed") {
                $('#suprecom').show();
            }
            switch (Status) {
                case "Submitted":
                    $('#<%= submitAppraisal.ClientID %>').text("Start Mid Year Review");
                    break;
                case "Mid Year Closed":
                    $('#<%= submitAppraisal.ClientID %>').text("Start End Year Review");
                    break;
                case "Moderation":
                    if (AppraisalempNo === EmpNo) {
                        $('#<%=recommend.ClientID %>').prop('disabled', true);
                    }
                    break;
                case "End Year Closed":
                    $('#<%=recommend.ClientID %>').prop('disabled', true);
                    break;
                case "Completed":                    
                    $('#<%=recommend.ClientID %>').prop('disabled', true);
                    break;
                case "End Year Submitted":
                    if (AppraisalempNo !== EmpNo) {
                        $('#<%=recommend.ClientID %>').prop('disabled', false);
                        $('#<%=supervisoreremarks.ClientID %>').prop('disabled', false);                        
                    }
            };

        };
        /*Add appraisal categories per section */
        function getAppraisalCategories(valid) {

            let Section = document.getElementById(valid.id).getAttribute("section");
            let value = {
                Section: Section,
            };
            let valJson = JSON.stringify(value);
            $.ajax({
                url: 'AppraisalApplication.aspx/SectionCategories',
                method: 'post',
                contentType: 'application/json',
                data: valJson,
                dataType: 'json',
                success: function (response) {
                    $("#sectionappcat").html(response.d);
                },
                error: (error) => {
                    console.log(JSON.stringify(error));
                }
            });
        }

        //Remove appraisal category section
        function RemoveAppSection(vald) {
            let k = confirm("Are you sure you want to delete the selected item?");
            if (k) {
                let SecCode = document.getElementById(vald.id).getAttribute("removecatsection");
                let EmpNo = document.getElementById("EmployeeNo").getAttribute("empNo");
                let value = {
                    SecCode: SecCode,
                    EmpNo: EmpNo
                };
                let valJson = JSON.stringify(value);
                $.ajax({
                    url: 'AppraisalApplication.aspx/RemoveAppSection',
                    method: 'post',
                    contentType: 'application/json',
                    data: valJson,
                    dataType: 'json',
                    success: function (data) {
                        let rvalue = JSON.parse(data.d);
                        if (rvalue["Msg"] == "Yes") {
                            location.reload();
                        } else {
                            alert("Delete all the appraisal categories under this section to continue");
                        }
                    },
                    error: (error) => {
                        console.log(JSON.stringify(error));
                    }
                });

            } else {
                return false;
            }

        }
        /*ADD APPRAISAL CATEGORY SECTION*/
        function AddAppraisalCategorySections() {
            let checkedValue = {};
            checkedValue["EmpNo"] = document.getElementById("EmployeeNo").getAttribute("empNo");
            let inputElements = document.getElementsByClassName('selectedApraisalSec');
            for (let i = 0; inputElements[i]; ++i) {
                if (inputElements[i].checked) {
                    let kename = inputElements[i].value;
                    let valu = document.getElementById(inputElements[i].value + "us").getAttribute("appraisaldescipt");
                    checkedValue[kename] = valu;
                }
            }
            let value = {
                SelectedAppraisalCat: checkedValue
            };
            let valJson = JSON.stringify(value);
            $.ajax({
                url: 'AppraisalApplication.aspx/AddAppraisalCategorySections',
                method: 'post',
                contentType: 'application/json',
                data: valJson,
                dataType: 'json',
                success: function (response) {
                    let dat = JSON.parse(response.d);
                    if (dat == "Yes") {
                        location.reload();
                    } else {
                        // Handle failure
                        alert("Failed to add appraisal categories,Try again");
                    }
                },
                error: (error) => {
                    console.log(JSON.stringify(error));
                }
            });
        }
        function DeleteAppraisalCategory(valu) {
            let k = confirm("Are you sure you want to delete the selected item?");
            if (k) {
                let CategoryKey = document.getElementById(valu.id).getAttribute("RemoveCategorycode");
                let EmpNo = document.getElementById("EmployeeNo").getAttribute("empNo");
                let searchParams = new URLSearchParams(window.location.search);
                let AppraisalNo = searchParams.get('An');
                if (AppraisalNo == null) {
                    AppraisalNo = document.getElementById("EmployeeNo").getAttribute("AppNo");
                };
                let value = {
                    AppNo: AppraisalNo,
                    EmpNo: EmpNo,
                    CategoryKey: CategoryKey
                };
                let valJson = JSON.stringify(value);
                $.ajax({
                    url: 'AppraisalApplication.aspx/RemoveAppraisalCategory',
                    method: 'post',
                    contentType: 'application/json',
                    data: valJson,
                    dataType: 'json',
                    success: function (data) {
                        let rvalue = JSON.parse(data.d);
                        if (rvalue["Msg"] == "Yes") {
                            location.reload();
                        } else {
                            alert("Delete all the appraisal lines under this category to continue");
                        }
                    },
                    error: (error) => {
                        console.log(JSON.stringify(error));
                    }
                });

            } else {
                return false;
            }
        };
        //delete appraisal line
        function DeleteAppraisalLine(val) {
            let idname = document.getElementById(val.id).getAttribute("appraisalcode");
            let searchParams = new URLSearchParams(window.location.search);
            let AppraisalNo = searchParams.get('An');
            if (AppraisalNo == null) {
                AppraisalNo = document.getElementById("EmployeeNo").getAttribute("AppNo");
            };
            let checkedValue = {};
            let atLeastOneChecked = false;
            let inputElements = document.getElementsByClassName('deleteLine');
            for (let i = 0; inputElements[i]; ++i) {
                if (inputElements[i].checked) {
                    atLeastOneChecked = true;
                    let kename = inputElements[i].value;
                    let valu = inputElements[i].value;
                    if (kename != "on") {
                        checkedValue[kename] = valu;
                    }
                }
            }
            if (atLeastOneChecked) {
                let userconfirm = confirm("Are you sure you need to delete the selected record");
                if (Object.keys(checkedValue).length === 0) {
                    location.reload();
                    return;
                }
                checkedValue["APNo"] = AppraisalNo;
                checkedValue["AppCategory"] = idname;
                if (userconfirm) {
                    let value = {
                        SelectedAppraisalLine: checkedValue
                    };
                    let valJson = JSON.stringify(value);
                    $.ajax({
                        url: 'AppraisalApplication.aspx/DeleteAppraisalLine',
                        method: 'post',
                        contentType: 'application/json',
                        data: valJson,
                        dataType: 'json',
                        success: function (data) {
                            let dat = JSON.parse(data.d);
                            let AppraisalCategory = idname;
                            let elem = document.getElementById(AppraisalCategory).replaceChildren();
                            LoadAppraisalLine(AppraisalCategory, dat, val);
                            if (data.d != "") {
                                showAlert('Item deleted successfully!');
                            } else {
                                showAlert('Error deleting item!');
                            }
                        },
                        error: (error) => {
                            console.log(JSON.stringify(error));
                        }
                    });
                }
            } else {
                alert("Kindly select  the record to delete");
            }
        }
        function dennis(val) {
            const cont = document.getElementById("noappraisal");
            const countcategory = cont.getAttribute("nomberofappraisalcategory");
            let searchParams = new URLSearchParams(window.location.search);
            let AppraisalNo = searchParams.get('An');
            if (AppraisalNo == null) {
                AppraisalNo = document.getElementById("EmployeeNo").getAttribute("AppNo");
            };
            document.getElementById(val.id).removeAttribute('onclick');
            for (let i = 1; i <= countcategory; i++) {
                let idname = "one" + i;
                if (idname == val.id) {
                    let AppraisalCategory = document.getElementById(idname).getAttribute("appraisalcode");
                    let value = {
                        AppCategory: AppraisalCategory,
                        AppraisalNo: AppraisalNo
                    };
                    let valJson = JSON.stringify(value);
                    $.ajax({
                        url: 'AppraisalApplication.aspx/LoadAppraisalLines',
                        method: 'post',
                        contentType: 'application/json',
                        data: valJson,
                        dataType: 'json',
                        beforeSend: function (xhr) {
                            // Let them know we are loading
                            $('.form-status-holder').html('Loading...');
                        },
                        success: function (data) {
                            //load lines
                            dat = JSON.parse(data.d);
                            let AppraisalCategory = document.getElementById(val.id).getAttribute("appraisalcode");
                            let elem = document.getElementById(AppraisalCategory).replaceChildren();
                            LoadAppraisalLine(AppraisalCategory, dat, val);
                            $('.form-status-holder').html("");
                        },
                        error: (error) => {
                            console.log(JSON.stringify(error));
                        }
                    });
                    break;
                }
            }
        }
        function addRow(event) {
            let idname = document.getElementById(event.id).getAttribute("addappraisalcode");
            let element = document.getElementById(idname);
            let x = document.getElementById(idname).rows.length + 1;

            //createForm Controls
            CreateFormControls(element, x, idname);

            document.getElementById(idname + x).onchange = function () { PopulateWeight(idname, x) };
            //call autosave
            document.getElementById(idname + x).focusout = function () { AutoSave(idname, x) };
            document.getElementById("Objective" + idname + x).focusout = function () { AutoSave(idname, x) };
            document.getElementById("Measure" + idname + x).focusout = function () { AutoSave(idname, x) };
            document.getElementById("SourceOfEvidence" + idname + x).focusout = function () { AutoSave(idname, x) };
            document.getElementById("Weight" + idname + x).oninput = function () { CalculateTotalWeight(idname) };
            document.getElementById("Weight" + idname + x).focusout = function () { AutoSave(idname, x) };
        }

        //Auto save function
        function AutoSave(idname, x) { 
            let searchParams = new URLSearchParams(window.location.search);
            let Status = searchParams.get('status');       
            if (Status === null) {
                Status = "New";
            }
            let EmpImprove = "";
            let EmpSuccess = " ";
            let EmployeeSCode = " ";
            let AppraiseeCom = " ";
            let EmployeRScore = "0";
            let EmploRDescr = "";
            let moderationcomm = "";
            let supscorecode = "", supscore = "0", supscoredescrip = "", agrdscorecode = "", agrdscore = "0", agrdscoredescrip = "", appraisercomm = "";
            let weigt = "",KpiCode="";
            if (Status === "New") {
                weigt = document.getElementById("Weight" + idname + x).value;
                KpiCode = document.getElementById(idname + x).value;
            }           
            let Objectiv = document.getElementById("Objective" + idname + x).value;
            let Measur = document.getElementById("Measure" + idname + x).value;
            let SourceOfevidence = document.getElementById("SourceOfEvidence" + idname + x).value;
            let LN = document.getElementById("LineNo" + idname + x).value;
            let Appraisalcategory = idname;
           
            let AppraisalType = searchParams.get("typ");
            let ApprNo = searchParams.get('An');           
            let AppraisalempN = searchParams.get('emp');
            let EmpN = $('#EmployeeNo').attr('empno');
           
            /*, , "Mid Year Review", "Mid Year submitted", "Mid Year Closed", "End Year Review", "End Year Submitted", "End Year Closed", Moderation, Completed;*/
            if (Status !== "New" && Status !== "Submitted") {
                EmpImprove = document.getElementById("EmpImprovementArea" + idname + x).value;
                EmpSuccess = document.getElementById("EmpSuccessArea" + idname + x).value;
                if (Status !== "Mid Year Review" && Status !== "Mid Year submitted" && Status !== "Mid Year Closed" && AppraisalType !== "Probationary") {
                    EmployeeSCode = document.getElementById("empscorecode" + idname + x).value;
                    AppraiseeCom = document.getElementById("Appraiseecomment" + idname + x).value;
                    EmployeRScore = document.getElementById("Empscore" + idname + x).value;
                    EmploRDescr = document.getElementById("ScoreDescrip" + idname + x).value;
                    if (Status !== "End Year Review") {
                        supscorecode = $("#Supscorecode" + idname + x).val();
                        supscore = $("#Supscore" + idname + x).val();
                        supscoredescrip = $("#SupScoreDescrip" + idname + x).val();
                        agrdscorecode = $("#AgrredRatinCode" + idname + x).val();
                        agrdscore = $("#AgreedScore" + idname + x).val();
                        agrdscoredescrip = $("#AgreedDescrip" + idname + x).val();
                        appraisercomm = $("#AppraiserComm" + idname + x).val();

                        if ((Status === "Moderation" && EmpN !== AppraisalempN) || Status === "Completed") {
                            moderationcomm = $("#ModerationComm" + idname + x).val();                          

                         }
                    }
                };
            };
            if (ApprNo == null) {
                ApprNo = document.getElementById("EmployeeNo").getAttribute("AppNo");
            };
            if (weigt ==="") {
                weigt = 0;
            }
            var value = {
                LineNo: LN,
                AppraisalNo: ApprNo,
                KPICode: KpiCode,
                Objective: Objectiv,
                Measure: Measur,
                SourceofEvidence: SourceOfevidence,
                AppraisalCategory: Appraisalcategory,
                EmpImprove: EmpImprove,
                EmpSuccess: EmpSuccess,
                EmployeeSCode: EmployeeSCode,
                EmployeeScore: EmployeRScore,
                EmployeeRDec: EmploRDescr,
                Wegh: weigt,
                AppraiseeCom: AppraiseeCom,
                supscorecode: supscorecode,
                supscore: supscore,
                supscoredescrip: supscoredescrip,
                agrdscorecode: agrdscorecode,
                agrdscore: agrdscore,
                agrdscoredescrip: agrdscoredescrip,
                appraisercomm: appraisercomm,
                moderationcomm: moderationcomm
            };
            let jsondata = JSON.stringify(value);
            $.ajax({
                url: 'AppraisalApplication.aspx/AutoSave',
                method: 'post',
                contentType: 'application/json',
                data: jsondata,
                dataType: 'json',
                beforeSend: function (xhr) {
                    // Let them know we are saving
                    $('.submitMsg').html('Saving...');
                },
                success: function (data) {
                    let dat = JSON.parse(data.d);
                    if (dat != "") {
                        if (dat == "Yes") {
                            $('.submitMsg').html("✅ Saved");
                        } else if (dat == "No") {
                            $('.submitMsg').html("Not Saved");
                        } else {
                            document.getElementById("LineNo" + idname + x).value = dat;
                            document.getElementById("del" + idname + x).value = dat;
                            $('.submitMsg').html("✅ Saved");
                        }
                    } else {
                        $('.submitMsg').html("Not saved");
                    }
                },
                error: (error) => {
                    console.log(JSON.stringify(error));
                }
            });
        }
        //load weight
        function PopulateWeight(idname, x) {
            let codeval = document.getElementById(idname + x).value;
            let value = {
                AppCategory: idname,
                Code: codeval
            };
            let valJson = JSON.stringify(value);
            $.ajax({
                url: 'AppraisalApplication.aspx/LoadWeight',
                method: 'post',
                contentType: 'application/json',
                data: valJson,
                dataType: 'json',
                success: function (data) {
                    let dat = JSON.parse(data.d);
                    let initialWeight = parseInt(document.getElementById("Weight" + idname + x).value);
                    document.getElementById("Weight" + idname + x).value = dat[0].Weight;
                    document.getElementById("Objective" + idname+x).value = dat[0].Description;
                    AutoSave(idname, x);
                    if (initialWeight == 0) {
                        document.getElementById("TotalWeight" + idname).innerHTML = (parseInt(document.getElementById("TotalWeight" + idname).innerHTML) + dat[0].Weight).toFixed(1) + " %"
                    } else {
                        document.getElementById("TotalWeight" + idname).innerHTML = ((parseInt(document.getElementById("TotalWeight" + idname).innerHTML) - initialWeight) + dat[0].Weight).toFixed(1) + "%"
                    }
                },
                error: (error) => {
                    console.log(JSON.stringify(error));
                }
            });
        }
        //calculate waight
        function CalculateTotalWeight(idname) {
            let rowCount = $('#' + idname).find('tr').length;
            let Totalweight = 0;
            let name = document.getElementById(idname + "c").getAttribute("catdesc");
            const substringToCheck = "beh";
            let id = '';
            for (let i = 1; i <= rowCount; i++) {
                if (!$.isNumeric($('#Weight' + idname + i).val()) && $('#Weight' + idname + i).val() != "") {
                    alert("This field accepts only numeric values");
                    $('#Weight' + idname + i).val("0");
                }
                Totalweight = Totalweight + parseInt($('#Weight' + idname + i).val(), 10);
                id = i;
            }
            $('#TotalWeight' + idname).text(Totalweight.toFixed(1) + " %");        

        }

        //employee rating and description
        function PopulateEmployeeRating(idname, x) {
            let searchParams = new URLSearchParams(window.location.search);
            let Status = searchParams.get('status');
            let codeval
            if (Status === "End Year Review") {
                codeval = document.getElementById("empscorecode" + idname + x).value;
            } else {
                codeval = document.getElementById("Supscorecode" + idname + x).value;
            }
            if (codeval === null) {
                return;
            }
            let value = {
                Code: codeval
            };
            let valJson = JSON.stringify(value);
            $.ajax({
                url: 'AppraisalApplication.aspx/LoadEmployeeRatingD',
                method: 'post',
                contentType: 'application/json',
                data: valJson,
                dataType: 'json',
                success: function (data) {
                    let dat = JSON.parse(data.d);
                    switch (Status) {
                        case "End Year Review":
                            document.getElementById("Empscore" + idname + x).value = dat[0].Score;
                            document.getElementById("ScoreDescrip" + idname + x).value = dat[0].Description;
                            AutoSave(idname, x);
                            break;
                        case "End Year Submitted":
                            //supervisore score code                            
                            document.getElementById("Supscore" + idname + x).value = dat[0].Score;
                            document.getElementById("SupScoreDescrip" + idname + x).value = dat[0].Description;
                            AutoSave(idname, x);
                    };
                },
                error: (error) => {
                    console.log(JSON.stringify(error));
                }
            });
        }
        function PopulateAgreedRating(idname, x) {
            let searchParams = new URLSearchParams(window.location.search);
            let Status = searchParams.get('status');
            let codeval = document.getElementById("AgrredRatinCode" + idname + x).value;
            let value = {
                Code: codeval
            };
            let valJson = JSON.stringify(value);
            $.ajax({
                url: 'AppraisalApplication.aspx/LoadEmployeeRatingD',
                method: 'post',
                contentType: 'application/json',
                data: valJson,
                dataType: 'json',
                success: function (data) {
                    let dat = JSON.parse(data.d);
                    document.getElementById("AgreedScore" + idname + x).value = dat[0].Score;
                    document.getElementById("AgreedDescrip" + idname + x).value = dat[0].Description;
                    AutoSave(idname, x);
                },
                error: (error) => {
                    console.log(JSON.stringify(error));
                }
            });
        }
        function LoadAppraisalLine(idname, AppraisalData, val) {
            //populate the dropdown            
            let value = {
                AppCategory: idname
            };
            let valJson = JSON.stringify(value);
            $.ajax({
                url: 'AppraisalApplication.aspx/LoadKPICodes',
                method: 'post',
                contentType: 'application/json',
                data: valJson,
                dataType: 'json',
                beforeSend: function (xhr) {
                    // Let them know we are loading
                    $('.form-status-holder').html('Loading...');
                },
                success: function (data) {
                    let dat = JSON.parse(data.d);
                    LoadAppraisalLines(AppraisalData, val, dat);

                    $('.form-status-holder').html("");
                    return true;
                },
                error: (error) => {
                    console.log(JSON.stringify(error));
                }
            });
        }
        function PopulateKPICode(idname, x) {
            //populate the dropdown            
            let value = {
                AppCategory: idname
            };
            let valJson = JSON.stringify(value);
            $.ajax({
                url: 'AppraisalApplication.aspx/LoadKPICodes',
                method: 'post',
                contentType: 'application/json',
                data: valJson,
                dataType: 'json',
                beforeSend: function (xhr) {
                    // Let them know we are loading
                    $('.form-status-holder').html('Loading...');
                },
                success: function (data) {
                    let dat = JSON.parse(data.d);
                    let option = '<option value=" ">..Select KPI Code..</option>';
                    for (let i = 0; i < dat.length; i++) {
                        option += '<option value="' + dat[i].Code + '">' + dat[i].Description + "</option>";
                        let id = idname + x;
                        document.getElementById(id).innerHTML = option;
                    }
                    $('.form-status-holder').html("");
                    return true;
                },
                error: (error) => {
                    console.log(JSON.stringify(error));
                }
            });
        }
        function PopulateAppraisalRatings(idname, x) {
            let searchParams = new URLSearchParams(window.location.search);
            let Status = searchParams.get('status');
            value = {};
            let valJson = JSON.stringify(value);
            $.ajax({
                url: 'AppraisalApplication.aspx/LoadAppraisalRating',
                method: 'post',
                contentType: 'application/json',
                data: valJson,
                dataType: 'json',
                success: function (data) {
                    let dat = JSON.parse(data.d);

                    if (Status === "End Year Review" || Status === "End Year Submitted" || Status === "End Year Closed" || Status === "Moderation" || Status === "Completed") {
                        let option = '<option value=" ">..Select Rating Code..</option>';
                        for (let i = 0; i < dat.length; i++) {
                            option += '<option value="' + dat[i].Code + '">' + dat[i].Description + "</option>";
                            let id = "empscorecode" + idname + x;
                            document.getElementById(id).innerHTML = option;
                        }
                    }
                    if (Status === "End Year Submitted" || Status === "End Year Closed" || Status === "Moderation" || Status === "Completed") {
                        let opti = '<option value=" ">..Select Rating Code..</option>';
                        for (let i = 0; i < dat.length; i++) {
                            opti += '<option value="' + dat[i].Code + '">' + dat[i].Description + "</option>";
                            let id = "Supscorecode" + idname + x;
                            let ida = "AgrredRatinCode" + idname + x;
                            document.getElementById(ida).innerHTML = opti;
                            document.getElementById(id).innerHTML = opti;
                        }
                    }
                    $('.form-status-holder').html("");
                    return true;
                },
                error: (error) => {
                    console.log(JSON.stringify(error));
                }
            });
        }
        function CreateFormControls(element, x, idname) {
            let fragment = document.createDocumentFragment();
            //create the controls dynamically
            const deleteRadio = document.createElement("input");
            deleteRadio.type = "checkbox";
            deleteRadio.name = "AppLNo";
            deleteRadio.id = "del" + idname + x;
            deleteRadio.classList = "deleteLine";

            const No = document.createElement("label");
            No.innerHTML = x;

            const Objective = document.createElement("textarea");
            Objective.cols = "50";
            Objective.id = "Objective" + idname + x;
            Objective.classList = "form-control";
            let LineNo = document.createElement("input");

            LineNo.type = "hidden";
            LineNo.id = "LineNo" + idname + x;
            LineNo.value = "0";

            const Measure = document.createElement("textarea");
            Measure.cols = "50";
            Measure.id = "Measure" + idname + x;
            Measure.classList = "form-control";


            const Kpi = document.createElement("select");
            Kpi.id = idname + x;
            Kpi.classList = "form-control"


            const SourceofEvidence = document.createElement("textarea");
            SourceofEvidence.cols = "50";
            SourceofEvidence.id = "SourceOfEvidence" + idname + x;
            SourceofEvidence.classList = "form-control"


            const Expected = document.createElement("input");
            Expected.type = "text";
            Expected.id = " Expected" + idname + x;
            Expected.classList = "form-control";
            Expected.disabled = true;
            Expected.value = document.getElementById("ExpectedValue").getAttribute("expectedscore");

            const Weight = document.createElement("input");
            Weight.type = "text";
            Weight.id = "Weight" + idname + x;
            Weight.classList = "form-control";
            Weight.value = 0;

            //PopulateWeight
            PopulateKPICode(idname, x);

            //attach the controls to the tbody
            const tr = document.createElement("tr");
            const td0 = document.createElement("td");
            const td1 = document.createElement("td");
            const td2 = document.createElement("td");
            const td3 = document.createElement("td");
            const td4 = document.createElement("td");
            const td5 = document.createElement("td");
            const td6 = document.createElement("td");
            const td7 = document.createElement("td");

            td0.appendChild(deleteRadio);
            td1.appendChild(No);
            td2.appendChild(Objective);
            td3.appendChild(Kpi);
            td4.appendChild(Measure);
            td5.appendChild(SourceofEvidence);
            td6.appendChild(Expected);
            td7.appendChild(Weight);

            tr.append(td0, td1, td2, td3, td4, td5, td6, td7);
            element.appendChild(tr);
            element.appendChild(LineNo);
        }


        //load data onpageload
        let ratings = [];
        if (ratings.length === 0) {
            $.ajax({
                url: 'AppraisalApplication.aspx/LoadAppraisalRating',
                method: 'post',
                contentType: 'application/json',
                dataType: 'json',
                success: function (data) {
                    let dat = JSON.parse(data.d);
                    ratings = dat.slice()
                },
                error: (error) => {
                    console.log(JSON.stringify(error));
                }
            });
        }

        function LoadAppraisalLines(dat, val, kpicodedata) {

            let TotalWeight = 0;
            let idname = document.getElementById(val.id).getAttribute("appraisalcode");
            let searchParams = new URLSearchParams(window.location.search);
            let Status = searchParams.get('status');
            let AppraisalType = searchParams.get("typ");
            let AppraisalempN = searchParams.get('emp');
            let EmpN = $('#EmployeeNo').attr('empno');
            if (Status == null) {
                Status = "New";
            }
            let element = document.getElementById(idname);
            for (let i = 0; i < dat.length; i++) {
                let x = document.getElementById(idname).rows.length + 1;

                //create the controls dynamically
                const deleteRadio = document.createElement("input");
                deleteRadio.type = "checkbox";
                deleteRadio.name = "AppLNo";
                deleteRadio.id = "del" + idname + x;
                deleteRadio.classList = "deleteLine";
                deleteRadio.value = dat[i].LineNo;

                const No = document.createElement("label");
                No.innerHTML = x;

                let Objective = document.createElement("textarea");
                Objective.cols = "50";
                Objective.id = "Objective" + idname + x;
                Objective.classList = "form-control new";
                Objective.value = dat[i].Objective;

                let LineNo = document.createElement("input");
                LineNo.type = "hidden";
                LineNo.id = "LineNo" + idname + x;
                LineNo.value = dat[i].LineNo;

                let Kpi = "";
                if (Status === "New") { 
                    Kpi = document.createElement("select");
                    Kpi.id = idname + x;
                    Kpi.classList = "form-control new";
                }

                let Measure = document.createElement("textarea");
                Measure.cols = "50";
                Measure.id = "Measure" + idname + x;
                Measure.classList = "form-control new";
                Measure.value = dat[i].Measure;

                let SourceofEvidence = document.createElement("textarea");
                SourceofEvidence.cols = "50";
                SourceofEvidence.id = "SourceOfEvidence" + idname + x;
                SourceofEvidence.classList = "form-control new";
                SourceofEvidence.value = dat[i].SourceOfEvidence;
                let Expected = "";
                let Weight = "";
                if (Status === "New") {                  
               
                    Expected = document.createElement("input");
                    Expected.type = "text";
                    Expected.id = " Expected" + idname + x;
                    Expected.classList = "form-control";
                    Expected.disabled = true;
                    Expected.value = document.getElementById("ExpectedValue").getAttribute("expectedscore");

                    Weight = document.createElement("input");
                    Weight.type = "text";
                    Weight.id = "Weight" + idname + x;
                    Weight.value = dat[i].Weight;
                    Weight.classList = "form-control new";
                }

                //Attach the elments to the tbody                               
                const tr = document.createElement("tr");
                const td0 = document.createElement("td");
                const td1 = document.createElement("td");
                const td2 = document.createElement("td");
                const td3 = document.createElement("td");
                const td4 = document.createElement("td");
                const td5 = document.createElement("td");
                const td6 = document.createElement("td");
                const td7 = document.createElement("td");

                td0.appendChild(deleteRadio);
                td1.appendChild(No);
                td2.appendChild(Objective);
                if (Status === "New") {
                    td3.appendChild(Kpi);
                    td6.appendChild(Expected);
                    td7.appendChild(Weight);
                }                
                td4.appendChild(Measure);
                td5.appendChild(SourceofEvidence);

               

                //Add employee comment controls
                /*, , "Mid Year Review", "Mid Year submitted", "Mid Year Closed", "End Year Review", "End Year Submitted", "End Year Closed", Moderation, Completed;*/
                let td8, td9, td10, td11, td12, td13, td14, td15, td16, td17, td18, td19, td20,td21;
                if (Status !== "New" && Status !== "Submitted") {
                    let EmpSuccessArea = document.createElement("textarea");
                    EmpSuccessArea.cols = "50";
                    EmpSuccessArea.id = "EmpSuccessArea" + idname + x;
                    EmpSuccessArea.classList = "form-control midyr";
                    EmpSuccessArea.value = dat[i].EmployeeSuccessArea;

                    let EmpImprovementArea = document.createElement("textarea");
                    EmpImprovementArea.cols = "50";
                    EmpImprovementArea.id = "EmpImprovementArea" + idname + x;
                    EmpImprovementArea.classList = "form-control midyr";
                    EmpImprovementArea.value = dat[i].EmployeeImprovementArea;

                    td8 = document.createElement("td");
                    td9 = document.createElement("td");

                    td8.appendChild(EmpSuccessArea);
                    td9.appendChild(EmpImprovementArea);

                    let empscorecode, Empscore, EmpscoreDescrip, Appraiseecom;
                    let supscorecode, Supscore, SupScoreDescrip, AppraiserComm, AgrredRatinCode, AgreedScore, AgreedDescrip, ModerationComm;

                    if (Status !== "Mid Year Review" && Status !== "Mid Year submitted" && Status !== "Mid Year Closed" && AppraisalType !== "Probationary") {

                        empscorecode = document.createElement("select");
                        empscorecode.id = "empscorecode" + idname + x;
                        empscorecode.classList = "form-control disableemprating";
                        empscorecode.value = dat[i].EmployeeScoreCode;

                        Empscore = document.createElement("input");
                        Empscore.type = "text";
                        Empscore.id = "Empscore" + idname + x;
                        Empscore.value = dat[i].EmployeeRating;
                        Empscore.disabled = true;
                        Empscore.classList = "form-control";

                        EmpscoreDescrip = document.createElement("input");
                        EmpscoreDescrip.type = "text";
                        EmpscoreDescrip.id = "ScoreDescrip" + idname + x;
                        EmpscoreDescrip.value = dat[i].EmployeeRatingDescription;
                        EmpscoreDescrip.disabled = true;
                        EmpscoreDescrip.classList = "form-control";

                        Appraiseecom = document.createElement("textarea");
                        Appraiseecom.cols = "50";
                        Appraiseecom.id = "Appraiseecomment" + idname + x;
                        Appraiseecom.classList = "form-control disableemprating";
                        Appraiseecom.value = dat[i].AppraiseeComment;

                        td10 = document.createElement("td");
                        td11 = document.createElement("td");
                        td12 = document.createElement("td");
                        td13 = document.createElement("td");

                        td10.appendChild(empscorecode);
                        td11.appendChild(Empscore);
                        td12.appendChild(EmpscoreDescrip);
                        td13.appendChild(Appraiseecom);

                        if (Status !== "End Year Review") {

                            if ((Status === "End Year Submitted" && EmpN !== AppraisalempN) || Status !== "End Year Submitted") {
                                supscorecode = document.createElement("select");
                                supscorecode.id = "Supscorecode" + idname + x;
                                supscorecode.classList = "form-control endsubmitted supervisor";

                                Supscore = document.createElement("input");
                                Supscore.type = "text";
                                Supscore.id = "Supscore" + idname + x;
                                Supscore.value = dat[i].SupervisorRating;
                                Supscore.disabled = true;
                                Supscore.classList = "form-control endsubmitted";


                                SupScoreDescrip = document.createElement("input");
                                SupScoreDescrip.type = "text";
                                SupScoreDescrip.id = "SupScoreDescrip" + idname + x;
                                SupScoreDescrip.value = dat[i].SupervisorRatingDescription;
                                SupScoreDescrip.disabled = true;
                                SupScoreDescrip.classList = "form-control endsubmitted";


                                AgrredRatinCode = document.createElement("select");
                                AgrredRatinCode.id = "AgrredRatinCode" + idname + x;
                                AgrredRatinCode.classList = "form-control endsubmitted moderation";

                                AgreedScore = document.createElement("input");
                                AgreedScore.type = "text";
                                AgreedScore.id = "AgreedScore" + idname + x;
                                AgreedScore.value = dat[i].AgreedRating;
                                AgreedScore.disabled = true;
                                AgreedScore.classList = "form-control endsubmitted";

                                AgreedDescrip = document.createElement("input");
                                AgreedDescrip.type = "text";
                                AgreedDescrip.id = "AgreedDescrip" + idname + x;
                                AgreedDescrip.value = dat[i].AgreedRatingDescription;
                                AgreedDescrip.disabled = true;
                                AgreedDescrip.classList = "form-control endsubmitted supervisorrating";

                                AppraiserComm = document.createElement("textarea");
                                AppraiserComm.cols = "50";
                                AppraiserComm.id = "AppraiserComm" + idname + x;
                                AppraiserComm.classList = "form-control endsubmitted supervisor";
                                AppraiserComm.value = dat[i].AppraiserComment;

                                td14 = document.createElement("td");
                                td15 = document.createElement("td");
                                td16 = document.createElement("td");
                                td17 = document.createElement("td");

                                td18 = document.createElement("td");
                                td19 = document.createElement("td");
                                td20 = document.createElement("td");

                                td14.appendChild(supscorecode)
                                td15.appendChild(Supscore);
                                td16.appendChild(SupScoreDescrip);
                                td17.appendChild(AgrredRatinCode);
                                td18.appendChild(AgreedScore);
                                td19.appendChild(AgreedDescrip);
                                td20.appendChild(AppraiserComm);
                                if ((Status === "Moderation" && EmpN !== AppraisalempN) || Status === "Completed") {
                                    ModerationComm = document.createElement("textarea");
                                    ModerationComm.cols = "50";
                                    ModerationComm.id = "ModerationComm" + idname + x;
                                    ModerationComm.classList = "form-control moderationcom";
                                    ModerationComm.value = dat[i].ModerationComment;

                                    td21 = document.createElement("td");

                                    td21.appendChild(ModerationComm);
                                }
                            }

                        }
                    }
                    /*check whether the score code element has been created*/
                    if (ModerationComm){
                        tr.append(td0, td1, td2, td4, td5, td8, td9, td10, td11, td12, td13, td14, td15, td16, td17, td18, td19, td20, td21);
                        element.appendChild(tr);
                        element.appendChild(LineNo);

                        //attach the events
                        $('#' + idname).on('focusout', '.moderationcom', function () {
                            AutoSave(idname, x);
                        });

                        $('#' + idname).on('focusout', '.supervisor', function () {
                            AutoSave(idname, x);
                        });
                        AttachEmployeeRatings(ratings, x, idname);
                        setSelectedIndex(document.getElementById("empscorecode" + idname + x), dat[i].EmployeeScoreCode);

                        AttachAgreedRatings(ratings, x, idname);
                        setSelectedIndex(document.getElementById("AgrredRatinCode" + idname + x), dat[i].AgreedScoreCode);

                        AttachSupervisorRatings(ratings, x, idname);
                        setSelectedIndex(document.getElementById("Supscorecode" + idname + x), dat[i].SupervisorScoreCode);
                        document.getElementById("AgrredRatinCode" + idname + x).onchange = function () { PopulateAgreedRating(idname, x) };
                        document.getElementById("Supscorecode" + idname + x).onchange = function () { PopulateEmployeeRating(idname, x) };
                    }
                    else if (supscorecode) {
                        tr.append(td0, td1, td2,td4, td5,td8, td9, td10, td11, td12, td13, td14, td15, td16, td17, td18, td19, td20);
                        element.appendChild(tr);
                        element.appendChild(LineNo);
                        //attach the events
                        $('#' + idname).on('focusout', '.supervisor', function () {
                            AutoSave(idname, x);
                        });

                        AttachEmployeeRatings(ratings, x, idname);
                        setSelectedIndex(document.getElementById("empscorecode" + idname + x), dat[i].EmployeeScoreCode);

                        AttachAgreedRatings(ratings, x, idname);
                        setSelectedIndex(document.getElementById("AgrredRatinCode" + idname + x), dat[i].AgreedScoreCode);

                        AttachSupervisorRatings(ratings, x, idname);
                        setSelectedIndex(document.getElementById("Supscorecode" + idname + x), dat[i].SupervisorScoreCode);
                        document.getElementById("AgrredRatinCode" + idname + x).onchange = function () { PopulateAgreedRating(idname, x) };
                        document.getElementById("Supscorecode" + idname + x).onchange = function () { PopulateEmployeeRating(idname, x) };
                    } else if (empscorecode) {
                        tr.append(td0, td1, td2,td4, td5,td8, td9, td10, td11, td12, td13);
                        element.appendChild(tr);
                        element.appendChild(LineNo);
                        //attach the events dynamically
                        AttachEmployeeRatings(ratings, x, idname);
                        setSelectedIndex(document.getElementById("empscorecode" + idname + x), dat[i].EmployeeScoreCode);
                        document.getElementById("empscorecode" + idname + x).onchange = function () { PopulateEmployeeRating(idname, x); AutoSave(idname, x) };
                        document.getElementById("Appraiseecomment" + idname + x).focusout = function () { AutoSave(idname, x) };
                    } else {
                        tr.append(td0, td1, td2,td4, td5, td8, td9);
                        element.appendChild(tr);
                        element.appendChild(LineNo);
                        //call autosave
                        //document.getElementById(idname + x).onchange = function () { PopulateWeight(idname, x) };
                        //attach the events dynamically
                        document.getElementById("EmpSuccessArea" + idname + x).focusout = function () { AutoSave(idname, x) };
                        document.getElementById("EmpImprovementArea" + idname + x).focusout = function () { AutoSave(idname, x) };
                    }
                } else {
                    if (Status === "New") {
                        tr.append(td0, td1, td2, td3, td4, td5, td6, td7);
                    } else {
                        tr.append(td0, td1, td2, td4, td5);
                    }
                   
                    element.appendChild(tr);
                    element.appendChild(LineNo);
                    //call autosave
                    if (Status === "New") {
                        document.getElementById(idname + x).onchange = function () { PopulateWeight(idname, x) };
                        document.getElementById(idname + x).focusout = function () { AutoSave(idname, x) };
                        document.getElementById("Weight" + idname + x).oninput = function () { CalculateTotalWeight(idname) };
                        document.getElementById("Weight" + idname + x).focusout = function () { AutoSave(idname, x) };
                    }                    
                    document.getElementById("Objective" + idname + x).focusout = function () { AutoSave(idname, x) };
                    document.getElementById("Measure" + idname + x).focusout = function () { AutoSave(idname, x) };
                    document.getElementById("SourceOfEvidence" + idname + x).focusout = function () { AutoSave(idname, x) };
                    
                }
                if (Status === "New") {
                    AttachSaveKPICode(kpicodedata, x, idname);
                    setSelectedIndex(document.getElementById(idname + x), dat[i].KPICode);
                }

                //hide checkbox and disable weight               
                if (Status != "New") {
                    $("#del" + idname + x).hide();                   
                    
                }
                TotalWeight = TotalWeight + dat[i].Weight;
            }

            document.getElementById("TotalWeight" + idname).innerHTML = TotalWeight.toFixed(1) + " %";
            //make the controls uneditable
            if (Status != "New") {
                let selects = document.getElementsByClassName("new");
                for (let i = 0; i < selects.length; i++) {
                    selects[i].disabled = true;
                }
               
            }

            //make the form controls non editable
            if (Status !== "Mid Year Review") {
                $('.midyr').prop('disabled', true);
            }
            if (Status !== "End Year Review") {
                $('.disableemprating').prop('disabled', true);
            }
            if (Status !== "New") {
                $('.new').prop('disabled', true);               
                
            }
            if (Status !== "End Year Submitted") {
                $('.endsubmitted').prop('disabled', true);
            }
            if (Status === "Moderation") {
                if (EmpN !== AppraisalempN) {
                    $('.moderation').prop('disabled', false);
                }
            }
            if (Status === "Completed") {                
                $('.moderationcom').prop('disabled', true);
            }

        }
        function setSelectedIndex(s, v) {
            for (let i = 0; i < s.options.length; i++) {
                if (s.options[i].value == v) {
                    s.options[i].selected = true;
                    break;
                }
            }
        }

        function AttachSaveKPICode(kpicodedata, x, idname) {
            let option = '<option value=" ">..Select KPI Code..</option>';
            for (let i = 0; i < kpicodedata.length; i++) {
                option += '<option value="' + kpicodedata[i].Code + '">' + kpicodedata[i].Description + "</option>";
                let id = idname + x;
                document.getElementById(id).innerHTML = option;
            }
        }

        function AttachEmployeeRatings(Ratingsdata, x, idname) {
            let optionr = '<option value=" ">..Select KPI Code..</option>';
            for (let i = 0; i < Ratingsdata.length; i++) {
                optionr += '<option value="' + Ratingsdata[i].Code + '">' + Ratingsdata[i].Description + "</option>";
                let id = "empscorecode" + idname + x;
                document.getElementById(id).innerHTML = optionr;
            }
        }
        function AttachAgreedRatings(Ratingsdata, x, idname) {
            let optiona = '<option value=" ">..Select KPI Code..</option>';
            for (let i = 0; i < Ratingsdata.length; i++) {
                optiona += '<option value="' + Ratingsdata[i].Code + '">' + Ratingsdata[i].Description + "</option>";
                let id = "AgrredRatinCode" + idname + x;
                document.getElementById(id).innerHTML = optiona;
            }

        }
        function AttachSupervisorRatings(Ratingsdata, x, idname) {
            let options = '<option value=" ">..Select KPI Code..</option>';
            for (let i = 0; i < Ratingsdata.length; i++) {
                options += '<option value="' + Ratingsdata[i].Code + '">' + Ratingsdata[i].Description + "</option>";
                let id = "Supscorecode" + idname + x;
                document.getElementById(id).innerHTML = options;
            }
        }

        //Load user selected categories
        function AddSecAppraisalCat() {
            let checkedValue = {};
            checkedValue["EmpNo"] = document.getElementById("EmployeeNo").getAttribute("empNo");
            let inputElements = document.getElementsByClassName('selectedSecApraisalCat');
            for (let i = 0; inputElements[i]; ++i) {
                if (inputElements[i].checked) {
                    let kename = inputElements[i].value;
                    let valu = "";
                    checkedValue[kename] = valu;
                }
            }
            let value = {
                SelectedAppraisalCat: checkedValue
            };
            let valJson = JSON.stringify(value);
            $.ajax({
                url: 'AppraisalApplication.aspx/WriteToJsonFile',
                method: 'post',
                contentType: 'application/json',
                data: valJson,
                dataType: 'json',
                success: function (response) {
                    let dat = JSON.parse(response.d);
                    if (dat == "Yes") {
                        location.reload();
                    } else {
                        // Handle failure
                        alert("Failed to add appraisal categories,Try again");
                    }
                },
                error: (error) => {
                    console.log(JSON.stringify(error));
                }
            });
        }
        // Function to show alert
        function showAlert(message) {
            var alertDiv = $('#alertMessage');
            alertDiv.html(message).fadeIn();
            setTimeout(function () {
                alertDiv.fadeOut();
            }, 5000); // Hide after 5 seconds
        }
    </script>    
<style>
    .minus-icon {
        font-size: 0.9rem;
        cursor: pointer;
    }
</style>
</asp:Content>
