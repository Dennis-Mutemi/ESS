<%@ Page Language="C#" Title="Leave Calendar" MasterPageFile="~/pages/Main.Master" AutoEventWireup="true" CodeBehind="LeaveCalendar.aspx.cs" Inherits="ZamaraESS.pages.LeaveCalendar" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Main1" runat="Server">
    <style>
        .calendar {
            display: grid;
            grid-template-columns: repeat(7, 1fr);
            gap: 1px;
            background-color: #f8f9fa;
            border: 1px solid #dee2e6;
            margin-top: 20px; /* Adjusted for spacing */
            border-radius: 5px; /* Rounded corners */
            overflow: hidden; /* Prevents border gaps from showing */
        }

        .day-header, .day {
            background-color: #fff;
            border: 1px solid #dee2e6;
            padding: 10px;
            text-align: center;
            position: relative;
        }

        .day-header {
            background-color: #e9ecef;
            font-weight: bold;
        }

        .day {
            height: 120px; /* Increased height for better layout */
            display: flex;
            flex-direction: column;
            align-items: center;
        }

        .day-number {
            position: absolute;
            top: 5px;
            right: 5px;
            font-weight: bold;
            z-index: 1;            
            color: #fff;
            padding: 5px;
            border-radius: 50%; /* Circular shape */
            color:black;
        }

        .leave-info {
            display: block;
            padding: 6px 10px;
            border-radius: 3px;
            margin-bottom: 3px;
            cursor: pointer;
            position: relative;
            background-color: #d4edda;
            font-size: 0.9em;
            width: 100%;
            text-align: center;
        }

        .leave-info:hover {
            background-color: #c3e6cb;
        }

        .leave-details {
            position: absolute;
            top: calc(100% + 10px);
            left: 50%;
            transform: translateX(-50%);
            width: max-content;
            background-color: #fff;
            border: 1px solid #28a745;
            padding: 10px;
            border-radius: 3px;
            z-index: 1000;
            box-shadow: 0 4px 8px rgba(0, 0, 0, 0.1);
            pointer-events: none;
            display: none;
        }

        .leave-info:hover .leave-details {
            display: block;
        }

        .leave-details .leave-name {
            font-weight: bold;
        }

        .leave-details .leave-dates {
            color: #777;
            font-size: 0.9em;
            margin-bottom: 5px;
        }


        .month-nav {
    display: flex;
    justify-content: space-between;
    align-items: center;
    margin-bottom: 10px;
    background-color: #f8f9fa; /* Light background color */
    padding: 10px;
    border: 1px solid #dee2e6;
    border-radius: 5px;
}

.month-nav h3 {
    margin: 0;
    font-size: 1.5em; /* Increased font size */
}

.btn {  
    color: #fff;
    border: none;
    padding: 8px 12px;
    cursor: pointer;
    border-radius: 3px;
    font-size: 1em; /* Increased font size */
    transition: background-color 0.3s;
}


    </style>
    <div class="content-wrapper pagepostion">
        <div class="container">
            <div class="month-nav">
                <button id="prevMonth" class="btn btn-success">&lt; Previous</button>
                <h3 id="monthYear"></h3>
                <button id="nextMonth" class="btn btn-success">Next &gt;</button>
            </div>
            <div class="calendar" id="calendar">
                <!-- Calendar days will be populated here -->
            </div>
        </div>
    </div>
    <script>
        document.addEventListener("DOMContentLoaded", function () {
            var rawData = <%= AppliedLeaves() %>;
            var changleaveData = rawData.map(function (item) {
                return {
                    name: item.EmployeeName,
                    type: item.Leave_Type === 'I' ? '' : item.Leave_Type,
                    startDate: new Date(item.Start_Date.Year, item.Start_Date.Month - 1, item.Start_Date.Day),
                    endDate: item.End_Date.Year > 1 ? new Date(item.End_Date.Year, item.End_Date.Month - 1, item.End_Date.Day) : null
                };
            });
            // Remove items with invalid start dates
            var leaveData = changleaveData.filter(function (item) {
                return !isNaN(item.startDate.getTime());
            });

            const monthNames = ["January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December"];
            const dayNames = ["Sun", "Mon", "Tue", "Wed", "Thu", "Fri", "Sat"];
            let currentMonth = new Date().getMonth();
            let currentYear = new Date().getFullYear();

            function renderCalendar(month, year) {
                const calendar = document.getElementById("calendar");
                calendar.innerHTML = "";
                document.getElementById("monthYear").textContent = `${monthNames[month]} ${year}`;

                // Calculate the first day of the month (0-6, where 0 is Sunday)
                const firstDay = new Date(year, month, 1).getDay();

                // Calculate the number of days in the month
                const daysInMonth = new Date(year, month + 1, 0).getDate();

                // Add day headers
                dayNames.forEach(day => {
                    const dayHeader = document.createElement("div");
                    dayHeader.className = "day-header";
                    dayHeader.textContent = day;
                    calendar.appendChild(dayHeader);
                });

                // Fill empty cells before the first day of the month
                for (let i = 0; i < firstDay; i++) {
                    const emptyDiv = document.createElement("div");
                    emptyDiv.className = "day";
                    calendar.appendChild(emptyDiv);
                }

                // Populate days with leave information
                for (let day = 1; day <= daysInMonth; day++) {
                    const date = new Date(year, month, day);
                    const dayDiv = document.createElement("div");
                    dayDiv.className = "day";
                    dayDiv.innerHTML = `<div class="day-number">${day}</div>`;

                    leaveData.forEach(leave => {
                        if (date.getTime() === leave.startDate.getTime()) {
                            const leaveInfo = document.createElement("div");
                            leaveInfo.className = "leave-info";
                            leaveInfo.textContent = leave.name;
                            leaveInfo.dataset.startDate = leave.startDate.getTime();
                            leaveInfo.dataset.endDate = leave.endDate.getTime();
                            leaveInfo.dataset.type = leave.type;
                            leaveInfo.dataset.name = leave.name;
                            leaveInfo.addEventListener("mouseenter", showLeaveDetails);
                            leaveInfo.addEventListener("mouseleave", hideLeaveDetails);
                            dayDiv.appendChild(leaveInfo);
                        }
                    });

                    calendar.appendChild(dayDiv);
                }
            }

            function showLeaveDetails(event) {
                const leaveInfo = event.target;
                const startDate = new Date(parseInt(leaveInfo.dataset.startDate));
                const endDate = new Date(parseInt(leaveInfo.dataset.endDate));
                const leaveType = leaveInfo.dataset.type;
                const leaveName = leaveInfo.dataset.name;

                const leaveDetails = document.createElement("div");
                leaveDetails.className = "leave-details";
                leaveDetails.innerHTML = `
                    <div class="leave-name">${leaveName}</div>
                    <div class="leave-dates">${monthNames[startDate.getMonth()]} ${startDate.getDate()} - ${monthNames[endDate.getMonth()]} ${endDate.getDate()}, ${endDate.getFullYear()}</div>
                    <div class="leave-type">${leaveType}</div>
                `;

                leaveInfo.appendChild(leaveDetails);
            }

            function hideLeaveDetails(event) {
                const leaveInfo = event.target;
                const leaveDetails = leaveInfo.querySelector(".leave-details");
                if (leaveDetails) {
                    leaveDetails.remove();
                }
            }

            function updateCalendar() {
                renderCalendar(currentMonth, currentYear);
            }

            document.getElementById("prevMonth").addEventListener("click", function (event) {
                event.preventDefault();
                currentMonth--;
                if (currentMonth < 0) {
                    currentMonth = 11;
                    currentYear--;
                }
                updateCalendar();
            });

            document.getElementById("nextMonth").addEventListener("click", function (event) {
                event.preventDefault();
                currentMonth++;
                if (currentMonth > 11) {
                    currentMonth = 0;
                    currentYear++;
                }
                updateCalendar();
            });

            updateCalendar();
        });
    </script>
</asp:Content>
