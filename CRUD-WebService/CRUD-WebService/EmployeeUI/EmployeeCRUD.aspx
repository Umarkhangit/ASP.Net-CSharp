<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EmployeeCRUD.aspx.cs" Inherits="CRUD_WebService.EmployeeUI.EmployeeCRUD" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Employees</title>

    <%-- Bootstrap CDN --%>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.3/dist/css/bootstrap.min.css" rel="stylesheet" integrity="sha384-QWTKZyjpPEjISv5WaRU9OFeRpok6YctnYmDr5pNlyT2bRjXh0JMhjY6hW+ALEwIH" crossorigin="anonymous"/>
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.3/dist/js/bootstrap.bundle.min.js" integrity="sha384-YvpcrYf0tY3lHB60NNkmXc5s9fDVZLESaAA55NDzOxhy9GkcIdslK1eN7N6jIeHz" crossorigin="anonymous"></script>
    <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/bootstrap-icons@1.11.3/font/bootstrap-icons.min.css"/>

    <%-- Axios CDN --%>
    <script src="https://cdn.jsdelivr.net/npm/axios/dist/axios.min.js"></script>

    <%-- JQuery CDN --%>
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.7.1/jquery.min.js"></script>

    <style>
        body{
            background-color:#f2f4f4
        }
        #add-new{
            margin-right:10px;
            padding:10px;
            border-radius:10px;
            background-color:white;
            cursor:pointer
        }
        #add-new:hover{
            box-shadow: 0 4px 8px rgba(0, 0, 0, 0.2), 0 6px 20px rgba(0, 0, 0, 0.19);
        }
        #search{
            padding:5px;
            border:2px solid cornflowerblue;
            border-radius:5px
        }
        #search input{
            border:none;
            background:none
        }
        #search input:focus{
            outline:none
        }
        #modal-form{
            width:40%;
            margin-left:30%;
            padding:1em;
            position:absolute;
            top:20%;
            background-color:white;
            border-radius:10px
        }

    </style>
</head>
<body>

    <div class="container-fluid" >
        <h1>Employees</h1>
        <div class="d-flex justify-content-end mb-2">
            <span id="add-new" onclick="openModal()"> <i class="bi bi-plus-lg" style="color:cornflowerblue;font-size:20px"></i> Add new</span>

            <div id="search">
                <input type="text" placeholder="Enter ID" id="emp-id"/>
                <span style="cursor:pointer" onclick="clearIdSearch()"><i class="bi bi-x-lg" style="color:red"></i></span>
                <span onclick="getEmployeeById()" style="cursor:pointer"><i class="bi bi-search" style="color:cornflowerblue"></i></span>
            </div>
        </div>
        
        <table class="table table-striped ">
            <thead>
                <tr>
                    <th>Id</th>
                    <th>Name</th>
                    <th>Role</th>
                    <th>Salary</th>
                    <th>Mobile</th>
                    <th>Actions</th>
                </tr>
            </thead>
            <tbody id="emp-table" class="table-group-divider"></tbody>
        </table>

    </div>

    <%-- Modal --%>
    <div id="modal-container"></div>

    
    <script>

        $(function () {
            getAllEmployees(); // to call the function after the document is ready
        })

        const tBody = document.getElementById("emp-table")

        const getAllEmployees = async () => {
            try {

                $.ajax({
                    type: 'POST',
                    url: 'EmployeeCRUD.aspx/SelectAll',
                    contentType: 'application/json; charset:utf-8',
                    dataType: 'json',
                    success: function (data, status) {
                        //console.log("all:", data)
                        const parsed = JSON.parse(data.d)
                        tBody.innerHTML = generateRows(parsed)
                    }
                })

                //const res = await axios.post("https://localhost:44392/Employees.asmx/SelectAllEmployees")
                ////console.log(res.data)
                //let employees = res.data
                //tBody.innerHTML = generateRows(employees)
            }
            catch (e) {
                console.error("Error",e)
            }
        }

        const getEmployeeById = async () => {
            const id = document.getElementById("emp-id").value

            if (id) {
                try {
                    $.ajax({
                        type: 'POST',
                        url: 'EmployeeCRUD.aspx/SelectById',
                        data: JSON.stringify({ empId: id }),
                        contentType: 'application/json; charset:utf-8',
                        dataType: 'json',
                        success: function (data, status) {
                            //console.log(JSON.parse(data.d))
                            const response = JSON.parse(data.d)
                            if (response.Status == "Failed") {
                                alert(response.Description)
                                return
                            }
                            tBody.innerHTML = generateRows(response)
                        }
                    })

                    //const res = await axios.post("https://localhost:44392/Employees.asmx/SelectEmployeeById", { empId: id })
                    ////console.log(res.data.d)
                    //const responseData = JSON.parse(res.data.d); // Parse the first part into JSON
                    ////console.log("rData",responseData)
                    //if (responseData.Status == "Failed") {
                    //    alert(responseData.Description)
                    //    return
                    //}
                    //tBody.innerHTML = generateRows(responseData)

                    //const rawResponse = res.data;
                    //const firstPart = rawResponse.split('{"d":null}')[0]; // Extract the first valid JSON part
                    //const responseData = JSON.parse(firstPart); // Parse the first part into JSON
                    //if (responseData.Status == "Success") {
                    //    tBody.innerHTML = generateRows(responseData.data)
                    //} else {
                    //    alert(responseData.Description)
                    //}
                }
                catch (e) {
                    console.error("Error",e)
                }
            } else {
                alert("Please enter the Id")
            }
        }


        const insertEmployee = () => {
            let empname = document.getElementById("name").value
            let emprole = document.getElementById("role").value
            let empsalary = document.getElementById("salary").value
            let empmobile = document.getElementById("mobile").value

            if (!empname || !emprole || !empsalary || !empmobile) {
                alert("Pls fill all the fields")
                return
            }

            if (isNaN(empsalary) || empsalary < 0) {
                alert("Salary can only have digits and non-negative values")
                return
            }

            if (!empmobile.match(/^\d{10}$/)) {
                alert("Mobile number should be of 10 digits")
                return
            }
            let newEmp = {
                empName: empname,
                empRole: emprole,
                empSalary: empsalary,
                empMobile: empmobile
            }
            
            $.ajax({
                type: 'POST',
                url: 'EmployeeCRUD.aspx/Insert',
                data: JSON.stringify(newEmp),
                contentType: 'application/json; charset:utf-8',
                dataType: 'json',
                success: function (data, status) {
                    //console.log(JSON.parse(data.d))
                    const response = JSON.parse(data.d)
                    if (response.Status == "Success") {
                        getAllEmployees()
                    }
                    alert(response.Description)
                }
            })

            closeModel()

            //axios.post("https://localhost:44392/Employees.asmx/InsertEmployee", newEmp)
            //    .then(res => {
            //        //console.log(res.data)
            //        const responseData = JSON.parse(res.data.d);
            //        if (responseData.Status == "Success") {
            //            getAllEmployees();
            //        }
            //        alert(responseData.Description)
            //    })
            //    .catch(e => console.error("Error",e))

        }

        const updateEmployee = () => {
            let empid = document.getElementById("id").value
            let empname = document.getElementById("name").value
            let emprole = document.getElementById("role").value
            let empsalary = document.getElementById("salary").value
            let empmobile = document.getElementById("mobile").value

            if (!empname || !emprole || !empsalary || !empmobile) {
                alert("Pls fill all the fields")
                return
            }

            if (isNaN(empsalary) || empsalary < 0) {
                alert("Salary can only have digits and non-negative values")
                return
            }

            if (!empmobile.match(/^\d{10}$/)) {
                alert("Mobile number should be of 10 digits")
                return
            }
            let udatedEmp = {
                empId: empid,
                empName: empname,
                empRole: emprole,
                empSalary: empsalary,
                empMobile: empmobile
            }
            $.ajax({
                type: 'POST',
                url: 'EmployeeCRUD.aspx/Update',
                data: JSON.stringify(udatedEmp),
                contentType: 'application/json; charset:utf-8',
                dataType: 'json',
                success: function (data, status) {
                    //console.log(JSON.parse(data.d))
                    const response = JSON.parse(data.d)
                    if (response.Status == "Success") {
                        getAllEmployees()
                    }
                    alert(response.Description)
                }
            })

            closeModel()

            //axios.post("https://localhost:44392/Employees.asmx/UpdateEmployee", udatedEmp)
            //    .then(res => {
            //        const responseData = JSON.parse(res.data.d);
            //        if (responseData.Status == "Success") {
            //            getAllEmployees();
            //        }
            //        alert(responseData.Description)
            //    })
            //    .catch(e => console.error("Error",e.response))

        }

        const deleteEmployee = (id) => {
            if (confirm("Are you sure you want to delete this employee ?")) {
                try {
                    $.ajax({
                        type: 'POST',
                        url: 'EmployeeCRUD.aspx/Delete',
                        data: JSON.stringify({ empId: id }),
                        contentType: 'application/json; charset:utf-8',
                        dataType: 'json',
                        success: function (data, status) {
                            //console.log(JSON.parse(data.d))
                            const response = JSON.parse(data.d)
                            if (response.Status == "Success") {
                                getAllEmployees()
                            }
                            alert(response.Description)
                        }
                    })
                }
                catch (e) {
                    console.error(e)
                }

                //axios.post("https://localhost:44392/Employees.asmx/DeleteEmployee", { empId: id })
                //     .then(res => {
                //         const responseData = JSON.parse(res.data.d);
                //         if (responseData.Status == "Success") {
                //             getAllEmployees();
                //         }
                //         alert(responseData.Description)
                //    })
                //    .catch(e => console.error(e))                
            } 
            
        }

        const generateRows = (data) => {
            let rows = ``
            if (data != null) {
                data.forEach(val => {
                    rows += `<tr>
                                 <td>${val.EmpID}</td>
                                 <td>${val.EmpName}</td>
                                 <td>${val.EmpRole}</td>
                                 <td>${val.EmpSalary}</td>
                                 <td>${val.EmpMobile}</td>
                                 <td style="font-size:20px"><i class="bi bi-pencil-square" style="color:cornflowerblue;cursor:pointer" onclick=update(${val.EmpID})></i> || 
                                 <i class="bi bi-trash3" style="color:red;cursor:pointer" onclick=deleteEmployee(${val.EmpID})></i></td>
                             </tr>`
                })
                return rows
            } else {
                rows += `<tr><td colspan="5">No data found</td></tr>`
                return rows
            }
        }

        const clearIdSearch = () => {
            document.getElementById("emp-id").value=""
            getAllEmployees();
        }

        const update = (id) => {
            axios.post("https://localhost:44392/Employees.asmx/SelectAllEmployees")
                .then(res => {
                    let filteredEmp = res.data.find(val => val.EmpID == id)
                    openModal(filteredEmp)
                })
                .catch(e => console.error(e))
        }

        const modalContainer = document.getElementById("modal-container")

        const openModal = (fEmp) => {
            let modal = ``
            if (fEmp) {
                modal = `<div id="modal-form">
                <h3>Update</h3>
                <div class="mb-3 row">
                    <label for="name" class="col-sm-2 col-form-label">Id</label>
                    <div class="col-sm-10">
                        <input type="text" class="form-control" value=${fEmp.EmpID} id = "id" name = "empId" required = "required" readonly/>
                    </div>
                </div>
                <div class="mb-3 row">
                    <label for="name" class="col-sm-2 col-form-label">Name</label>
                    <div class="col-sm-10">
                        <input type="text" class="form-control" value=${fEmp.EmpName} id = "name" name = "empName" required = "required" />
                    </div>
                </div>
                <div class="mb-3 row">
                    <label for="role" class="col-sm-2 col-form-label">Role</label>
                    <div class="col-sm-10">
                        <input type="text" class="form-control" value=${fEmp.EmpRole} id="role" name="empRole" required="required"/>
                    </div>
                </div>
                <div class="mb-3 row">
                    <label for="salary" class="col-sm-2 col-form-label">Salary</label>
                    <div class="col-sm-10">
                        <input type="text" class="form-control" value=${fEmp.EmpSalary} id="salary" name="empSalary" required="required"/>
                    </div>
                </div>
                <div class="mb-3 row">
                    <label for="mobile" class="col-sm-2 col-form-label">Mobile</label>
                    <div class="col-sm-10">
                        <input type="text" class="form-control" value=${fEmp.EmpMobile} id="mobile" name="empMobile" required="required"/>
                    </div>
                </div>
                <div id="actions" class="d-flex justify-content-end">
                  <button type="button" class="btn btn-primary me-2" onclick="updateEmployee()">Submit</button>
                  <button type="button" class="btn btn-secondary" onclick="closeModel()">Cancel</button>
                </div>
                </div>`
            } else {
                modal = `<div id="modal-form">
                <h3>Add new</h3>
                <div class="mb-3 row">
                    <label for="name" class="col-sm-2 col-form-label">Name</label>
                    <div class="col-sm-10">
                        <input type="text" class="form-control" id = "name" name="empName" required = "required" />
                    </div>
                </div>
                <div class="mb-3 row">
                    <label for="role" class="col-sm-2 col-form-label">Role</label>
                    <div class="col-sm-10">
                        <input type="text" class="form-control" id="role" name="empRole" required="required"/>
                    </div>
                </div>
                <div class="mb-3 row">
                    <label for="salary" class="col-sm-2 col-form-label">Salary</label>
                    <div class="col-sm-10">
                        <input type="text" class="form-control" id="salary" name="empSalary" required="required"/>
                    </div>
                </div>
                <div class="mb-3 row">
                    <label for="mobile" class="col-sm-2 col-form-label">Mobile</label>
                    <div class="col-sm-10">
                        <input type="text" class="form-control" id="mobile" name="empMobile" required="required"/>
                    </div>
                </div>
                <div id="actions" class="d-flex justify-content-end">
                  <button type="button" class="btn btn-primary me-2" onclick="insertEmployee()">Submit</button>
                  <button type="button" class="btn btn-secondary" onclick="closeModel()">Cancel</button>
                </div>
            </div>`
            }
                modalContainer.innerHTML = modal
        }

        const closeModel = () => {
            modalContainer.removeChild(modalContainer.firstElementChild)
        }
    </script>

</body>
</html>
